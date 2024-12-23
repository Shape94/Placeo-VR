using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundY : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Start()
    {
        // Ruota l'oggetto attorno all'asse Y della trasformazione globale utilizzando iTween
        iTween.RotateBy(gameObject, iTween.Hash(
            "y", 1,
            "speed", rotationSpeed,
            "looptype", iTween.LoopType.loop,
            "space", Space.World
        ));
    }
}
