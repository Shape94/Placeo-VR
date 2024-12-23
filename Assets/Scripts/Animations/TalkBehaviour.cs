using UnityEngine;

public class TalkBehaviour : StateMachineBehaviour
{
    // Questo viene chiamato quando l'animazione inizia
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Logica per l'entrata nello stato di talk
    }

    // Questo viene chiamato mentre l'animazione è in corso
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Logica per l'aggiornamento mentre parla
    }

    // Questo viene chiamato quando l'animazione finisce
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Logica per l'uscita dallo stato di talk
    }
}

