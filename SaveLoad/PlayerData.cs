using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public static PlayerData DefaultValues
    {
        get
        {
            PlayerData defaultData = new PlayerData();
            return defaultData;
        }
    }   
}

[System.Serializable]
public class PlayerDataHighscore
{
    public static PlayerDataHighscore DefaultValues
    {
        get
        {
            PlayerDataHighscore defaultData = new PlayerDataHighscore();
            return defaultData;
        }
    }
    public int highscore = 0;
}

[System.Serializable]
public class PlayerDataCustomCosts
{
    public static PlayerDataCustomCosts DefaultValues
    {
        get
        {
            PlayerDataCustomCosts defaultData = new PlayerDataCustomCosts();
            return defaultData;
        }
    }
}

[System.Serializable]
public class PlayerDataCustomize
{
    public static PlayerDataCustomize DefaultValues
    {
        get
        {
            PlayerDataCustomize defaultData = new PlayerDataCustomize();
            return defaultData;
        }
    }
    public int color_no = 0;
    public int selected_color = 0;
    public int trail_no = 0;
    public int selected_trail = 0;
}

[System.Serializable]
public class PlayerDataLocked
{
    public static PlayerDataLocked DefaultValues
    {
        get
        {
            PlayerDataLocked defaultData = new PlayerDataLocked();
            return defaultData;
        }
    }


    public bool color_0_locked = false;
    public bool color_1_locked = true;
    public bool color_2_locked = true;
    public bool color_3_locked = true;
    public bool color_4_locked = true;

    public bool trail_0_locked = false;
    public bool trail_1_locked = true;
    public bool trail_2_locked = true;
}

[System.Serializable]
public class PlayerDataCoins
{
    public static PlayerDataCoins DefaultValues
    {
        get
        {
            PlayerDataCoins defaultData = new PlayerDataCoins();
            return defaultData;
        }
    }

    public int coins = 0;
}

[System.Serializable]
public class PlayerDataVolumes
{
    public static PlayerDataVolumes DefaultValues
    {
        get
        {
            PlayerDataVolumes defaultData = new PlayerDataVolumes();
            return defaultData;
        }
    }

    public int m_volume = 3;
    public int s_volume = 3;
}

[System.Serializable]
public class PlayerDataQuality
{
    public static PlayerDataQuality DefaultValues
    {
        get
        {
            PlayerDataQuality defaultData = new PlayerDataQuality();
            return defaultData;
        }
    }

    public int quality_selection = 1;
}
