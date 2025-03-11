using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public PlayerBulletData[] commonBulletDatas; //일반 총알 데이터
    public Dictionary<string, PlayerBulletData> commonBullets; //딕셔너리로 정의할 것
    float power, maxPower = 100; //스킬을 사용하기 위한 값. 최대 100까지 모을 수 있다.
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

        //딕셔너리화한 이후 현재 무기상태를 초기화한다
        commonBullets = new Dictionary<string, PlayerBulletData>(); 
        for(int i = 0; i < commonBulletDatas.Length; i++)
        {
            commonBullets[commonBulletDatas[i].weaponName] = commonBulletDatas[i];
        }
        SetCurrentCommonBulletData(CurrentPlayerBullet.Wind); //현재 무기 초기화

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

            //PoolManager.Instance.Pools["TestSkillBullet"].Get();
            SetCurrentCommonBulletData(CurrentPlayerBullet.Wind);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {

            //PoolManager.Instance.Pools["TestSkillBullet"].Get();
            SetCurrentCommonBulletData(CurrentPlayerBullet.Iced);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {

            //PoolManager.Instance.Pools["TestSkillBullet"].Get();
            SetCurrentCommonBulletData(CurrentPlayerBullet.Fire);
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

        //현재 무기에 따른 능력치 적용 확인용 텍스트 출력
        text.text = "sprite : " + attackStats.sprite + "\nanimCtrl : " + attackStats.animCtrl + "\ndamage : " +
            attackStats.damage + "\nmoveSpeed : " + attackStats.moveSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //적이나 적의 공격을 받은 경우에 데미지 처리
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "EnemyAttack")
        {
            Character instanceEnemyInfo = collision.GetComponent<Character>();
            
            
        }
    }
}
