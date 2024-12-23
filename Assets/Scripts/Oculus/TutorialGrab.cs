using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;


public class TutorialGrab : MonoBehaviour
{
    public AnimateScalePopInOut animateScalePopInOut;
    public Tutorial tutorial;
    public AudioSource audioSource;
    public GameObject logo;
    private bool done = false;
    private XRGrabInteractable grabInteractable;
    public GameObject particles;
    private Vector3 originalLogoPosition;
    private Quaternion originalLogoRotation;

    public AudioSource audiosource2;
    private float originalVolume;
    private Rigidbody rb;
    public Transform leftHand2;
    public Transform rightHand2;
    private Vector3 originalLogoPosition2;
    private Quaternion originalLogoRotation2;


    private void Awake()
    {
        originalLogoPosition = logo.transform.localPosition;
        originalLogoRotation = logo.transform.localRotation;
        rb = GetComponent<Rigidbody>();

        originalLogoPosition2 = rb.transform.localPosition;
        originalLogoRotation2 = rb.transform.localRotation;

        originalVolume = audioSource.volume;
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);
    }

    void OnEnable(){
        grabInteractable.enabled = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(Vector3.zero, ForceMode.Acceleration);
        rb.Sleep();
        
        rb.transform.localPosition = originalLogoPosition2;
        rb.transform.localRotation = originalLogoRotation2;


        logo.transform.localPosition = originalLogoPosition;
        logo.transform.localRotation = originalLogoRotation;
        
        rb.WakeUp();
    }


    private void OnGrab(XRBaseInteractor interactor)
    {
        /*leftHand2.gameObject.SetActive(false);
        rightHand2.gameObject.SetActive(false);*/

        foreach (ParticleSystem ps in particles.GetComponentsInChildren<ParticleSystem>())
        {
            var main = ps.main;
            main.loop = false;
        }
       StartCoroutine(FadeOutAudio(audioSource, 3f));
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
        /*leftHand2.gameObject.SetActive(true);
        rightHand2.gameObject.SetActive(true);*/
        
        if (!done)
        {
            done = true;
            grabInteractable.enabled = false;
            animateScalePopInOut.ReduceScale();
            audiosource2.Play();
            StartCoroutine(ExecuteAfterDelay(1.5f));
        }
    }

    private IEnumerator ExecuteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        animateScalePopInOut.RestoreScale();
        done = false;
        logo.SetActive(true);
        logo.GetComponent<AnimateScalePopInOut>().RestoreScale();

        foreach (ParticleSystem ps in particles.GetComponentsInChildren<ParticleSystem>())
        {
            var main = ps.main;
            main.loop = true;
        }

        audioSource.volume = originalVolume;
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(Vector3.zero, ForceMode.Acceleration);
        
        rb.Sleep();
        //rb.WakeUp();

        tutorial.OnNextButtonClick();
    }

    private IEnumerator FadeOutAudio(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }
        audioSource.volume = 0;
        audioSource.Stop();
    }
}