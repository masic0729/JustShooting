using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//임시로 때려박은거
using UnityEngine.UI;


//플레이어 활동 범위는 x = 9.5f, y = 4.5f
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

    public PlayerPower powerStats;
    public PlayerBulletData[] commonBulletDatas; //일반 총알 데이터
    public Dictionary<string, PlayerBulletData> commonBullets; //딕셔너리로 정의할 것
    [Header("플레이어가 각 속성의 스킬을 사용하기 위한 기능들의 모임")]
    public GameObject[] buffObjectsData, skillObjectsData;
    public Dictionary<string, GameObject> buffObject,skillObjects;

    float attackTime; //플레이어의 일반 총알을 발사하려는 시간
    const float powerRestartTime = 5f;
    public int windBulletHitCount;


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
        UserInput();
        //현재 무기에 따른 능력치 적용 확인용 텍스트 출력
        text.text = "hp : " + GetHp() + "\nweapon : " + currentBullet + "\ndamage : " + this.attackStats.damage * attackStats.damageMultiplier + 
            "\nAttackDelay : " + attackDelay * attackStats.attackDelayMultify +
            "\nmoveSpeed : " + attackStats.moveSpeed + "\npower : " + powerStats.powerUpValue + 
            "\nplayerMoveSpeed : " + moveSpeed * objectMoveSpeedMultify + "\nPlayerPowerValue : " + powerStats.playerPower;
    }

    protected override void Init()
    {
        base.Init();
        //이동 구역 제한
        maxMoveX = 9.5f;
        maxMoveY = 4.5f;
        attackDelay = 0.1f;
        SetMoveSpeed(10f);
        powerStats = gameObject.GetComponent<PlayerPower>();

        //딕셔너리화한 이후 현재 무기상태 및 스킬을 초기화한다
        commonBullets = new Dictionary<string, PlayerBulletData>();
        buffObject = new Dictionary<string, GameObject>();
        skillObjects = new Dictionary<string, GameObject>();
        for (int i = 0; i < commonBulletDatas.Length; i++)
        {
            //무기 상태, 스킬을 딕셔너리
            commonBullets[commonBulletDatas[i].weaponName] = commonBulletDatas[i];
            buffObject[commonBulletDatas[i].weaponName] = buffObjectsData[i];
            skillObjects[commonBulletDatas[i].weaponName] = skillObjectsData[i];
        }
        SetCurrentCommonBulletData(CurrentPlayerBullet.Wind); //현재 무기 초기화

        StartCoroutine(powerStats.DefaultPowerUp());

        //Instantiate(skillObjects["Wind"]);

    }

    void UserInput()
    {
        MoveInput();
        Attack();
        TransWeapon();   
    }

    void MoveInput()
    {
        if(Input.GetKey(KeyCode.LeftArrow) && -maxMoveX < this.transform.position.x)
        {
            ObjectMove(Vector3.left);
        }
        if (Input.GetKey(KeyCode.RightArrow) && maxMoveX > this.transform.position.x)
        {
            ObjectMove(Vector3.right);
        }
        if (Input.GetKey(KeyCode.UpArrow) && maxMoveY > this.transform.position.y)
        {
            ObjectMove(Vector3.up);   
        }
        if (Input.GetKey(KeyCode.DownArrow) && -maxMoveY < this.transform.position.y)
        {
            ObjectMove(Vector3.down);
        }
    }

    void Attack() //공격하는 기능들의 모음
    {
        //발사하기 위한 대기 시간을 카운트 하는 행위 
        attackTime += Time.deltaTime;
        //일반 총알 공격이 준비되었다면 발사
        if (attackTime >= attackDelay * attackStats.attackDelayMultify)
        {
            ShootCommmonBullet(currentBullet);
            attackTime = 0;

        }
        //스킬 키. 추후 추가 예정

    }


    //현재는 테스트 의도로 키를 입력한다.
    void TransWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TransCurrentWeapon();
            if(powerStats.isPowerMax == true)
            {
                powerStats.playerPower = 0;
                powerStats.isPowerMax = false;
                powerStats.SetIsActivedSkill(true);
                PowerSkill();

                Invoke("PowerUpRestart", powerRestartTime);
            }
        }
    }

    void TransCurrentWeapon()
    {
        if (currentBullet == CurrentPlayerBullet.Lightning)
            currentBullet = 0;
        else
            currentBullet += 1;
        SetCurrentCommonBulletData(currentBullet);
    }

    /// <summary>
    /// 번개를 제외한 일반 총알공격
    /// </summary>
    /// <param name="currentState">현재 플레이어가 발사하려는 일반 총알 정보</param>
    void ShootCommmonBullet(CurrentPlayerBullet currentState)
    {
        int shootBulletCount;
        float rotateValue;

        //불 속성만 유일하게 5개의 탄환을 동시에 발사하여 산탄한다. 이를 고려하여 예외처리
        if (currentState == CurrentPlayerBullet.Fire)
        {
            shootBulletCount = 5;
            rotateValue = -20f;
        }
        else
        {
            shootBulletCount = 1;
            rotateValue = 0;
        }

        //총알 최종 발사
        for (int i = 1; i <= shootBulletCount; i++)
        {
            GameObject instanceCommonBullet = PoolManager.Instance.Pools["PlayerCommonBullet"].Get(); //인스턴스화
            SetCommonBulletData(ref instanceCommonBullet); //발사체 리소스 데이터 로드
            attackManage.ShootBulletRotate(ref instanceCommonBullet, shootTransform["CommonBullet"], rotateValue);
            rotateValue += 10f;
        }
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
        commonBullet.GetComponent<Projectile>().SetDamage(this.attackStats.damage * attackStats.damageMultiplier);
        commonBullet.GetComponent<Projectile>().SetMoveSpeed(this.attackStats.moveSpeed);
        commonBullet.GetComponent<PlayerCommonBullet>().bulletName = currentBullet.ToString();
        commonBullet.GetComponent<Projectile>().SetLifeTime(commonBullets[currentBullet.ToString()].lifeTime);
    }

    /// <summary>
    /// 무기의 시스템정보를 플레이어의 공격 데이터에 저장
    /// </summary>
    /// <param name="bulletState"></param>
    void SetCurrentCommonBulletData(CurrentPlayerBullet bulletState)
    {
        currentBullet = bulletState;

        //현재 발사하려는 총알 종류를 변경 및 그에 맞는 능력치 적용
        attackStats.sprite = commonBullets[bulletState.ToString()].sprite;
        attackStats.animCtrl = commonBullets[bulletState.ToString()].animCtrl;
        //attackStats.damage = commonBullets[bulletState.ToString()].damage;
        attackStats.moveSpeed = commonBullets[bulletState.ToString()].moveSpeed;
        powerStats.powerUpValue = commonBullets[bulletState.ToString()].powerValue;
        attackStats.attackDelayMultify = commonBullets[bulletState.ToString()].attackDelayMultify;
        attackStats.damageMultiplier = commonBullets[bulletState.ToString()].attackMultify;
        windBulletHitCount = 0; //바람 속성 단독 초기화
    }

    /// <summary>
    /// 플레이어가 파워를 모두 획득한 순간 발동되는 버프 효과
    /// </summary>
    public void PowerOn()
    {
        /*
        switch (currentBullet)
        {
            case CurrentPlayerBullet.Wind:
                Instantiate(buffObject["Wind"]);
                break;
            case CurrentPlayerBullet.Iced:
                Instantiate(buffObject["Iced"]);
                break;
            case CurrentPlayerBullet.Fire:
                Instantiate(buffObject["Fire"]);
                break;
            case CurrentPlayerBullet.Lightning:
                Debug.Log("전기 버프는 아직 없어요");
                break;
        }
        */
        Instantiate(buffObject[currentBullet.ToString()]);

    }

    /// <summary>
    /// 파워가 100이 된 이후 유저가 스킬 키를 발동할 때 실행
    /// </summary>
    void PowerSkill()
    {
        /*
        switch(currentBullet)
        {
            case CurrentPlayerBullet.Wind:
                Instantiate(skillObjects["Wind"]);
                break;
            case CurrentPlayerBullet.Iced:
                Instantiate(skillObjects["Iced"]);
                break;
            case CurrentPlayerBullet.Fire:
                Instantiate(skillObjects["Fire"]);
                break;
            case CurrentPlayerBullet.Lightning:
                Debug.Log("전기 스킬은 아직 없어요");
                break;
        }
        */
        Instantiate(skillObjects[currentBullet.ToString()]);

    }

    /// <summary>
    /// 스킬 사용 후 일정 시간 지나야 파워 수급을 시작하려는 기능
    /// </summary>
    void PowerUpRestart()
    {
        powerStats.SetIsActivedSkill(false);
    }

    /// <summary>
    /// 바람스킬 코루틴을 사용하기 위한 함수 
    /// </summary>
    /// <param name="bulletInstance"></param>
    /// <param name="shootCount"></param>
    public void WindSkill(GameObject bulletInstance, int shootCount)
    {
        StartCoroutine(WindBulletShoot(bulletInstance, shootCount));
    }

    public void IcedSkill(GameObject bulletInstance)
    {
        StartCoroutine(IcedBulletShoot(bulletInstance));

    }

    /// <summary>
    /// 바람 발사체를 발사하는 코루틴
    /// </summary>
    /// <param name="windBullet">바람 총알의 정보</param>
    /// <param name="shootCount">바람 총알을 발사할 수 있는 개수</param>
    /// <returns></returns>
    IEnumerator WindBulletShoot(GameObject windBullet, int shootCount)
    {
        
        for (int i = 0; i < shootCount; i++)
        {
            GameObject instance = Instantiate(windBullet, shootTransform["Skill"].position,
                shootTransform["Skill"].rotation);
            
            /*
            if(instance.GetComponent<PlayerWindBullet>()) //바람 속성이라면, 첫 생성 시 연출을 위한 회전 값을 부여
            {
                float rotateRandom = Random.Range(30f, 60f);
                int randValue = Random.Range(0, 2);
                rotateRandom = randValue == 1 ? rotateRandom *= -1 : rotateRandom;
                instance.transform.Rotate(0, 0, rotateRandom);
            }
            */

            //일단 플레이어가 발사하는 주체이므로, 태그 값은 플레이어로 고정
            if (instance != null)
            {
                instance.transform.tag = "Player";
                instance.GetComponent<Projectile>().SetMoveSpeed(attackStats.moveSpeed * 2f);
                instance.GetComponent<Projectile>().SetDamage(this.attackStats.damage * attackStats.damageMultiplier * 0.8f);
            }
            yield return new WaitForSeconds(attackDelay * attackStats.attackDelayMultify);
        }
    }

    /// <summary>
    /// 추후 바람 총알 발사 코루틴과 합쳐낼 예정.
    /// </summary>
    /// <param name="icedBullet"></param>
    /// <returns></returns>
    IEnumerator IcedBulletShoot(GameObject icedBullet)
    {
        
        for (int i = 0; i < 15; i++)
        {
            GameObject instance = Instantiate(icedBullet, shootTransform["Skill"].position,
                shootTransform["Skill"].rotation);

            //일단 플레이어가 발사하는 주체이므로, 태그 값은 플레이어로 고정
            if (instance != null)
            {
                instance.transform.tag = "Player";
                instance.GetComponent<Projectile>().SetMoveSpeed(attackStats.moveSpeed * 2f);
                instance.GetComponent<Projectile>().SetDamage(this.attackStats.damage * attackStats.damageMultiplier * 0.7f);
            }
            yield return new WaitForSeconds(attackDelay * attackStats.attackDelayMultify);

        }
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

    public string GetPlayerWeaponName()
    {
        return currentBullet.ToString();
    }
         
}