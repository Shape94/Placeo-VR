using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public AudioClip bounceSound; // Il suono del rimbalzo
    private AudioSource audioSource; // L'AudioSource da cui riprodurre il suono
    private Rigidbody myRigidbody; // Il componente Rigidbody della pallina

    void Start()
    {
        // Ottieni l'AudioSource e il Rigidbody dal game object
        audioSource = GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Calcola il volume in base alla velocità della pallina senza limiti
        float volume = myRigidbody.velocity.magnitude / 10f; // Puoi regolare il valore divisorio per adattare il volume ai tuoi gusti

        // Riproduci il suono del rimbalzo con il volume calcolato
        audioSource.PlayOneShot(bounceSound, volume);
    }
}
