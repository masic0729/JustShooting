using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public enum ScreenInfo
    {
        Win,
        Lose,
        Pause
    }

    public static UI_Manager instance;
    [SerializeField]
    private GameObject[] GameScreens;
    Dictionary<string, GameObject> GameScreenName;

    [Header("PlayerWeapon")]
    public GameObject[] currentWeapon;
    public GameObject[] nextWeapon;

    public GameObject[] hpIcons;
    [Header("BossUI_Info")]
    public GameObject bossHp;
    //public GameObject lightningUI;

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
        bossHp.SetActive(false);
    }

    public void ShowScreen(ScreenInfo state)
    {
        GameScreenName[state.ToString()].SetActive(true);
        Time.timeScale = 0.0f;
    }

    

    public void UpdateWeaponUI(string bulletType)
    {
        for(int i = 0; i < currentWeapon.Length;i++)
        {
            currentWeapon[i].SetActive(false);
            nextWeapon[i].SetActive(false);
        }
        //lightningUI.SetActive(false);

        switch (bulletType)
        {
            case "Wind":
                currentWeapon[0].SetActive(true);
                nextWeapon[1].SetActive(true);
                break;
            case "Iced":
                currentWeapon[1].SetActive(true);
                nextWeapon[2].SetActive(true);
                break;
            case "Fire":
                currentWeapon[2].SetActive(true);
                nextWeapon[0].SetActive(true);
                break;
            /*case "Lightning":
                lightningUI.SetActive(true);
                break;*/
        }
    }

    public void UpdatePlayerHP(float hpValue)
    {
        for(int i = 0; i < hpIcons.Length; i++)
        {
            hpIcons[i].SetActive(false);

        }
        /*for (int i = 0; i < (int)hpValue - 1; i++)
        {
            hpIcons[i].SetActive(true);
        }*/
        for (int i = 0; i < Mathf.Clamp((int)hpValue, 0, hpIcons.Length); i++)
        {
            hpIcons[i].SetActive(true);
        }
    }

    public void ShowBossHp(string bossName)
    {
        bossHp.SetActive(true);
        bossHp.GetComponentInChildren<TextMeshProUGUI>().text = bossName;
    }

    public void HideBossHp()
    {
        bossHp.SetActive(false);
    }
}
