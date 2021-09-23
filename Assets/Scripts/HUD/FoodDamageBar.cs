using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodDamageBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    
    /*
     * Set the max food health and set the slider to match.
     */
    public void SetMaxFoodHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    /*
     * Update the slider when food takes damage.
     */
    public void SetFoodDamage(float damage)
    {
        slider.value = damage;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
