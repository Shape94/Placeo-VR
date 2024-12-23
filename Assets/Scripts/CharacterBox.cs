using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Character{
    public string name;
    public float level;
    public float health;

    public Character(string name, float level, float health){
        this.name = name;
        this.level = level;
        this.health = health;
    }
}
public class CharacterBox : MonoBehaviour
{
    public InputField nameInput;
    public Slider levelSlider;
    public Slider healthSlider;

    public Character ReturnClass(){
        return new Character(nameInput.text, levelSlider.value, healthSlider.value);
    }

    public void SetUi(Character character){
        nameInput.text = character.name;
        levelSlider.value = character.level;
        healthSlider.value = character.health;
    }
}
