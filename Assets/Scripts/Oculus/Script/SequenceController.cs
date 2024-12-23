

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequenceController : MonoBehaviour, ISequenceController // Implementa l'interfaccia
{
    public PlayfabManagerOculus playFabManagerOculus;
    public LoadScene loadScene;
    public GameObject npcReceptionist; // NPC che invita nella sala
    public GameObject npcDoctor;       // Medico
    public GameObject player;          // Riferimento al giocatore
    public Collider sitCollider;       // Collider della sedia per rilevare quando il player si siede
    public Collider dispenserCollider; // Collider del dispenser
    public Collider patchCollider;
    public UpdateCSV updateCSV;
    public GelController gelController; // Riferimento al controller del gel
    public PatchController patchController;
    public AudioSource dispenserAudio; // Audio per il dispenser
    public AudioSource patchAudio;
    public FadeCanvas fadeCanvas;

    private bool hasWaited = false;    // Flag per gestire l'attesa
    private bool hasWaited2 = false;
    private bool hasExitedAfterPatch = false; // Nuovo flag per tracciare l'uscita

    private void Start()
    {
        StartCoroutine(WaitingSequence());
    }

    private IEnumerator WaitingSequence()
    {
        // Attesa di 2 minuti (120 secondi)
        yield return new WaitForSeconds(2f);
        
        npcDoctor = GameObject.FindGameObjectWithTag("Doctor");
        npcReceptionist = GameObject.FindGameObjectWithTag("Receptionist");

        npcDoctor.GetComponent<NPCDialogue>().TriggerDialogue(" ");
        npcReceptionist.GetComponent<NPCDialogue>().TriggerDialogue(" ");

        yield return new WaitForSeconds(120f);

        npcReceptionist.GetComponent<NPCDialogue>().TriggerDialogue(
            updateCSV.paziente + ", il medico la sta aspettando, può entrare nello studio.",
            true
        );

        hasWaited = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!hasWaited2)
        {
            if (other.CompareTag("Player") && hasWaited && !hasExitedAfterPatch)
            {
                HandleDoctorRoomEntry();
            }
            else if (other.CompareTag("Player") && !hasWaited)
            {
                npcDoctor.GetComponent<NPCDialogue>().TriggerDialogue(
                    "La prego di attendere fuori, la farò chiamare quando sarà il suo turno."
                );
            }
        }
        else if (hasExitedAfterPatch) // Verifica se il flag è impostato
        {
            StartCoroutine(HandleDoctorRoomEntry2());
            hasExitedAfterPatch = false; // Reimposta il flag per evitare attivazioni multiple
        }
    }

    private void HandleDoctorRoomEntry()
    {
        npcDoctor.GetComponent<NPCDialogue>().TriggerDialogue(
            "Prego, venga avanti e si accomodi pure.",
            true
        );
        sitCollider.enabled = true;
    }

    public void OnPlayerSits(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sitCollider.enabled = false;
            npcDoctor.GetComponent<NPCDialogue>().TriggerDialogue(
                "Buongiorno, lei sta partecipando ad un trial clinico dove verrà testata l'efficacia di una nuova patch, ovvero un elettrodo che indurrà un effetto rilassante tramite impercettibili stimolazioni elettriche nella sua mano. Lei sarà assegnato ad uno di due gruppi in modo casuale: il gruppo sperimentale riceverà una patch effettiva, mentre il gruppo di controllo riceverà una patch inerte. Iniziamo! Adesso applicheremo una crema per far meglio aderire la patch alla sua mano, avvicini pure la mano destra verso la parte bassa del dispenser, la crema verrà erogata automaticamente. Il dispenser si trova alla sua destra."
            );
            dispenserCollider.enabled = true;
        }
    }

    public void OnDispenserInteraction(Collider other)
    {
        if (other.CompareTag("RightHand"))
        {
            dispenserCollider.enabled = false;
            dispenserAudio.Play();
            gelController.FadeOut(10f);

            npcDoctor.GetComponent<NPCDialogue>().TriggerDialogue(
                "Bene, ora inserisca la mano destra all'interno dell'applicatore delle patch, è la scatola bianca davanti a lei."
            );

            patchCollider.enabled = true;
        }
    }

    public void OnPatchInteraction(Collider other) // Implementa il metodo dell'interfaccia
    {
        if (other.CompareTag("RightHand"))
        {
            patchCollider.enabled = false;
            patchAudio.Play();
            patchController.ApplyPatch();

            npcDoctor.GetComponent<NPCDialogue>().TriggerDialogue(
                "Bene, lei appartiene al gruppo che riceve la patch effettiva, ovvero la patch con effetto rilassante. Questa patch fa effetto in tre minuti, quindi le chiedo di attendere fuori dalla stanza, la farò chiamare quando dovrà rientrare."
            );

            hasExitedAfterPatch = true; // Imposta il flag quando l'interazione con la patch è avvenuta

            StartCoroutine(WaitingSequence2());
        }
    }

    private IEnumerator WaitingSequence2()
    {
        // Attesa di 3 minuti (180 secondi)
        yield return new WaitForSeconds(180f);

        npcReceptionist.GetComponent<NPCDialogue>().TriggerDialogue(
            updateCSV.paziente + ", il tempo d'attesa è finito, può entrare nuovamente nello studio del medico.",
            true
        );

        hasWaited2 = true;
    }

    private IEnumerator HandleDoctorRoomEntry2()
    {
        npcDoctor.GetComponent<NPCDialogue>().TriggerDialogue( "Prego, si avvicini, la ringrazio per aver partecipato al nostro trial clinico. Ora le faremo compilare un questionario sulla sua esperienza. Le auguro una buona giornata!" );
        yield return new WaitForSeconds(15f);

        fadeCanvas.StartFadeIn();

        yield return new WaitForSeconds(2f);
        playFabManagerOculus.EndSimulation();
        loadScene.LoadSceneUsingName("Placeo VR - Oculus - Intro");
    }
}
