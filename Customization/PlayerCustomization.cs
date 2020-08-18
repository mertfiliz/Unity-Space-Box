using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomization : MonoBehaviour
{
    public GameObject[] Colors, Trails;
    public int color_selected, trail_selected;
    public GameObject Player;

    void Start()
    {
        LoadCustomization();        
    }

    public void LoadCustomization()
    {
        PlayerDataCustomize loadedDataCustomize = SaveLoadCustomize.LoadPlayer();
        color_selected = loadedDataCustomize.selected_color;
        trail_selected = loadedDataCustomize.selected_trail;

        Player.GetComponent<Image>().color = Colors[color_selected].gameObject.GetComponent<Image>().color;
        Change_Trail(trail_selected);
    }

    public void Change_Trail(int selection)
    {
        var Trail_Settings = Player.transform.GetChild(1).gameObject;
        Destroy(Trail_Settings);
        Instantiate(Trails[selection].gameObject, Trail_Settings.transform.position, Quaternion.identity, Player.transform);
    }
}
