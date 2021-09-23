using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField]
    private FoodDamage FoodDamage;
    [SerializeField]
    private OrderManager orderManager;
    private PlayerDamage PlayerDamage;
    private IsPaused pauseManager;
    private PlayerCarController PlayerCarController;
    [Range(0.1f, 0.9f)]
    [SerializeField]
    private float damageSlowFactor = .75f;
    [HideInInspector] public float topSpeed;
    private AudioSource collisionAudio;

    [HideInInspector]
    public int speed = 0;
    [HideInInspector]
    public int speedUpgrade = 0;
    [HideInInspector]
    public bool upgradeCar;
    public int maxUpgrades = 5;
    [SerializeField]
    private float speedUpgradePercentage = 10;

    private bool speedMax;
    private bool speedOne;
    private bool speedTwo;
    private bool speedThree;
    [SerializeField] public bool changeSpeed;

    private float speed75;
    private float speed50;
    private float speed25;

    [HideInInspector]
    public bool carUpgrade = false;

    [HideInInspector]
    public bool sturdyDeliveryBag = false;
    [HideInInspector]
    public bool styrofoamBox = false;
    [HideInInspector]
    public bool bubbleWrappedBox = false;
    [HideInInspector]
    public bool foamBox = false;
    [HideInInspector]
    public int numberOfBagUpgrades = 0;
    [HideInInspector]
    public bool upgradeDeliveryBag = false;

    [SerializeField] 
    private string[] objectsToIgnore;

    private void Awake()
    {
        PlayerCarController = GetComponent<PlayerCarController>();
        pauseManager = FindObjectOfType<IsPaused>();
        PlayerDamage = GetComponent<PlayerDamage>();

        topSpeed = PlayerCarController.MaxSpeed;
        speed75 = topSpeed * damageSlowFactor;
        speed50 = speed75 * damageSlowFactor;
        speed25 = speed50 * damageSlowFactor;

        collisionAudio = GameObject.Find("Cameras/CM FreeLook1/Audio Source/Crash").GetComponent<AudioSource>();
        speedUpgradePercentage = (speedUpgradePercentage / 100) + 1;
    }

    private void Update()
    {
        DamageAdjustSpeed();
        SetSpeed();
        UpgradeSpeed();
        FoodDamageUpgrades();

        if (carUpgrade)
        {
            carUpgrade = false;
            PlayerCarController = GetComponent<PlayerCarController>();
            PlayerDamage = GetComponent<PlayerDamage>();
            topSpeed = PlayerCarController.MaxSpeed;
            speed75 = topSpeed * damageSlowFactor;
            speed50 = speed75 * damageSlowFactor;
            speed25 = speed50 * damageSlowFactor;
        }

        if (pauseManager.isPaused)
        {
            StopAllCoroutines();
        }
    }

    void FoodDamageUpgrades()
    {
        if (upgradeDeliveryBag)
        {
            switch (numberOfBagUpgrades)
            {
                case 0:
                    break;
                case 1:
                    upgradeDeliveryBag = false;
                    sturdyDeliveryBag = true;
                    styrofoamBox = false;
                    bubbleWrappedBox = false;
                    foamBox = false;
                    break;
                case 2:
                    upgradeDeliveryBag = false;
                    styrofoamBox = true;
                    sturdyDeliveryBag = false;
                    bubbleWrappedBox = false;
                    foamBox = false;
                    break;
                case 3:
                    upgradeDeliveryBag = false;
                    bubbleWrappedBox = true;
                    sturdyDeliveryBag = false;
                    styrofoamBox = false;
                    foamBox = false;
                    break;
                case 4:
                    upgradeDeliveryBag = false;
                    foamBox = true;
                    sturdyDeliveryBag = false;
                    styrofoamBox = false;
                    bubbleWrappedBox = false;
                    break;
            }
        }
    }

    private bool CompareTag(Collision col)
    {
        Collider hit = col.collider;
        for (int i = 0; i < objectsToIgnore.Length; i++)
        {
            if (hit.CompareTag(objectsToIgnore[i]))
            {
                return true;
            }
        }

        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(PlayHaptics(.5f, .25f));
        collisionAudio.Play();

        IEnumerator PlayHaptics(float seconds, float motorSpeed)
        {
            if (Gamepad.current != null)
            {
                Gamepad.current.SetMotorSpeeds(motorSpeed, motorSpeed);
                yield return new WaitForSeconds(seconds);
                InputSystem.ResetHaptics();
            }
        }

        ////////////// Player Damage ///////////////////
        if (CompareTag(collision))
        {
            return;
        }
        else
        {
            PlayerDamage.TakeDamage(collision.relativeVelocity.magnitude / 6);
        }
        ///////////// End Player Damage ////////////////////////

        ///////////////////  Food Damage ///////////////////////
        if (!sturdyDeliveryBag && !styrofoamBox && !bubbleWrappedBox && !foamBox)
        {
            if (orderManager.foodCanDamage)
            {
                FoodDamage.TakeDamage(collision.relativeVelocity.magnitude / 2);
            }
        }
        if (sturdyDeliveryBag && !styrofoamBox && !bubbleWrappedBox && !foamBox)
        {
            if (orderManager.foodCanDamage)
            {
                FoodDamage.TakeDamage(collision.relativeVelocity.magnitude / 2.25f);
            }
        }
        if (!sturdyDeliveryBag && styrofoamBox && !bubbleWrappedBox && !foamBox)
        {
            if (orderManager.foodCanDamage)
            {
                FoodDamage.TakeDamage(collision.relativeVelocity.magnitude / 2.5f);
            }
        }
        if (!sturdyDeliveryBag && !styrofoamBox && bubbleWrappedBox && !foamBox)
        {
            if (orderManager.foodCanDamage)
            {
                FoodDamage.TakeDamage(collision.relativeVelocity.magnitude / 2.75f);
            }
        }
        /////////////// End Food Damage //////////////////////
    }

    void DamageAdjustSpeed()
    {
        if (PlayerDamage.currentHealth >= 76f)
        {
            speed = 0;
            PlayerDamage.currentDamageLevel = 0;
            changeSpeed = true;
        }
        if (PlayerDamage.currentHealth <= 75f && PlayerDamage.currentHealth >= 50f)
        {
            speed = 1;
            PlayerDamage.currentDamageLevel = 1;
            changeSpeed = true;
        }
        if (PlayerDamage.currentHealth <= 49 && PlayerDamage.currentHealth > 25)
        {
            speed = 2;
            PlayerDamage.currentDamageLevel = 2;
            changeSpeed = true;
        }
        if (PlayerDamage.currentHealth <= 24)
        {
            speed = 3;
            PlayerDamage.currentDamageLevel = 3;
            changeSpeed = true;
        }

        if (changeSpeed)
        {
            switch (speed)
            {
                case 0:
                    changeSpeed = false;
                    speedMax = true;
                    speedOne = false;
                    speedTwo = false;
                    speedThree = false;
                    break;
                case 1:
                    changeSpeed = false;
                    speedOne = true;
                    speedMax = false;
                    speedTwo = false;
                    speedThree = false;
                    break;
                case 2:
                    changeSpeed = false;
                    speedTwo = true;
                    speedMax = false;
                    speedOne = false;
                    speedThree = false;
                    break;
                case 3:
                    changeSpeed = false;
                    speedThree = true;
                    speedMax = false;
                    speedOne = false;
                    speedTwo = false;
                    break;
            }
        }
    }

    void SetSpeed()
    {
        if (speedMax)
        {
            PlayerCarController.m_Topspeed = topSpeed;
        }
        if (speedOne)
        {
            PlayerCarController.m_Topspeed = speed75;
        }
        if (speedTwo)
        {
            PlayerCarController.m_Topspeed = speed50;
        }
        if (speedThree)
        {
            PlayerCarController.m_Topspeed = speed25;
        }
    }

    void UpgradeSpeed()
    {
        if (upgradeCar)
        {
            speedUpgrade++;

            if (speedUpgrade <= maxUpgrades)
            {
                upgradeCar = false;
                topSpeed *= speedUpgradePercentage;
                speed75 *= speedUpgradePercentage;
                speed50 *= speedUpgradePercentage;
                speed25 *= speedUpgradePercentage;
                Debug.Log(topSpeed);
            }
        }
    }
}
