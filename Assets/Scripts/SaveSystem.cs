using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    //Creates a save file
    public static void SaveGame(Money playerMoney, RespectPoints playerRp, Weapons weapons, PlayerDamage playerDamage, CarSwitch carSwitch, PlayerCarController playerCarController,
        PlayerCollisions playerCollisions, EmployeeUpgrades employee, OrderManager orderManager, SetVolume setVolume)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(playerMoney, playerRp, weapons, playerDamage, carSwitch, playerCarController,
        playerCollisions, employee, orderManager, setVolume);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Decodes the save file
    public static GameData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
