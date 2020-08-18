using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadHighscore
{
    private static string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/highscore.abc";
        }
    }

    public static void SavePlayer(PlayerDataHighscore data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataHighscore LoadPlayer()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            PlayerDataHighscore loadedData = formatter.Deserialize(stream) as PlayerDataHighscore;
            stream.Close();

            return loadedData;
        }
        else
        {
            SavePlayer(PlayerDataHighscore.DefaultValues);

            return PlayerDataHighscore.DefaultValues;
        }
    }
}
public class SaveLoadCustomize
{
    private static string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/customize.abc";
        }
    }

    public static void SavePlayer(PlayerDataCustomize data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataCustomize LoadPlayer()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            PlayerDataCustomize loadedData = formatter.Deserialize(stream) as PlayerDataCustomize;
            stream.Close();

            return loadedData;
        }
        else
        {
            SavePlayer(PlayerDataCustomize.DefaultValues);

            return PlayerDataCustomize.DefaultValues;
        }
    }
}
public class SaveLoadLocked
{
    private static string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/locked2.abc";
        }
    }

    public static void SavePlayer(PlayerDataLocked data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataLocked LoadPlayer()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);
            PlayerDataLocked loadedData = formatter.Deserialize(stream) as PlayerDataLocked;
            stream.Close();

            return loadedData;
        }
        else
        {
            SavePlayer(PlayerDataLocked.DefaultValues);

            return PlayerDataLocked.DefaultValues;
        }
    }
}


public class SaveLoadCoins
{
    private static string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/coins.abc";
        }
    }

    public static void SavePlayer(PlayerDataCoins data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataCoins LoadPlayer()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            PlayerDataCoins loadedData = formatter.Deserialize(stream) as PlayerDataCoins;
            stream.Close();

            return loadedData;
        }
        else
        {
            SavePlayer(PlayerDataCoins.DefaultValues);

            return PlayerDataCoins.DefaultValues;
        }
    }
}

public class SaveLoadVolumes
{
    private static string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/volumes.abc";
        }
    }

    public static void SavePlayer(PlayerDataVolumes data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataVolumes LoadPlayer()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            PlayerDataVolumes loadedData = formatter.Deserialize(stream) as PlayerDataVolumes;
            stream.Close();

            return loadedData;
        }
        else
        {
            SavePlayer(PlayerDataVolumes.DefaultValues);

            return PlayerDataVolumes.DefaultValues;
        }
    }
}

public class SaveLoadQuality
{
    private static string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/quality.abc";
        }
    }

    public static void SavePlayer(PlayerDataQuality data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataQuality LoadPlayer()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            PlayerDataQuality loadedData = formatter.Deserialize(stream) as PlayerDataQuality;
            stream.Close();

            return loadedData;
        }
        else
        {
            SavePlayer(PlayerDataQuality.DefaultValues);

            return PlayerDataQuality.DefaultValues;
        }
    }
}