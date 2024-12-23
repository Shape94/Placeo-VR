using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class KeyboardLoginManager : MonoBehaviour, ISelectHandler
{
    public TMP_InputField inputKeyboard;
    public TMP_InputField inputMail;
    public TMP_InputField inputPassword;

    private TMP_InputField selectedInputField;

    void Start()
    {
        // Inizializza il campo di input selezionato come null
        selectedInputField = null;

        // Aggiungi listener agli input field per gestire la selezione
        inputMail.onSelect.AddListener(delegate { OnSelectInputField(inputMail); });
        inputPassword.onSelect.AddListener(delegate { OnSelectInputField(inputPassword); });

        // Aggiungi listener per l'aggiornamento del testo della tastiera virtuale
        inputKeyboard.onValueChanged.AddListener(OnVirtualKeyboardInputChanged);
    }

    // Metodo chiamato quando un campo di input è selezionato
public void OnSelectInputField(TMP_InputField inputField)
{
    selectedInputField = inputField;

    // Cancella il testo nei campi di input mail e password se sono stati selezionati
    if (selectedInputField == inputMail || selectedInputField == inputPassword)
    {
        selectedInputField.text = "";
    }

    if (string.IsNullOrEmpty(selectedInputField.text))
    {
        inputKeyboard.text = "";
        inputKeyboard.placeholder.GetComponent<TextMeshProUGUI>().text = selectedInputField.placeholder.GetComponent<TextMeshProUGUI>().text;
    }
    else
    {
        inputKeyboard.text = selectedInputField.text;
        inputKeyboard.placeholder.GetComponent<TextMeshProUGUI>().text = "";
    }

    inputKeyboard.Select(); // Assicurati che la tastiera virtuale sia selezionata per l'input
}


    // Metodo chiamato quando il testo della tastiera virtuale cambia
    public void OnVirtualKeyboardInputChanged(string text)
    {
        if (selectedInputField != null)
        {
            selectedInputField.text = text;
        }
    }

    // Metodo richiesto dall'interfaccia ISelectHandler, ma non utilizzato qui
    public void OnSelect(BaseEventData eventData) { }
}
