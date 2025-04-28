using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputControlExtensions;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameEnd = false;                                 //½ÂÆÐ¸¦ ¶°³ª¼­, ¾îÂ¶µç ³¡³µÀ» ¶§ Ã³¸®ÇÏ´Â ½ºÅÄ½º


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isGameEnd == false)
        {
            UI_Manager.instance.ShowScreen(UI_Manager.ScreenInfo.PauseScreen);
        }
    }

    public void GameEnd(UI_Manager.ScreenInfo enumState)
    {
        UI_Manager.instance.ShowScreen(enumState);
    }
}
