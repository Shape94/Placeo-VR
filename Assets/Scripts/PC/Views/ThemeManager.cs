using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class ThemeManager : MonoBehaviour
{
    [SerializeField] private ThemeSO mainTheme;

    public static ThemeManager Instance;

    public void Awake(){
        Instance = this;
    }

    public ThemeSO GetMainTheme(){
        return mainTheme;
    }
}
