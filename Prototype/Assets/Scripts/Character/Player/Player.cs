using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
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

    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Init();
        //Time.timeScale = 0;

    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        UserInput();
        
    }

    protected override void Init()
    {
        base.Init();
        //�̵� ���� ����
        maxMoveX = 9.5f;
        maxMoveY = 4.5f;

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
        //�Ϲ� �Ѿ� ����
        if (attackTime >= attackDelay)
        {
            ShootCommmonBullet(currentBullet);
            attackTime = 0;

        }
        //��ų Ű. ���� �߰� ����

    }


    //����� �׽�Ʈ �ǵ��� Ű�� �Է��Ѵ�.
    void TransWeapon()
    {
        //���� �׽�Ʈ�� ���� ����
        /*
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
        */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TransCurrentWeapon();
            if(powerStats.isPowerMax == true)
            {
                powerStats.playerPower = 0;
                powerStats.isPowerMax = false;
                PowerSkill();
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
    /// �Ϲ� �Ѿ˰���
    /// </summary>
    /// <param name="currentState">���� �÷��̾ �߻��Ϸ��� �Ϲ� �Ѿ� ����</param>
    void ShootCommmonBullet(CurrentPlayerBullet currentState)
    {
        GameObject instanceCommonBullet = PoolManager.Instance.Pools["PlayerCommonBullet"].Get(); //�ν��Ͻ�ȭ
        SetCommonBulletData(ref instanceCommonBullet); //�߻�ü ���ҽ� ������ �ε�
        instanceCommonBullet.transform.position = shootPositions["CommonBullet"].position; //�߻�ü ��ġ ����
        instanceCommonBullet.transform.rotation = shootPositions["CommonBullet"].rotation; //�߻�ü ȸ�� �� ����
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
        commonBullet.GetComponent<Projectile>().SetDamage(this.attackStats.damage);
        commonBullet.GetComponent<Projectile>().SetMoveSpeed(this.attackStats.moveSpeed);
    }

    void SetCurrentCommonBulletData(CurrentPlayerBullet bulletState)
    {
        currentBullet = bulletState;

        //���� �߻��Ϸ��� �Ѿ� ������ ���� �� �׿� �´� �ɷ�ġ ����
        attackStats.sprite = commonBullets[bulletState.ToString()].sprite;
        attackStats.animCtrl = commonBullets[bulletState.ToString()].animCtrl;
        attackStats.damage = commonBullets[bulletState.ToString()].damage;
        attackStats.moveSpeed = commonBullets[bulletState.ToString()].moveSpeed;
        powerStats.powerUpValue = commonBullets[bulletState.ToString()].powerValue;
        attackDelay = commonBullets[bulletState.ToString()].attackDelay;
        //���� ���⿡ ���� �ɷ�ġ ���� Ȯ�ο� �ؽ�Ʈ ���
        text.text = "sprite : " + attackStats.sprite + "\nanimCtrl : " + attackStats.animCtrl + "\ndamage : " +
            attackStats.damage + "\nmoveSpeed : " + attackStats.moveSpeed + "\npower : " + powerStats.powerUpValue; ;
    }

    /// <summary>
    /// �÷��̾ �Ŀ��� ��� ȹ���� ���� �ߵ��Ǵ� ���� ȿ��
    /// </summary>
    public void PowerOn()
    {
        switch (currentBullet)
        {
            case CurrentPlayerBullet.Wind:
                Debug.Log("�ٶ� ������ ���� �����.");
                break;
            case CurrentPlayerBullet.Iced:
                Instantiate(buffObject["Iced"]);
                break;
            case CurrentPlayerBullet.Fire:

                break;
            case CurrentPlayerBullet.Lightning:
                break;
        }

    }

    void PowerSkill()// �Ŀ��� 100�� �� ���� ������ ��ų Ű�� �ߵ��� ��
    {
        switch(currentBullet)
        {
            case CurrentPlayerBullet.Wind:
                Instantiate(skillObjects["Wind"]);
                Debug.Log("�ٶ� ��ų �ߵ�");
                break;
            case CurrentPlayerBullet.Iced:
                Instantiate(skillObjects["Iced"]);
                Debug.Log("���� ��ų �ߵ�");
                break;
            case CurrentPlayerBullet.Fire:
                Debug.Log("�� ��ų �ߵ�");

                break;
            case CurrentPlayerBullet.Lightning:
                Debug.Log("���� ��ų �ߵ�");

                break;
        }
    }

    public void WindSkill(GameObject bulletInstance, int shootCount)
    {
        StartCoroutine(WindBulletShoot(bulletInstance, shootCount));
    }

    IEnumerator WindBulletShoot(GameObject windBullet, int shootCount)
    {
        Transform shootVector = transform.GetChild(1).transform;

        for (int i = 0; i < shootCount; i++)
        {
            GameObject instance = Instantiate(windBullet, shootVector.position, shootVector.rotation);
            float rotateRandom = Random.Range(-45f, 45f);
            instance.transform.Rotate(0,0, rotateRandom);
            //�ϴ� �÷��̾ �߻��ϴ� ��ü�̹Ƿ�, �±� ���� �÷��̾�� ����
            if (instance != null)
            {
                instance.transform.tag = "Player";
                instance.GetComponent<Projectile>().SetMoveSpeed(attackStats.moveSpeed);
                instance.GetComponent<Projectile>().SetDamage(attackStats.damage);

            }
            yield return new WaitForSeconds(0.2f);
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
}