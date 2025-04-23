using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class EnemyState
{
    protected Enemy enemy;
    protected float stateTime; //추후 구조를 추가하여 위치 변경하거나, 삭제될 수 있음
    protected Vector2 ariivePosition;
    public EnemyState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public virtual void Enter() 
    {
        enemy.stateIndex++;
    }
    public virtual void Update()
    {
        //이하 동일
        stateTime -= Time.deltaTime;
    }
    public virtual void Exit() { }
}
