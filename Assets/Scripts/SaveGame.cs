using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveGame : MonoBehaviour
{
    public Money money;
    public RespectPoints rp;
    public Weapons weapons;
    public PlayerDamage playerDamage;
    public CarSwitch carSwitch;
    public PlayerCarController carController;
    public PlayerCollisions playerCollisions;
    public EmployeeUpgrades employee;
    public OrderManager orderManager;
    public SetVolume setVolume;
    public GameObject startPanel;
    public IsPaused paused;
    public DrivingAroundMusic drivingMusic;
    public StartMenu startMenu;
    public Shop shop;
    public Speed speed;
    public GameObject pausePanel;
    public PauseMenu pauseMenu;

    public void SavePlayer()
    {
        SaveSystem.SaveGame(money, rp, weapons, playerDamage, carSwitch, carController, playerCollisions, employee, orderManager, setVolume);
        Debug.Log("Saved Game");
    }

    public void LoadPlayer()
    {
        GameData data = SaveSystem.LoadPlayer();

        /////////////// Set Enemy Position and Rotation /////////////////////
        Vector3 pos;
        pos.x = data.enemyPosition[0];
        pos.y = data.enemyPosition[1];
        pos.z = data.enemyPosition[2];
        carSwitch.enemyStarterCars[0].transform.position = pos;

        Vector3 pos1;
        pos1.x = data.enemyPosition[3];
        pos1.y = data.enemyPosition[4];
        pos1.z = data.enemyPosition[5];
        carSwitch.enemyStarterCars[1].transform.position = pos1;

        Vector3 pos2;
        pos2.x = data.enemyPosition[6];
        pos2.y = data.enemyPosition[7];
        pos2.z = data.enemyPosition[8];
        carSwitch.enemyStarterCars[2].transform.position = pos2;

        Vector3 pos3;
        pos3.x = data.enemyPosition[9];
        pos3.y = data.enemyPosition[10];
        pos3.z = data.enemyPosition[11];
        carSwitch.enemySedans[0].transform.position = pos3;

        Vector3 pos4;
        pos4.x = data.enemyPosition[12];
        pos4.y = data.enemyPosition[13];
        pos4.z = data.enemyPosition[14];
        carSwitch.enemySedans[1].transform.position = pos4;

        Vector3 pos5;
        pos5.x = data.enemyPosition[15];
        pos5.y = data.enemyPosition[16];
        pos5.z = data.enemyPosition[17];
        carSwitch.enemySedans[2].transform.position = pos5;

        Vector3 pos6;
        pos6.x = data.enemyPosition[18];
        pos6.y = data.enemyPosition[19];
        pos6.z = data.enemyPosition[20];
        carSwitch.enemySuperCars[0].transform.position = pos6;

        Vector3 pos7;
        pos7.x = data.enemyPosition[21];
        pos7.y = data.enemyPosition[22];
        pos7.z = data.enemyPosition[23];
        carSwitch.enemySuperCars[1].transform.position = pos7;

        Vector3 pos8;
        pos8.x = data.enemyPosition[24];
        pos8.y = data.enemyPosition[25];
        pos8.z = data.enemyPosition[26];
        carSwitch.enemySuperCars[2].transform.position = pos8;

        Quaternion rot = Quaternion.Euler(data.enemyRotation[0], data.enemyRotation[1], data.enemyRotation[2]);
        carSwitch.enemyStarterCars[0].transform.rotation = rot;

        Quaternion rot1 = Quaternion.Euler(data.enemyRotation[3], data.enemyRotation[4], data.enemyRotation[5]);
        carSwitch.enemyStarterCars[1].transform.rotation = rot1;

        Quaternion rot2 = Quaternion.Euler(data.enemyRotation[6], data.enemyRotation[7], data.enemyRotation[8]);
        carSwitch.enemyStarterCars[2].transform.rotation = rot2;

        Quaternion rot3 = Quaternion.Euler(data.enemyRotation[9], data.enemyRotation[10], data.enemyRotation[11]);
        carSwitch.enemySedans[0].transform.rotation = rot3;

        Quaternion rot4 = Quaternion.Euler(data.enemyRotation[12], data.enemyRotation[13], data.enemyRotation[14]);
        carSwitch.enemySedans[1].transform.rotation = rot4;

        Quaternion rot5 = Quaternion.Euler(data.enemyRotation[15], data.enemyRotation[16], data.enemyRotation[17]);
        carSwitch.enemySedans[2].transform.rotation = rot5;

        Quaternion rot6 = Quaternion.Euler(data.enemyRotation[18], data.enemyRotation[19], data.enemyRotation[20]);
        carSwitch.enemySuperCars[0].transform.rotation = rot6;

        Quaternion rot7 = Quaternion.Euler(data.enemyRotation[21], data.enemyRotation[22], data.enemyRotation[23]);
        carSwitch.enemySuperCars[1].transform.rotation = rot7;

        Quaternion rot8 = Quaternion.Euler(data.enemyRotation[24], data.enemyRotation[25], data.enemyRotation[26]);
        carSwitch.enemySuperCars[2].transform.rotation = rot8;

        //////////////// End Enemy Position and Rotation /////////////////

        // Set Car Type
        drivingMusic.introPlayed = true;
        carSwitch.numberOfUpgrades = data.numberOfUpgrades;
        carSwitch.Upgrade();
        playerDamage.currentHealth = data.carDamage;
        playerDamage.changeTexture = true;

        //Set Money and RP
        money.m_cash = data.money;
        rp.points = data.rp;
        
        //Set Weapon Counts
        weapons.coneCount = data.cones;
        weapons.brickCount = data.bricks;
        weapons.anvilCount = data.anvils;

        //Set Player Position and Rotation
        carSwitch.currentCar.GetComponent<Rigidbody>().isKinematic = true;

        Vector3 position;
        position.x = data.playerPosition[0];
        position.y = data.playerPosition[1];
        position.z = data.playerPosition[2];
        carController.transform.position = position;

        Quaternion rotation = Quaternion.Euler(data.playerRotation[0], data.playerRotation[1], data.playerRotation[2]);
        carController.transform.rotation = rotation;

        carSwitch.currentCar.GetComponent<Rigidbody>().isKinematic = false;

        //Set Player Car Stats and Upgrades
        carController.m_FullTorqueOverAllWheels = data.torque;
        playerCollisions.speedUpgrade = data.speedUpgrades;
        playerCollisions.topSpeed = data.maxSpeed;
        playerCollisions.numberOfBagUpgrades = data.numberOfBagUpgrades;
        employee.numberOfEmployees = data.numberOfEmployees;
        employee.multiplyer = data.employeeMultiplyer;
        playerCollisions.upgradeDeliveryBag = true;
        employee.hire = true;
        playerCollisions.changeSpeed = true;
        shop.updateText = true;
        speed.UpdateMaxSpeed();

        //Set Current Orders
        orderManager.currentOneStarOrder = data.currentOneStarOrder;
        orderManager.currentTwoStarOrder = data.currentTwoStarOrder;
        orderManager.currentThreeStarOrder = data.currentThreeStarOrder;

        //Set Volume
        setVolume.SetLevel(data.musicVolume);
        setVolume.SetSfxVolume(data.sfxVolume);
        setVolume.SetVoiceVolume(data.voiceVolume);

        //Set Music, Close Menus, and Unpause
        startPanel.SetActive(false);
        pausePanel.SetActive(false);
        drivingMusic.drivingMusic.clip = null;
        startMenu.startPanelOpen = false;
        pauseMenu.pauseOpen = false;
        paused.isPaused = false;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
