using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {

    public GameObject secondHand;
    public GameObject minuteHand;
    public GameObject hourHand;

    float secondsRotation;
    float minutesRotation;
    float hoursRotation;

    void Start() {
        System.DateTime time = System.DateTime.Now;
        secondsRotation = time.Second * 6; // 360 gradi / 60 secondi = 6 gradi per secondo
        minutesRotation = (time.Minute + time.Second / 60.0f) * 6; // 360 gradi / 60 minuti = 6 gradi per minuto
        hoursRotation = (time.Hour + time.Minute / 60.0f) * 30; // 360 gradi / 12 ore = 30 gradi per ora

        iTween.RotateTo(secondHand, iTween.Hash("x", secondsRotation, "time", 0.5, "easetype", "easeOutQuint"));
        iTween.RotateTo(minuteHand, iTween.Hash("x", minutesRotation, "time", 0.5, "easetype", "easeOutQuint"));
        iTween.RotateTo(hourHand, iTween.Hash("x", hoursRotation, "time", 0.5, "easetype", "easeOutQuint"));

        StartCoroutine("UpdateClock");
    }

    IEnumerator UpdateClock() {
        while (true) {

         iTween.RotateAdd(secondHand, iTween.Hash("x", 6, "time", 0.1, "easetype", "easeOutQuint"));
         iTween.RotateAdd(minuteHand, iTween.Hash("x", 0.1, "time", 0.0, "easetype", "easeOutQuint"));
         iTween.RotateAdd(hourHand, iTween.Hash("x", 0.00833, "time", 0.0, "easetype", "easeOutQuint"));

            yield return new WaitForSeconds(1);
        }
    }
}
