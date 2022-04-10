using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxMana(float mana)
    {
        //set slider max to max health
        slider.maxValue = mana;
        //set slider to full
        slider.value = mana;
    }

    //change slider value
    public void SetMana(float mana)
    {
        slider.value = mana;
    }
}

