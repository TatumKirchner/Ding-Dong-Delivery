using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Weapons : MonoBehaviour
{
    public int coneCount = 10;
    public int brickCount = 5;
    public int anvilCount = 0;
    private int weaponIndex = 0;

    [SerializeField] private GameObject[] weapon;
    [SerializeField] private Transform throwPos;
    [SerializeField] private Text weaponsText;
    [SerializeField] private TaskPanelController taskPanelController;
    [SerializeField] private IsPaused isPaused;
    [SerializeField] private RepairSpot repairSpot;
    private GameplayControls playerControls;

    [SerializeField] private float throwForce = 50f;

    public bool fired = false;

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
        playerControls.Gameplay.WeaponSwitch.performed += _ => GetInput();
        playerControls.Gameplay.Fire.performed += _ => ThrowWeapon();
    }

    private void Update()
    {
        //ThrowWeapon();

        /*TODO - Update the UI when the weapon count changes*/
        DisplayWeaponCount();
    }

    void ThrowWeapon()
    {
        //If a gamepad is detected vibrate when thrown
        IEnumerator PlayHaptics(float seconds, float motorSpeed)
        {
            if (Gamepad.current != null)
            {
                Gamepad.current.SetMotorSpeeds(motorSpeed, motorSpeed);
                yield return new WaitForSeconds(seconds);
                InputSystem.ResetHaptics();
            }
        }

        //Depending on which weapon is selected spawn it and deduct it from the player inventory

        //Spawn Cone
        if (playerControls.Gameplay.Fire.ReadValue<float>() > 0 && coneCount > 0 && weaponIndex == 0 && !fired && !taskPanelController.panelActive && !isPaused.isPaused && !repairSpot.shopOpen)
        {
            StartCoroutine(PlayHaptics(.15f, .15f));
            GameObject weaponInstance = Instantiate(weapon[0], throwPos.position, Quaternion.identity);
            weaponInstance.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
            coneCount -= 1;
            coneCount = Mathf.Clamp(coneCount, 0, 99);
            fired = true;
        }
        //Spawn Brick
        if (playerControls.Gameplay.Fire.ReadValue<float>() > 0 && brickCount > 0 && weaponIndex == 1 && !fired && !taskPanelController.panelActive && !isPaused.isPaused && !repairSpot.shopOpen)
        {
            StartCoroutine(PlayHaptics(.15f, .15f));
            GameObject weaponInstance = Instantiate(weapon[1], throwPos.position, Quaternion.identity);
            weaponInstance.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
            brickCount -= 1;
            brickCount = Mathf.Clamp(brickCount, 0, 99);
            fired = true;
        }
        //Spawn Anvil
        if (playerControls.Gameplay.Fire.ReadValue<float>() > 0 && anvilCount > 0 && weaponIndex == 2 && !fired && !taskPanelController.panelActive && !isPaused.isPaused && !repairSpot.shopOpen)
        {
            StartCoroutine(PlayHaptics(.15f, .15f));
            GameObject weaponInstance = Instantiate(weapon[2], throwPos.position, Quaternion.identity);
            anvilCount -= 1;
            anvilCount = Mathf.Clamp(anvilCount, 0, 99);
            fired = true;
        }

        fired = false;
    }

    //Mouse Wheel input will change the weapon
    void GetInput()
    {
        if (playerControls.Gameplay.WeaponSwitch.ReadValue<float>() > 0 || playerControls.Gameplay.WeaponSwitch.ReadValue<float>() < 0)
        {
            weaponIndex++;

            if (weaponIndex > 2)
                weaponIndex = 0;
            if (weaponIndex < 0)
                weaponIndex = 2;
        }
    }

    //Called to add a weapon to the player inventory
    public void AddWeapon(int amtToAdd, int weaponIndex)
    {
        if (weaponIndex == 1)
            coneCount += amtToAdd; coneCount = Mathf.Clamp(coneCount, 0, 99);
        if (weaponIndex == 2)
            brickCount += amtToAdd; brickCount = Mathf.Clamp(brickCount, 0, 99);
        if (weaponIndex == 3)
            anvilCount += amtToAdd; anvilCount = Mathf.Clamp(anvilCount, 0, 99);
    }

    //Updates the UI to show the correct amount of weapons.
    void DisplayWeaponCount()
    {
        if (weaponIndex == 0)
            weaponsText.text = "Cones: " + coneCount;
        if (weaponIndex == 1)
            weaponsText.text = "Bricks: " + brickCount;
        if (weaponIndex == 2)
            weaponsText.text = "Anvils: " + anvilCount;
    }
}
