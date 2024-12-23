

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door; // Il primo figlio di porta
    public GameObject mainCamera; // La main camera
    public float rotationY = 115f; // Il valore di rotazione nell'asse Y
    public float animationTime = 1f; // La durata dell'animazione
    public AudioClip openingSound; // Il suono di apertura della porta
    public AudioClip closingSound; // Il suono di chiusura della porta
    public OcclusionPortal occlusionPortal;

    private Vector3 originalRotation; // La rotazione originale della porta
    public AudioSource audioSource; // L'AudioSource per riprodurre i suoni

    public GameObject script; // Riferimento al GameObject che contiene il SequenceController
    private ISequenceController sequenceController; // Riferimento all'interfaccia

    void Start()
    {
        // Salva la rotazione originale della porta
        originalRotation = door.transform.eulerAngles;

        // Ottiene l'AudioSource dal GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Se non c'è un AudioSource, ne aggiunge uno
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Cerca di ottenere il componente ISequenceController dal GameObject specificato
        sequenceController = script.GetComponentInChildren<ISequenceController>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Controlla se l'oggetto che è entrato nel collider è la main camera
        if (other.gameObject == mainCamera)
        {
            occlusionPortal.open = true;
            // Fai ruotare la porta con un'animazione
            iTween.RotateTo(door, iTween.Hash("y", rotationY, "time", animationTime, "onstart", "PlayOpeningSound", "onstarttarget", gameObject));

            // Se il sequenceController è stato trovato, chiama OnTriggerEnter
            if (sequenceController != null)
            {
                sequenceController.OnTriggerEnter(other); // Chiama il metodo comune
            }
            else
            {
                // Se nessun controller è stato trovato, logga un messaggio di errore
                Debug.LogError("Nessun componente ISequenceController trovato nel GameObject specificato.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Controlla se l'oggetto che è uscito dal collider è la main camera
        if (other.gameObject == mainCamera)
        { 
            // Fai tornare la porta alla posizione originale con un'animazione
            iTween.RotateTo(door, iTween.Hash("y", originalRotation.y, "time", animationTime, "oncomplete", "PlayClosingSound", "oncompletetarget", gameObject));
        }
    }

    // Riproduce il suono di apertura della porta
    void PlayOpeningSound()
    {
        audioSource.clip = openingSound;
        audioSource.Play();
    }

    // Riproduce il suono di chiusura della porta
    void PlayClosingSound()
    {
        occlusionPortal.open = false;
        audioSource.clip = closingSound;
        audioSource.Play();
    }
}
