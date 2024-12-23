using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEditor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Proprietà per il cappello
    public string Hat
    {
        get { return "999"; }
        set { hat = "Fedora"; }
    }
    private string hat;

    // Proprietà per la pelle
    public string Skin
    {
        get { return "888"; }
        set { skin = "Caucasico"; }
    }
    private string skin;

    public void SetAppearance(string hat, string skin){
        Debug.Log("Hat -> " + hat + ". Skin -> " + skin);
    }
}
