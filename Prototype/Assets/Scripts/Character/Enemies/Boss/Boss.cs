using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    private GameObject bossHpBar;
    public GameObject root;
    //public int stateIndex = 0;
    protected const float attackEndStopTime = 3f;
    public string bossName;

    protected override void Start()
    {
        base.Start();   
    }

    protected override void Update()
    {
        base.Update();
        enemyState.Update();

    }

    protected override void Init()
    {
        base.Init();
        SetIsBoss(true);
        OnCharacterDamaged += PrintHp;
        OnCharacterDeath += HideHpBar;
        enemyState = new StateMachine();
        ChangeState(new BossSpawnState(this));
        root = gameObject.transform.GetChild(0).gameObject;
        Debug.Log(root.name);
    }

    private void OnEnable()
    {
        //������ ���������� �����Ǹ� ���̺� ��ü�� ���߰� �ȴ�.
        StopWave();
        ShowBossHp();
    }

    void PrintHp()
    {
        if (bossHpBar == null)
            return;

        float ratio = GetHp() / GetMaxHp();
        bossHpBar.GetComponent<Image>().fillAmount = ratio;
    }

    void HideHpBar()
    {
        bossHpBar.GetComponent<Image>().fillAmount = 1f;

        UI_Manager.instance.HideBossHp();
    }

    void ShowBossHp()
    {
        UI_Manager.instance.ShowBossHp(bossName);
        bossHpBar = GameObject.Find("Boss_HpBar");
    }

    protected void RestartWave()
    {
        Debug.Log("���̺� �簳");
        SpawnManager.instance.SetIsBossSpawn(false);
    }

    protected void StopWave()
    {
        Debug.Log("���̺갡 ���ߴ� ���");
        SpawnManager.instance.SetIsBossSpawn(true);
    }

    

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}