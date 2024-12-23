using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;

    private string[] ageLabels = { "Giovane", "Adulto", "Anziano", "Anziano con rughe" };

    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = ageLabels.Length - 1;

        // Ascolta l'evento di cambiamento del valore dello slider
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        // Chiamare manualmente il gestore dell'evento con il valore iniziale dello slider
        OnSliderValueChanged(slider.value);
    }

    void OnSliderValueChanged(float value)
    {
        sliderText.text = ageLabels[(int)value];
    }

   
}
