using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;

public class PlayfabManager : MonoBehaviour
{

    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswordInput;
    public TextMeshProUGUI loginMessage;
    public Toggle loginCheckbox;
    public TextMeshProUGUI registerUsernameInput;
    public TextMeshProUGUI registerEmailInput;
    public TMP_InputField registerPasswordInput;
    public TextMeshProUGUI registerMessage;
    public Toggle registerCheckbox;
    public GameObject tabContent;
    public GameObject singleParamPrefab;



    //HIDE/SHOW UI
    public GameObject activeGameObject;
    public GameObject deactiveGameObject;
    public GameObject activeSimulazioneTabs;
    public GameObject splashScreen;
    public GameObject noDataScreen;
    public GameObject dataScreen;
    public GameObject newSImulationScreen;
    public GameObject mainView;
    public GameObject showSimulationPrefab;

    //TITLE DATA
    public TextMeshProUGUI messageText;

    public CharacterEditor characterEditor;

    //CHARACTERBOXES
    public CharacterBox[] characterBoxes;

    public SaveParametersJSON[] saveParametersJSONs;
    public ReadNewSimulationJSON readNewSimulationJSON;

    public TextMeshProUGUI simulationName;
    public TextMeshProUGUI simulationDescription;

    void Start()
    {   
        newSImulationScreen.SetActive(false);
        mainView.SetActive(false);
        
        //Login();

        // Percorso del file PlayfabConfig.json
        string filePath = Path.Combine(Application.dataPath, "Resources/PlayfabConfig.json");

        if (File.Exists(filePath))
        {
            // Leggi il contenuto del file JSON
            string jsonContent = File.ReadAllText(filePath);

            var configData = JsonConvert.DeserializeObject<PlayfabConfig>(jsonContent);

            if(configData.Email != "" && configData.Password != ""){
                Login(configData.Email, configData.Password);
            }

        }

    }



    //LOGIN
    void Login(string mail, string psw){
        /*var request = new LoginWithCustomIDRequest{
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
        */


        var request = new LoginWithEmailAddressRequest{Email = mail, Password = psw};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);




    }

    public void LoginAsGuest(){
        var request = new LoginWithEmailAddressRequest{Email = "guest@guest.com" + "​", Password = "Guest1234"};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result){
        
        loginMessage.text = "Login effettuato!";
        Debug.Log("Accesso eseguito! / Account creato!");
        GetPlayFabUsername();

        
        mainView.SetActive(false);
        newSImulationScreen.SetActive(false);

        GetParameters();

        if(loginCheckbox.isOn){
             // Percorso del file PlayfabConfig.json
        string filePath = Path.Combine(Application.dataPath, "Resources/PlayfabConfig.json");

        if (File.Exists(filePath))
        {
            // Leggi il contenuto del file JSON
            string jsonContent = File.ReadAllText(filePath);

            var configData = JsonConvert.DeserializeObject<PlayfabConfig>(jsonContent);

            // Modifica il campo TitleId con titleIDInputField.text
            configData.Email = loginEmailInput.text + "​";
            configData.Password = loginPasswordInput.text;

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

            Debug.Log("Email impostata su: " + configData.Email);
            Debug.Log("Password impostata su: " + loginPasswordInput.text);

        }
        else
        {
            Debug.LogError("Il file PlayfabConfig.json non esiste");
        }
    }
        

        activeGameObject.SetActive(true);
        
        deactiveGameObject.SetActive(false);
        
    }

    void OnError(PlayFabError error){
        Debug.Log("Errore durante il login / creazione dell'account!");
        Debug.Log(error.GenerateErrorReport());
        loginMessage.text = error.ErrorMessage;
        registerMessage.text = error.ErrorMessage;
    }

    //PLAYER DATA
    public void GetAppearance(){
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecievied, OnError);
    }

    void OnDataRecievied(GetUserDataResult result){
        Debug.Log("Dati dell'utente ricevuti!");
        if(result.Data != null && result.Data.ContainsKey("Hat") && result.Data.ContainsKey("Skin")){
            characterEditor.SetAppearance(result.Data["Hat"].Value, result.Data["Skin"].Value);
        } else{
                Debug.Log("Dati dell'utente incompleti!");
        }
    }


    public void SaveAppearance(){
        var request = new UpdateUserDataRequest{
            Data = new Dictionary<string, string>{
                {"Hat", characterEditor.Hat },
                {"Skin", characterEditor.Skin }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    void OnDataSend(UpdateUserDataResult result){
        Debug.Log("Invio dei dati dell'utente riuscito!");
        }

    //LEADERBOARD
    public void SendLeaderboard(int score){
        var request = new UpdatePlayerStatisticsRequest{
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate{
                    StatisticName = "LeaderboardTest",
                    Value = score
                 }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result){
        Debug.Log("Invio alla leaderboard riuscito!");
    }

    public void GetLeaderboard(){
        var request = new GetLeaderboardRequest {
            StatisticName = "LeaderboardTest",
            StartPosition = 0,
            MaxResultsCount = 10
      };
     PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result){
        foreach (var item in result.Leaderboard){
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
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

    //SEND MULTIPLE DATA JSON
    public void SaveCharacters(){
        List<Character> characters = new List<Character>();
        foreach (var item in characterBoxes){
            characters.Add(item.ReturnClass());
            }
        var request = new UpdateUserDataRequest{
            Data = new Dictionary<string, string>{
                {"Characters", JsonConvert.SerializeObject(characters) }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    public void GetCharacters(){
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnCharactersDataRecieved, OnError);
        }
    void OnCharactersDataRecieved(GetUserDataResult result){
        Debug.Log("Dati degli utenti ricevuti!");
        if(result.Data != null && result.Data.ContainsKey("Characters")){
            List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(result.Data["Characters"].Value);
            for (int i = 0; i < characterBoxes.Length; i++){
                characterBoxes[i].SetUi(characters[i]);
                }
            }
        }

    //REGISTRAZIONE/LOGIN/RESET PASSWORD
    public TextMeshProUGUI usernameInput;
    public TextMeshProUGUI emailInput;
    public TextMeshProUGUI passwordInput;
    

    public void RegisterButton() {
        if (registerPasswordInput.text.Length < 6){
            registerMessage.text = "Password troppo corta!";
            return;
        }

       
        string user = registerUsernameInput.text;
        user = user.Substring(0, user.Length - 1);
        Debug.Log("Email = " + registerEmailInput.text + ". Password = " + registerPasswordInput.text + ". Username = " + user);
        //Test con la nuova, altrimenti decommenta quello sopra
        var registerRequest = new RegisterPlayFabUserRequest{Email = registerEmailInput.text, Password = registerPasswordInput.text, Username = user};
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnError);
     }

     void OnRegisterSuccess(RegisterPlayFabUserResult result){
        registerMessage.text = "Registrato e login effettuato";

        if(registerCheckbox.isOn){
             // Percorso del file PlayfabConfig.json
        string filePath = Path.Combine(Application.dataPath, "Resources/PlayfabConfig.json");

        if (File.Exists(filePath))
        {
            // Leggi il contenuto del file JSON
            string jsonContent = File.ReadAllText(filePath);

            var configData = JsonConvert.DeserializeObject<PlayfabConfig>(jsonContent);

            // Modifica il campo TitleId con titleIDInputField.text
            configData.Email = registerEmailInput.text + "​";
            configData.Password = registerPasswordInput.text;

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

            Debug.Log("Email impostata su: " + configData.Email);
            Debug.Log("Password impostata su: " + registerPasswordInput.text);

        }
        else
        {
            Debug.LogError("Il file PlayfabConfig.json non esiste");
        }
    }
        
        activeGameObject.SetActive(true);
        deactiveGameObject.SetActive(false);
        }

     public void LoginButton() {
        var request = new LoginWithEmailAddressRequest{Email = loginEmailInput.text + "​", Password = loginPasswordInput.text};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);

    }

    public void ResetPasswordButton() {
        var request = new SendAccountRecoveryEmailRequest{
            Email = loginEmailInput.text + "​",
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

    //Scrivi JSON coi parametri impostati per la simulazione

    public Toggle runSimulationNow;
    public void SaveParameters()
{   
    saveParametersJSONs[0].TotalParameters();

    // Ottieni l'ID dalla data e dall'orario attuali
    string currentDateTime = DateTime.Now.ToString("dd/MM/yy_HH:mm:ss");
    string simulationID = "Simulation_" + currentDateTime;

    if(runSimulationNow.isOn == true){
        simulationID = "Simulation_" + currentDateTime + "!";
    }
    

    var request = new UpdateUserDataRequest
    {
        Data = new Dictionary<string, string>
        {
            {simulationID, JsonConvert.SerializeObject(saveParametersJSONs[0].ReturnClass(simulationName.text, simulationDescription.text)) }
        }
    };
    PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
}

public void UploadJSON(string key, string values){

var request = new UpdateUserDataRequest
    {
        Data = new Dictionary<string, string>
        {
            {key, values}
        }
    };
    PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
}


void OnDataRemoved(UpdateUserDataResult result){
        Debug.Log("Simulazione eliminata!");
        }



    //TEST
    public void GetParameters(){
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnParametersDataRecieved, OnError);
        }

        private Dictionary<string, GameObject> instantiatedPrefabs = new Dictionary<string, GameObject>();

    void OnParametersDataRecieved(GetUserDataResult result){
        
        splashScreen.SetActive(true);
        dataScreen.SetActive(false);
        noDataScreen.SetActive(false);
        
        Debug.Log("Dati della simulazione ricevuti!");
        if(result.Data != null && result.Data.ContainsKey("Simulation")){
                Parameters parameters = JsonConvert.DeserializeObject<Parameters>(result.Data["Simulation"].Value);
                readNewSimulationJSON.save(parameters);
                
                activeSimulazioneTabs.SetActive(true);
                noDataScreen.SetActive(false);
                splashScreen.SetActive(false);


                


            }else{
                activeSimulazioneTabs.SetActive(false);
                dataScreen.SetActive(false);
                splashScreen.SetActive(false);
            } 
            
HashSet<string> currentSimulations = new HashSet<string>();

            int simulationCount = 0;
        foreach(var key in result.Data.Keys){

            if(key.StartsWith("Simulation_")){
                currentSimulations.Add(key);
                if (!instantiatedPrefabs.ContainsKey(key))
                    {
                        GameObject imageInstance = Instantiate(showSimulationPrefab, dataScreen.transform.GetChild(0).GetChild(0).GetChild(0));
                        instantiatedPrefabs[key] = imageInstance;

                        // Ottieni il componente TextMeshPro e modifica il testo
            TextMeshProUGUI textComponent = imageInstance.GetComponentInChildren<TextMeshProUGUI>();
            ShowSimulationValues showSimulationValues = imageInstance.GetComponent<ShowSimulationValues>();
            SingleParamJSON singleParamJSON = imageInstance.GetComponentInChildren<SingleParamJSON>();
            ShowParameterValue showParameterValue = tabContent.GetComponentInChildren<ShowParameterValue>();
            if (textComponent != null)
            {
                Parameters parameters2 = JsonConvert.DeserializeObject<Parameters>(result.Data[key].Value);
                showSimulationValues.nome.text = parameters2.simulationName;
                showSimulationValues.descrizione.text = parameters2.simulationDescription;
                showSimulationValues.parametersJson.text = result.Data[key].Value.ToString();
                showSimulationValues.data.text = parameters2.simulationDate + " " + parameters2.simulationTime.Substring(0, parameters2.simulationTime.Length - 3);
                showSimulationValues.SimulationKey.name = key;
                
                singleParamJSON.nomeParametro.text = showParameterValue.name;
                singleParamJSON.valoreParametro.text = showParameterValue.GetParameterValueforJSON(0).ToString();

                
                for(int N = 0; N < JsonConvert.DeserializeObject<Dictionary<string, object>>(result.Data[key].Value).Count-4; N++)
                {
                    GameObject singleParam = Instantiate(singleParamPrefab, imageInstance.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0));
                    SingleParamJSON singleParamJSON2 = singleParam.GetComponentInChildren<SingleParamJSON>();

                    singleParamJSON2.nomeParametro.text = "";
                    
                    
                    Debug.Log(JsonConvert.DeserializeObject<Dictionary<string, object>>(result.Data[key].Value).Skip(N).Take(1).First());
                }

            }
                    }
                simulationCount++;

            }
        }
        Debug.Log("Numero totale di chiavi che iniziano con 'Simulation_': " + simulationCount);

// Remove prefabs for missing keys
            List<string> keysToRemove = new List<string>();
            foreach (var key in instantiatedPrefabs.Keys)
            {
                if (!currentSimulations.Contains(key))
                {
                    Debug.Log("instatitated prefab key: " + instantiatedPrefabs[key]);
                    Destroy(instantiatedPrefabs[key]);
                    keysToRemove.Add(key);
                }
            }

            foreach (var key in keysToRemove)
            {
                instantiatedPrefabs.Remove(key);
            }

            if (currentSimulations.Count > 0)
            {
                dataScreen.SetActive(true);
            }
            else
            {
                noDataScreen.SetActive(true);
                dataScreen.SetActive(false);
            }
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

    public class PlayfabConfig
{
    public string Studio;
    public string TitleId;
    public string Email;
    public string Password;
    public string DeveloperSecretKey;
}

}
