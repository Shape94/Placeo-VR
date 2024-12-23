using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class SimulationReady : MonoBehaviour
{
    public GameObject startButton;
    public GameObject readyButton;

    public void UpdateKeyName()
    {
        var getRequest = new GetUserDataRequest { Keys = null };

        PlayFabClientAPI.GetUserData(getRequest, result =>
        {
            if (result.Data != null)
            {
                var data = result.Data;
                var keysToUpdate = new Dictionary<string, string>();

                // Identifica le chiavi che terminano con "!"
                foreach (var key in data.Keys)
                {
                    if (key.EndsWith("!") && key.StartsWith("Simulation_"))
                    {
                        string newKey = key.TrimEnd('!');
                        keysToUpdate[newKey] = data[key].Value;
                    }
                }

                if (keysToUpdate.Count > 0)
                {
                    // Aggiorna i dati utente con le nuove chiavi
                    var updateOldKeysRequest = new UpdateUserDataRequest
                    {
                        Data = keysToUpdate
                    };

                    PlayFabClientAPI.UpdateUserData(updateOldKeysRequest, updateOldKeysResult =>
                    {
                        // Rimuovi le vecchie chiavi
                        var keysToRemove = new List<string>(keysToUpdate.Keys.Count);
                        foreach (var key in keysToUpdate.Keys)
                        {
                            keysToRemove.Add(key + "!");
                        }

                        var deleteOldKeysRequest = new UpdateUserDataRequest
                        {
                            KeysToRemove = keysToRemove
                        };

                        PlayFabClientAPI.UpdateUserData(deleteOldKeysRequest, deleteOldKeysResult =>
                        {
                            Debug.Log("Old keys renamed and removed successfully!");

                            // Continua con la logica originale
                            if (data.ContainsKey(gameObject.name))
                            {
                                string oldValue = data[gameObject.name].Value;
                                string newKeyName = gameObject.name + "!";

                                var updateRequest = new UpdateUserDataRequest
                                {
                                    Data = new Dictionary<string, string> { { newKeyName, oldValue } }
                                };

                                PlayFabClientAPI.UpdateUserData(updateRequest, updateResult =>
                                {
                                    Debug.Log("New key created successfully!");

                                    var deleteRequest = new UpdateUserDataRequest
                                    {
                                        KeysToRemove = new List<string> { gameObject.name }
                                    };

                                    PlayFabClientAPI.UpdateUserData(deleteRequest, deleteResult =>
                                    {
                                        Debug.Log("Old key removed successfully!");
                                        startButton.SetActive(false);
                                        readyButton.SetActive(true);
                                    }, error =>
                                    {
                                        Debug.Log("Error removing old key: " + error.GenerateErrorReport());
                                    });

                                }, error =>
                                {
                                    Debug.Log("Error creating new key: " + error.GenerateErrorReport());
                                });
                            }

                        }, error =>
                        {
                            Debug.Log("Error removing old keys: " + error.GenerateErrorReport());
                        });

                    }, error =>
                    {
                        Debug.Log("Error updating old keys: " + error.GenerateErrorReport());
                    });
                }
                else
                {
                    // Se non ci sono chiavi da aggiornare, continua con la logica originale
                    if (data.ContainsKey(gameObject.name))
                    {
                        string oldValue = data[gameObject.name].Value;
                        string newKeyName = gameObject.name + "!";

                        var updateRequest = new UpdateUserDataRequest
                        {
                            Data = new Dictionary<string, string> { { newKeyName, oldValue } }
                        };

                        PlayFabClientAPI.UpdateUserData(updateRequest, updateResult =>
                        {
                            Debug.Log("New key created successfully!");

                            var deleteRequest = new UpdateUserDataRequest
                            {
                                KeysToRemove = new List<string> { gameObject.name }
                            };

                            PlayFabClientAPI.UpdateUserData(deleteRequest, deleteResult =>
                            {
                                Debug.Log("Old key removed successfully!");
                                startButton.SetActive(false);
                                readyButton.SetActive(true);
                            }, error =>
                            {
                                Debug.Log("Error removing old key: " + error.GenerateErrorReport());
                            });

                        }, error =>
                        {
                            Debug.Log("Error creating new key: " + error.GenerateErrorReport());
                        });
                    }
                }
            }
        }, error =>
        {
            Debug.Log("Error getting user data: " + error.GenerateErrorReport());
        });
    }
}
