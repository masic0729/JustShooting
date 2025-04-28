using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public enum ScreenInfo
    {
        WinScreen,
        LoseScreen,
        PauseScreen
    }

    public static UI_Manager instance;
    [SerializeField]
    private GameObject[] GameScreens;
    Dictionary<string, GameObject> GameScreenName;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        GameScreenName = new Dictionary<string, GameObject>();
        for (int i = 0; i < GameScreens.Length; i++)
        {
            GameScreenName[GameScreens[i].name] = GameScreens[i];
        }
    }

    public void ShowScreen(ScreenInfo state)
    {
        GameScreenName[state.ToString()].SetActive(true);
        Time.timeScale = 0.0f;

    }
}
