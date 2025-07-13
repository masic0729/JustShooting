using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class UI_Manager : MonoBehaviour
{
    public enum ScreenInfo
    {
        Win,
        Lose,
        Pause
    }

    public static UI_Manager instance; // �̱��� �ν��Ͻ�

    public GameObject CountImage; // ī��Ʈ�ٿ� �̹��� �׷�
    [SerializeField]
    private GameObject[] GameScreens; // ����/�Ͻ����� ���� ȭ�� �гε�
    Dictionary<string, GameObject> GameScreenName; // �̸����� �� �гο� �����ϱ� ���� ��ųʸ�

    [Header("PlayerWeapon")]
    public GameObject[] currentWeapon; // ���� ���� UI ǥ��
    public GameObject[] nextWeapon;    // ���� ���� UI ǥ��

    public GameObject[] hpIcons; // �÷��̾� HP �����ܵ�

    [Header("BossUI_Info")]
    public GameObject bossHp; // ���� HP UI �г�
    //public GameObject lightningUI;

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

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }



    void Init()
    {
        GameScreenName = new Dictionary<string, GameObject>();
        for (int i = 0; i < GameScreens.Length; i++)
        {
            GameScreenName[GameScreens[i].name] = GameScreens[i]; // �̸��� ������� ��ųʸ�ȭ
        }
        bossHp.SetActive(false); // ���� UI�� ó���� ��Ȱ��ȭ
    }

    // ���� ���� ȭ��(Pause, Win, Lose) ���
    public void ShowScreen(ScreenInfo state)
    {
        GameScreenName[state.ToString()].SetActive(true);
        Time.timeScale = 0.0f; // ���� �Ͻ�����
    }

    // ���� ���� �� UI ǥ�� ����
    public void UpdateWeaponUI(string bulletType)
    {
        for (int i = 0; i < currentWeapon.Length; i++)
        {
            currentWeapon[i].SetActive(false);
            nextWeapon[i].SetActive(false);
        }

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

    // �÷��̾��� HP ��ġ�� ���� UI ������ Ȱ��ȭ
    public void UpdatePlayerHP(float hpValue)
    {
        for (int i = 0; i < hpIcons.Length; i++)
        {
            hpIcons[i].SetActive(false);
        }

        // ���� HP ����ŭ �������� �ٽ� Ȱ��ȭ
        for (int i = 0; i < Mathf.Clamp((int)hpValue, 0, hpIcons.Length); i++)
        {
            hpIcons[i].SetActive(true);
        }
    }

    // ���� �� ī��Ʈ�ٿ� ����
    public IEnumerator CountingStart()
    {
        Time.timeScale = 0;
        CountImage.SetActive(true);
        GameObject currentImage;

        for (int i = 0; i < CountImage.transform.childCount; i++)
        {
            if (i < CountImage.transform.childCount - 1)
            {
                AudioManager.Instance.PlaySFX("CountSound");
            }
            else
            {
                AudioManager.Instance.PlaySFX("StartSound");
            }

            currentImage = CountImage.transform.GetChild(i).gameObject;

            currentImage.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            currentImage.SetActive(false);
        }

        GameManager.instance.gameState = GameManager.GameState.Play; // ���� ����
        Time.timeScale = 1;
    }

    // ���� ü�¹� ��� �� �̸� ǥ��
    public void ShowBossHp(string bossName)
    {
        bossHp.SetActive(true);
        bossHp.GetComponentInChildren<TextMeshProUGUI>().text = bossName;
    }

    // ���� ü�¹� �����
    public void HideBossHp()
    {
        bossHp.SetActive(false);
    }
}
