using System.Collections;
using UnityEngine;

public class WalkBehaviour : StateMachineBehaviour
{
    public float distanceThreshold = 6.0f; // Soglia di distanza per iniziare a camminare
    private Transform targetObject; // Riferimento al GameObject nella scena 1
    public float probability = 0.05f; // 5% di probabilità
    public float interval = 60f; // Intervallo di controllo in secondi (1 minuto)

    // OnStateEnter è chiamato quando inizia la transizione e la state machine inizia a valutare questo stato
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Imposta il targetObject al GameObject di riferimento se è disponibile
        if (GameObjectReference.Instance != null)
        {
            targetObject = GameObjectReference.Instance.targetObject;
        }

        // Avvia la coroutine tramite il CoroutineRunner
        CoroutineRunner.Instance.StartCoroutine(CheckWalkingProbability(animator, layerIndex));
    }

    // Coroutine che controlla periodicamente la distanza e imposta il parametro IsWalking con una probabilità del 5%
    private IEnumerator CheckWalkingProbability(Animator animator, int layerIndex)
    {
        while (true)
        {
            // Aspetta l'intervallo specificato (1 minuto)
            yield return new WaitForSeconds(interval);

            // Ottieni informazioni sullo stato corrente dell'animatore
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(layerIndex);

            if (targetObject != null)
            {
                // Calcola la distanza tra il personaggio e il targetObject
                float distance = Vector3.Distance(animator.transform.position, targetObject.position);

                // Controlla la distanza e imposta IsWalking con una probabilità del 5%
                if (distance > distanceThreshold && Random.value < probability && currentState.IsName("Idle"))
                {
                    animator.SetTrigger("Walk");
                }
            }
        }
    }
}

