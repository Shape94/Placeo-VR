using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedScarf.EasyCSV;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class UpdateCSV : MonoBehaviour
{
    CsvTable table;
    int currentRow = 0;
    DateTime time2;

    public string paziente;

    void Start()
    {
        CsvHelper.Init();
        GetCurrentCSV();
    }

    IEnumerator CallAddRowEveryTenSeconds(){
    while(true){
        AddRow();
        yield return new WaitForSeconds(10);
    }
}

    void GetCurrentCSV()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnCSVsDataRecieved, OnError);
    }

    void OnCSVsDataRecieved(GetUserDataResult result)
    {
        foreach (var entry in result.Data)
        {
            if (entry.Key.StartsWith("CSV_") && entry.Key.EndsWith("*"))
            {
                table = CsvHelper.Create(entry.Key, entry.Value.Value);

                string currentDate = System.DateTime.Now.ToString("dd/MM/yyyy");
                table.Write(1, 5, currentDate);

                string currentTime = System.DateTime.Now.ToString("HH:mm:ss");
                table.Write(1, 6, currentTime);
                table.Write(1, 7, currentTime);

                time2 = System.DateTime.Parse(table.Read(1, 6));

                paziente = table.Read(1, 2) + " " + table.Read(1, 3);

                StartCoroutine(CallAddRowEveryTenSeconds());
            }
        }
    }
    

    void AddRow()
{
    currentRow++;
    
    // Utilizza un ciclo for per leggere e scrivere i valori nelle celle (X, 0) a (X, 4)
    for (int i = 0; i <= 6; i++)
    {
        string value = table.Read(1, i);
        table.Write(currentRow, i, value);
    }

    string currentTime = System.DateTime.Now.ToString("HH:mm:ss");
    
    table.Write(currentRow, 7, currentTime);

     // Calcola la differenza di orario
    DateTime time1 = System.DateTime.Parse(table.Read(currentRow, 7));
    
    TimeSpan timeDifference = time1 - time2;

    // Scrivi la differenza di orario nella cella (X, 8)
    table.Write(currentRow, 8, timeDifference.ToString());

     // Ottieni la posizione del GameObject
    Vector3 position = transform.position;

    // Scrivi le coordinate X, Y e Z nelle celle (X, 9), (X, 10) e (X, 11)
    table.Write(currentRow, 9, position.x.ToString());
    table.Write(currentRow, 10, position.y.ToString());
    table.Write(currentRow, 11, position.z.ToString());

    // Ottieni la rotazione del GameObject in valore quaternione
    Quaternion rotation = transform.rotation;

    // Scrivi le rotazioni X, Y, Z, W nelle celle (X, 12), (X, 13), (X, 14), (X, 15)
    table.Write(currentRow, 12, rotation.x.ToString());
    table.Write(currentRow, 13, rotation.y.ToString());
    table.Write(currentRow, 14, rotation.z.ToString());
    table.Write(currentRow, 15, rotation.w.ToString());

    UploadCSV();
}


    void UploadCSV()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> { { table.Name, table.GetData() } }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataUploaded, OnError);
    }

    void OnDataUploaded(UpdateUserDataResult result)
    {
        Debug.Log("CSV caricato con successo");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Errore lettura CSV");
        Debug.Log(error.GenerateErrorReport());
    }
}

