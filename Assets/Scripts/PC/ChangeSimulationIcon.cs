using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSimulationIcon : MonoBehaviour
{
    public GameObject startButton;
    public GameObject readyButton;
    public GameObject runningButton;

    void Start()
    {
        DeactivateAllButtons();

        switch (gameObject.name[gameObject.name.Length - 1])
        {
            case '!':
                readyButton.SetActive(true);
                break;
            case '*':
                runningButton.SetActive(true);
                break;
            default:
                startButton.SetActive(true);
                break;
        }
    }

    void DeactivateAllButtons()
    {
        startButton.SetActive(false);
        readyButton.SetActive(false);
        runningButton.SetActive(false);
    }
}

