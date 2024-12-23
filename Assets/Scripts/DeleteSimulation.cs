using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class DeleteSimulationButton : MonoBehaviour
{
    public GameObject nomeSimulazione;
    public void DeleteSimulationData()
{
    var request = new UpdateUserDataRequest
    {
        KeysToRemove = new List<string> { nomeSimulazione.name }
    };

    PlayFabClientAPI.UpdateUserData(request, OnDataRemoved, OnError);

    
}

void OnDataRemoved(UpdateUserDataResult result){
        Debug.Log("Simulazione eliminata!");
        Destroy(nomeSimulazione);
        }

 void OnError(PlayFabError error){
        Debug.Log("Errore durante il login / creazione dell'account!");
        Debug.Log(error.GenerateErrorReport());
    
    }
}