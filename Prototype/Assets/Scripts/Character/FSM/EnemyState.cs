using UnityEngine;
public abstract class EnemyState
{
    protected Enemy enemy;
    protected GameObject thisGameObject;
    protected float stateTime; //���� ������ �߰��Ͽ� ��ġ �����ϰų�, ������ �� ����
    protected Vector2 ariivePosition;
    public EnemyState(Enemy enemy)
    {
        this.enemy = enemy;
        thisGameObject = this.enemy.gameObject;
    }

    public virtual void Enter() 
    {
        enemy.stateIndex++;
    }
    public virtual void Update()
    {
        //���� ����
        stateTime -= Time.deltaTime;
    }
    public virtual void Exit() { }
}
