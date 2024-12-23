using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Linq;


public class ElementManager : MonoBehaviour
{
    // Enumerazioni per definire i tipi di contenitori e di elementi interattivi.
    public enum ContainerTypeOptions { settingContainer, settingContainerExpandable, settingContainerMini, textSection }
    public enum InteragibleTypeOptions { toggle, slider, dropdown, checkbox }

    // Strutture per contenere le informazioni sui contenitori e sugli elementi interattivi.
    [System.Serializable]
    public struct Container
    {
        public GameObject container;
    }

    [System.Serializable]
    public struct Interactable
    {
        public GameObject interactable;
    }

    // Struttura per contenere le informazioni sugli elementi (Parametri).
    [System.Serializable]
    public struct Element
    {
        public string key;   
        public ContainerTypeOptions containerType;
        public Sprite immagine;
        public string nome;
        public string descrizione;
        public InteragibleTypeOptions interagibleType;
        public List<string> interagibleOptions;
    }

    // Liste per contenere i contenitori, gli elementi interattivi e gli elementi.
    public List<Element> elementi = new List<Element>();
    public List<Container> container = new List<Container>();
    public List<Interactable> interactable = new List<Interactable>();
    
    // Aggiungi una lista per le macrocategorie
    [HideInInspector]
    public List<string> macroCategories = new List<string>();

    // Aggiungi un campo pubblico per selectedIndex
    [HideInInspector]
    public int selectedIndex;

    public void AggiornaParametridaJSON()
{
    TextAsset configFile = Resources.Load<TextAsset>("configParams");

    if (configFile != null)
    {
        var configData = JsonConvert.DeserializeObject<Dictionary<string, object>>(configFile.text);

        // Aggiorna la lista delle macrocategorie con le chiavi del file JSON
        macroCategories = new List<string>(configData.Keys);

        // Ottieni la macrocategoria selezionata
        string selectedMacroCategory = macroCategories[selectedIndex];

        // Verifica se la macrocategoria selezionata esiste nel file JSON
        if (configData.ContainsKey(selectedMacroCategory))
        {
            // Ottieni i dati della macrocategoria selezionata
            Dictionary<string, object> selectedCategoryData = JsonConvert.DeserializeObject<Dictionary<string, object>>(configData[selectedMacroCategory].ToString());

            // Creiamo un elenco di chiavi da elementi per facilitare la ricerca
            List<string> elementKeys = elementi.Where(e => e.containerType != ContainerTypeOptions.textSection).Select(e => e.key).ToList();

            List<Element> nuoviElementi = new List<Element>();

            foreach (KeyValuePair<string, object> entry in selectedCategoryData)
            {
                Debug.Log("Key: " + entry.Key + ", Value: " + entry.Value);

                // Verifica se l'elemento esiste già
                if (elementKeys.Contains(entry.Key))
                {
                    // Se l'elemento esiste già, lo aggiungiamo alla nuova lista
                    nuoviElementi.Add(elementi.Find(e => e.key == entry.Key));
                }
                else
                {
                    // Se l'elemento non esiste, creiamo un nuovo elemento e lo aggiungiamo alla nuova lista
                    Element nuovoElemento = new Element();
                    nuovoElemento.key = entry.Key;
                    //nuovoElemento.nome = entry.Key;



                    // Deserializza l'oggetto JSON corrispondente
        var entryData = JsonConvert.DeserializeObject<Dictionary<string, object>>(entry.Value.ToString());

        // Imposta il nome in base al campo 'name' dell'oggetto JSON
        if (entryData.ContainsKey("name"))
        {
            nuovoElemento.nome = entryData["name"].ToString();
        }

        // Imposta la descrizione in base al campo 'description' dell'oggetto JSON
        if (entryData.ContainsKey("description"))
        {
            nuovoElemento.descrizione = entryData["description"].ToString();
        }

        // Imposta interagibleType in base al campo 'type' dell'oggetto JSON
        if (entryData.ContainsKey("type"))
        {
            string type = entryData["type"].ToString();
            switch (type)
            {
                case "toggle":
                    nuovoElemento.interagibleType = InteragibleTypeOptions.toggle;
                    break;
                case "slider":
                    nuovoElemento.interagibleType = InteragibleTypeOptions.slider;
                    break;
                case "dropdown":
                    nuovoElemento.interagibleType = InteragibleTypeOptions.dropdown;
                    break;
                case "checkbox":
                    nuovoElemento.interagibleType = InteragibleTypeOptions.checkbox;
                    break;
                default:
                    Debug.Log("Tipo di interazione non riconosciuto: " + type);
                    break;
            }
        }

        // Imposta interagibleOptions in base al campo 'options' dell'oggetto JSON
        if (entryData.ContainsKey("options"))
        {
            nuovoElemento.interagibleOptions = JsonConvert.DeserializeObject<List<string>>(entryData["options"].ToString());
        }

        if (entryData.ContainsKey("containerType"))
{
    nuovoElemento.containerType = (ContainerTypeOptions)System.Enum.Parse(typeof(ContainerTypeOptions), entryData["containerType"].ToString());
}






                    nuoviElementi.Add(nuovoElemento);
                }
            }

            // Aggiungi gli elementi con ContainerTypeOptions.textSection alla lista nuoviElementi
            //nuoviElementi.AddRange(elementi.Where(e => e.containerType == ContainerTypeOptions.textSection));

            // Sostituisci la vecchia lista di elementi con la nuova lista
            elementi = nuoviElementi;
        }
        else
        {
            Debug.Log("Macrocategory '" + selectedMacroCategory + "' not found in JSON file");
        }
    }
    else
    {
        Debug.Log("File 'configParams.json' not found in Resources folder");
    }
}




    private void Awake()
    {
        // Questa variabile tiene traccia dell'ultimo contenitore espandibile creato (per inserire dentro il mini al suo interno).
        GameObject settingsExpandableInstance = null;

        foreach (var elemento in elementi)
        {
            // Crea un nuovo contenitore scelto dall'utente nell'editor.
            GameObject istanza = Instantiate(container[(int)elemento.containerType].container);
            
           if(elemento.containerType == ContainerTypeOptions.settingContainerExpandable){
                Transform child = istanza.GetComponent<Transform>().GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
                child.name = elemento.key;
                }
                else
                {
                    istanza.name = elemento.key;
                }
            

            // Se il contenitore è di tipo "settingContainerMini", allora il contenitore padre sarà l'ultimo contenitore espandibile creato.
            if (elemento.containerType == ContainerTypeOptions.settingContainerMini)
                istanza.transform.SetParent(settingsExpandableInstance.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0));
            else
                istanza.transform.SetParent(transform);

            // Ottiene i componenti Image e TextMeshProUGUI dal contenitore.
            Image[] images = istanza.GetComponentsInChildren<Image>();
            TextMeshProUGUI[] textMeshPros = istanza.GetComponentsInChildren<TextMeshProUGUI>();

            // Imposta il nome dell'elemento.
            textMeshPros[0].text = elemento.nome;

            // Se il contenitore è di tipo "settingContainer" o "settingContainerExpandable"...
            if (elemento.containerType == ContainerTypeOptions.settingContainer || elemento.containerType == ContainerTypeOptions.settingContainerExpandable)
            {
                // Imposta la descrizione e l'immagine dell'elemento.
                textMeshPros[1].text = elemento.descrizione;
                images[elemento.containerType == ContainerTypeOptions.settingContainer ? 1 : 4].sprite = elemento.immagine;

                // Se il contenitore è di tipo "settingContainerExpandable", allora lo memorizza come l'ultimo contenitore espandibile creato (utile per inserire il mini al suo interno).
                if (elemento.containerType == ContainerTypeOptions.settingContainerExpandable)
                {
                    settingsExpandableInstance = istanza;
                }
            }

            // Se il contenitore è di tipo "settingContainer" o "settingContainerMini"...
            if (elemento.containerType == ContainerTypeOptions.settingContainer || elemento.containerType == ContainerTypeOptions.settingContainerMini)
            {
                // Crea un nuovo elemento interattivo selezionato da editor (checkbox  disponibile solo se è un contenitore mini).
                GameObject interagibileIstanza = null;
                if (elemento.interagibleType != InteragibleTypeOptions.checkbox)
                {
                    Transform interactableGerarchy = istanza.transform.GetChild(0).GetChild(istanza.transform.GetChild(0).childCount - 1);
                    interagibileIstanza = Instantiate(interactable[(int)elemento.interagibleType].interactable, interactableGerarchy);
                }

                // Configura l'elemento interattivo in base al suo tipo (toggle non ne ha bisogno).
                switch (elemento.interagibleType)
                {
                    case InteragibleTypeOptions.slider:
                        GenericSlider genericSlider = interagibileIstanza.transform.GetChild(0).GetComponent<GenericSlider>();
                        genericSlider.labels.AddRange(elemento.interagibleOptions);
                        break;
                    case InteragibleTypeOptions.dropdown:
                        TMP_Dropdown dropdown = interagibileIstanza.GetComponent<TMP_Dropdown>();
                        dropdown.options.AddRange(elemento.interagibleOptions.ConvertAll(option => new TMP_Dropdown.OptionData(option)));
                        break;
                    case InteragibleTypeOptions.checkbox:
                        if (elemento.containerType == ContainerTypeOptions.settingContainerMini)
                        {
                            istanza.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                        }
                        break;
                }
            }
        }
    }
}




