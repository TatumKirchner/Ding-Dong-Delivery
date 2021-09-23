using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    
    [Header("Upgrade Costs")]
    [SerializeField] private float repairCost = 5f;
    [SerializeField] private float upgradeCost = 50f;
    [SerializeField] private float rpUpgradeCost = 25f;
    [SerializeField] private float rpNewCarCost = 150f;
    [SerializeField] private float bagUpgradeOneCost = 75f;
    [SerializeField] private float bagUpgradeTwoCost = 150f;
    [SerializeField] private float bagUpgradeThreeCost = 250f;
    [SerializeField] private float bagUpgradeFourCost = 500f;

    [Header("Audio")]
    [SerializeField] private AudioSource success;
    [SerializeField] private AudioSource fail;

    [Header("Referenced Scripts")]
    [SerializeField] private PlayerCollisions playerCollisions;
    [SerializeField] private CarSwitch carSwitch;
    [SerializeField] private RespectPoints respectPoints;
    [SerializeField] private PlayerDamage playerDamage;
    [SerializeField] private EmployeeUpgrades employee;
    [SerializeField] private PlayerCarController PlayerCarController;
    [SerializeField] private RepairSpot RepairSpot;
    [SerializeField] private Money Money;
    [SerializeField] private Speed speed;
    private GameplayControls playerControls;

    [Header("Shop Text Objects")]
    [SerializeField] private Text repairText;
    [SerializeField] private Text upgradeCarText;
    [SerializeField] private Text buyNewCarText;
    [SerializeField] private Text bagUpgradeText;
    [SerializeField] private Text employeeText;
    [HideInInspector] public bool updateText = false;

    private bool newCar = false;
    [HideInInspector] public bool shopActive = false;

    private void Awake()
    {
        playerControls = new GameplayControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerCollisions = carSwitch.currentCar.GetComponent<PlayerCollisions>();
        playerDamage = carSwitch.currentCar.GetComponent<PlayerDamage>();
        Debug.Log(playerCollisions.gameObject.name);
        playerControls.Gameplay.Pause.performed += _ => Exit();
        success.ignoreListenerPause = true;
        fail.ignoreListenerPause = true;
        repairText.text = "$" + repairCost;
        newCar = true;
        UpdateText();
    }

    private void Update()
    {
        //If the player buys a new car change the collisions and damage handlers to the new car.
        if (newCar)
        {
            playerCollisions = carSwitch.currentCar.GetComponent<PlayerCollisions>();
            playerDamage = carSwitch.currentCar.GetComponent<PlayerDamage>();
            UpdateText();
            newCar = false;
        }

        //Used for other menus to check if the shop is open.
        if (gameObject.activeInHierarchy)
        {
            shopActive = true;
        }

        //Used to update the text if the player buys an upgrade
        if (updateText)
        {
            updateText = false;
            upgradeCarText.text = "$" + upgradeCost + " | RP - " + rpUpgradeCost;
            UpdateText();
        }
    }

    //Used when the player upgrades their car.
    public void UpgradeCar()
    {
        if (Money.m_cash >= upgradeCost && respectPoints.points >= rpUpgradeCost && playerCollisions.speedUpgrade <= playerCollisions.maxUpgrades)
        {
            //Play a sound, increase the torque, and remove currency from players.
            success.Play();
            PlayerCarController.m_FullTorqueOverAllWheels += 250f;
            playerCollisions.upgradeCar = true;
            Money.SpendMoney(upgradeCost);
            respectPoints.RemovePoints(rpUpgradeCost);
            speed.UpdateMaxSpeed();
        }
        else
        {
            fail.Play();
        }

        //Check if player has all upgrades
        if (playerCollisions.speedUpgrade == playerCollisions.maxUpgrades)
        {
            upgradeCarText.text = "Max Upgrades";
        }
    }

    public void RepairCar()
    {
        if (Money.m_cash >= repairCost && playerDamage.currentHealth < playerDamage.maxHealth)
        {
            success.Play();
            playerDamage.Repair();
            Money.SpendMoney(repairCost);
        }
        else
        {
            fail.Play();
        }
    }

    public void Exit()
    {
        if (gameObject.activeInHierarchy)
        {
            shopActive = false;
            RepairSpot.Close();
        }        
    }

    //When the player buys a new car access the CarSwitch class and transfer ownership and player data. 
    public void BuyCar()
    {
        if (Money.m_cash >= carSwitch.upgradeCost && respectPoints.points >= rpNewCarCost && carSwitch.numberOfUpgrades < 1)
        {
            carSwitch.numberOfUpgrades++;
            carSwitch.upgradeCar = true;
            Money.SpendMoney(carSwitch.upgradeCost);
            respectPoints.RemovePoints(rpNewCarCost);
            newCar = true;
            success.Play();
        }
        else
        {
            fail.Play();
        }
    }

    //Check if player has a fully upgraded bag. If not take money and give an upgrade.
    public void UpgradeDeliveryBag()
    {
        if (playerCollisions.numberOfBagUpgrades == 0 && Money.m_cash >= bagUpgradeOneCost)
        {
            playerCollisions.numberOfBagUpgrades++;
            playerCollisions.upgradeDeliveryBag = true;
            Money.SpendMoney(bagUpgradeOneCost);
            success.Play();
            bagUpgradeText.text = "$ " + bagUpgradeTwoCost;
            return;
        }
        else
        {
            fail.Play();
        }

        if (playerCollisions.numberOfBagUpgrades == 1 && Money.m_cash >= bagUpgradeTwoCost)
        {
            playerCollisions.numberOfBagUpgrades++;
            playerCollisions.upgradeDeliveryBag = true;
            Money.SpendMoney(bagUpgradeTwoCost);
            success.Play();
            bagUpgradeText.text = "$ " + bagUpgradeThreeCost;
            return;
        }
        else
        {
            fail.Play();
        }

        if (playerCollisions.numberOfBagUpgrades == 2 && Money.m_cash >= bagUpgradeThreeCost)
        {
            playerCollisions.numberOfBagUpgrades++;
            playerCollisions.upgradeDeliveryBag = true;
            Money.SpendMoney(bagUpgradeThreeCost);
            success.Play();
            bagUpgradeText.text = "$ " + bagUpgradeFourCost;
            return;
        }
        else
        {
            fail.Play();
        }

        if (playerCollisions.numberOfBagUpgrades == 3 && Money.m_cash >= bagUpgradeFourCost)
        {
            playerCollisions.numberOfBagUpgrades++;
            playerCollisions.upgradeDeliveryBag = true;
            Money.SpendMoney(bagUpgradeFourCost);
            success.Play();
            bagUpgradeText.text = "Max Upgrades";
            return;
        }
        else
        {
            fail.Play();
        }
    }

    //Check if player has all employees, if not hire one.
    public void HireEmployee()
    {
        if (employee.numberOfEmployees == 5)
        {
            employeeText.text = "Max Upgrades";
        }

        if (Money.m_cash >= employee.employeeCost && employee.numberOfEmployees < 5)
        {
            Money.SpendMoney(employee.employeeCost);
            employee.numberOfEmployees++;
            employee.hire = true;
            success.Play();
        }
        else
        {
            fail.Play();
        }
    }

    //Called to update the UI text in the shop.
    void UpdateText()
    {
        if (employee.numberOfEmployees == 5)
        {
            employeeText.text = "Max Upgrades";
        }
        else
        {
            employeeText.text = "$" + employee.employeeCost;
        }

        if (playerCollisions.numberOfBagUpgrades >= 3)
        {
            bagUpgradeText.text = "Max Upgrades";
        }
        else if (playerCollisions.numberOfBagUpgrades == 2)
        {
            bagUpgradeText.text = "$ " + bagUpgradeFourCost;
        }
        else if (playerCollisions.numberOfBagUpgrades == 1)
        {
            bagUpgradeText.text = "$ " + bagUpgradeThreeCost;
        }
        else if (playerCollisions.numberOfBagUpgrades == 0)
        {
            bagUpgradeText.text = "$ " + bagUpgradeTwoCost;
        }
        else
        {
            bagUpgradeText.text = "$ " + bagUpgradeOneCost;
        }

        if (playerCollisions.speedUpgrade >= playerCollisions.maxUpgrades)
        {
            upgradeCarText.text = "Max Upgrades";
        }
        else
        {
            upgradeCarText.text = "$" + upgradeCost + " | RP - " + rpUpgradeCost;
        }

        if (carSwitch.currentCar == carSwitch.superCar)
        {
            buyNewCarText.text = "All Cars Owned";
        }
        else
        {
            buyNewCarText.text = "$" + carSwitch.upgradeCost + " | RP - " + rpNewCarCost;
        }
    }
}
