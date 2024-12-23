using UnityEngine;
using TMPro;
using UnityEngine.UI ;

public class GestoreTesto : MonoBehaviour
{
    public TMP_Text oggettoSorgente; // Riferimento all'oggetto TextMeshPro da cui copiare il testo
    public TMP_Text oggettoDestinazione; // Riferimento all'oggetto TextMeshPro in cui incollare il testo

    private void Start()
    {
        // Assicurati che gli oggetti TextMeshPro siano assegnati nell'Inspector
        if (oggettoSorgente == null || oggettoDestinazione == null)
        {
            Debug.LogError("Assegna gli oggetti TextMeshPro nei campi dell'Inspector!");
            return;
        }
    }

    public void CopiaTesto()
    {
        // Copia il testo dall'oggetto sorgente all'oggetto destinazione
        oggettoDestinazione.text = oggettoSorgente.text;
    }
}
