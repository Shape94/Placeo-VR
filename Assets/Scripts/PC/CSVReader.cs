using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using RedScarf.EasyCSV;
using TMPro;
using System.Linq;
using System;

public class CSVReader : MonoBehaviour
{
    CsvTable table;
    public GameObject showCSVPrefab;

    public GameObject splashScreen;
    public GameObject noDataScreen;
    public GameObject dataScreen;
    public GameObject Content;

    private Dictionary<string, GameObject> instantiatedCSVs = new Dictionary<string, GameObject>();

    void Start()
    {
        CsvHelper.Init();
    }

    public void GetCSVs(){
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnCSVsDataRecieved, OnError);
    }

    void OnCSVsDataRecieved(GetUserDataResult result){

        splashScreen.SetActive(true);

        // Crea un elenco temporaneo per tenere traccia delle chiavi ricevute
        HashSet<string> receivedCSVKeys = new HashSet<string>(result.Data.Keys);

        // Elimina i prefab per le chiavi che non sono più presenti
        List<string> keysToRemove = new List<string>();
        foreach(var key in instantiatedCSVs.Keys)
        {
            if(!receivedCSVKeys.Contains(key))
            {
                Destroy(instantiatedCSVs[key]);
                keysToRemove.Add(key);
            }
        }

        // Rimuovi le chiavi eliminate dall'elenco istanziato
        foreach(var key in keysToRemove)
        {
            instantiatedCSVs.Remove(key);
        }

        
        

        // Istanza nuovi prefab per le nuove chiavi
        foreach(var entry in result.Data)
        {
            

            if(entry.Key.StartsWith("CSV_") && !instantiatedCSVs.ContainsKey(entry.Key))
            {
                // Crea la tabella CSV dalla stringa CSV
                table = CsvHelper.Create(entry.Key, entry.Value.Value);
                // Leggi il "Nome Utente" dalla seconda riga e terza colonna (indice 1,2)
                string nomeUtente = table.Read(1, 2);
                string cognomeUtente = table.Read(1, 3);
                string nomeSimulazione = table.Read(1, 1);
                string data = table.Read(1, 5);
                string text = entry.Value.Value;
                string IDSim = table.Read(1, 0);
                string timeStart = table.Read(1, 6);
                string timeEnd = table.Read(table.RowCount-1, 7);
                string duration = table.Read(table.RowCount-1, 8);
                string note = table.Read(1,4);

                dataScreen.SetActive(true);
                GameObject csvPrefabInstance = Instantiate(showCSVPrefab, gameObject.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0));
                csvPrefabInstance.name = entry.Key;

                // Modifica il nome nell'oggetto ShowSimulationValues
            //ShowSimulationValues showSimValues = csvPrefabInstance.GetComponent<ShowSimulationValues>();
            ShowCSVValues showCSVValues = csvPrefabInstance.GetComponent<ShowCSVValues>();
            
            if(showCSVValues != null)
            {
                showCSVValues.nome.text = nomeUtente + " " + cognomeUtente;
                showCSVValues.nomeSimulazionbe.text = nomeSimulazione;
                showCSVValues.parametersJson.text = text;
                showCSVValues.IDSimulazione.text = IDSim;

                
                 // Controlla se timeStart e timeEnd sono inizializzati prima di utilizzarli
    if (!string.IsNullOrEmpty(timeStart))
    {
        showCSVValues.oraInizio.text = timeStart.Substring(0, timeStart.Length - 3);
    }
    if (!string.IsNullOrEmpty(timeEnd))
    {
        showCSVValues.oraFine.text = timeEnd.Substring(0, timeEnd.Length - 3);
    }

    if (!string.IsNullOrEmpty(duration))
    {
    // Supponiamo che 'duration' sia una stringa nel formato "HH:mm:ss"
    TimeSpan time = TimeSpan.Parse(duration);

    string hoursText = time.Hours > 0 ? time.Hours + (time.Hours == 1 ? " ora, " : " ore, ") : "";
    string minutesText = time.Minutes > 0 ? time.Minutes + (time.Minutes == 1 ? " minuto, " : " minuti, ") : "";
    string secondsText = time.Seconds > 0 ? time.Seconds + (time.Seconds == 1 ? " secondo" : " secondi") : "";

    string humanReadableDuration = hoursText + minutesText + secondsText;

    // Rimuovi l'ultima virgola e lo spazio, se presenti
    if (humanReadableDuration.EndsWith(", "))
    {
        humanReadableDuration = humanReadableDuration.Remove(humanReadableDuration.Length - 2);
    }

    showCSVValues.durata.text = humanReadableDuration;
    }

    






                    for (int i = 1; i < table.RowCount; i++)
                {
                    // Leggi i valori dalla riga corrente
                    string value1 = table.Read(i, 9);
                    string value2 = table.Read(i, 10);
                    string value3 = table.Read(i, 11);

                    //Destroy(showCSVValues.posizione);

                    // Istanza il prefab 'positionTextPrefab'
                    TextMeshProUGUI positionInstance = Instantiate(showCSVValues.posizione, showCSVValues.posizione.transform.parent);

                    // Ottieni il componente TextMeshProUGUI e imposta il testo
                    TextMeshProUGUI textMesh = positionInstance.GetComponent<TextMeshProUGUI>();
                    if (textMesh != null)
                    {
                        textMesh.text = value1 + ", " + value2 + ", " + value3;
                    }
                }
                Destroy(showCSVValues.posizione.gameObject);








                showCSVValues.note.text = note;
                if(data != ""){
                    showCSVValues.data.text = data;
                }
            }


                instantiatedCSVs.Add(entry.Key, csvPrefabInstance); // Aggiungi la chiave e il prefab all'elenco istanziato
            }
        }

        if(Content.transform.childCount == 0){
            noDataScreen.SetActive(true);
            splashScreen.SetActive(false);
            dataScreen.SetActive(false);
        }else{
            dataScreen.SetActive(true);
            noDataScreen.SetActive(false);
            splashScreen.SetActive(false);
        }
        
    }

    void OnError(PlayFabError error){
        Debug.Log("Errore lettura CSV");
        Debug.Log(error.GenerateErrorReport());
    }
}