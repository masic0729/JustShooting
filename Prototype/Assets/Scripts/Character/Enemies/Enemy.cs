using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public StateMachine enemyState;                                     //FSM ��ũ��Ʈ
    public int stateIndex = 0;
    public Vector2 arriveVector;                                               //��ü�� �����Ϸ��� ��ġ��

    //public StateMachine stateMachine;                                   

    public GameObject[] enemyProjectiles;                               //�ν����Ϳ� ��ϵ� �߻�ü ����
    protected Dictionary<string, GameObject> enemyProjectile;           //�߻�ü ������ ��ųʸ�ȭ
    protected Vector2 currentTargetPos;

    [Header("Enemy�� ���� ������")]
    public EnemyAttackData attackData;

    public ObjectMovement movement;
    
    protected GameObject thisGameObject;
    protected float targetMoveSpeed;
    protected float distanceNeedValue = 1f;




    [SerializeField]
    bool isBoss = false; //���� ���� Ȯ��. �⺻���� ����
    protected bool isSelfPosition = true;

    protected virtual void Awake()
    {
        //stateMachine = new StateMachine();
        //Debug.Log("�ϴ� ������ �Ǵµ�, ����� �ȵ� �� �ִ�?");
    }

    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckOverGameZone();
    }

    private void OnDestroy()
    {

    }

    protected override void Init()
    {
        base.Init();
        movement = new ObjectMovement();
        thisGameObject = this.gameObject;
        enemyProjectile = new Dictionary<string, GameObject>();
        if(enemyProjectiles != null)
        {
            for (int i = 0; i < enemyProjectiles.Length; i++)
            {
                enemyProjectile[enemyProjectiles[i].name] = enemyProjectiles[i];
            }
        }
        arriveVector = new Vector2(3f, 0);
    }

    void CheckOverGameZone()
    {
        if(this.transform.position.x < -12f)
        {
            Destroy(this.gameObject);
        }
    }

    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        const float damageValue = 1f;
        if(collision.transform.name == "Player")
        {
            //�÷��̾� �����͸� �ҷ��� ���ظ��ش�
            Character instancePlayer = collision.gameObject.GetComponent<Character>();
            if(instancePlayer != null && characterInteraction != null)
            {
                characterInteraction.SendDamage(ref instancePlayer, damageValue);
                Debug.Log(instancePlayer.GetHp());
            }
        }
    }
    /// <summary>
    /// getset
    /// </summary>
    /// <param name="pos"></param>
    public void SetTargetPosition(Vector2 pos)
    {
        isSelfPosition = false;
        currentTargetPos = pos;
    }

    public bool GetIsBoss()
    {
        return isBoss;
    }

    public void SetIsBoss(bool state)
    {
        isBoss = state;
    }

    
}
