using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarDamageBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    /*
     * Sets the slider to match the players max health.
     */
    public void SetMaxCarHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }
    /*
     *When the car is damaged or repaired set the slider to the damage level and update the sliders color.
     */
    public void SetCarDamage(float damage)
    {
        slider.value = damage;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
