using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem{

    public static void SavePlayer(PlayerSaveManager _saveManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "player.piguset";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(_saveManager);

        formatter.Serialize(stream, data);
        stream.Close();

        //Debug.Log("saved");
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "player.piguset";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("save file not found");
            return null;
        }
    }

    public static void SaveBlock(BlockData[] blocks)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "blocks.blockPigu";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, blocks);
        stream.Close();

        //Debug.Log("saved");
    }

    public static BlockData[] LoadBlocks()
    {
        string path = Application.persistentDataPath + "blocks.blockPigu";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            BlockData[] blocksInScene = formatter.Deserialize(stream) as BlockData[];
            stream.Close();

            return blocksInScene;
        }
        else
        {
            Debug.Log("save file not found");
            return null;
        }
    }
}
