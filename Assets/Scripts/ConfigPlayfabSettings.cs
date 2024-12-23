using System;
using System.IO;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class ConfigPlayfabSettings : MonoBehaviour
{
    // Riferimento alla tua istanza di PlayFabSharedSettings
    public PlayFabSharedSettings sharedSettings;
    //public PlayFab.PfEditor.PlayFabEditorPrefsSO playfabEditorPrefsSO;
 
    public TextMeshProUGUI titleIDbutton;
    public TextMeshProUGUI titleIDInputField;

    public void updateTitleID(){
        if (string.IsNullOrEmpty(titleIDInputField.text))
        {
            Debug.LogError("titleIDInputField.text è vuoto o nullo");
            return;
        }

        sharedSettings.TitleId = titleIDInputField.text;

        // Percorso del file PlayfabConfig.json
        string filePath = Path.Combine(Application.dataPath, "Resources/PlayfabConfig.json");

        if (File.Exists(filePath))
        {
            // Leggi il contenuto del file JSON
            string jsonContent = File.ReadAllText(filePath);

            var configData = JsonConvert.DeserializeObject<PlayfabConfig>(jsonContent);

            // Modifica il campo TitleId con titleIDInputField.text
            configData.TitleId = titleIDInputField.text;

            // Serializza l'oggetto modificato in una stringa JSON
            string updatedJsonContent = JsonConvert.SerializeObject(configData, Formatting.Indented);

            try
            {
                // Scrivi la stringa JSON nel file
                File.WriteAllText(filePath, updatedJsonContent);
                Debug.Log("File JSON aggiornato con successo: " + filePath);
            }
            catch (Exception e)
            {
                Debug.LogError("Errore durante la scrittura del file: " + e.Message);
            }

            Debug.Log("Title ID impostato su: " + sharedSettings.TitleId);

            titleIDbutton.text = "Title ID:\n" + configData.TitleId;
            titleIDInputField.text = configData.TitleId;
        }
        else
        {
            Debug.LogError("Il file PlayfabConfig.json non esiste");
        }
    }

    /*void Start()
    {
        // Percorso del file PlayfabConfig.json
        string filePath = Path.Combine(Application.dataPath, "Resources/PlayfabConfig.json");

        if (File.Exists(filePath))
        {
            // Leggi il contenuto del file JSON
            string jsonContent = File.ReadAllText(filePath);

            // Deserializza il contenuto JSON in un oggetto
            var configData = JsonConvert.DeserializeObject<PlayfabConfig>(jsonContent);

            // Imposta il TitleId e Secret Key dal valore nel file JSON
            //playfabEditorPrefsSO.SelectedStudio = configData.Studio;
            sharedSettings.TitleId = configData.TitleId;
            sharedSettings.DeveloperSecretKey = configData.DeveloperSecretKey;
            
            // Ora puoi utilizzare sharedSettings.TitleId nel resto del tuo gioco
            Debug.Log("Title ID impostato su: " + sharedSettings.TitleId);
            Debug.Log("Secret key impostata su: " + sharedSettings.DeveloperSecretKey);

            titleIDbutton.text = "Title ID:\n" + configData.TitleId;
            titleIDInputField.text = configData.TitleId;
        }
        else
        {
            Debug.LogError("Il file PlayfabConfig.json non esiste");
        }
    }*/
}

[System.Serializable]
public class PlayfabConfig
{
    public string Studio;
    public string TitleId;
    public string Email;
    public string Password;
    public string DeveloperSecretKey;
}

