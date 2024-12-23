using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextViewModel : MonoBehaviour
{
   public CustomText textView; //dovrebbe essere 'Text' ma vabbuò

   public void OnHealthChanged(Component sender, object data){
    if (data is int health){
        string healthString = health.ToString();
        textView.SetText(healthString);
    }
   }
}