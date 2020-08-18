using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public AudioSource UI_Button_Source;

    public void Load_Menu()
    {
        UI_Button_Source.Play();
        Time.timeScale = 1f;
        Invoke("LoadMenuScene", 0.5f);        
    }
    public void LoadMenuScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
