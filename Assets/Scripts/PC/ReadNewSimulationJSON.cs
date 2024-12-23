using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ReadNewSimulationJSON : MonoBehaviour
{
    //Valori impostabili da editor
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
    public Toggle furni;
    public Slider furniQuantity;
    public TMP_Dropdown furniStyle;
    public TMP_Dropdown plant;
    public TMP_Dropdown clock;
    public Toggle certificate;
    public Toggle degree;
    public Toggle moreCertificates;
    public Toggle computer;
    public TMP_Dropdown computerPosition;
    public TMP_Dropdown computerOrientation;
    public Toggle window;
    public TMP_Dropdown windowColor;
    public TMP_Dropdown windowLandscape;

    [Header("Ambiente - Audio e Suoni")]
    public Toggle noise;
    public Slider noiseVolume;
    public TMP_Dropdown noiseType;
    public Toggle music;
    public Slider musicVolume;
    public TMP_Dropdown musicType;

    [Header("Ambiente - Altro")]
    public TMP_Dropdown time;
    public Slider hospitalStaff;

    [Header("Accessibilità")]
    public Toggle teleport;
    public Toggle snapTurn;
    public Toggle distanceGrab;
    public Toggle devMode;

    public void save(Parameters parameters){
        genre.value = parameters.genre;
        age.value = parameters.age;
        ethnic.value = parameters.ethnic;
        hair.value = parameters.hair;  
        scrub.value = parameters.scrub;
        tie.value = parameters.tie;  
        shirt.value = parameters.shirt; 
        glasses.isOn = parameters.glasses;    
        stethoscope.isOn = parameters.stethoscope;    
        pen.isOn = parameters.pen; 

        toneOfVoice.value = parameters.toneOfVoice;
        accent.value = parameters.accent;
        grammaticalErrors.value = parameters.grammaticalErrors;
        empatheticWords.value = parameters.empatheticWords;
        prescriptionWords.value = parameters.prescriptionWords;
        bodyOrientation.value = parameters.bodyOrientation;
        position.value = parameters.position;
        prescriptionPosition.value = parameters.prescriptionPosition;
        gazeAngle.value = parameters.gazeAngle;
        gazeType.value = parameters.gazeType;
        gazeAnglePrescription.value = parameters.gazeAnglePrescription;
        facialExpression.value = parameters.facialExpression;
        gestures.value = parameters.gestures;
        nodding.value = parameters.nodding;
        touch.isOn = parameters.touch;
        touchArea.value = parameters.touchArea;
        touchVelocity.value = parameters.touchVelocity;
        smile.value = parameters.smile;

        furni.isOn = parameters.furni;
        furniQuantity.value = parameters.furniQuantity;
        furniStyle.value = parameters.furniStyle;
        plant.value = parameters.plant;
        clock.value = parameters.clock;
        certificate.isOn = parameters.certificate;
        degree.isOn = parameters.degree;
        moreCertificates.isOn = parameters.moreCertificates;
        computer.isOn = parameters.computer;
        computerPosition.value = parameters.computerPosition;
        computerOrientation.value = parameters.computerOrientation;
        window.isOn = parameters.window;
        windowColor.value = parameters.windowColor;
        windowLandscape.value = parameters.windowLandscape;
        noise.isOn = parameters.noise;
        noiseVolume.value = parameters.noiseVolume;
        noiseType.value = parameters.noiseType;
        music.isOn = parameters.music;
        musicVolume.value = parameters.musicVolume;
        musicType.value = parameters.musicType;
        time.value = parameters.time;
        hospitalStaff.value = parameters.hospitalStaff;
    
        teleport.isOn = parameters.teleport;
        snapTurn.isOn = parameters.snapTurn;
        distanceGrab.isOn = parameters.distanceGrab;
        devMode.isOn = parameters.devMode;
    }
}
