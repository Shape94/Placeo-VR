using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomText : CustomUIComponent
   {
    public TextSO textData;
    public Style style;

    private TextMeshProUGUI textMeshProUGUI;

    /*private void Awake(){
        Init();
    }

    public void Init(){
        Setup();
        Configure();
    }*/

    public override void Setup(){
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void Configure(){
        ThemeSO theme = GetMainTheme();
        if (theme == null) return;

        textMeshProUGUI.color = theme.GetTextColor(style);
        textMeshProUGUI.font = textData.font;
        textMeshProUGUI.fontSize = textData.size;
    }

    public void SetText(string text){
        textMeshProUGUI.text = text;
    }

    /*public void OnValidate(){
        Init();
    }*/

}

