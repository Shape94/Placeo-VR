using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ElementManager))]
public class ElementManagerEditor : Editor
{
    int selectedIndex = 0;

    public override void OnInspectorGUI()
    {
        ElementManager myScript = (ElementManager)target;

        // Draw the dropdown list
        string[] options = myScript.macroCategories.ToArray();
        selectedIndex = EditorGUILayout.Popup("Macro Category", selectedIndex, options);
        
        //Passare l'indice della macrocategoria selezionata a Element Manager
        myScript.selectedIndex = selectedIndex;


        if(GUILayout.Button("Aggiorna Parametri da JSON (Resources/configParams.json)"))
        {
            myScript.AggiornaParametridaJSON();
        }
        
        DrawDefaultInspector();
    }
}
