

using UnityEngine;

public class PatchDispenserController : MonoBehaviour
{
    public GameObject script; // Riferimento al GameObject che contiene il SequenceController

    private ISequenceController sequenceController; // Riferimento all'interfaccia

    void Start()
    {
        // Cerca di ottenere il componente ISequenceController dal GameObject specificato
        sequenceController = script.GetComponentInChildren<ISequenceController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Se il sequenceController è stato trovato, chiama OnPatchInteraction
        if (sequenceController != null)
        {
            sequenceController.OnPatchInteraction(other); // Chiama il metodo comune
        }
        else
        {
            // Se nessun controller è stato trovato, logga un messaggio di errore
            Debug.LogError("Nessun componente ISequenceController trovato nel GameObject specificato.");
        }
    }
}
