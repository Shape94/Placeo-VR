using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DigitalClock : MonoBehaviour
{
    public TextMeshProUGUI orario;

    void Start()
    {
        StartCoroutine(UpdateClock());
    }

    IEnumerator UpdateClock()
    {
        while (true)
        {
            // Ottieni l'orario attuale
            System.DateTime time = System.DateTime.Now;

            // Formatta l'orario in ore e minuti
            string timeText = time.ToString("HH:mm");

            // Mostra l'orario nel TextMeshProUGUI
            orario.text = timeText;

            // Aspetta 60 secondi prima del prossimo aggiornamento
            yield return new WaitForSeconds(60);
        }
    }
}

