using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
    }
    // -------------------------------------------------------------------------
    public void SetMaxHealth(int max_blood)
    {
        slider.maxValue = max_blood + 3;
        slider.value = max_blood + 3;
    }
    // -------------------------------------------------------------------------
    public void SetHealth(int blood)
    {
        slider.value = blood + 2;
    }
}
