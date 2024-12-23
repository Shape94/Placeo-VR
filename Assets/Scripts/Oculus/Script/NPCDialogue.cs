using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{

    public SimulationTTS simulationTTS; // Riferimento al componente ReceptionistTTS
    private Animator animator; // Riferimento all'animator

    private void Start()
    {
        // Ottieni il componente ReceptionistTTS dallo stesso GameObject
        simulationTTS = GetComponent<SimulationTTS>();

        if (simulationTTS == null)
        {
            Debug.LogError("imulationTTS non trovato su questo GameObject.");
        }

        // Ottieni il componente Animator dallo stesso GameObject
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator non trovato su questo GameObject.");
        }
    }

    public void TriggerDialogue(string message, bool inviting = false)
    {
        simulationTTS.Speak(message);
        


        // Attiva l'animazione di inizio dialogo
        if (inviting)
        {
            animator.SetTrigger("StartInvite");
        }
        else
        {
            animator.SetTrigger("StartTalk");
        }
    }

    private void Update()
    {
        
    }
}
