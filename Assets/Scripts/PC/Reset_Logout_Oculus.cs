using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System;
using System.IO;

public class Reset_Logout_Oculus : MonoBehaviour
{
    public void Restart()
    {
       
        string path = Path.Combine(Application.persistentDataPath, "login.json");
        
        DeleteJsonFile(path);

        SceneManager.LoadScene(0);
    }

     void DeleteJsonFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("File eliminato in: " + filePath);
        }
        else
        {
            Debug.Log("File non trovato in: " + filePath);
        }
    }

    
}
