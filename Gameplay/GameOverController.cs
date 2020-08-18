using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public AudioSource UI_Button_Sound;
    public static GameObject End_Panel, Revive_Panel, GameOver_Panel;
    public static GameOverController instance;
    public static GameObject Checkpoint_Zone;
    public static GameObject ReviveSlider;
    public static float restart_timer;
    public static bool game_over = false, game_over_menu = false, revive_yes = false;
    public Text Score_Text, Score_UI, Coin_UI;
    private bool restart_enabled;
    private bool internet_connection;

    public int sound_volume;
    public int restart_ct = 0;
    public int current_coins = 0;

    void Awake()
    {
        instance = this;
        End_Panel = GameObject.Find("End_Panel");
        Revive_Panel = GameObject.Find("Revive_Panel");
        GameOver_Panel = GameObject.Find("GameOver_Panel");
        ReviveSlider = GameObject.Find("ReviveSlider");

        End_Panel.SetActive(false);
        GameOver_Panel.SetActive(false);

        internet_connection = false;
        restart_enabled = true;
    }

    void Start()
    {
        Load_Volumes();
    }

    public static void GameOver()
    {       
        AdManager.AdManage.Show_Banner();

        if(instance.restart_enabled && instance.internet_connection && AdManager.AdManage.rewardedAd.IsLoaded())
        {
            End_Panel.SetActive(true);
            GameOver_Panel.SetActive(false);
            Revive_Panel.SetActive(true);

            game_over_menu = true;
            game_over = true;
            restart_timer = 5f;

            instance.StartCoroutine(instance.RestartTimerCountdown());
        }
        else
        {
            game_over_menu = true;
            game_over = true;
            End_Panel.SetActive(true);
            instance.GameEnded();
        }
       
        AdManager.AdManage.Request_Interstitial();
        AdManager.AdManage.Request_Reward();
    }

    public IEnumerator RestartTimerCountdown()
    {
        restart_enabled = false;

        while (restart_timer >= 0f)
        {
            yield return new WaitForSeconds(0.01f);
            restart_timer -= 0.01f;
        }

        if(game_over && !revive_yes)
        {
            GameEnded();
        }
        
        yield return null;
        game_over = false;
        revive_yes = false;
    }

    public void GameEnded()
    {
        AdManager.AdManage.bannerView.Destroy();
       
        Revive_Panel.SetActive(false);
        GameOver_Panel.SetActive(true);
        game_over = false;
        //game_over_menu = true;
        Calculate_Coin();
        Save_Coin();
        Score_UI.text = Movement.ins.score.ToString("F0");
        Coin_UI.text = current_coins.ToString("F0");

        PlayerDataHighscore loadedDataHighscore = SaveLoadHighscore.LoadPlayer();
        var temp_highscore = loadedDataHighscore.highscore;

        if(Movement.ins.score > temp_highscore)
        {
            Score_Text.text = "NEW HIGHSCORE";
            PlayerDataHighscore SaveDataHighscore = new PlayerDataHighscore();
            SaveDataHighscore.highscore = Movement.ins.score;
            SaveLoadHighscore.SavePlayer(SaveDataHighscore);
        }
        else
        {
            Score_Text.text = "HIGHSCORE";
        }
    }

    public void Calculate_Coin()
    {
        current_coins = Movement.ins.score / 1000;
    }

    public void Save_Coin()
    {
        PlayerDataCoins loadedDataCoins= SaveLoadCoins.LoadPlayer();
        int loaded_coins = loadedDataCoins.coins;

        int total_coins = loaded_coins + current_coins;

        PlayerDataCoins SaveDataCoins = new PlayerDataCoins();
        SaveDataCoins.coins = total_coins;
        SaveLoadCoins.SavePlayer(SaveDataCoins);
    }

    void Update()
    {        
        if (game_over)
        {
            ReviveSlider.GetComponent<Slider>().value = restart_timer;
        }
        
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            internet_connection = false;
        }
        else
        {
            internet_connection = true;
        }
    }

    public void Revive_YES()
    {
        game_over_menu = false;
        AdManager.AdManage.bannerView.Destroy();
        AdManager.AdManage.Show_RewardedAd();

        restart_enabled = false;
        UI_Button_Sound.Play();
        revive_yes = true;
        game_over_menu = false;
    }

    public void Revive_NO()
    {
        UI_Button_Sound.Play();
        GameEnded();
    }

    public void Revive_Success()
    {
        game_over_menu = false;
        AdManager.AdManage.bannerView.Destroy();
        End_Panel.SetActive(false);
        GameOver_Panel.SetActive(false);
        Revive_Panel.SetActive(false);

        Movement.ins.GameSound.Play();
        Movement.ins.ResetPlayer();
        game_over_menu = false;
    }

    public void Revive_Failed()
    {        
        AdManager.AdManage.Show_Banner();
        GameEnded();
    }

    public void Restart_Game()
    {
        AdManager.AdManage.bannerView.Destroy();        
        UI_Button_Sound.Play();
        game_over_menu = false;
        Invoke("LoadGameScene", 0.5f);        
        restart_ct++;

        if(restart_ct % 3 == 0)
        {
            AdManager.AdManage.Show_RewardedAd();
        }
        else
        {
            AdManager.AdManage.Show_Interstitial();
        }       

        AdManager.AdManage.Request_Banner();
        AdManager.AdManage.Request_Interstitial();
        AdManager.AdManage.Request_Reward();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void Load_Volumes()
    {
        PlayerDataVolumes loadedDataVolumes = SaveLoadVolumes.LoadPlayer();
        sound_volume = loadedDataVolumes.s_volume;

        Set_Volume_Settings();
    }

    public void Set_Volume_Settings()
    {        
        if (sound_volume == 0)
        {
            UI_Button_Sound.volume = 0f;
        }
        if (sound_volume == 1)
        {
            UI_Button_Sound.volume = 0.2f;
        }
        if (sound_volume == 2)
        {
            UI_Button_Sound.volume = 0.4f;
        }
        if (sound_volume == 3)
        {
            UI_Button_Sound.volume = 0.6f;
        }
        if (sound_volume == 4)
        {
            UI_Button_Sound.volume = 0.8f;
        }
        if (sound_volume == 5)
        {
            UI_Button_Sound.volume = 1f;
        }
    }
}
