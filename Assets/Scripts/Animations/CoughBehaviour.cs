using System.Collections;
using UnityEngine;

public class CoughBehaviour : StateMachineBehaviour
{
    public float probability = 0.01f; // 1% di probabilità
    public float interval = 10f; // Intervallo di controllo in secondi (10 secondi)
    public AudioClip[] coughSounds; // Clip audio per il suono della tosse
    private AudioSource audioSource; // Riferimento all'AudioSource

    // OnStateEnter è chiamato quando inizia la transizione e la state machine inizia a valutare questo stato
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         // Ottieni il componente AudioSource dall'animator
        audioSource = animator.GetComponentInChildren<AudioSource>();

        // Avvia la coroutine tramite il CoroutineRunner
        CoroutineRunner.Instance.StartCoroutine(CheckCoughingProbability(animator, layerIndex));
    }

    // Coroutine che controlla periodicamente se impostare IsCoughing con una probabilità dell'1%
    private IEnumerator CheckCoughingProbability(Animator animator, int layerIndex)
    {
        while (true)
        {
            // Aspetta l'intervallo specificato (10 secondi)
            yield return new WaitForSeconds(interval);

            // Ottieni informazioni sullo stato corrente dell'animatore
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(layerIndex);

            // Controlla se il nome dello stato corrente è "Walk"
            if (Random.value < probability && currentState.IsName("Idle"))
            {
                animator.SetTrigger("Cough");

                // Ritarda di un secondo la riproduzione dell'audio
                yield return new WaitForSeconds(1f);
                PlayCoughSound();
            }
        }
    }

    // Metodo per riprodurre il suono della tosse
    private void PlayCoughSound()
    {
        if (audioSource != null && coughSounds != null)
        {
            AudioClip randomCoughSound = coughSounds[Random.Range(0, coughSounds.Length)];
            audioSource.PlayOneShot(randomCoughSound);
        }
    }
}
