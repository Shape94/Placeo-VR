using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialTexts;
    public GameObject tutorialImages;
    public AudioSource audioSource;
    public PlayfabManagerOculusIntro playfabManagerOculusIntro;
    public TutorialTTS tutorialTTS;
    public TextMeshProUGUI tutorialTTSField;
    public ActionBasedController leftHandController;
    public ActionBasedController rightHandController;
    private Transform leftHand1;
    private Transform rightHand1;
    public Transform leftHand2;
    public Transform rightHand2;
    public Transform leftHand1Prefab;
    public Transform rightHand1Prefab;
    public Transform leftHand2Prefab;
    public Transform rightHand2Prefab;

    public int currentIndex = 0;

    private ActionBasedController leftController;
    private ActionBasedController rightController;

    void Start()
    {
        leftController = leftHandController.GetComponent<ActionBasedController>();
        rightController = rightHandController.GetComponent<ActionBasedController>();

        leftHand1 = leftHandController.transform.GetChild(1);
        rightHand1 = rightController.transform.GetChild(1);
        
        // Attiva il primo figlio
        SetActiveChild(currentIndex, true);

    }

    public void OnNextButtonClick()
    {
        //IncreaseVolume();
        // Disattiva il figlio corrente
        SetActiveChild(currentIndex, false);

        currentIndex = (currentIndex + 1) % tutorialTexts.transform.childCount;

        // Attiva il prossimo figlio
        SetActiveChild(currentIndex, true);

        // Se il primo figlio viene riattivato, abbassa il volume
        if (currentIndex == 0)
        {   
            

            playfabManagerOculusIntro.miniSplashScreen.transform.parent.gameObject.SetActive(true);
            StartCoroutine(FadeOut(audioSource, 2f));
        }else{
          

            playfabManagerOculusIntro.miniSplashScreen.transform.parent.gameObject.SetActive(false);
            IncreaseVolume();
            tutorialTTSField.text = tutorialTexts.transform.GetChild(currentIndex).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text;
            tutorialTTS.ButtonClick();
        }
    }

    private void SetActiveChild(int index, bool isActive)
    {
        tutorialTexts.transform.GetChild(index).gameObject.SetActive(isActive);
        tutorialImages.transform.GetChild(index).gameObject.SetActive(isActive);
    }

    void IncreaseVolume()
    {
        StartCoroutine(FadeIn(audioSource, 2f, 0.01f));
    }

    IEnumerator FadeIn(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        audioSource.volume = targetVolume;
    }

    IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, 0, currentTime / duration);
            yield return null;
        }
        audioSource.volume = 0;
    }
}

