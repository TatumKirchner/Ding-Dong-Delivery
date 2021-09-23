using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Saves game data

    public int cones;
    public int bricks;
    public int anvils;
    public int numberOfUpgrades;
    public int speedUpgrades;
    public int numberOfBagUpgrades;
    public int numberOfEmployees;
    public int currentOneStarOrder;
    public int currentTwoStarOrder;
    public int currentThreeStarOrder;
    public float musicVolume;
    public float sfxVolume;
    public float voiceVolume;
    public float employeeMultiplyer;
    public float[] enemyPosition;
    public float[] enemyRotation;
    public float carDamage;
    public float[] playerPosition;
    public float[] playerRotation;
    public float money;
    public float rp;
    public float torque;
    public float maxSpeed;

    public GameData(Money playerMoney, RespectPoints playerRp, Weapons weapons, PlayerDamage playerDamage, CarSwitch carSwitch, PlayerCarController playerCarController, 
        PlayerCollisions playerCollisions, EmployeeUpgrades employee, OrderManager orderManager, SetVolume setVolume)
    {
        money = playerMoney.m_cash;
        rp = playerRp.points;
        cones = weapons.coneCount;
        bricks = weapons.brickCount;
        anvils = weapons.anvilCount;
        carDamage = playerDamage.currentHealth;

        playerPosition = new float[3];
        playerPosition[0] = playerCarController.transform.position.x;
        playerPosition[1] = playerCarController.transform.position.y;
        playerPosition[2] = playerCarController.transform.position.z;

        playerRotation = new float[3];
        playerRotation[0] = playerCarController.transform.rotation.eulerAngles.x;
        playerRotation[1] = playerCarController.transform.rotation.eulerAngles.y;
        playerRotation[2] = playerCarController.transform.rotation.eulerAngles.z;

        numberOfUpgrades = carSwitch.numberOfUpgrades;
        torque = playerCarController.m_FullTorqueOverAllWheels;
        speedUpgrades = playerCollisions.speedUpgrade;
        maxSpeed = playerCollisions.topSpeed;
        numberOfBagUpgrades = playerCollisions.numberOfBagUpgrades;
        numberOfEmployees = employee.numberOfEmployees;
        currentOneStarOrder = orderManager.currentOneStarOrder;
        currentTwoStarOrder = orderManager.currentTwoStarOrder;
        currentThreeStarOrder = orderManager.currentThreeStarOrder;

        musicVolume = setVolume.musicVol;
        sfxVolume = setVolume.sfxVol;
        voiceVolume = setVolume.voiceVol;
        employeeMultiplyer = employee.multiplyer;

        //////////// Enemy Position //////////////
        enemyPosition = new float[27];
        enemyPosition[0] = carSwitch.enemyStarterCars[0].transform.position.x;
        enemyPosition[1] = carSwitch.enemyStarterCars[0].transform.position.y;
        enemyPosition[2] = carSwitch.enemyStarterCars[0].transform.position.z;

        enemyPosition[3] = carSwitch.enemyStarterCars[1].transform.position.x;
        enemyPosition[4] = carSwitch.enemyStarterCars[1].transform.position.y;
        enemyPosition[5] = carSwitch.enemyStarterCars[1].transform.position.z;

        enemyPosition[6] = carSwitch.enemyStarterCars[2].transform.position.x;
        enemyPosition[7] = carSwitch.enemyStarterCars[2].transform.position.y;
        enemyPosition[8] = carSwitch.enemyStarterCars[2].transform.position.z;

        enemyPosition[9] = carSwitch.enemySedans[0].transform.position.x;
        enemyPosition[10] = carSwitch.enemySedans[0].transform.position.y;
        enemyPosition[11] = carSwitch.enemySedans[0].transform.position.z;

        enemyPosition[12] = carSwitch.enemySedans[1].transform.position.x;
        enemyPosition[13] = carSwitch.enemySedans[1].transform.position.y;
        enemyPosition[14] = carSwitch.enemySedans[1].transform.position.z;

        enemyPosition[15] = carSwitch.enemySedans[2].transform.position.x;
        enemyPosition[16] = carSwitch.enemySedans[2].transform.position.y;
        enemyPosition[17] = carSwitch.enemySedans[2].transform.position.z;

        enemyPosition[18] = carSwitch.enemySuperCars[0].transform.position.x;
        enemyPosition[19] = carSwitch.enemySuperCars[0].transform.position.y;
        enemyPosition[20] = carSwitch.enemySuperCars[0].transform.position.z;

        enemyPosition[21] = carSwitch.enemySuperCars[1].transform.position.x;
        enemyPosition[22] = carSwitch.enemySuperCars[1].transform.position.y;
        enemyPosition[23] = carSwitch.enemySuperCars[1].transform.position.z;

        enemyPosition[24] = carSwitch.enemySuperCars[2].transform.position.x;
        enemyPosition[25] = carSwitch.enemySuperCars[2].transform.position.y;
        enemyPosition[26] = carSwitch.enemySuperCars[2].transform.position.z;

        /////////////Enemy Rotation//////////////
        enemyRotation = new float[27];
        enemyRotation[0] = carSwitch.enemyStarterCars[0].transform.rotation.eulerAngles.x;
        enemyRotation[1] = carSwitch.enemyStarterCars[0].transform.rotation.eulerAngles.y;
        enemyRotation[2] = carSwitch.enemyStarterCars[0].transform.rotation.eulerAngles.z;

        enemyRotation[3] = carSwitch.enemyStarterCars[1].transform.rotation.eulerAngles.x;
        enemyRotation[4] = carSwitch.enemyStarterCars[1].transform.rotation.eulerAngles.y;
        enemyRotation[5] = carSwitch.enemyStarterCars[1].transform.rotation.eulerAngles.z;

        enemyRotation[6] = carSwitch.enemyStarterCars[2].transform.rotation.eulerAngles.x;
        enemyRotation[7] = carSwitch.enemyStarterCars[2].transform.rotation.eulerAngles.y;
        enemyRotation[8] = carSwitch.enemyStarterCars[2].transform.rotation.eulerAngles.z;

        enemyRotation[9] = carSwitch.enemySedans[0].transform.rotation.eulerAngles.x;
        enemyRotation[10] = carSwitch.enemySedans[0].transform.rotation.eulerAngles.y;
        enemyRotation[11] = carSwitch.enemySedans[0].transform.rotation.eulerAngles.z;

        enemyRotation[12] = carSwitch.enemySedans[1].transform.rotation.eulerAngles.x;
        enemyRotation[13] = carSwitch.enemySedans[1].transform.rotation.eulerAngles.y;
        enemyRotation[14] = carSwitch.enemySedans[1].transform.rotation.eulerAngles.z;

        enemyRotation[15] = carSwitch.enemySedans[2].transform.rotation.eulerAngles.x;
        enemyRotation[16] = carSwitch.enemySedans[2].transform.rotation.eulerAngles.y;
        enemyRotation[17] = carSwitch.enemySedans[2].transform.rotation.eulerAngles.z;

        enemyRotation[18] = carSwitch.enemySuperCars[0].transform.rotation.eulerAngles.x;
        enemyRotation[19] = carSwitch.enemySuperCars[0].transform.rotation.eulerAngles.y;
        enemyRotation[20] = carSwitch.enemySuperCars[0].transform.rotation.eulerAngles.z;

        enemyRotation[21] = carSwitch.enemySuperCars[1].transform.rotation.eulerAngles.x;
        enemyRotation[22] = carSwitch.enemySuperCars[1].transform.rotation.eulerAngles.y;
        enemyRotation[23] = carSwitch.enemySuperCars[1].transform.rotation.eulerAngles.z;

        enemyRotation[24] = carSwitch.enemySuperCars[2].transform.rotation.eulerAngles.x;
        enemyRotation[25] = carSwitch.enemySuperCars[2].transform.rotation.eulerAngles.y;
        enemyRotation[26] = carSwitch.enemySuperCars[2].transform.rotation.eulerAngles.z;
    }
}
