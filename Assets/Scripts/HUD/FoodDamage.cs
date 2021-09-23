using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDamage : MonoBehaviour
{
    public float m_foodQuality = 100;
    public bool tookDamage = false;

    [SerializeField] private FoodDamageBar FoodDamageBar;

    private void Start()
    {
        FoodDamageBar.SetMaxFoodHealth(m_foodQuality);
    }

    /*
     * When the food takes damage call this method to set the damage, and update the UI element.
     */
    public void TakeDamage(float damage)
    {
        m_foodQuality -= damage;
        m_foodQuality = Mathf.Clamp(m_foodQuality, 0f, 100f);
        tookDamage = true;
        FoodDamageBar.SetFoodDamage(m_foodQuality);
    }
}
