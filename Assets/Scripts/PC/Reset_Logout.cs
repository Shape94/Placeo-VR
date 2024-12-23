using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System;
using System.IO;

public class Reset_Logout : MonoBehaviour
{
    public void Restart()
    {
        // Percorso del file PlayfabConfig.json
        string filePath = Path.Combine(Application.dataPath, "Resources/PlayfabConfig.json");

        if (File.Exists(filePath))
        {
            // Leggi il contenuto del file JSON
            string jsonContent = File.ReadAllText(filePath);

            var configData = JsonConvert.DeserializeObject<PlayfabConfig>(jsonContent);

            configData.Email = "";
            configData.Password = "";

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

            Debug.Log("Email impostata su: " + configData.Email);
            Debug.Log("Password impostata su: " + configData.Password);
        }
        else
        {
            Debug.LogError("Il file PlayfabConfig.json non esiste");
        }

        SceneManager.LoadScene("Placeo VR - PC");
    }

    public class PlayfabConfig
    {
        public string Studio;
        public string TitleId;
        public string Email;
        public string Password;
        public string DeveloperSecretKey;
    }
}
