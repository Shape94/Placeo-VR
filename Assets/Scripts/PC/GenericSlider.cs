using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GenericSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;

    [SerializeField] public List<string> labels = new List<string>();


    void Start()
    {
        if (labels != null && labels.Count > 0)
        {
            slider.minValue = 0;
            slider.maxValue = labels.Count - 1;

            // Ascolta l'evento di cambiamento del valore dello slider
            slider.onValueChanged.AddListener(OnSliderValueChanged);

            // Chiamare manualmente il gestore dell'evento con il valore iniziale dello slider
            OnSliderValueChanged(slider.value);
        }
        else
        {
            Debug.LogError("Assicurati di fornire etichette (labels) per lo slider.");
        }
    }

    void OnSliderValueChanged(float value)
    {
        if (labels != null && labels.Count > 0)
        {
            sliderText.text = labels[Mathf.RoundToInt(value)];
        }
    }

   
}
