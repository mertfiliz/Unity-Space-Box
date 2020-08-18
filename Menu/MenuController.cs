using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public AudioSource UI_Button_Sound;

    public void Start()
    {
        Application.targetFrameRate = 100;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    public void Load_Game()
    {
        UI_Button_Sound.Play();
        GameOverController.game_over_menu = false;
        Invoke("LoadGameScene", 0.5f);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
