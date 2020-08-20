using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // -------------------------------------------------------------------------
    public void SetMaxHealth(int max_blood)
    {
        GetComponent<Slider>().maxValue = max_blood;
        GetComponent<Slider>().value = max_blood;
    }
    // -------------------------------------------------------------------------
    public void SetHealth(int blood)
    {
        GetComponent<Slider>().value = blood;
    }
}
