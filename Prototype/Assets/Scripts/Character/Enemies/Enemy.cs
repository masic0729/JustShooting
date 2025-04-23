using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    /// <summary>
    /// 기본적인 적 상태머신
    /// </summary>
    public enum FSM_Info
    {
        Spawn = 0,
        Idle,
        Move,
        Attack,
        Skill,
        Stun,
        Die   
    }
    public FSM_Info[] enemyFSM_List;

    public StateMachine stateMachine;                                   //FSM 스크립트

    public GameObject[] enemyProjectiles;                               //인스펙터에 등록된 발사체 종류
    protected Dictionary<string, GameObject> enemyProjectile;           //발사체 종류를 딕셔너리화
    protected Vector2 currentTargetPos;

    [Header("Enemy의 공격 데이터")]
    public EnemyAttackData attackData;

    protected ObjectMovement movement;
    
    protected GameObject thisGameObject;
    protected float targetMoveSpeed;
    protected float distanceNeedValue = 1f;

    public int stateIndex = 0;



    [SerializeField]
    bool isBoss = false; //보스 유무 확인. 기본값은 거짓
    protected bool isSelfPosition = true;

    protected virtual void Awake()
    {
        stateMachine = new StateMachine();
        //Debug.Log("일단 실행은 되는데, 기능은 안될 수 있다?");
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
            //플레이어 데이터를 불러와 피해를준다
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
