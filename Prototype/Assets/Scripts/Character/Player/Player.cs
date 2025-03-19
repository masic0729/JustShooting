using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;


//임시로 때려박은거
using UnityEngine.UI;

public class Player : Character
{
    public Text text; //임시용

    enum CurrentPlayerBullet //플레이어의 현재 공격타입 확인용
    {
        Wind,
        Iced,
        Fire,
        Lightning
    }
    CurrentPlayerBullet currentBullet;

    List<Coroutine> powerCoritineList;
    public PlayerPower powerStats;
    public PlayerBulletData[] commonBulletDatas; //일반 총알 데이터
    public Dictionary<string, PlayerBulletData> commonBullets; //딕셔너리로 정의할 것

    float attackSpeed = 0.5f; //플레이어의 공격 주기.기본값은 0.5이다.
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Init();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        Attack();
        TestFunctions();
    }

    protected override void Init()
    {
        base.Init();
        powerStats = gameObject.AddComponent<PlayerPower>();
        powerCoritineList = new List<Coroutine>();

        //딕셔너리화한 이후 현재 무기상태를 초기화한다
        commonBullets = new Dictionary<string, PlayerBulletData>(); 
        for(int i = 0; i < commonBulletDatas.Length; i++)
        {
            commonBullets[commonBulletDatas[i].weaponName] = commonBulletDatas[i];
        }
        SetCurrentCommonBulletData(CurrentPlayerBullet.Wind); //현재 무기 초기화

        StartCoroutine(powerStats.DefaultPowerUp());

    }

    void Attack() //공격하는 기능들의 모음
    {
        //일반 총알 공격
        if (Input.GetKeyDown(KeyCode.Space)) 
        {            
            //PoolManager.Instance.Pools["TestSkillBullet"].Get();
            ShootCommmonBullet(currentBullet);
        }
        //스킬 키. 추후 추가 예정
    }

    
    //현재는 테스트 의도로 키를 입력한다.
    void TestFunctions()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetCurrentCommonBulletData(CurrentPlayerBullet.Wind);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetCurrentCommonBulletData(CurrentPlayerBullet.Iced);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetCurrentCommonBulletData(CurrentPlayerBullet.Fire);
        }
        if(powerStats.isPowerMax)
        {

        }
    }

    /// <summary>
    /// 일반 총알공격
    /// </summary>
    /// <param name="currentState">현재 플레이어가 발사하려는 일반 총알 정보</param>
    void ShootCommmonBullet(CurrentPlayerBullet currentState)
    {
        GameObject instanceCommonBullet = PoolManager.Instance.Pools["PlayerCommonBullet"].Get(); //인스턴스화
        SetCommonBulletData(ref instanceCommonBullet); //발사체 리소스 데이터 로드
        instanceCommonBullet.transform.position = shootPositions["CommonBullet"].position; //발사체 위치 조정
    }


    /// <summary>
    /// 총알의 리소스에 요구하는 모든 데이터를 attackStats에 불러와 저장
    /// </summary>
    /// <param name="commonBullet">일반 총알 오브젝트를 가리킨다</param>
    void SetCommonBulletData(ref GameObject commonBullet)
    {
        commonBullet.tag = "Player";
        commonBullet.GetComponent<SpriteRenderer>().sprite = attackStats.sprite;
        commonBullet.GetComponent<Animator>().runtimeAnimatorController = attackStats.animCtrl;
        commonBullet.GetComponent<Projectile>().SetDamage(this.attackStats.damage);
        commonBullet.GetComponent<Projectile>().SetMoveSpeed(this.attackStats.moveSpeed);
    }

    void SetCurrentCommonBulletData(CurrentPlayerBullet bulletState)
    {
        currentBullet = bulletState;

        //현재 발사하려는 총알 종류를 변경 및 그에 맞는 능력치 적용
        attackStats.sprite = commonBullets[bulletState.ToString()].sprite;
        attackStats.animCtrl = commonBullets[bulletState.ToString()].animCtrl;
        attackStats.damage = commonBullets[bulletState.ToString()].damage;
        attackStats.moveSpeed = commonBullets[bulletState.ToString()].moveSpeed;
        powerStats.powerUpValue = commonBullets[bulletState.ToString()].powerValue;

        //현재 무기에 따른 능력치 적용 확인용 텍스트 출력
        text.text = "sprite : " + attackStats.sprite + "\nanimCtrl : " + attackStats.animCtrl + "\ndamage : " +
            attackStats.damage + "\nmoveSpeed : " + attackStats.moveSpeed + "\npower : " + powerStats.powerUpValue; ;
    }

    /// <summary>
    /// 플레이어가 파워를 모두 획득한 순간 발동되는 버프 효과
    /// </summary>
    public void PowerOn()
    {
        switch (currentBullet)
        {
            case CurrentPlayerBullet.Wind:

                break;
            case CurrentPlayerBullet.Iced:

                break;
            case CurrentPlayerBullet.Fire:

                break;
            case CurrentPlayerBullet.Lightning:
                break;
        }

    }

    void PowerSkill ()// 파워가 100이 된 이후 유저가 스킬 키를 발동할 때
    {
        switch(currentBullet)
        {
            case CurrentPlayerBullet.Wind:

                break;
            case CurrentPlayerBullet.Iced:

                break;
            case CurrentPlayerBullet.Fire:

                break;
            case CurrentPlayerBullet.Lightning:

                break;
        }
    }

    void WindSkill()
    {
        //주위의 적 총알을 흡수하고, 유도탄으로 발사한다.(흡수 기능 및 유도탄 발사)
        //GameObject instance = Instantiate(WindPuller, transform.position, transform.rotation);
        //instance.transform.parent = this.transform; //플레이어에 고정
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //적이나 적의 공격을 받은 경우에 데미지 처리
        if (collision.transform.tag == "Enemy")
        {
            //적 객체의 충돌에 의한 피해를 받는 과정
            Character instanceEnemyInfo = collision.GetComponent<Character>();
        }
    }
}