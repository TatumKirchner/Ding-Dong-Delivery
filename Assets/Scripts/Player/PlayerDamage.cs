using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    [SerializeField] private CarDamageBar CarDamageBar;
    private PlayerCollisions PlayerCollisions;

    [SerializeField] private Texture2D[] damageTextures;
    private Renderer mainRenderer;

    [HideInInspector] public int currentDamageLevel = 0;

    [HideInInspector] public bool changeTexture = false;
    [SerializeField] private bool sedan = false;
    [SerializeField] private bool starterCar = false;
    [SerializeField] private bool superCar = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        CarDamageBar.SetMaxCarHealth(maxHealth);
        PlayerCollisions = GetComponent<PlayerCollisions>();

        if (sedan)
            mainRenderer = transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Renderer>();
        if (starterCar)
            mainRenderer = transform.GetChild(0).GetComponent<Renderer>();
        if (superCar)
            mainRenderer = transform.GetChild(0).GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CarDamageBar.SetCarDamage(currentHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateTexture();
    }

    //Called when the player takes damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        changeTexture = true;
    }

    //Called when the player repairs in the shop
    public void Repair()
    {
        currentHealth = maxHealth;
        PlayerCollisions.speed = 0;
        currentDamageLevel = 0;
        changeTexture = true;
    }

    //When the player reaches a damage threshold swap textures to match the damage level
    void UpdateTexture()
    {
        if (changeTexture)
        {
            switch (currentDamageLevel)
            {
                case 0:
                    mainRenderer.material.mainTexture = damageTextures[0];
                    changeTexture = false;
                    break;
                case 1:
                    mainRenderer.material.mainTexture = damageTextures[1];
                    changeTexture = false;
                    break;
                case 2:
                    mainRenderer.material.mainTexture = damageTextures[2];
                    changeTexture = false;
                    break;
                case 3:
                    mainRenderer.material.mainTexture = damageTextures[3];
                    changeTexture = false;
                    break;
            }
        }
    }
}
