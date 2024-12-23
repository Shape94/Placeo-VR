using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedScarf.EasyCSV;
using TMPro;
using System;
using PlayFab;
using PlayFab.ClientModels;
using System.Linq;

public class CreateCSV : MonoBehaviour
{
    //public TextAsset text;
        CsvTable table;
        public TextMeshProUGUI nomeUtente;
        public TextMeshProUGUI cognomeUtente;
        public TextMeshProUGUI note;
        public TextMeshProUGUI nomeSimulazione;

        string headers = "ID Simulazione, Nome Simulazione, Nome Utente, Cognome Utente, Note, Data, Ora Inizio, Ora Fine, Durata, Posizione X, Posizione Y, Posizione Z, Rotazione X, Rotazione Y, Rotazione Z, Rotazione W";

        public void UploadCSV()
        {
            string currentDateTime = DateTime.Now.ToString("dd/MM/yy_HH:mm:ss");
            string csvName ="CSV_" + nomeUtente.text + "_" + cognomeUtente.text + "_" + currentDateTime + "!";

            CsvHelper.Init();
            table = CsvHelper.Create(csvName, headers);

            table.Write(1,0,gameObject.name);
            table.Write(1,1,nomeSimulazione.text);
            table.Write(1,2,nomeUtente.text);
            table.Write(1,3,cognomeUtente.text);
            table.Write(1,4,note.text);

            Debug.Log(table.Read(1,2));
            Debug.Log(table.Read(1,3));
            Debug.Log(table.Read(1,4));
            Debug.Log(table.GetData());
            Debug.Log(csvName);

            SendCSV(csvName, table.GetData());
        }

        public void SendCSV(string csvNome, string csvData){
            DeleteUnusedCSV();
        var request = new UpdateUserDataRequest
    {
        Data = new Dictionary<string, string>
        {
            {csvNome, csvData}
        }
    };
    PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    void OnDataSend(UpdateUserDataResult result){
        Debug.Log("Invio dei dati dell'utente riuscito!");
        
        }

        void OnError(PlayFabError error){
        Debug.Log("Errore nell'invio del CSV");
        Debug.Log(error.GenerateErrorReport());
    }

    public void DeleteUnusedCSV()
{
    PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
    {
        var keysToRemove = result.Data.Keys.Where(key => key.StartsWith("CSV_") && key.EndsWith("!")).ToList();
        if (keysToRemove.Count > 0)
        {
            var request = new UpdateUserDataRequest
            {
                KeysToRemove = keysToRemove
            };

            PlayFabClientAPI.UpdateUserData(request, OnDataRemoved, OnError);
        }
    }, OnError);
}


void OnDataRemoved(UpdateUserDataResult result){
        Debug.Log("CSV eliminato");
        }
}
