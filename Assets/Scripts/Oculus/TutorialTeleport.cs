using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTeleport : MonoBehaviour
{
   public AnimateScalePopInOut animateScalePopInOut;
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
       gameObject.SetActive(false);
       logo.SetActive(true);
   }
}
