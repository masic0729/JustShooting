using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱��� �ν��Ͻ�
    [SerializeField] Player player;
    [SerializeField] SpriteRenderer fadeScreen;
    [SerializeField] int stage = 0;
    [SerializeField] BackGroundScroll[] backgrounds;
    //public bool isGameEnd = false;                                 //���и� ������, ��¶�� ������ �� ó���ϴ� ���Ľ�

    public enum GameState
    {
        Ready, // �غ� ���� (ī��Ʈ�ٿ� ��)
        Play,  // ���� ���� ��
        End    // ���� �����
    }

    public GameState gameState; // ���� ���� ����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // �̱��� �Ҵ�
        }
        else
        {
            Destroy(this.gameObject); // �ߺ� �ν��Ͻ� ����
        }
    }

    private void Start()
    {
        gameState = GameState.Ready; // ���� �� ���� ���´� Ready

        // ī��Ʈ �̹����� �����ϸ� ī��Ʈ�ٿ� ����
        if (UI_Manager.instance.CountImage != null)
        {
            StartCoroutine(UI_Manager.instance.CountingStart());
        }
        else
        {
            gameState = GameState.Play; // ī��Ʈ ���� �ٷ� ����
        }
    }

    void Update()
    {
        // ���� �� ESC�� ������ �Ͻ����� ȭ�� ǥ��
        if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.Play)
        {
            UI_Manager.instance.ShowScreen(UI_Manager.ScreenInfo.Pause);
        }
    }

    // ���� ���� ó��
    public void GameEnd(UI_Manager.ScreenInfo enumState)
    {

        if (gameState != GameState.End)
        {
            //isGameEnd = true;
            gameState = GameState.End;
            StartCoroutine(EndStart(enumState)); // ���� ȭ�� ��ȯ
        }
    }

    public void PlayerWinReAction()
    {
        player.anim.SetTrigger("Win");
    }

    public void StageUp()
    {
        //�̰��� ��� ��ȯ �� ȭ�� ���̵� �ƿ�/���̵� ��
        StartCoroutine(FadeInOutAction());
    }

    public int GetStage()
    {
        return stage;
    }

    // ���� ���� (1�� ��� �� ȭ�� ��ȯ + ����� ����)
    IEnumerator EndStart(UI_Manager.ScreenInfo enumState)
    {
        yield return new WaitForSeconds(1f);
        UI_Manager.instance.ShowScreen(enumState); // ���� ȭ�� ���
        //isGameEnd = true;
        gameState = GameState.End;
        AudioManager.Instance.PlayBGM("Soft1"); // �ε巯�� ����� ��ȯ
    }
    
    IEnumerator FadeInOutAction()
    {
        for (int i = 0; i <= 100; i++)
        {
            fadeScreen.color = new Vector4(0, 0, 0, i /100f);
            yield return new WaitForSeconds(Time.deltaTime);

        }/*
        fadeScreen.color = new Vector4(0, 0, 0, 1f);*/

        stage++;

        //��� ��ȯ. ���� ���ҽ� ���� �� �����ϸ� ��
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetBackgroundPartByStage();
        }

        //�̰����������� ��ȯ

        for (int i = 100; i >= 0; i--)
        {
            fadeScreen.color = new Vector4(0, 0, 0, i / 100f);
            yield return new WaitForSeconds(Time.deltaTime);

        }
    }
}
