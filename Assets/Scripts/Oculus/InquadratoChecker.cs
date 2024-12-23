using UnityEngine;
using System.Collections;

public class InquadratoChecker : MonoBehaviour
{
    private Camera mainCamera;
    private float timeInView = 0f;
    private bool isInView = false;
    public AnimateScalePopInOut animateScalePopInOut;
    public Tutorial tutorial;
    public AudioSource audioSource;
    private bool done = false;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (onScreen)
        {
            if (!isInView)
            {
                isInView = true;
                timeInView = 0f;
            }
            timeInView += Time.deltaTime;

            if (timeInView >= 1f && done == false)
            {
                done = true;
                audioSource.Play();
                animateScalePopInOut.ReduceScale();
                StartCoroutine(ExecuteAfterDelay(1.5f));
            }
        }
        else
        {
            isInView = false;
            timeInView = 0f;
        }
    }

    private IEnumerator ExecuteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animateScalePopInOut.RestoreScale();
        done = false;
        tutorial.OnNextButtonClick();
    }
}
