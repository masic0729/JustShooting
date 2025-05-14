using UnityEngine;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public bool isGameEnd = false;                                 //½ÂÆÐ¸¦ ¶°³ª¼­, ¾îÂ¶µç ³¡³µÀ» ¶§ Ã³¸®ÇÏ´Â ½ºÅÄ½º
    public enum GameState
    {
        Ready,
        Play,
        End
    }
    public GameState gameState;

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

    private void Start()
    {
        gameState = GameState.Ready;
        //UI_Manager.instance.
        if (UI_Manager.instance.CountImage != null)
        {
            StartCoroutine(UI_Manager.instance.CountingStart());
        }
        else
        {
            gameState = GameState.Play;
        }
    }

    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Escape) && isGameEnd == false)
        {
            
            UI_Manager.instance.ShowScreen(UI_Manager.ScreenInfo.Pause);
        }*/
        if(Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.Play)
        {
            UI_Manager.instance.ShowScreen(UI_Manager.ScreenInfo.Pause);
        }
    }

    public void GameEnd(UI_Manager.ScreenInfo enumState)
    {
        /*if(isGameEnd == false)
        {
            isGameEnd = true;
            gameState = GameState.End;
            StartCoroutine(EndStart(enumState));
        }*/
        if(gameState != GameState.End)
        {
            //isGameEnd = true;
            gameState = GameState.End;
            StartCoroutine(EndStart(enumState));
        }
    }

    IEnumerator EndStart(UI_Manager.ScreenInfo enumState)
    {
        yield return new WaitForSeconds(1f);
        UI_Manager.instance.ShowScreen(enumState);
        //isGameEnd = true;
        gameState = GameState.End;
        AudioManager.Instance.PlayBGM("Soft1");
    }

}
