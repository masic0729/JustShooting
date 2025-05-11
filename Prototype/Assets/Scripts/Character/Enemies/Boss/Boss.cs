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
        //보스는 공통적으로 스폰되면 웨이브 자체가 멈추게 된다.
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
        Debug.Log("웨이브 재개");
        SpawnManager.instance.SetIsBossSpawn(false);
    }

    protected void StopWave()
    {
        Debug.Log("웨이브가 멈추는 기능");
        SpawnManager.instance.SetIsBossSpawn(true);
    }

    

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}