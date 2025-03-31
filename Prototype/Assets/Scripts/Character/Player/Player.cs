using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ӽ÷� ����������
using UnityEngine.UI;


//�÷��̾� Ȱ�� ������ x = 9.5f, y = 4.5f
public class Player : Character
{
    public Text text; //�ӽÿ�

    enum CurrentPlayerBullet //�÷��̾��� ���� ����Ÿ�� Ȯ�ο�
    {
        Wind,
        Iced,
        Fire,
        Lightning
    }
    CurrentPlayerBullet currentBullet;

    public PlayerPower powerStats;
    public PlayerBulletData[] commonBulletDatas; //�Ϲ� �Ѿ� ������
    public Dictionary<string, PlayerBulletData> commonBullets; //��ųʸ��� ������ ��
    [Header("�÷��̾ �� �Ӽ��� ��ų�� ����ϱ� ���� ��ɵ��� ����")]
    public GameObject[] buffObjectsData, skillObjectsData;
    public Dictionary<string, GameObject> buffObject,skillObjects;

    float attackTime; //�÷��̾��� �Ϲ� �Ѿ��� �߻��Ϸ��� �ð�
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
        //���� ���⿡ ���� �ɷ�ġ ���� Ȯ�ο� �ؽ�Ʈ ���
        text.text = "hp : " + GetHp() + "\nweapon : " + currentBullet + "\ndamage : " + this.attackStats.damage * attackStats.damageMultiplier + 
            "\nAttackDelay : " + attackDelay * attackStats.attackDelayMultify +
            "\nmoveSpeed : " + attackStats.moveSpeed + "\npower : " + powerStats.powerUpValue + 
            "\nplayerMoveSpeed : " + moveSpeed * objectMoveSpeedMultify + "\nPlayerPowerValue : " + powerStats.playerPower;
    }

    protected override void Init()
    {
        base.Init();
        //�̵� ���� ����
        maxMoveX = 9.5f;
        maxMoveY = 4.5f;
        attackDelay = 0.1f;
        SetMoveSpeed(10f);
        powerStats = gameObject.GetComponent<PlayerPower>();

        //��ųʸ�ȭ�� ���� ���� ������� �� ��ų�� �ʱ�ȭ�Ѵ�
        commonBullets = new Dictionary<string, PlayerBulletData>();
        buffObject = new Dictionary<string, GameObject>();
        skillObjects = new Dictionary<string, GameObject>();
        for (int i = 0; i < commonBulletDatas.Length; i++)
        {
            //���� ����, ��ų�� ��ųʸ�
            commonBullets[commonBulletDatas[i].weaponName] = commonBulletDatas[i];
            buffObject[commonBulletDatas[i].weaponName] = buffObjectsData[i];
            skillObjects[commonBulletDatas[i].weaponName] = skillObjectsData[i];
        }
        SetCurrentCommonBulletData(CurrentPlayerBullet.Wind); //���� ���� �ʱ�ȭ

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

    void Attack() //�����ϴ� ��ɵ��� ����
    {
        //�߻��ϱ� ���� ��� �ð��� ī��Ʈ �ϴ� ���� 
        attackTime += Time.deltaTime;
        //�Ϲ� �Ѿ� ������ �غ�Ǿ��ٸ� �߻�
        if (attackTime >= attackDelay * attackStats.attackDelayMultify)
        {
            ShootCommmonBullet(currentBullet);
            attackTime = 0;

        }
        //��ų Ű. ���� �߰� ����

    }


    //����� �׽�Ʈ �ǵ��� Ű�� �Է��Ѵ�.
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
    /// ������ ������ �Ϲ� �Ѿ˰���
    /// </summary>
    /// <param name="currentState">���� �÷��̾ �߻��Ϸ��� �Ϲ� �Ѿ� ����</param>
    void ShootCommmonBullet(CurrentPlayerBullet currentState)
    {
        int shootBulletCount;
        float rotateValue;

        //�� �Ӽ��� �����ϰ� 5���� źȯ�� ���ÿ� �߻��Ͽ� ��ź�Ѵ�. �̸� ����Ͽ� ����ó��
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

        //�Ѿ� ���� �߻�
        for (int i = 1; i <= shootBulletCount; i++)
        {
            GameObject instanceCommonBullet = PoolManager.Instance.Pools["PlayerCommonBullet"].Get(); //�ν��Ͻ�ȭ
            SetCommonBulletData(ref instanceCommonBullet); //�߻�ü ���ҽ� ������ �ε�
            attackManage.ShootBulletRotate(ref instanceCommonBullet, shootTransform["CommonBullet"], rotateValue);
            rotateValue += 10f;
        }
    }


    /// <summary>
    /// �Ѿ��� ���ҽ��� �䱸�ϴ� ��� �����͸� attackStats�� �ҷ��� ����
    /// </summary>
    /// <param name="commonBullet">�Ϲ� �Ѿ� ������Ʈ�� ����Ų��</param>
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
    /// ������ �ý��������� �÷��̾��� ���� �����Ϳ� ����
    /// </summary>
    /// <param name="bulletState"></param>
    void SetCurrentCommonBulletData(CurrentPlayerBullet bulletState)
    {
        currentBullet = bulletState;

        //���� �߻��Ϸ��� �Ѿ� ������ ���� �� �׿� �´� �ɷ�ġ ����
        attackStats.sprite = commonBullets[bulletState.ToString()].sprite;
        attackStats.animCtrl = commonBullets[bulletState.ToString()].animCtrl;
        //attackStats.damage = commonBullets[bulletState.ToString()].damage;
        attackStats.moveSpeed = commonBullets[bulletState.ToString()].moveSpeed;
        powerStats.powerUpValue = commonBullets[bulletState.ToString()].powerValue;
        attackStats.attackDelayMultify = commonBullets[bulletState.ToString()].attackDelayMultify;
        attackStats.damageMultiplier = commonBullets[bulletState.ToString()].attackMultify;
        windBulletHitCount = 0; //�ٶ� �Ӽ� �ܵ� �ʱ�ȭ
    }

    /// <summary>
    /// �÷��̾ �Ŀ��� ��� ȹ���� ���� �ߵ��Ǵ� ���� ȿ��
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
                Debug.Log("���� ������ ���� �����");
                break;
        }
        */
        Instantiate(buffObject[currentBullet.ToString()]);

    }

    /// <summary>
    /// �Ŀ��� 100�� �� ���� ������ ��ų Ű�� �ߵ��� �� ����
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
                Debug.Log("���� ��ų�� ���� �����");
                break;
        }
        */
        Instantiate(skillObjects[currentBullet.ToString()]);

    }

    /// <summary>
    /// ��ų ��� �� ���� �ð� ������ �Ŀ� ������ �����Ϸ��� ���
    /// </summary>
    void PowerUpRestart()
    {
        powerStats.SetIsActivedSkill(false);
    }

    /// <summary>
    /// �ٶ���ų �ڷ�ƾ�� ����ϱ� ���� �Լ� 
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
    /// �ٶ� �߻�ü�� �߻��ϴ� �ڷ�ƾ
    /// </summary>
    /// <param name="windBullet">�ٶ� �Ѿ��� ����</param>
    /// <param name="shootCount">�ٶ� �Ѿ��� �߻��� �� �ִ� ����</param>
    /// <returns></returns>
    IEnumerator WindBulletShoot(GameObject windBullet, int shootCount)
    {
        
        for (int i = 0; i < shootCount; i++)
        {
            GameObject instance = Instantiate(windBullet, shootTransform["Skill"].position,
                shootTransform["Skill"].rotation);
            
            /*
            if(instance.GetComponent<PlayerWindBullet>()) //�ٶ� �Ӽ��̶��, ù ���� �� ������ ���� ȸ�� ���� �ο�
            {
                float rotateRandom = Random.Range(30f, 60f);
                int randValue = Random.Range(0, 2);
                rotateRandom = randValue == 1 ? rotateRandom *= -1 : rotateRandom;
                instance.transform.Rotate(0, 0, rotateRandom);
            }
            */

            //�ϴ� �÷��̾ �߻��ϴ� ��ü�̹Ƿ�, �±� ���� �÷��̾�� ����
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
    /// ���� �ٶ� �Ѿ� �߻� �ڷ�ƾ�� ���ĳ� ����.
    /// </summary>
    /// <param name="icedBullet"></param>
    /// <returns></returns>
    IEnumerator IcedBulletShoot(GameObject icedBullet)
    {
        
        for (int i = 0; i < 15; i++)
        {
            GameObject instance = Instantiate(icedBullet, shootTransform["Skill"].position,
                shootTransform["Skill"].rotation);

            //�ϴ� �÷��̾ �߻��ϴ� ��ü�̹Ƿ�, �±� ���� �÷��̾�� ����
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
        //���̳� ���� ������ ���� ��쿡 ������ ó��
        if (collision.transform.tag == "Enemy")
        {
            //�� ��ü�� �浹�� ���� ���ظ� �޴� ����
            Character instanceEnemyInfo = collision.GetComponent<Character>();
            
        }
    }

    public string GetPlayerWeaponName()
    {
        return currentBullet.ToString();
    }
         
}