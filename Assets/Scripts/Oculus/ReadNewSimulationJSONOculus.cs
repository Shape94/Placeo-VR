using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class ReadNewSimulationJSONOculus : MonoBehaviour 
{
    [Header("Medico - Anagrafe e Aspetto")]
    public TMP_Dropdown genre;
    public Slider age;
    public TMP_Dropdown ethnic;
    public TMP_Dropdown hair;

    [Header("Medico - Abbigliamento e Accessori")]
    public TMP_Dropdown scrub;
    public TMP_Dropdown tie;
    public TMP_Dropdown shirt;
    public Toggle glasses;
    public Toggle stethoscope;
    public Toggle pen;

    [Header("Comunicazione - Comunicazione verbale")]
    public Slider toneOfVoice;
    public TMP_Dropdown accent;
    public Slider grammaticalErrors;
    public Slider empatheticWords;
    public Slider prescriptionWords;


    [Header("Comunicazione - Comunicazione non verbale")]
    public TMP_Dropdown bodyOrientation;
    public TMP_Dropdown position;
    public TMP_Dropdown prescriptionPosition;
    public Slider gazeAngle;
    public TMP_Dropdown gazeType;
    public TMP_Dropdown gazeAnglePrescription;
    public Slider facialExpression;
    public Slider gestures;
    public Slider nodding;
    public Toggle touch;
    public TMP_Dropdown touchArea;
    public TMP_Dropdown touchVelocity;
    public TMP_Dropdown smile;

    [Header("Ambiente - Arredamento")]
    /*public Toggle furni;
    public Slider furniQuantity;
    public TMP_Dropdown furniStyle;*/
    public GameObject noMobili;
    public GameObject mobili;
    public GameObject mobiliRovinati;
    public GameObject mobiliEconomici;
    public GameObject mobiliStandard;
    public GameObject mobiliDiLusso;

    //public TMP_Dropdown plant;
    public GameObject piante;
    public GameObject pianteVerdi;
    public GameObject pianteSecche;
    public GameObject piantaVerdeTavolino;
    public GameObject piantaSeccaTavolino;

    //public TMP_Dropdown clock;
    public GameObject orologi;
    public GameObject orologioDigitale;
    public GameObject orologioAnalogico;

    //public Toggle certificate;
    public GameObject certificati;
    public GameObject laurea;
    public GameObject altriCertificati;
    
    //public Toggle degree;
    //public Toggle moreCertificates;

    public GameObject computer;
    //public Toggle computer;
    //public TMP_Dropdown computerPosition;
    //public TMP_Dropdown computerOrientation;
    //public Toggle window;
    //public TMP_Dropdown windowColor;
    //TEEEEEST
    public GameObject noFinestre;
    public GameObject finestre;
    public Material coloreFinestre;
    public GameObject panorama;
    public GameObject panoramaPalazzi;
    public GameObject panoramaAlberi;
    public GameObject birds;

    
    //public TMP_Dropdown windowLandscape;

    [Header("Ambiente - Audio e Suoni")]
    //public Toggle noise;
    public GameObject relaxNoise;
    public GameObject neutralNoise;
    public GameObject annoyingNoise;
    //public Slider noiseVolume;
    //public TMP_Dropdown noiseType;
    public GameObject musica;
    public GameObject noMusica;
    public GameObject daAmbiente;
    public GameObject classica;
    public GameObject rock;
    /*public Toggle music;
    public Slider musicVolume;
    public TMP_Dropdown musicType;*/

    [Header("Ambiente - Altro")]
    public TMP_Dropdown time;
    public Slider hospitalStaff;

    [Header("Ambiente - Sala d'attesa - Arredamento")]
    public GameObject noMobili_WaitingRoom;
    public GameObject mobili_WaitingRoom;
    public GameObject mobiliRovinati_WaitingRoom;
    public GameObject mobiliEconomici_WaitingRoom;
    public GameObject mobiliStandard_WaitingRoom;
    public GameObject mobiliDiLusso_WaitingRoom;
    public GameObject piante_WaitingRoom;
    public GameObject pianteVerdi_WaitingRoom;
    public GameObject pianteSecche_WaitingRoom;
    public GameObject piantaVerdeTavolino_WaitingRoom;
    public GameObject piantaSeccaTavolino_WaitingRoom;
    public GameObject orologi_WaitingRoom;
    public GameObject orologioDigitale_WaitingRoom;
    public GameObject orologioAnalogico_WaitingRoom;
    public GameObject noFinestre_WaitingRoom;
    public GameObject finestre_WaitingRoom;
    public Material coloreFinestre_WaitingRoom;
    public GameObject panorama_WaitingRoom;
    public GameObject panoramaPalazzi_WaitingRoom;
    public GameObject panoramaAlberi_WaitingRoom;

    [Header("Ambiente - Sala d'attesa - Audio e Suoni")]
    public GameObject relaxNoise_WaitingRoom;
    public GameObject neutralNoise_WaitingRoom;
    public GameObject annoyingNoise_WaitingRoom;
    public GameObject musica_WaitingRoom;
    public GameObject noMusica_WaitingRoom;
    public GameObject daAmbiente_WaitingRoom;
    public GameObject classica_WaitingRoom;
    public GameObject rock_WaitingRoom;


    [Header("Accessibilità")]
    //public Toggle teleport;
    /*public GameObject tappetoAncora;
    public GameObject tappetoArea1;
    public GameObject tappetoArea2;*/
    public GameObject teleportStanzaMedico;
    public GameObject teleport_WaitingRoom;
    //public Toggle snapTurn;
    public Toggle snapTurn;
    public Toggle distanceGrab;
    //public Toggle devMode;
    public GameObject fpsOverlay;

    [Header("Altro")]
    public GameObject xrRig;

    public GameObject CostruttoreDellaStanza;
    public GameObject door;
    public Material doorGlass;

    public void save(Parameters parameters){

        if(parameters.waitingRoom && SceneManager.GetSceneByBuildIndex(2).isLoaded == false && SceneManager.GetSceneByBuildIndex(1).isLoaded == true)
                {
                    xrRig.GetComponent<TeleportPlayer>().Teleport();
                    SceneManager.LoadScene(2, LoadSceneMode.Additive);
                }

                if(parameters.waitingRoom && SceneManager.GetSceneByBuildIndex(4).isLoaded == false && SceneManager.GetSceneByBuildIndex(3).isLoaded == true)
                {
                    xrRig.GetComponent<TeleportPlayer>().Teleport();
                    SceneManager.LoadScene(4, LoadSceneMode.Additive);
                }

        if(noMobili_WaitingRoom != null){
            
    

            if(parameters.waitingRoom){

          

            if(parameters.furni_WaitingRoom){
            Destroy(noMobili_WaitingRoom);
            mobili_WaitingRoom.SetActive(true);

            switch(parameters.furniStyle_WaitingRoom){
                case 0: //Rovinati
                    Destroy(mobiliEconomici_WaitingRoom);
                    Destroy(mobiliStandard_WaitingRoom);
                    Destroy(mobiliDiLusso_WaitingRoom);

                    mobiliRovinati_WaitingRoom.SetActive(true);
                    
                    switch(parameters.furniQuantity_WaitingRoom){
                        case 0: //Pochi
                            Destroy(mobiliRovinati_WaitingRoom.transform.GetChild(1).gameObject); //Distruggi Medi
                            Destroy(mobiliRovinati_WaitingRoom.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliRovinati_WaitingRoom.transform.GetChild(0).gameObject.SetActive(true);
                        break;

                        case 1: //Medi
                            Destroy(mobiliRovinati_WaitingRoom.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliRovinati_WaitingRoom.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliRovinati_WaitingRoom.transform.GetChild(1).gameObject.SetActive(true);
                        break;

                        case 2: //Tanti
                            mobiliRovinati_WaitingRoom.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliRovinati_WaitingRoom.transform.GetChild(1).gameObject.SetActive(true);
                            mobiliRovinati_WaitingRoom.transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    }

                break;

                case 1: //Economici
                    Destroy(mobiliRovinati_WaitingRoom);
                    Destroy(mobiliStandard_WaitingRoom);
                    Destroy(mobiliDiLusso_WaitingRoom);

                    mobiliEconomici_WaitingRoom.SetActive(true);
                    
                    switch(parameters.furniQuantity_WaitingRoom){
                        case 0: //Pochi
                            Destroy(mobiliEconomici_WaitingRoom.transform.GetChild(1).gameObject); //Distruggi Medi
                            Destroy(mobiliEconomici_WaitingRoom.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliEconomici_WaitingRoom.transform.GetChild(0).gameObject.SetActive(true);
                        break;

                        case 1: //Medi
                            Destroy(mobiliEconomici_WaitingRoom.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliEconomici_WaitingRoom.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliEconomici_WaitingRoom.transform.GetChild(1).gameObject.SetActive(true);
                        break;

                        case 2: //Tanti
                            mobiliEconomici_WaitingRoom.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliEconomici_WaitingRoom.transform.GetChild(1).gameObject.SetActive(true);
                            mobiliEconomici_WaitingRoom.transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    }

                break;

                case 2: //Standard
                    Destroy(mobiliRovinati_WaitingRoom);
                    Destroy(mobiliEconomici_WaitingRoom);
                    Destroy(mobiliDiLusso_WaitingRoom);

                    mobiliStandard_WaitingRoom.SetActive(true);
                    
                    switch(parameters.furniQuantity_WaitingRoom){
                        case 0: //Pochi
                            Destroy(mobiliStandard_WaitingRoom.transform.GetChild(1).gameObject); //Distruggi Medi
                            Destroy(mobiliStandard_WaitingRoom.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliStandard_WaitingRoom.transform.GetChild(0).gameObject.SetActive(true);
                        break;

                        case 1: //Medi
                            Destroy(mobiliStandard_WaitingRoom.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliStandard_WaitingRoom.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliStandard_WaitingRoom.transform.GetChild(1).gameObject.SetActive(true);
                        break;

                        case 2: //Tanti
                            mobiliStandard_WaitingRoom.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliStandard_WaitingRoom.transform.GetChild(1).gameObject.SetActive(true);
                            mobiliStandard_WaitingRoom.transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    }

                break;

                case 3: //lusso
                    Destroy(mobiliRovinati_WaitingRoom);
                    Destroy(mobiliEconomici_WaitingRoom);
                    Destroy(mobiliStandard_WaitingRoom);

                    mobiliDiLusso_WaitingRoom.SetActive(true);
                   
                    
                    switch(parameters.furniQuantity_WaitingRoom){
                        case 0: //Pochi
                            Destroy(mobiliDiLusso_WaitingRoom.transform.GetChild(1).gameObject); //Distruggi Medi
                            Destroy(mobiliDiLusso_WaitingRoom.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliDiLusso_WaitingRoom.transform.GetChild(0).gameObject.SetActive(true);
                        break;

                        case 1: //Medi
                            Destroy(mobiliDiLusso_WaitingRoom.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliDiLusso_WaitingRoom.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliDiLusso_WaitingRoom.transform.GetChild(1).gameObject.SetActive(true);
                        break;

                        case 2: //Tanti
                            mobiliDiLusso_WaitingRoom.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliDiLusso_WaitingRoom.transform.GetChild(1).gameObject.SetActive(true);
                            mobiliDiLusso_WaitingRoom.transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    }
                    


                break;
            }

        }else{
            Destroy(mobili_WaitingRoom);
            noMobili_WaitingRoom.SetActive(true);
        }
    
        }

        switch(parameters.plant_WaitingRoom){
            case 0:
                Destroy(piante_WaitingRoom);
            break;

            case 1:
                pianteSecche_WaitingRoom.SetActive(true);
                Destroy(pianteVerdi_WaitingRoom);
                if(parameters.furniQuantity_WaitingRoom != 2 || !parameters.furni_WaitingRoom){
                    Destroy(piantaSeccaTavolino_WaitingRoom);
                }                
            break;

            case 2:
                pianteVerdi_WaitingRoom.SetActive(true);
                Destroy(pianteSecche_WaitingRoom);
                if(parameters.furniQuantity_WaitingRoom != 2 || !parameters.furni_WaitingRoom){
                    Destroy(piantaVerdeTavolino_WaitingRoom);
                } 
            break;
        }

        switch(parameters.clock_WaitingRoom){
            case 0:
                Destroy(orologi_WaitingRoom);
            break;

            case 1:
                orologioDigitale_WaitingRoom.SetActive(true);
                Destroy(orologioAnalogico_WaitingRoom);                
            break;

            case 2:
                orologioAnalogico_WaitingRoom.SetActive(true);
                Destroy(orologioDigitale_WaitingRoom);
            break;
        }

        if(parameters.window_WaitingRoom){
            finestre_WaitingRoom.SetActive(true);
            Destroy(noFinestre_WaitingRoom);

      

            switch(parameters.windowColor_WaitingRoom){
            case 0:
                coloreFinestre_WaitingRoom.color = new Color(1.0f, 1.0f, 1.0f, 75.0f / 255.0f);
            break;

            case 1:
                coloreFinestre_WaitingRoom.color = new Color(0.0f, 0.0f, 0.0f, 75.0f / 255.0f);               
            break;
            }

            switch(parameters.windowLandscape_WaitingRoom){
            case 0:
                Destroy(panorama_WaitingRoom);
            break;

            case 1:
                panoramaPalazzi_WaitingRoom.SetActive(true);
                Destroy(panoramaAlberi_WaitingRoom);                
            break;

            case 2:
                panoramaAlberi_WaitingRoom.SetActive(true);
                Destroy(panoramaPalazzi_WaitingRoom);
            break;
        }



        }else{
            noFinestre_WaitingRoom.SetActive(true);
            Destroy(finestre_WaitingRoom);
        }

        if(parameters.music_WaitingRoom){

            AudioSource selectedAudioSource = null;

            switch(parameters.musicType_WaitingRoom){
                case 0:
                    Destroy(noMusica_WaitingRoom);
                    Destroy(classica_WaitingRoom);
                    Destroy(rock_WaitingRoom);
                    daAmbiente_WaitingRoom.SetActive(true);

                    switch(parameters.musicVolume_WaitingRoom){
                    case 0:
                        daAmbiente_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 0.25f;
                    break;

                    case 1:
                        daAmbiente_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 0.70f;
                    break;

                    case 2:
                        daAmbiente_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 1.0f;
                    break;
                }

                    
                break;

                case 1:
                    Destroy(noMusica_WaitingRoom);
                    Destroy(daAmbiente_WaitingRoom);
                    Destroy(rock_WaitingRoom);
                    classica_WaitingRoom.SetActive(true);

                    switch(parameters.musicVolume_WaitingRoom){
                    case 0:
                        classica_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 0.25f;
                    break;

                    case 1:
                        classica_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 0.70f;
                    break;

                    case 2:
                        classica_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 1.0f;
                    break;
                }

                break;

                case 2:
                    Destroy(noMusica_WaitingRoom);
                    Destroy(daAmbiente_WaitingRoom);
                    Destroy(classica_WaitingRoom);
                    rock_WaitingRoom.SetActive(true);

                    switch(parameters.musicVolume_WaitingRoom){
                    case 0:
                        rock_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 0.1f;
                    break;

                    case 1:
                        rock_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 0.50f;
                    break;

                    case 2:
                        rock_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 0.8f;
                    break;
                }
                break;

                
            }
        }else{
            Destroy(classica_WaitingRoom);
            Destroy(daAmbiente_WaitingRoom);
            Destroy(rock_WaitingRoom);
            noMusica_WaitingRoom.SetActive(true);
        }


    if(parameters.noise_WaitingRoom){

            AudioSource selectedAudioSource = null;

            switch(parameters.noiseType_WaitingRoom){
                case 0:
                    Destroy(annoyingNoise_WaitingRoom);
                    
                    relaxNoise_WaitingRoom.GetComponentInChildren<AudioSource>().enabled = true;

                    switch(parameters.noiseVolume){
                    case 0:
                        relaxNoise_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 0.2f;
                    break;

                    case 1:
                        relaxNoise_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 0.35f;
                    break;

                    case 2:
                        relaxNoise_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 0.5f;
                    break;
                }

                   

                break;

                case 1:
                    Destroy(annoyingNoise_WaitingRoom);
                    Destroy(relaxNoise_WaitingRoom);
                    
                    AudioSource[] audioSources = neutralNoise_WaitingRoom.GetComponentsInChildren<AudioSource>();
                    foreach (AudioSource audioSource in audioSources)
                    {
                        audioSource.enabled = true;
                    }

                    switch(parameters.noiseVolume_WaitingRoom){
                    case 0:
                        //neutralNoise_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 0.25f;
                        foreach (AudioSource audioSource in audioSources)
                    {
                        audioSource.volume = 0.03f;
                    }
                    break;

                    case 1:
                        //neutralNoise_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 0.70f;
                        foreach (AudioSource audioSource in audioSources)
                    {
                        audioSource.volume = 0.10f;
                    }
                    break;

                    case 2:
                        //neutralNoise_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 1.0f;
                        foreach (AudioSource audioSource in audioSources)
                    {
                        audioSource.volume = 0.15f;
                    }
                    break;
                }

                   

                break;

                

                case 2:
                    Destroy(relaxNoise_WaitingRoom);

                    annoyingNoise_WaitingRoom.GetComponent<AudioSource>().enabled = true;

                    switch(parameters.noiseVolume_WaitingRoom){
                    case 0:
                        annoyingNoise_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 0.25f;
                    break;

                    case 1:
                        annoyingNoise_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 0.70f;
                    break;

                    case 2:
                        annoyingNoise_WaitingRoom.GetComponentInChildren<AudioSource>().volume = 1.0f;
                    break;
                }
                break;

                
            }
        }else{
            Destroy(annoyingNoise_WaitingRoom);
            Destroy(relaxNoise_WaitingRoom);
        }
     
}
        

    if(SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 3){

        if(parameters.waitingRoom){
            door.GetComponent<DoorController>().enabled = true;
            door.GetComponent<BoxCollider>().enabled = true;
            /*doorGlass.SetFloat("_Surface", 1);
            doorGlass.color = new Color(0.91f, 0.91f, 0.91f, 91.0f / 255.0f);
            doorGlass.SetFloat("_Smoothness", 0.75f);*/
        }else{
            Destroy(door.GetComponent<DoorController>());
            Destroy(door.GetComponent<BoxCollider>());
            if(teleport_WaitingRoom != null){
                teleport_WaitingRoom.SetActive(false);
                teleport_WaitingRoom.GetComponent<TeleportAreaWithFade>().enabled = false;
            }
            /*doorGlass.SetFloat("_Surface", 0);
            doorGlass.color = new Color(0.91f, 0.91f, 0.91f, 255.0f / 255.0f);
            doorGlass.SetFloat("_Smoothness", 0.3f);*/

        }

        if(parameters.furni){
            Destroy(noMobili);
            mobili.SetActive(true);

            switch(parameters.furniStyle){
                case 0: //Rovinati
                    Destroy(mobiliEconomici);
                    Destroy(mobiliStandard);
                    Destroy(mobiliDiLusso);

                    mobiliRovinati.SetActive(true);
                    
                    switch(parameters.furniQuantity){
                        case 0: //Pochi
                            Destroy(mobiliRovinati.transform.GetChild(1).gameObject); //Distruggi Medi
                            Destroy(mobiliRovinati.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliRovinati.transform.GetChild(0).gameObject.SetActive(true);
                        break;

                        case 1: //Medi
                            Destroy(mobiliRovinati.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliRovinati.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliRovinati.transform.GetChild(1).gameObject.SetActive(true);
                        break;

                        case 2: //Tanti
                            mobiliRovinati.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliRovinati.transform.GetChild(1).gameObject.SetActive(true);
                            mobiliRovinati.transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    }

                break;

                case 1: //Economici
                    Destroy(mobiliRovinati);
                    Destroy(mobiliStandard);
                    Destroy(mobiliDiLusso);

                    mobiliEconomici.SetActive(true);
                    
                    switch(parameters.furniQuantity){
                        case 0: //Pochi
                            Destroy(mobiliEconomici.transform.GetChild(1).gameObject); //Distruggi Medi
                            Destroy(mobiliEconomici.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliEconomici.transform.GetChild(0).gameObject.SetActive(true);
                        break;

                        case 1: //Medi
                            Destroy(mobiliEconomici.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliEconomici.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliEconomici.transform.GetChild(1).gameObject.SetActive(true);
                        break;

                        case 2: //Tanti
                            mobiliEconomici.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliEconomici.transform.GetChild(1).gameObject.SetActive(true);
                            mobiliEconomici.transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    }

                break;

                case 2: //Standard
                    Destroy(mobiliRovinati);
                    Destroy(mobiliEconomici);
                    Destroy(mobiliDiLusso);

                    mobiliStandard.SetActive(true);
                    
                    switch(parameters.furniQuantity){
                        case 0: //Pochi
                            Destroy(mobiliStandard.transform.GetChild(1).gameObject); //Distruggi Medi
                            Destroy(mobiliStandard.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliStandard.transform.GetChild(0).gameObject.SetActive(true);
                        break;

                        case 1: //Medi
                            Destroy(mobiliStandard.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliStandard.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliStandard.transform.GetChild(1).gameObject.SetActive(true);
                        break;

                        case 2: //Tanti
                            mobiliStandard.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliStandard.transform.GetChild(1).gameObject.SetActive(true);
                            mobiliStandard.transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    }

                break;

                case 3: //lusso
                    Destroy(mobiliRovinati);
                    Destroy(mobiliEconomici);
                    Destroy(mobiliStandard);

                    mobiliDiLusso.SetActive(true);
                    //mobiliDiLusso.transform.GetChild(0).gameObject.SetActive(true);
                    
                    switch(parameters.furniQuantity){
                        case 0: //Pochi
                            Destroy(mobiliDiLusso.transform.GetChild(1).gameObject); //Distruggi Medi
                            Destroy(mobiliDiLusso.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliDiLusso.transform.GetChild(0).gameObject.SetActive(true);
                        break;

                        case 1: //Medi
                            Destroy(mobiliDiLusso.transform.GetChild(2).gameObject); //Distruggi Tanti
                            mobiliDiLusso.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliDiLusso.transform.GetChild(1).gameObject.SetActive(true);
                        break;

                        case 2: //Tanti
                            mobiliDiLusso.transform.GetChild(0).gameObject.SetActive(true);
                            mobiliDiLusso.transform.GetChild(1).gameObject.SetActive(true);
                            mobiliDiLusso.transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    }
                    


                break;
            }

        }else{
            Destroy(mobili);
            noMobili.SetActive(true);
        }


      
        
        switch(parameters.plant){
            case 0:
                Destroy(piante);
            break;

            case 1:
                pianteSecche.SetActive(true);
                Destroy(pianteVerdi);
                if(parameters.furniQuantity != 2 || !parameters.furni){
                    Destroy(piantaSeccaTavolino);
                }                
            break;

            case 2:
                pianteVerdi.SetActive(true);
                Destroy(pianteSecche);
                if(parameters.furniQuantity != 2 || !parameters.furni){
                    Destroy(piantaVerdeTavolino);
                } 
            break;
        }
        
   

        switch(parameters.clock){
            case 0:
                Destroy(orologi);
            break;

            case 1:
                orologioDigitale.SetActive(true);
                Destroy(orologioAnalogico);                
            break;

            case 2:
                orologioAnalogico.SetActive(true);
                Destroy(orologioDigitale);
            break;
        }

        
        if(parameters.certificate){
            if(parameters.degree){
                laurea.SetActive(true);
            }else{
                Destroy(laurea);
            }
            if(parameters.moreCertificates){
                altriCertificati.SetActive(true);
            }else{
                Destroy(altriCertificati);
            }
        }else{
            Destroy(certificati);
        }

        if(parameters.computer){
            computer.SetActive(true);
        }else{
            Destroy(computer);
        }


    
        

        if(parameters.window){
            finestre.SetActive(true);
            Destroy(noFinestre);



            switch(parameters.windowColor){
            case 0:
                coloreFinestre.color = new Color(1.0f, 1.0f, 1.0f, 75.0f / 255.0f);
            break;

            case 1:
                coloreFinestre.color = new Color(0.0f, 0.0f, 0.0f, 75.0f / 255.0f);               
            break;
            }

            switch(parameters.windowLandscape){
            case 0:
                Destroy(panorama);
            break;

            case 1:
                panoramaPalazzi.SetActive(true);
                Destroy(panoramaAlberi);                
            break;

            case 2:
                panoramaAlberi.SetActive(true);
                Destroy(panoramaPalazzi);
            break;
        }



        }else{
            noFinestre.SetActive(true);
            Destroy(finestre);
        }


        if(parameters.window){
            switch(parameters.windowLandscape){
                case 0:
                birds.transform.GetChild(1).gameObject.SetActive(false);
                break;

                case 1:
                birds.transform.GetChild(1).gameObject.SetActive(false);
                break;
            }
        }else{
                birds.transform.GetChild(1).gameObject.SetActive(false);
        }

        if(parameters.window_WaitingRoom){
            switch(parameters.windowLandscape_WaitingRoom){
                case 0:
                birds.transform.GetChild(2).gameObject.SetActive(false);
                break;

                case 1:
                birds.transform.GetChild(2).gameObject.SetActive(false);
                break;
            }
            
        }else{
                birds.transform.GetChild(2).gameObject.SetActive(false);
        }

        birds.transform.GetChild(0).gameObject.SetActive(true);


        if(parameters.window == false && parameters.window_WaitingRoom == false){
            Destroy(birds);
        }




        if(parameters.music){

            AudioSource selectedAudioSource = null;

            switch(parameters.musicType){
                case 0:
                    Destroy(noMusica);
                    Destroy(classica);
                    Destroy(rock);
                    daAmbiente.SetActive(true);

                    switch(parameters.musicVolume){
                    case 0:
                        daAmbiente.GetComponentInChildren<AudioSource>().volume = 0.25f;
                    break;

                    case 1:
                        daAmbiente.GetComponentInChildren<AudioSource>().volume = 0.70f;
                    break;

                    case 2:
                        daAmbiente.GetComponentInChildren<AudioSource>().volume = 1.0f;
                    break;
                }

                    
                break;

                case 1:
                    Destroy(noMusica);
                    Destroy(daAmbiente);
                    Destroy(rock);
                    classica.SetActive(true);

                    switch(parameters.musicVolume){
                    case 0:
                        classica.GetComponentInChildren<AudioSource>().volume = 0.25f;
                    break;

                    case 1:
                        classica.GetComponentInChildren<AudioSource>().volume = 0.70f;
                    break;

                    case 2:
                        classica.GetComponentInChildren<AudioSource>().volume = 1.0f;
                    break;
                }

                   

                break;

                

                case 2:
                    Destroy(noMusica);
                    Destroy(daAmbiente);
                    Destroy(classica);
                    rock.SetActive(true);

                    switch(parameters.musicVolume){
                    case 0:
                        rock.GetComponentInChildren<AudioSource>().volume = 0.1f;
                    break;

                    case 1:
                        rock.GetComponentInChildren<AudioSource>().volume = 0.50f;
                    break;

                    case 2:
                        rock.GetComponentInChildren<AudioSource>().volume = 0.8f;
                    break;
                }
                break;

                
            }
        }else{
            Destroy(classica);
            Destroy(daAmbiente);
            Destroy(rock);
            noMusica.SetActive(true);
        }


    if(parameters.noise){

            AudioSource selectedAudioSource = null;

            switch(parameters.noiseType){
                case 0:
                    Destroy(annoyingNoise);
                    
                    relaxNoise.GetComponentInChildren<AudioSource>().enabled = true;

                    switch(parameters.noiseVolume){
                    case 0:
                        relaxNoise.GetComponentInChildren<AudioSource>().volume = 0.1f;
                    break;

                    case 1:
                        relaxNoise.GetComponentInChildren<AudioSource>().volume = 0.2f;
                    break;

                    case 2:
                        relaxNoise.GetComponentInChildren<AudioSource>().volume = 0.3f;
                    break;
                }

                   

                break;

                case 1:
                    Destroy(annoyingNoise);
                    Destroy(relaxNoise);
                    
                    neutralNoise.GetComponent<AudioSource>().enabled = true;

                    switch(parameters.noiseVolume){
                    case 0:
                        neutralNoise.GetComponentInChildren<AudioSource>().volume = 0.25f;
                    break;

                    case 1:
                        neutralNoise.GetComponentInChildren<AudioSource>().volume = 0.70f;
                    break;

                    case 2:
                        neutralNoise.GetComponentInChildren<AudioSource>().volume = 1.0f;
                    break;
                }

                   

                break;

                

                case 2:
                    Destroy(neutralNoise);
                    Destroy(relaxNoise);

                    annoyingNoise.GetComponent<AudioSource>().enabled = true;

                    switch(parameters.noiseVolume){
                    case 0:
                        annoyingNoise.GetComponentInChildren<AudioSource>().volume = 0.25f;
                    break;

                    case 1:
                        annoyingNoise.GetComponentInChildren<AudioSource>().volume = 0.70f;
                    break;

                    case 2:
                        annoyingNoise.GetComponentInChildren<AudioSource>().volume = 1.0f;
                    break;
                }
                break;

                
            }
        }else{
            Destroy(relaxNoise);
            Destroy(neutralNoise);
        }



        teleportStanzaMedico.GetComponent<TeleportAreaWithFade>().enabled = parameters.teleport;
        if(!parameters.teleport && teleport_WaitingRoom != null){
            //Destroy(teleport_WaitingRoom);
            teleport_WaitingRoom.SetActive(false);
        }
        snapTurn.isOn = parameters.snapTurn;
        distanceGrab.isOn = !parameters.distanceGrab;
        distanceGrab.isOn = parameters.distanceGrab;

        if(parameters.devMode){
            fpsOverlay.SetActive(true);
        }else{
            Destroy(fpsOverlay);
        }

        Destroy(CostruttoreDellaStanza);
    }
    
}
}