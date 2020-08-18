using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    public AudioSource Menu_Sound, UI_Button_Sound, BuySound;

    public GameObject[] Colors;

    public Sprite[] TrailsImg;
    public GameObject[] Trails;

    public GameObject Player_Idle;

    public GameObject Selected_Color;
    public GameObject Selected_Trail;

    public Text SelectedBuyColor_Text;
    public Text SelectedBuyTrail_Text;

    public Text PlayerCoins_Text;
    public Text ColorCost_Text, TrailCost_Text;
    public GameObject BuyValuesColor, BuyValuesTrail;

    // VOLUMES    
    public GameObject Music_Icon, Sound_Icon;
    public GameObject m_vol_1, m_vol_2, m_vol_3, m_vol_4, m_vol_5;
    public GameObject s_vol_1, s_vol_2, s_vol_3, s_vol_4, s_vol_5;
    public Sprite Mute_Music, Unmute_Music, Mute_Sound, Unmute_Sound;
    public Sprite music_vol_active, music_vol_deactive;
    public Sprite sound_vol_active, sound_vol_deactive;

    // QUALITY
    public GameObject Quality_Low, Quality_High;
    public Sprite quality_unselected, quality_selected;    

    public int music_volume, sound_volume;
    public int quality_selection;

    public int color_selection, trail_selection;

    public bool color_0_locked, color_1_locked, color_2_locked, color_3_locked, color_4_locked;
    public bool trail_0_locked, trail_1_locked, trail_2_locked;

    public int selected_color;
    public int selected_trail;
    
    public int player_coins;

    public int color_costs = 5000;
    public int trail_costs = 5000;

    // SETTINGS
    private bool show_settings = false;
    public GameObject Settings_Panel;
    
    public int highscore;
    public Text Highscore;

    void Start()
    {
        Load_Customization();
        Load_PlayerCoins();
        Load_Locked();
        Load_Volumes();
        Load_QualitySettings();
        Load_Highscore();
    }
    public void White_Color_Change()
    {
        Selected_Color.gameObject.GetComponent<Image>().color = Colors[0].gameObject.GetComponent<Image>().color;
        color_selection = 0;
        Set_Locked();
    }    
        
    public void Blue_Color_Change()
    {
        Selected_Color.gameObject.GetComponent<Image>().color = Colors[1].gameObject.GetComponent<Image>().color;
        color_selection = 1;
        Set_Locked();
    }
    public void Green_Color_Change()
    {
        Selected_Color.gameObject.GetComponent<Image>().color = Colors[2].gameObject.GetComponent<Image>().color;
        color_selection = 2;
        Set_Locked();
    }
    public void Orange_Color_Change()
    {
        Selected_Color.gameObject.GetComponent<Image>().color = Colors[3].gameObject.GetComponent<Image>().color;
        color_selection = 3;
        Set_Locked();
    }
    public void Pink_Color_Change()
    {
        Selected_Color.gameObject.GetComponent<Image>().color = Colors[4].gameObject.GetComponent<Image>().color;
        color_selection = 4;
        Set_Locked();
    }

    public void Blue_Trail_Change()
    {
        Selected_Trail.gameObject.GetComponent<Image>().sprite = TrailsImg[0]; 
        trail_selection = 0;
        Set_Locked();
    }

    public void Orange_Trail_Change()
    {
        Selected_Trail.gameObject.GetComponent<Image>().sprite = TrailsImg[1];
        trail_selection = 1;
        Set_Locked();
    }

    public void Purple_Trail_Change()
    {
        Selected_Trail.gameObject.GetComponent<Image>().sprite = TrailsImg[2];       
        trail_selection = 2;
        Set_Locked();
    }

    public void Save_Customization()
    {
        PlayerDataCustomize SaveDataCustomize = new PlayerDataCustomize();
        SaveDataCustomize.color_no = color_selection;
        SaveDataCustomize.selected_color = selected_color;
        SaveDataCustomize.trail_no = trail_selection;
        SaveDataCustomize.selected_trail = selected_trail;

        SaveLoadCustomize.SavePlayer(SaveDataCustomize);
    }
    public void Load_Customization()
    {
        PlayerDataCustomize loadedDataCustomize = SaveLoadCustomize.LoadPlayer();
        color_selection = loadedDataCustomize.color_no;
        selected_color = loadedDataCustomize.selected_color;
        trail_selection = loadedDataCustomize.trail_no;
        selected_trail = loadedDataCustomize.selected_trail;

        Selected_Color.gameObject.GetComponent<Image>().color = Colors[color_selection].gameObject.GetComponent<Image>().color;
        Player_Idle.GetComponent<Image>().color = Colors[selected_color].gameObject.GetComponent<Image>().color;

        Selected_Trail.gameObject.GetComponent<Image>().sprite = TrailsImg[trail_selection];
        Change_Trail(selected_trail);
    }

    public void Change_Trail(int selection)
    {
        var Trail_Settings = GameObject.Find("Player_Idle").gameObject.transform.GetChild(0).gameObject;
        var Player_Settings = GameObject.Find("Player_Idle").gameObject;
        Destroy(Trail_Settings);
        Instantiate(Trails[selection].gameObject, Trail_Settings.transform.position, Quaternion.identity, Player_Settings.transform);        
    }

    public void Load_Locked()
    {
        PlayerDataLocked loadedDataLocked = SaveLoadLocked.LoadPlayer();
        
        color_0_locked = loadedDataLocked.color_0_locked;
        color_1_locked = loadedDataLocked.color_1_locked;
        color_2_locked = loadedDataLocked.color_2_locked;
        color_3_locked = loadedDataLocked.color_3_locked;
        color_4_locked = loadedDataLocked.color_4_locked;

        trail_0_locked = loadedDataLocked.trail_0_locked;
        trail_1_locked = loadedDataLocked.trail_1_locked;
        trail_2_locked = loadedDataLocked.trail_2_locked;   

        Set_Locked();        
    }

    public void Load_PlayerCoins()
    {
        PlayerDataCoins loadedDataCoins = SaveLoadCoins.LoadPlayer();
        player_coins = loadedDataCoins.coins;

        PlayerCoins_Text.text = player_coins.ToString("F0");
    }

    public void Set_Locked()
    {
        ColorCost_Text.text = color_costs.ToString();
        TrailCost_Text.text = trail_costs.ToString();
        
        if (color_selection == 0)
        {
            if(!color_0_locked)
            {
                BuyValuesColor.SetActive(false);
                if (selected_color == color_selection)
                {
                    SelectedBuyColor_Text.text = "SELECTED";
                    
                }
                else
                {
                    SelectedBuyColor_Text.text = "SELECT";
                }                
            }
            else
            {
                BuyValuesColor.SetActive(true);
                SelectedBuyColor_Text.text = "BUY";
            }
        }
        if (color_selection == 1)
        {
            if (!color_1_locked)
            {
                BuyValuesColor.SetActive(false);
                if (selected_color == color_selection)
                {
                    SelectedBuyColor_Text.text = "SELECTED";
                }
                else
                {
                    SelectedBuyColor_Text.text = "SELECT";
                }
            }
            else
            {
                BuyValuesColor.SetActive(true);
                SelectedBuyColor_Text.text = "BUY";
            }
        }
        if (color_selection == 2)
        {
            if (!color_2_locked)
            {
                BuyValuesColor.SetActive(false);
                if (selected_color == color_selection)
                {
                    SelectedBuyColor_Text.text = "SELECTED";
                }
                else
                {
                    SelectedBuyColor_Text.text = "SELECT";
                }
            }
            else
            {
                BuyValuesColor.SetActive(true);
                SelectedBuyColor_Text.text = "BUY";
            }
        }
        
        if (color_selection == 3)
        {
            if (!color_3_locked)
            {
                BuyValuesColor.SetActive(false);
                if (selected_color == color_selection)
                {
                    SelectedBuyColor_Text.text = "SELECTED";
                }
                else
                {
                    SelectedBuyColor_Text.text = "SELECT";
                }
            }
            else
            {
                BuyValuesColor.SetActive(true);
                SelectedBuyColor_Text.text = "BUY";
            }
        }        
        if (color_selection == 4)
        {
            if (!color_4_locked)
            {
                BuyValuesColor.SetActive(false);
                if (selected_color == color_selection)
                {
                    SelectedBuyColor_Text.text = "SELECTED";
                }
                else
                {
                    SelectedBuyColor_Text.text = "SELECT";
                }
            }
            else if(color_4_locked)
            {
                BuyValuesColor.SetActive(true);
                SelectedBuyColor_Text.text = "BUY";
            }
        }

        if (trail_selection == 0)
        {
            if (!trail_0_locked)
            {
                BuyValuesTrail.SetActive(false);
                if (selected_trail == trail_selection)
                {
                    SelectedBuyTrail_Text.text = "SELECTED";
                }
                else
                {
                    SelectedBuyTrail_Text.text = "SELECT";
                }
            }
            else
            {
                BuyValuesTrail.SetActive(true);
                SelectedBuyTrail_Text.text = "BUY";
            }
        }
        if (trail_selection == 1)
        {
            if (!trail_1_locked)
            {
                BuyValuesTrail.SetActive(false);
                if (selected_trail == trail_selection)
                {
                    SelectedBuyTrail_Text.text = "SELECTED";
                }
                else
                {
                    SelectedBuyTrail_Text.text = "SELECT";
                }
            }
            else
            {
                BuyValuesTrail.SetActive(true);
                SelectedBuyTrail_Text.text = "BUY";
            }
        }
        if (trail_selection == 2)
        {
            if (!trail_2_locked)
            {
                BuyValuesTrail.SetActive(false);
                if (selected_trail == trail_selection)
                {
                    SelectedBuyTrail_Text.text = "SELECTED";
                }
                else
                {
                    SelectedBuyTrail_Text.text = "SELECT";
                }
            }
            else
            {
                BuyValuesTrail.SetActive(true);
                SelectedBuyTrail_Text.text = "BUY";
            }
        }
    }
    
    public void Buy_Color()
    {
        if (color_selection == 0)
        {            
            if(color_0_locked)
            {
                if(player_coins >= color_costs)
                {
                    player_coins -= color_costs;
                    color_0_locked = false;
                    selected_color = 0;
                    BuySound.Play();
                    SelectedBuyColor_Text.text = "SELECTED";
                    Player_Idle.GetComponent<Image>().color = Colors[selected_color].gameObject.GetComponent<Image>().color;                    
                }
            }
            else
            {
                UI_Button_Sound.Play();
                selected_color = 0;
                SelectedBuyColor_Text.text = "SELECTED";
                Player_Idle.GetComponent<Image>().color = Colors[selected_color].gameObject.GetComponent<Image>().color;
            }
            Save_Customization();
            SaveCoins();
        }
        if (color_selection == 1)
        {
            if (color_1_locked)
            {
                if (player_coins >= color_costs)
                {                    
                    player_coins -= color_costs;
                    color_1_locked = false;
                    selected_color = 1;
                    BuySound.Play();
                    SelectedBuyColor_Text.text = "SELECTED";
                    Player_Idle.GetComponent<Image>().color = Colors[selected_color].gameObject.GetComponent<Image>().color;                   
                }
            }
            else
            {
                UI_Button_Sound.Play();
                selected_color = 1;
                SelectedBuyColor_Text.text = "SELECTED";
                Player_Idle.GetComponent<Image>().color = Colors[selected_color].gameObject.GetComponent<Image>().color;
            }
            Save_Customization();
            SaveCoins();
        }
        if (color_selection == 2)
        {
            if (color_2_locked)
            {
                if (player_coins >= color_costs)
                {
                    player_coins -= color_costs;
                    color_2_locked = false;
                    selected_color = 2;
                    BuySound.Play();
                    SelectedBuyColor_Text.text = "SELECTED";
                    Player_Idle.GetComponent<Image>().color = Colors[selected_color].gameObject.GetComponent<Image>().color;                    
                }
            }
            else
            {                       
                UI_Button_Sound.Play();
                selected_color = 2;
                SelectedBuyColor_Text.text = "SELECTED";
                Player_Idle.GetComponent<Image>().color = Colors[selected_color].gameObject.GetComponent<Image>().color; 
            }
            Save_Customization();
            SaveCoins();
        }
        if (color_selection == 3)
        {
            if (color_3_locked)
            {
                if (player_coins >= color_costs)
                {
                    player_coins -= color_costs;
                    color_3_locked = false;
                    selected_color = 3;
                    BuySound.Play();
                    SelectedBuyColor_Text.text = "SELECTED";
                    Player_Idle.GetComponent<Image>().color = Colors[selected_color].gameObject.GetComponent<Image>().color;
                }
            }
            else
            {
                UI_Button_Sound.Play();
                selected_color = 3;
                SelectedBuyColor_Text.text = "SELECTED";
                Player_Idle.GetComponent<Image>().color = Colors[selected_color].gameObject.GetComponent<Image>().color;
            }
            Save_Customization();
            SaveCoins();
        }
        if (color_selection == 4)
        {
            if (color_4_locked)
            {
                if (player_coins >= color_costs)
                {
                    player_coins -= color_costs;
                    color_4_locked = false;
                    selected_color = 4;
                    BuySound.Play();
                    SelectedBuyColor_Text.text = "SELECTED";
                    Player_Idle.GetComponent<Image>().color = Colors[selected_color].gameObject.GetComponent<Image>().color;                    
                }
            }
            else
            {
                UI_Button_Sound.Play();
                selected_color = 4;
                SelectedBuyColor_Text.text = "SELECTED";
                Player_Idle.GetComponent<Image>().color = Colors[selected_color].gameObject.GetComponent<Image>().color;
            }
            Save_Customization();
            SaveCoins();
        }

        SaveLocked();
    }

    public void Buy_Trail()
    {
        if (trail_selection == 0)
        {
            if (trail_0_locked)
            {
                if (player_coins >= trail_costs)
                {
                    player_coins -= trail_costs;
                    trail_0_locked = false;
                    selected_trail = 0;
                    BuySound.Play();
                    SelectedBuyTrail_Text.text = "SELECTED";
                    Change_Trail(selected_trail);
                }
            }
            else
            {
                UI_Button_Sound.Play();
                selected_trail = 0;
                SelectedBuyTrail_Text.text = "SELECTED";
                Change_Trail(selected_trail);
            }
            Save_Customization();
            SaveCoins();
        }
        if (trail_selection == 1)
        {
            if (trail_1_locked)
            {
                if (player_coins >= trail_costs)
                {
                    player_coins -= trail_costs;
                    trail_1_locked = false;
                    selected_trail = 1;
                    BuySound.Play();
                    SelectedBuyTrail_Text.text = "SELECTED";
                    Change_Trail(selected_trail);
                }
            }
            else
            {
                UI_Button_Sound.Play();
                selected_trail = 1;
                SelectedBuyTrail_Text.text = "SELECTED";
                Change_Trail(selected_trail);
            }
            Save_Customization();
            SaveCoins();
        }
        if (trail_selection == 2)
        {
            if (trail_2_locked)
            {
                if (player_coins >= trail_costs)
                {
                    player_coins -= trail_costs;
                    trail_2_locked = false;
                    selected_trail = 2;
                    BuySound.Play();
                    SelectedBuyTrail_Text.text = "SELECTED";
                    Change_Trail(selected_trail);
                }
            }
            else
            {
                UI_Button_Sound.Play();
                selected_trail = 2;
                SelectedBuyTrail_Text.text = "SELECTED";
                Change_Trail(selected_trail);
            }
            Save_Customization();
            SaveCoins();
        }

        SaveLocked();
    }

    public void SaveLocked()
    {
        PlayerDataLocked SaveDataLocked = new PlayerDataLocked();
        SaveDataLocked.color_0_locked = color_0_locked;
        SaveDataLocked.color_1_locked = color_1_locked;
        SaveDataLocked.color_2_locked = color_2_locked;
        SaveDataLocked.color_3_locked = color_3_locked;
        SaveDataLocked.color_4_locked = color_4_locked;

        SaveDataLocked.trail_0_locked = trail_0_locked;
        SaveDataLocked.trail_1_locked = trail_1_locked;
        SaveDataLocked.trail_2_locked = trail_2_locked;

        SaveLoadLocked.SavePlayer(SaveDataLocked);

        Load_Locked();
    }

    public void SaveCoins()
    {
        PlayerDataCoins SaveDataCoins = new PlayerDataCoins();
        SaveDataCoins.coins = player_coins;
        SaveLoadCoins.SavePlayer(SaveDataCoins);

        Load_PlayerCoins();
    }

    public void Toggle_Settings()
    {
        UI_Button_Sound.Play();
        show_settings = !show_settings;
        Settings_Panel.SetActive(show_settings);
    }

    public void Music_Mute()
    {
        Music_Icon.GetComponent<Image>().sprite = Mute_Music;
        music_volume = 0;

        m_vol_1.GetComponent<Image>().sprite = music_vol_deactive;
        m_vol_2.GetComponent<Image>().sprite = music_vol_deactive;
        m_vol_3.GetComponent<Image>().sprite = music_vol_deactive;
        m_vol_4.GetComponent<Image>().sprite = music_vol_deactive;
        m_vol_5.GetComponent<Image>().sprite = music_vol_deactive;

        Save_Volumes();
    }
    public void M_Vol_1()
    {
        Music_Icon.GetComponent<Image>().sprite = Unmute_Music;
        music_volume = 1;

        m_vol_1.GetComponent<Image>().sprite = music_vol_active;
        m_vol_2.GetComponent<Image>().sprite = music_vol_deactive;
        m_vol_3.GetComponent<Image>().sprite = music_vol_deactive;
        m_vol_4.GetComponent<Image>().sprite = music_vol_deactive;
        m_vol_5.GetComponent<Image>().sprite = music_vol_deactive;

        Save_Volumes();
    }
    public void M_Vol_2()
    {
        Music_Icon.GetComponent<Image>().sprite = Unmute_Music;
        music_volume = 2;

        m_vol_1.GetComponent<Image>().sprite = music_vol_active;
        m_vol_2.GetComponent<Image>().sprite = music_vol_active;
        m_vol_3.GetComponent<Image>().sprite = music_vol_deactive;
        m_vol_4.GetComponent<Image>().sprite = music_vol_deactive;
        m_vol_5.GetComponent<Image>().sprite = music_vol_deactive;

        Save_Volumes();
    }
    public void M_Vol_3()
    {
        Music_Icon.GetComponent<Image>().sprite = Unmute_Music;
        music_volume = 3;

        m_vol_1.GetComponent<Image>().sprite = music_vol_active;
        m_vol_2.GetComponent<Image>().sprite = music_vol_active;
        m_vol_3.GetComponent<Image>().sprite = music_vol_active;
        m_vol_4.GetComponent<Image>().sprite = music_vol_deactive;
        m_vol_5.GetComponent<Image>().sprite = music_vol_deactive;

        Save_Volumes();
    }
    public void M_Vol_4()
    {
        Music_Icon.GetComponent<Image>().sprite = Unmute_Music;
        music_volume = 4;

        m_vol_1.GetComponent<Image>().sprite = music_vol_active;
        m_vol_2.GetComponent<Image>().sprite = music_vol_active;
        m_vol_3.GetComponent<Image>().sprite = music_vol_active;
        m_vol_4.GetComponent<Image>().sprite = music_vol_active;
        m_vol_5.GetComponent<Image>().sprite = music_vol_deactive;

        Save_Volumes();
    }
    public void M_Vol_5()
    {
        Music_Icon.GetComponent<Image>().sprite = Unmute_Music;
        music_volume = 5;

        m_vol_1.GetComponent<Image>().sprite = music_vol_active;
        m_vol_2.GetComponent<Image>().sprite = music_vol_active;
        m_vol_3.GetComponent<Image>().sprite = music_vol_active;
        m_vol_4.GetComponent<Image>().sprite = music_vol_active;
        m_vol_5.GetComponent<Image>().sprite = music_vol_active;

        Save_Volumes();
    }
    public void Sound_Mute()
    {
        Sound_Icon.GetComponent<Image>().sprite = Mute_Sound;
        sound_volume = 0;

        s_vol_1.GetComponent<Image>().sprite = sound_vol_deactive;
        s_vol_2.GetComponent<Image>().sprite = sound_vol_deactive;
        s_vol_3.GetComponent<Image>().sprite = sound_vol_deactive;
        s_vol_4.GetComponent<Image>().sprite = sound_vol_deactive;
        s_vol_5.GetComponent<Image>().sprite = sound_vol_deactive;

        Save_Volumes();
    }
    public void S_Vol_1()
    {
        Sound_Icon.GetComponent<Image>().sprite = Unmute_Sound;
        sound_volume = 1;

        s_vol_1.GetComponent<Image>().sprite = sound_vol_active;
        s_vol_2.GetComponent<Image>().sprite = sound_vol_deactive;
        s_vol_3.GetComponent<Image>().sprite = sound_vol_deactive;
        s_vol_4.GetComponent<Image>().sprite = sound_vol_deactive;
        s_vol_5.GetComponent<Image>().sprite = sound_vol_deactive;

        Save_Volumes();
    }
    public void S_Vol_2()
    {
        Sound_Icon.GetComponent<Image>().sprite = Unmute_Sound;
        sound_volume = 2;

        s_vol_1.GetComponent<Image>().sprite = sound_vol_active;
        s_vol_2.GetComponent<Image>().sprite = sound_vol_active;
        s_vol_3.GetComponent<Image>().sprite = sound_vol_deactive;
        s_vol_4.GetComponent<Image>().sprite = sound_vol_deactive;
        s_vol_5.GetComponent<Image>().sprite = sound_vol_deactive;

        Save_Volumes();
    }
    public void S_Vol_3()
    {
        Sound_Icon.GetComponent<Image>().sprite = Unmute_Sound;
        sound_volume = 3;

        s_vol_1.GetComponent<Image>().sprite = sound_vol_active;
        s_vol_2.GetComponent<Image>().sprite = sound_vol_active;
        s_vol_3.GetComponent<Image>().sprite = sound_vol_active;
        s_vol_4.GetComponent<Image>().sprite = sound_vol_deactive;
        s_vol_5.GetComponent<Image>().sprite = sound_vol_deactive;

        Save_Volumes();
    }
    public void S_Vol_4()
    {
        Sound_Icon.GetComponent<Image>().sprite = Unmute_Sound;
        sound_volume = 4;

        s_vol_1.GetComponent<Image>().sprite = sound_vol_active;
        s_vol_2.GetComponent<Image>().sprite = sound_vol_active;
        s_vol_3.GetComponent<Image>().sprite = sound_vol_active;
        s_vol_4.GetComponent<Image>().sprite = sound_vol_active;
        s_vol_5.GetComponent<Image>().sprite = sound_vol_deactive;

        Save_Volumes();
    }
    public void S_Vol_5()
    {
        Sound_Icon.GetComponent<Image>().sprite = Unmute_Sound;
        sound_volume = 5;

        s_vol_1.GetComponent<Image>().sprite = sound_vol_active;
        s_vol_2.GetComponent<Image>().sprite = sound_vol_active;
        s_vol_3.GetComponent<Image>().sprite = sound_vol_active;
        s_vol_4.GetComponent<Image>().sprite = sound_vol_active;
        s_vol_5.GetComponent<Image>().sprite = sound_vol_active;

        Save_Volumes();
    }

    public void Save_Volumes()
    {
        PlayerDataVolumes SaveDataVolumes = new PlayerDataVolumes();
        SaveDataVolumes.m_volume = music_volume;
        SaveDataVolumes.s_volume = sound_volume;
        SaveLoadVolumes.SavePlayer(SaveDataVolumes);

        Load_Volumes();
    }

    public void Load_Volumes()
    {
        PlayerDataVolumes loadedDataVolumes = SaveLoadVolumes.LoadPlayer();
        music_volume = loadedDataVolumes.m_volume;
        sound_volume = loadedDataVolumes.s_volume;

        Set_Volume_Settings();
        Change_Volume_Settings();
    }

    public void Set_Volume_Settings()
    {
        if(music_volume == 0)
        {
            Menu_Sound.volume = 0;
        }
        if (music_volume == 1)
        {
            Menu_Sound.volume = 0.08f;
        }
        if (music_volume == 2)
        {
            Menu_Sound.volume = 0.16f;
        }
        if (music_volume == 3)
        {
            Menu_Sound.volume = 0.24f;
        }
        if (music_volume == 4)
        {
            Menu_Sound.volume = 0.32f;
        }
        if (music_volume == 5)
        {
            Menu_Sound.volume = 0.4f;
        }

        if(sound_volume == 0)
        {
            UI_Button_Sound.volume = 0f;
            BuySound.volume = 0f;
        }
        if (sound_volume == 1)
        {
            UI_Button_Sound.volume = 0.2f;
            BuySound.volume = 0.2f;
        }
        if (sound_volume == 2)
        {
            UI_Button_Sound.volume = 0.4f;
            BuySound.volume = 0.4f;
        }
        if (sound_volume == 3)
        {
            UI_Button_Sound.volume = 0.6f;
            BuySound.volume = 0.6f;
        }
        if (sound_volume == 4)
        {
            UI_Button_Sound.volume = 0.8f;
            BuySound.volume = 0.8f;
        }
        if (sound_volume == 5)
        {
            UI_Button_Sound.volume = 1f;
            BuySound.volume = 1f;
        }
    }

    public void Change_Volume_Settings()
    {
        if(music_volume == 0)
        {
            Music_Icon.GetComponent<Image>().sprite = Mute_Music;

            m_vol_1.GetComponent<Image>().sprite = music_vol_deactive;
            m_vol_2.GetComponent<Image>().sprite = music_vol_deactive;
            m_vol_3.GetComponent<Image>().sprite = music_vol_deactive;
            m_vol_4.GetComponent<Image>().sprite = music_vol_deactive;
            m_vol_5.GetComponent<Image>().sprite = music_vol_deactive;
        }
        if (music_volume == 1)
        {
            Music_Icon.GetComponent<Image>().sprite = Unmute_Music;

            m_vol_1.GetComponent<Image>().sprite = music_vol_active;
            m_vol_2.GetComponent<Image>().sprite = music_vol_deactive;
            m_vol_3.GetComponent<Image>().sprite = music_vol_deactive;
            m_vol_4.GetComponent<Image>().sprite = music_vol_deactive;
            m_vol_5.GetComponent<Image>().sprite = music_vol_deactive;
        }
        if (music_volume == 2)
        {
            Music_Icon.GetComponent<Image>().sprite = Unmute_Music;

            m_vol_1.GetComponent<Image>().sprite = music_vol_active;
            m_vol_2.GetComponent<Image>().sprite = music_vol_active;
            m_vol_3.GetComponent<Image>().sprite = music_vol_deactive;
            m_vol_4.GetComponent<Image>().sprite = music_vol_deactive;
            m_vol_5.GetComponent<Image>().sprite = music_vol_deactive;
        }
        if (music_volume == 3)
        {
            Music_Icon.GetComponent<Image>().sprite = Unmute_Music;

            m_vol_1.GetComponent<Image>().sprite = music_vol_active;
            m_vol_2.GetComponent<Image>().sprite = music_vol_active;
            m_vol_3.GetComponent<Image>().sprite = music_vol_active;
            m_vol_4.GetComponent<Image>().sprite = music_vol_deactive;
            m_vol_5.GetComponent<Image>().sprite = music_vol_deactive;
        }
        if (music_volume == 4)
        {
            Music_Icon.GetComponent<Image>().sprite = Unmute_Music;

            m_vol_1.GetComponent<Image>().sprite = music_vol_active;
            m_vol_2.GetComponent<Image>().sprite = music_vol_active;
            m_vol_3.GetComponent<Image>().sprite = music_vol_active;
            m_vol_4.GetComponent<Image>().sprite = music_vol_active;
            m_vol_5.GetComponent<Image>().sprite = music_vol_deactive;
        }
        if (music_volume == 5)
        {
            Music_Icon.GetComponent<Image>().sprite = Unmute_Music;

            m_vol_1.GetComponent<Image>().sprite = music_vol_active;
            m_vol_2.GetComponent<Image>().sprite = music_vol_active;
            m_vol_3.GetComponent<Image>().sprite = music_vol_active;
            m_vol_4.GetComponent<Image>().sprite = music_vol_active;
            m_vol_5.GetComponent<Image>().sprite = music_vol_active;
        }
        if (sound_volume == 0)
        {
            Sound_Icon.GetComponent<Image>().sprite = Mute_Sound;

            s_vol_1.GetComponent<Image>().sprite = sound_vol_deactive;
            s_vol_2.GetComponent<Image>().sprite = sound_vol_deactive;
            s_vol_3.GetComponent<Image>().sprite = sound_vol_deactive;
            s_vol_4.GetComponent<Image>().sprite = sound_vol_deactive;
            s_vol_5.GetComponent<Image>().sprite = sound_vol_deactive;
        }
        if (sound_volume == 1)
        {
            Sound_Icon.GetComponent<Image>().sprite = Unmute_Sound;

            s_vol_1.GetComponent<Image>().sprite = sound_vol_active;
            s_vol_2.GetComponent<Image>().sprite = sound_vol_deactive;
            s_vol_3.GetComponent<Image>().sprite = sound_vol_deactive;
            s_vol_4.GetComponent<Image>().sprite = sound_vol_deactive;
            s_vol_5.GetComponent<Image>().sprite = sound_vol_deactive;
        }
        if (sound_volume == 2)
        {
            Sound_Icon.GetComponent<Image>().sprite = Unmute_Sound;

            s_vol_1.GetComponent<Image>().sprite = sound_vol_active;
            s_vol_2.GetComponent<Image>().sprite = sound_vol_active;
            s_vol_3.GetComponent<Image>().sprite = sound_vol_deactive;
            s_vol_4.GetComponent<Image>().sprite = sound_vol_deactive;
            s_vol_5.GetComponent<Image>().sprite = sound_vol_deactive;
        }
        if (sound_volume == 3)
        {
            Sound_Icon.GetComponent<Image>().sprite = Unmute_Sound;

            s_vol_1.GetComponent<Image>().sprite = sound_vol_active;
            s_vol_2.GetComponent<Image>().sprite = sound_vol_active;
            s_vol_3.GetComponent<Image>().sprite = sound_vol_active;
            s_vol_4.GetComponent<Image>().sprite = sound_vol_deactive;
            s_vol_5.GetComponent<Image>().sprite = sound_vol_deactive;
        }
        if (sound_volume == 4)
        {
            Sound_Icon.GetComponent<Image>().sprite = Unmute_Sound;

            s_vol_1.GetComponent<Image>().sprite = sound_vol_active;
            s_vol_2.GetComponent<Image>().sprite = sound_vol_active;
            s_vol_3.GetComponent<Image>().sprite = sound_vol_active;
            s_vol_4.GetComponent<Image>().sprite = sound_vol_active;
            s_vol_5.GetComponent<Image>().sprite = sound_vol_deactive;
        }
        if (sound_volume == 5)
        {
            Sound_Icon.GetComponent<Image>().sprite = Unmute_Sound;

            s_vol_1.GetComponent<Image>().sprite = sound_vol_active;
            s_vol_2.GetComponent<Image>().sprite = sound_vol_active;
            s_vol_3.GetComponent<Image>().sprite = sound_vol_active;
            s_vol_4.GetComponent<Image>().sprite = sound_vol_active;
            s_vol_5.GetComponent<Image>().sprite = sound_vol_active;
        }
    }

    public void Set_Quality_Low()
    {
        quality_selection = 0;
        Save_Quality();
    }

    public void Set_Quality_High()
    {
        quality_selection = 1;
        Save_Quality();
    }

    public void Save_Quality()
    {
        PlayerDataQuality SaveDataQuality = new PlayerDataQuality();
        SaveDataQuality.quality_selection = quality_selection;
        SaveLoadQuality.SavePlayer(SaveDataQuality);

        Load_QualitySettings();
    }

    public void Load_QualitySettings()
    {
        PlayerDataQuality loadedDataQuality = SaveLoadQuality.LoadPlayer();
        quality_selection = loadedDataQuality.quality_selection;

        if(quality_selection == 0)
        {
            Quality_Low.gameObject.GetComponent<Image>().sprite = quality_selected;
            Quality_High.gameObject.GetComponent<Image>().sprite = quality_unselected;
        }
        else if (quality_selection == 1)
        {
            Quality_Low.gameObject.GetComponent<Image>().sprite = quality_unselected;
            Quality_High.gameObject.GetComponent<Image>().sprite = quality_selected;
        }
    }

    public void Load_Highscore()
    {
        PlayerDataHighscore loadedDataHighscore = SaveLoadHighscore.LoadPlayer();
        highscore = loadedDataHighscore.highscore;
        Highscore.text = highscore.ToString("F0");
    }

    public void RESET_ALL()
    {
        PlayerDataCustomize SaveDataCustomize = new PlayerDataCustomize();
        SaveDataCustomize.color_no = 0;
        SaveDataCustomize.selected_color = 0;
        SaveDataCustomize.trail_no = 0;
        SaveDataCustomize.selected_trail = 0;
        SaveLoadCustomize.SavePlayer(SaveDataCustomize);

        Player_Idle.GetComponent<Image>().color = Colors[0].gameObject.GetComponent<Image>().color;

        Change_Trail(0);

        PlayerDataLocked SaveDataLocked = new PlayerDataLocked();
        SaveDataLocked.color_0_locked = false;
        SaveDataLocked.color_1_locked = true;
        SaveDataLocked.color_2_locked = true;
        SaveDataLocked.color_3_locked = true;
        SaveDataLocked.color_4_locked = true;

        SaveDataLocked.trail_0_locked = false;
        SaveDataLocked.trail_1_locked = true;
        SaveDataLocked.trail_2_locked = true;
        SaveLoadLocked.SavePlayer(SaveDataLocked);

        PlayerDataCoins SaveDataCoins = new PlayerDataCoins();
        SaveDataCoins.coins = 0;
        SaveLoadCoins.SavePlayer(SaveDataCoins);

        PlayerDataHighscore SaveDataHighscore = new PlayerDataHighscore();
        SaveDataHighscore.highscore = 0;
        SaveLoadHighscore.SavePlayer(SaveDataHighscore);

        PlayerDataVolumes SaveDataVolumes = new PlayerDataVolumes();
        SaveDataVolumes.m_volume = 3;
        SaveDataVolumes.s_volume = 3;
        SaveLoadVolumes.SavePlayer(SaveDataVolumes);

        PlayerDataQuality SaveDataQuality = new PlayerDataQuality();
        SaveDataQuality.quality_selection = 1;
        SaveLoadQuality.SavePlayer(SaveDataQuality);

        Load_Customization();
        Load_PlayerCoins();
        Load_Locked();
        Load_Volumes();
        Load_QualitySettings();
        Load_Highscore();
    }
}
