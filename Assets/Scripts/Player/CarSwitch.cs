using Cinemachine;
using UnityEngine;

public class CarSwitch : MonoBehaviour
{
    [Header("Player Cars")]
    public float upgradeCost = 500f;
    public GameObject starterCar;
    public GameObject sedan;
    public GameObject superCar;
    [HideInInspector] public GameObject currentCar;

    [Header("Player Components")]
    [SerializeField] private CinemachineFreeLook mainCamera;
    [SerializeField] private Speed speedGauge;
    [SerializeField] private FollowPlayer miniMapTarget;
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private PlayerCollisions playerCollisions;
    [SerializeField] private MinimapCamera minimapCamera;
    [SerializeField] private Speed speed;
    private EnemyTarget enemyTarget;
    [HideInInspector] public bool upgradeCar = false;
    [SerializeField] private Shop shop;
    private SaveGame save;
    private DrivingAroundMusic drivingMusic;

    [Header("Enemy Cars")]
    public GameObject[] enemySedans;
    public GameObject[] enemyStarterCars;
    public GameObject[] enemySuperCars;
    public int numberOfUpgrades = -1;

    private void Awake()
    {
        currentCar = starterCar;

        if (sedan != null)
            sedan.SetActive(false);

        if (superCar != null)
            superCar.SetActive(false);

        mainCamera.Follow = currentCar.transform;
        mainCamera.LookAt = currentCar.transform;

        miniMapTarget.target = currentCar.transform;

        speedGauge.player = currentCar.GetComponent<Rigidbody>();
        enemyTarget = GetComponent<EnemyTarget>();
        save = GetComponent<SaveGame>();
        drivingMusic = GetComponent<DrivingAroundMusic>();
    }

    private void Start()
    {
        foreach (GameObject go in enemySedans)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in enemySuperCars)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in enemyStarterCars)
        {
            go.SetActive(false);
        }
    }

    private void Update()
    {
        if (upgradeCar)
        {
            Upgrade();
        }
    }

    /*
     * When called check how many upgrades the player has. Then transfer the player stats over to the new car(i.e. ammo and upgrades). 
     * We also turn off the current enemies and turn the new enemies on.
     * then tell the managing scripts that the new car is the player (i.e GPS, Speed gage, and mini map)
     */
    public void Upgrade()
    {
        //Once the intro is finished turn the enemies on
        if (numberOfUpgrades == -1 && drivingMusic.introPlayed)
        {
            foreach (GameObject go in enemyStarterCars)
            {
                go.SetActive(true);
            }
            foreach (GameObject go in enemySedans)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in enemySuperCars)
            {
                go.SetActive(false);
            }
        }

        //Upgrade the car
        if (numberOfUpgrades == 0)
        {
            upgradeCar = false;
            sedan.SetActive(true);
            sedan.transform.SetPositionAndRotation(currentCar.transform.position, currentCar.transform.rotation);
            sedan.GetComponent<PlayerCollisions>().numberOfBagUpgrades = currentCar.GetComponent<PlayerCollisions>().numberOfBagUpgrades;
            sedan.GetComponent<PlayerCollisions>().upgradeDeliveryBag = true;
            sedan.GetComponent<Weapons>().coneCount = currentCar.GetComponent<Weapons>().coneCount;
            sedan.GetComponent<Weapons>().brickCount = currentCar.GetComponent<Weapons>().brickCount;
            sedan.GetComponent<Weapons>().anvilCount = currentCar.GetComponent<Weapons>().anvilCount;
            shop.updateText = true;
            starterCar.SetActive(false);
            currentCar = sedan;
            save.weapons = currentCar.GetComponent<Weapons>();
            save.playerDamage = currentCar.GetComponent<PlayerDamage>();
            save.carController = currentCar.GetComponent<PlayerCarController>();
            save.playerCollisions = currentCar.GetComponent<PlayerCollisions>();
            speed.UpdateMaxSpeed();
            enemyTarget.target = currentCar.transform.Find("GPS");
            mainCamera.Follow = currentCar.transform;
            mainCamera.LookAt = currentCar.transform;
            speedGauge.player = currentCar.GetComponent<Rigidbody>();
            miniMapTarget.target = currentCar.transform;
            orderManager.gps = currentCar.transform.Find("GPS").GetComponent<GpsPath>();
            playerCollisions.carUpgrade = true;
            minimapCamera.player = currentCar.transform;

            foreach (GameObject go in enemySedans)
            {
                go.SetActive(true);
            }
            foreach (GameObject go in enemyStarterCars)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in enemySuperCars)
            {
                go.SetActive(false);
            }
        }

        if (numberOfUpgrades == 1)
        {
            upgradeCar = false;
            superCar.SetActive(true);
            superCar.transform.position = currentCar.transform.position;
            superCar.transform.rotation = currentCar.transform.rotation;
            superCar.GetComponent<PlayerCollisions>().numberOfBagUpgrades = currentCar.GetComponent<PlayerCollisions>().numberOfBagUpgrades;
            superCar.GetComponent<PlayerCollisions>().upgradeDeliveryBag = true;
            superCar.GetComponent<Weapons>().coneCount = currentCar.GetComponent<Weapons>().coneCount;
            superCar.GetComponent<Weapons>().brickCount = currentCar.GetComponent<Weapons>().brickCount;
            superCar.GetComponent<Weapons>().anvilCount = currentCar.GetComponent<Weapons>().anvilCount;
            starterCar.SetActive(false);
            sedan.SetActive(false);
            currentCar = superCar;
            save.weapons = currentCar.GetComponent<Weapons>();
            save.playerDamage = currentCar.GetComponent<PlayerDamage>();
            save.carController = currentCar.GetComponent<PlayerCarController>();
            save.playerCollisions = currentCar.GetComponent<PlayerCollisions>();
            enemyTarget.target = currentCar.transform.Find("GPS");
            mainCamera.Follow = currentCar.transform;
            mainCamera.Follow = currentCar.transform;
            mainCamera.LookAt = currentCar.transform;
            speedGauge.player = currentCar.GetComponent<Rigidbody>();
            miniMapTarget.target = currentCar.transform;
            orderManager.gps = currentCar.transform.GetChild(4).GetComponent<GpsPath>();
            playerCollisions.carUpgrade = true;
            minimapCamera.player = currentCar.transform;

            foreach (GameObject go in enemyStarterCars)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in enemySedans)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in enemySuperCars)
            {
                go.SetActive(true);
            }
        }
    }
}
