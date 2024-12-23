using UnityEngine;
using System.Collections;

public class TutorialMove : MonoBehaviour
{
    public AnimateScalePopInOut animateScalePopInOut;
    public Tutorial tutorial;
    public AudioSource audioSource;
    public GameObject logo;
    private bool done = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!done)
        {
            done = true;
            audioSource.Play();
            animateScalePopInOut.ReduceScale();
            StartCoroutine(ExecuteAfterDelay(1.5f));
        }
    }

    private IEnumerator ExecuteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animateScalePopInOut.RestoreScale();
        done = false;
        if(logo != null){
            logo.SetActive(true);
            logo.GetComponent<AnimateScalePopInOut>().RestoreScale();
            gameObject.SetActive(false);
        }
        tutorial.OnNextButtonClick();
    }
}
