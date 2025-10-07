using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 보스 적 캐릭터 클래스.
/// Enemy 클래스를 상속하며 보스 관련 추가 기능 포함.
/// </summary>
public class Boss : Enemy
{
    // 보스 체력 바 오브젝트 참조
    private GameObject bossHpBar;
    // 보스 루트 오브젝트 참조
    public GameObject root;


    // 공격 종료 후 정지 시간 상수
    protected const float attackEndStopTime = 3f;
    // 보스 이름
    public string bossName;

    /// <summary>
    /// 초기화 및 부모 Start 호출
    /// </summary>
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// 매 프레임 상태 업데이트
    /// </summary>
    protected override void Update()
    {
        base.Update();
        enemyState.Update();
    }

    /// <summary>
    /// 초기 설정 및 이벤트 등록
    /// </summary>
    protected override void Init()
    {
        base.Init();
        SetIsBoss(true);
        attackData.moveSpeed = 15f;
        //enemyCol.enabled = false;
        OnDamage += PrintHp; // 데미지 입으면 체력 UI 갱신
        OnCharacterDeath += HideHpBar; // 사망 시 체력 UI 숨김
        enemyState = new StateMachine();
        ChangeState(new BossSpawnState(this));
        //root = gameObject.transform.GetChild(0).gameObject;
    }

    /// <summary>
    /// 오브젝트 활성화 시 웨이브 중지 및 보스 체력바 표시
    /// </summary>
    private void OnEnable()
    {
        StopWave();
        ShowBossHp();
    }

    /// <summary>
    /// 체력 UI 갱신
    /// </summary>
    void PrintHp()
    {
        if (bossHpBar == null)
            return;

        float ratio = GetHp() / GetMaxHp();
        bossHpBar.GetComponent<Image>().fillAmount = ratio;
    }

    /// <summary>
    /// 체력 UI 숨기기
    /// </summary>
    void HideHpBar()
    {
        bossHpBar.GetComponent<Image>().fillAmount = 1f;
        UI_Manager.instance.HideBossHp();
    }

    /// <summary>
    /// 보스 체력 UI 보이기
    /// </summary>
    void ShowBossHp()
    {
        UI_Manager.instance.ShowBossHp(bossName);
        bossHpBar = GameObject.Find("Boss_HpBar");
        bossHpBar.GetComponent<Image>().fillAmount = 1f;
    }

    /// <summary>
    /// 웨이브 재개 (보스 퇴장 시 호출)
    /// </summary>
    protected void RestartWave()
    {
        Debug.Log("웨이브 재개");
        SpawnManager.instance.SetIsBossSpawn(false);
    }

    /// <summary>
    /// 웨이브 중지 (보스 등장 시 호출)
    /// </summary>
    protected void StopWave()
    {
        Debug.Log("웨이브가 멈추는 기능");
        SpawnManager.instance.SetIsBossSpawn(true);
    }

    /// <summary>
    /// 충돌 처리, 부모 클래스 호출
    /// </summary>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
