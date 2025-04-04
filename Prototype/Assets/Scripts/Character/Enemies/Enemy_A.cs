using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_A : Enemy
{
    public enum EnemyState
    { 
        Spawn,
        Move,
        Action,
        Attack
    }
    public EnemyState enemyState;
    
    [SerializeField]
    float targetPosY, targetPosX;
    float targetMoveSpeed;
    Vector2 currentTargetPosY;
    Vector2 currentTargetPosX;

    bool isArriveTargetPos = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Init();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //ObjectMove(Vector2.left);
        currentTargetPosY = new Vector2(transform.position.x, targetPosY);

        if (Vector2.Distance(transform.position, currentTargetPosY) > 0.1f && isArriveTargetPos == false)
        {
            //transform.position = Vector2.MoveTowards(transform.position, currentTargetPos, Time.deltaTime * GetMoveSpeed() * 5f);
            movement.MoveToPointLerp(ref thisGameObject, currentTargetPosY, targetMoveSpeed);
            //movement.MoveToPointLerp(ref thisGameObject, currentTargetPosX, targetMoveSpeed);
        }
        else if(isArriveTargetPos == false)
        {
            isArriveTargetPos = true;
            Invoke("SetTransTargetTransform", 0.2f);
        }
    }

    void SetTransTargetTransform()
    {
        targetPosY *= -1;
        //targetPosX = transform.position.x - GetMoveSpeed() * 1.5f;
        //currentTargetPosX = new Vector2(targetPosX, transform.position.y);
        isArriveTargetPos = false;
    }

    protected override void Init()
    {
        base.Init();
        targetMoveSpeed = GetMoveSpeed() / 2f * Time.deltaTime;
        SetTransTargetTransform();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}