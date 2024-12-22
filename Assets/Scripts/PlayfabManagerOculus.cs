using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.UI;

public class PlayfabManagerOculus : MonoBehaviour
{

    //TEST PER JSON
    public ReadNewSimulationJSONOculus readNewSimulationJSONOculus;

    //TITLE DATA
    public TextMeshProUGUI messageText;
    public TeleportPlayer teleportPlayer;

    void Awake()
    {
        //Login();
        GetParametersOculus();
        //LoginButton();
        if (teleportPlayer != null){
            teleportPlayer.Teleport();
        }
    }

    

    //LOGIN
    void Login(){
   
        var request = new LoginWithEmailAddressRequest{Email =  "​", Password = ""};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public GameObject canvasLogin;
    public GameObject canvasWaitingSimulation;
    public GameObject canvasLogout;
    public GameObject keyBoard;
    void OnLoginSuccess(LoginResult result){
        messageText.text = "Login effettuato!";
        Debug.Log("Accesso eseguito! / Account creato!");
        GetPlayFabUsername();


        GetTitleData();
        GetParametersOculus();

        
    }

    void OnError(PlayFabError error){
        Debug.Log("Errore durante il login / creazione dell'account!");
        Debug.Log(error.GenerateErrorReport());
        messageText.text = error.ErrorMessage;
        messageText.text = error.GenerateErrorReport();
    }

    //PLAYER DATA
  

    void OnDataSend(UpdateUserDataResult result){
        Debug.Log("Invio dei dati dell'utente riuscito!");
     
        
        }

    //LEADERBOARD
 

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result){
        Debug.Log("Invio alla leaderboard riuscito!");
    }

    
    
    //TITLE DATA
    void GetTitleData(){
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), OnTitleDataRecieved, OnError);
    }

    void OnTitleDataRecieved(GetTitleDataResult result){
        if (result.Data == null || result.Data.ContainsKey("Message") == false){
            Debug.Log("Nessun messaggio!");
            return;
        }
        messageText.text = result.Data["Message"];
    }

   

    //REGISTRAZIONE/LOGIN/RESET PASSWORD
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    public void RegisterButton() {
        if (passwordInput.text.Length < 6){
            messageText.text = "Password troppo corta!";
            return;
        }

        var request = new RegisterPlayFabUserRequest {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
     }

     void OnRegisterSuccess(RegisterPlayFabUserResult result){
        messageText.text = "Registrato e login effettuato";
        
  
        }

     public void LoginButton() {
        var request = new LoginWithEmailAddressRequest {
            Email = emailInput.text + "​", Password = passwordInput.text 
            
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public void ResetPasswordButton() {
        var request = new SendAccountRecoveryEmailRequest{
            Email = emailInput.text,
            TitleId = ""
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result){
        messageText.text = "Email per il ripristino della password inviata!";
    }

    //CLOUD SCRIPT
    public TextMeshProUGUI output;
    public TextMeshProUGUI input;
    public void ExecuteButton() {
        var request = new ExecuteCloudScriptRequest{
            FunctionName = "hello",
            FunctionParameter = new{
                name = input.text
            }
        };
        PlayFabClientAPI.ExecuteCloudScript(request, OnExecuteSuccess, OnError);
    }

    void OnExecuteSuccess(ExecuteCloudScriptResult result){
        output.text = result.FunctionResult.ToString();
    }

    

    //TEST PER PRENDERE I PARAMETRI DAL JSON E FARCI COSE
    public string currentSimulation;
    public string currentCSV;
    public void GetParametersOculus(){
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnParametersDataRecievedOculus, OnError);
        }
    void OnParametersDataRecievedOculus(GetUserDataResult result){
        Debug.Log("Dati della simulazione ricevuti!");

        foreach(var entry in result.Data){
            if(entry.Key.StartsWith("Simulation_") && entry.Key.EndsWith("*")){
                currentSimulation = entry.Key;
           

                Parameters parameters = JsonConvert.DeserializeObject<Parameters>(entry.Value.Value);

                readNewSimulationJSONOculus.save(parameters);
                
               
            }
             if(entry.Key.StartsWith("CSV_") && entry.Key.EndsWith("*")){
                currentCSV = entry.Key;
            }}
        } 

    public TextMeshProUGUI userName;
    public void GetPlayFabUsername()
    {
        GetAccountInfoRequest request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnGetAccountInfoFailure);
    }

    void OnGetAccountInfoSuccess(GetAccountInfoResult result)
    {
        string playFabUsername = result.AccountInfo.Username;
        Debug.Log("Il PlayFab username è: " + playFabUsername);
        userName.text = "Ciao,\n" + playFabUsername;
    }

    void OnGetAccountInfoFailure(PlayFabError error)
    {
        Debug.LogError("Si è verificato un errore nel recuperare il PlayFab username: " + error.GenerateErrorReport());
    }

    public void EndSimulation(){
    if (string.IsNullOrEmpty(currentSimulation))
    {
        Debug.LogError("currentSimulation è vuoto!");
        return;
    }

    // Rimuovi l'ultimo carattere da currentSimulation
    string key = currentSimulation.TrimEnd('*');
    string csv = currentCSV.TrimEnd('*');

    // Ottieni i dati esistenti associati a currentSimulation
    PlayFabClientAPI.GetUserData(new GetUserDataRequest { Keys = new List<string> { currentSimulation } }, result =>
    {
        if (result.Data != null && result.Data.ContainsKey(currentSimulation))
        {
            // Utilizza i dati esistenti per la nuova chiave
            var updateRequest = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string> { { key, result.Data[currentSimulation].Value } }
            };

            // Invia la richiesta di aggiornamento a PlayFab
            PlayFabClientAPI.UpdateUserData(updateRequest, OnDataSend, OnError);

            // Crea una richiesta per eliminare la chiave currentSimulation
            var deleteRequest = new UpdateUserDataRequest
            {
                KeysToRemove = new List<string> { currentSimulation }
            };

            // Invia la richiesta di eliminazione a PlayFab
            PlayFabClientAPI.UpdateUserData(deleteRequest, OnDataSend, OnError);
        }
        else
        {
            Debug.LogError("Nessun dato associato a currentSimulation!");
        }
    }, OnError);


    PlayFabClientAPI.GetUserData(new GetUserDataRequest { Keys = new List<string> { currentCSV } }, result =>
    {
        if (result.Data != null && result.Data.ContainsKey(currentCSV))
        {
            // Utilizza i dati esistenti per la nuova chiave
            var updateRequest = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string> { { csv, result.Data[currentCSV].Value } }
            };

            // Invia la richiesta di aggiornamento a PlayFab
            PlayFabClientAPI.UpdateUserData(updateRequest, OnDataSend, OnError);

            // Crea una richiesta per eliminare la chiave currentCSV
            var deleteRequest = new UpdateUserDataRequest
            {
                KeysToRemove = new List<string> { currentCSV }
            };

            // Invia la richiesta di eliminazione a PlayFab
            PlayFabClientAPI.UpdateUserData(deleteRequest, OnDataSend, OnError);
        }
        else
        {
            Debug.LogError("Nessun dato associato a currentCSV!");
        }
    }, OnError);
}





}
