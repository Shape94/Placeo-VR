using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowParameterValue : MonoBehaviour
{
    public object value;
    public string name;
    public TextMeshProUGUI nome;
    public string valore;

    public object GetParameterValue()
    {
        Transform lastChild = transform.GetChild(transform.childCount - 1);

        Toggle toggle = lastChild.GetComponentInChildren<Toggle>();
        TMP_Dropdown dropdown = lastChild.GetComponentInChildren<TMP_Dropdown>();
        Slider slider = lastChild.GetComponentInChildren<Slider>();

        if (dropdown != null) // Verifica che il dropdown esista
        {
            value = dropdown.value;
        }
        else if (slider != null) // Verifica che lo slider esista
        {
            value = slider.value;
        }
        else if (toggle != null) // Verifica che il toggle/checkbox esista
        {
            value = toggle.isOn;
        }

        return value;
    }

    private int n;

    public object GetParameterValueforJSON(int n)
    {
        Transform lastChild = transform.GetChild(transform.childCount - 1);

        TMP_Dropdown dropdown = lastChild.GetComponentInChildren<TMP_Dropdown>();
        GenericSlider slider = lastChild.GetComponentInChildren<GenericSlider>();

        if (dropdown != null) // Verifica che il dropdown esista
        {
            valore = dropdown.options[n].text;
        }
        else if (slider != null) // Verifica che lo slider esista
        {
            valore = slider.labels[n];
        }
        return valore;

    }



    public void Start(){

        name = nome.text;
    
    }
}