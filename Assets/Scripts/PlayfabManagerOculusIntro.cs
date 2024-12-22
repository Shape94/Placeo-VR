using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayfabManagerOculusIntro : MonoBehaviour
{

    //TEST PER JSON
    public ReadNewSimulationJSONOculus readNewSimulationJSONOculus;

    //TITLE DATA
    public FadeCanvas fadeCanvas;
    public TextMeshProUGUI messageText;
    public Tutorial tutorial;
    public Toggle distanceGrab;

    void Start()
    {
        //Login();
        //LoginButton();
        distanceGrab.isOn = false;
        distanceGrab.isOn = true;
        LoginRemembered();
    }

   

    //LOGIN
    void Login(){
       
        var request = new LoginWithEmailAddressRequest{Email = "​", Password = ""};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public void LoginAsGuest(){
        var request = new LoginWithEmailAddressRequest{Email = "guest@guest.com" + "​", Password = "Guest1234"};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public GameObject canvasLogin;
    public GameObject canvasWaitingSimulation;
    public GameObject canvasLogout;
    public GameObject keyBoard;

     public class User
    {
        public string mail;
        public string psw;
    }
    public Toggle rememberme;

    void LoginRemembered(){
        string path = Path.Combine(Application.persistentDataPath, "login.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            User user = JsonUtility.FromJson<User>(json);

            Debug.Log("Nome: " + user.mail);
            Debug.Log("Cognome: " + user.psw);

            var request = new LoginWithEmailAddressRequest{Email = user.mail, Password = user.psw};
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
        }
       
    }
    

    void OnLoginSuccess(LoginResult result){
        messageText.text = "Login effettuato!";
        Debug.Log("Accesso eseguito! / Account creato!");

        if(rememberme.isOn == true){
            User user = new User();
            user.mail = emailInput.text + "​";
            user.psw = passwordInput.text;

            string json = JsonUtility.ToJson(user);
            string path = Path.Combine(Application.persistentDataPath, "login.json");
            File.WriteAllText(path, json);

            Debug.Log("File salvato in: " + path);

        }


        GetPlayFabUsername();
        canvasWaitingSimulation.SetActive(true);
        canvasLogout.SetActive(true);
        canvasLogin.SetActive(false);
        keyBoard.SetActive(false);

        //SceneManager.LoadScene(1);
        
        GetTitleData();
        //GetParametersOculus();
        StartCoroutine(CallGetParametersOculusEveryTenSeconds());

        //SceneManager.LoadScene(1);

        //GetCharacters();

        //GetParameters();

        //activeGameObject.SetActive(true);
        //deactiveGameObject.SetActive(false);
        
    }

    

    IEnumerator CallGetParametersOculusEveryTenSeconds(){
    while(true){
        if (tutorial.currentIndex == 0) // Controlla se currentIndex è 0
            {
                GetParametersOculus();
            }
        yield return new WaitForSeconds(10);
    }
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

   

    public GameObject miniSplashScreen;
    public GameObject miniReadyScreen;

    public void GetParametersOculus() {
    PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnParametersDataRecievedOculus, OnError);
}

void OnParametersDataRecievedOculus(GetUserDataResult result) {
    Debug.Log("Dati della simulazione ricevuti!");
    if(result.Data != null) {
        foreach(var key in result.Data.Keys) {

            //MODIFICA ROVINATO
            if(key.StartsWith("Simulation_Rovinato") && key.EndsWith("!")) {
                miniReadyScreen.SetActive(true);
                miniSplashScreen.SetActive(false);

                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                    KeysToRemove = new List<string> { key }
                }, result => Debug.Log("Chiave eliminata con successo!"), error => Debug.Log("Errore nell'eliminazione della chiave!"));

                string newKey = key.Remove(key.Length - 1) + "*";

                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                    Data = new Dictionary<string, string>() {
                        {newKey, result.Data[key].Value}
                    }
                }, result => Debug.Log("Dati aggiornati con successo!"), error => Debug.Log("Errore nell'aggiornamento dei dati!"));

                fadeCanvas.StartFadeIn();
                Invoke("LoadRovinatoSceneAfterDelay", 4f);
            }




            else if(key.StartsWith("Simulation_") && key.EndsWith("!")) {
                miniReadyScreen.SetActive(true);
                miniSplashScreen.SetActive(false);

                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                    KeysToRemove = new List<string> { key }
                }, result => Debug.Log("Chiave eliminata con successo!"), error => Debug.Log("Errore nell'eliminazione della chiave!"));

                string newKey = key.Remove(key.Length - 1) + "*";

                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                    Data = new Dictionary<string, string>() {
                        {newKey, result.Data[key].Value}
                    }
                }, result => Debug.Log("Dati aggiornati con successo!"), error => Debug.Log("Errore nell'aggiornamento dei dati!"));

                fadeCanvas.StartFadeIn();
                Invoke("LoadSceneAfterDelay", 4f);
            }


            


            if(key.StartsWith("CSV_") && key.EndsWith("!")) {
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                    KeysToRemove = new List<string> { key }
                }, result => Debug.Log("Chiave eliminata con successo!"), error => Debug.Log("Errore nell'eliminazione della chiave!"));

                string newKey = key.Remove(key.Length - 1) + "*";

                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                    Data = new Dictionary<string, string>() {
                        {newKey, result.Data[key].Value}
                    }
                }, result => Debug.Log("Dati aggiornati con successo!"), error => Debug.Log("Errore nell'aggiornamento dei dati!"));
            }
        }
    } 
} 

void LoadSceneAfterDelay() {
    SceneManager.LoadScene(1);
}

void LoadRovinatoSceneAfterDelay() {
    SceneManager.LoadScene(3);
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

}
