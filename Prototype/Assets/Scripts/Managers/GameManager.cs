using System.Collections;
using UnityEngine;

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
            
            UI_Manager.instance.ShowScreen(UI_Manager.ScreenInfo.Pause);
        }
    }

    public void GameEnd(UI_Manager.ScreenInfo enumState)
    {
        if(isGameEnd == false)
        {
            isGameEnd = true;
            StartCoroutine(EndStart(enumState));
        }
    }

    IEnumerator EndStart(UI_Manager.ScreenInfo enumState)
    {
        yield return new WaitForSeconds(1f);
        UI_Manager.instance.ShowScreen(enumState);
        isGameEnd = true;
        AudioManager.Instance.PlayBGM("Soft1");
    }

}
