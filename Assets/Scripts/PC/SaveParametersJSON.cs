using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Parameters{

    //Medico
    public int genre;
    public float age;
    public int ethnic;
    public int hair;
    public int scrub;
    public int tie;
    public int shirt;
    public bool glasses;
    public bool stethoscope;
    public bool pen;
    
    //Comunicazione
    public float toneOfVoice;
    public int accent;
    public float grammaticalErrors;
    public float empatheticWords;
    public float prescriptionWords;
    public int bodyOrientation;
    public int position;
    public int prescriptionPosition;
    public float gazeAngle;
    public int gazeType;
    public int gazeAnglePrescription;
    public float facialExpression;
    public float gestures;
    public float nodding;
    public bool touch;
    public int touchArea;
    public int touchVelocity;
    public int smile;

    //Ambiente
    public bool furni;
    public float furniQuantity;
    public int furniStyle;
    public int plant;
    public int clock;
    public bool certificate;
    public bool degree;
    public bool moreCertificates;
    public bool computer;
    public int computerPosition;
    public int computerOrientation;
    public bool window;
    public int windowColor;
    public int windowLandscape;
    public bool noise;
    public float noiseVolume;
    public int noiseType;
    public bool music;
    public float musicVolume;
    public int musicType;
    public int time;
    public float hospitalStaff;

    //Ambiente - Sala d'aspetto
    public bool waitingRoom;
    public bool furni_WaitingRoom;
    public float furniQuantity_WaitingRoom;
    public int furniStyle_WaitingRoom;
    public int plant_WaitingRoom;
    public int clock_WaitingRoom;
    public bool window_WaitingRoom;
    public int windowColor_WaitingRoom;
    public int windowLandscape_WaitingRoom;
    public bool noise_WaitingRoom;
    public float noiseVolume_WaitingRoom;
    public int noiseType_WaitingRoom;
    public bool music_WaitingRoom;
    public float musicVolume_WaitingRoom;
    public int musicType_WaitingRoom;
    public bool staff_WaitingRoom;





    //Accessibilità
    public bool teleport;
    public bool snapTurn;
    public bool distanceGrab;
    public bool devMode;

    public string simulationName;
    public string simulationDescription;
    public string simulationDate;
    public string simulationTime;
}

public class SaveParametersJSON : MonoBehaviour // CharacterBox
{
    public void TotalParameters()
    {
        int count = CountChildrenWithScriptRecursive(transform);
    }

    int CountChildrenWithScriptRecursive(Transform parent)
    {
        int count = 0;
        foreach (Transform child in parent)
        {
            //Conta solo i figli che hanno ShowParameterValue (ovvero i parametri presenti nella schermata)
            ShowParameterValue showParameterValue = child.gameObject.GetComponent<ShowParameterValue>();
            if (showParameterValue != null)
            {
                count++;
            }
            count += CountChildrenWithScriptRecursive(child);
        }
        return count;
    }

    public Dictionary<string, object> ReturnClass(string simulationName, string simulationDescription)
    {
        Dictionary<string, object> parameters = GetChildParametersRecursive(transform);
        parameters.Add("simulationName", simulationName);
        parameters.Add("simulationDescription", simulationDescription);
        
        string currentDate = DateTime.Now.ToString("dd/MM/yy");
        string currentTime = DateTime.Now.ToString("HH:mm:ss");
        parameters.Add("simulationDate", currentDate);
        parameters.Add("simulationTime", currentTime);
        return parameters;
    }

    Dictionary<string, object> GetChildParametersRecursive(Transform parent)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        
        foreach (Transform child in parent)
        {
            ShowParameterValue showParameterValue = child.gameObject.GetComponent<ShowParameterValue>();
            if (showParameterValue != null)
            {
                object parameterValue = showParameterValue.GetParameterValue();
                parameters.Add(child.name, parameterValue);
            }
            foreach (var parametro in GetChildParametersRecursive(child))
            {
                parameters.Add(parametro.Key, parametro.Value);
            }
        }
        return parameters;
    }

}