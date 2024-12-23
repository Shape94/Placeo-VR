using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnd : MonoBehaviour
{
    public Tutorial tutorial;
    public GameObject disconnect;
    public GameObject tutorialButton;

    void OnEnable(){
        StartCoroutine(ExecuteAfterDelay(12f));
    }

    private IEnumerator ExecuteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        disconnect.SetActive(true);
        tutorialButton.SetActive(true);
        
        tutorial.OnNextButtonClick();
    }
}
