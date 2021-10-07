using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider Slider;
    public Gradient Gradient;
    public Image Fill;
    public void SetMaxHealth(int Health)
    {
        Slider.maxValue = Health;
        Slider.value = Health;
        Fill.color = Gradient.Evaluate(1f);
    }
    public void SetHealth(int Health)
    {
        Slider.value = Health;
        Fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }
}
