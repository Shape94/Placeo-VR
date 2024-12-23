using UnityEngine;

public class GestoreAudio : MonoBehaviour
{
    public AudioSource audioSource; // Riferimento all'AudioSource
    public AudioClip[] brani; // Array di AudioClip

    System.Collections.IEnumerator RiproduciBraniSequenzialmente()
{
    yield return null; // Attendi un frame

    while (true)  // Loop infinito per ripetere la sequenza
    {
        for (int i = 0; i < brani.Length; i++)
        {
            // Assegna l'AudioClip corrente all'AudioSource
            audioSource.clip = brani[i];

            // Avvia la riproduzione
            audioSource.Play();

            // Attendi che l'AudioClip finisca di suonare
            while (audioSource.isPlaying)
            {
                yield return null;
            }

            // Aggiungi una pausa di 1 secondo prima del prossimo brano
            yield return new WaitForSeconds(1f);
        }
    }
}


    void Start()
    {
        StartCoroutine(RiproduciBraniSequenzialmente());
    }


    // Per avviare la sequenza, chiama StartCoroutine(RiproduciBraniSequenzialmente());
}
