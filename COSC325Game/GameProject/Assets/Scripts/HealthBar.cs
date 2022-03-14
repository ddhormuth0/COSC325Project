using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;
    
    public void SetMaxHealth(int health)
    {
        //set slider max to max health
        slider.maxValue = health;
        //set slider to full
        slider.value = health;
    }

    //change slider value
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
