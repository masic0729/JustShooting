using UnityEngine;

public abstract class EnemyState
{
    // 상태가 참조하는 적 객체
    protected Enemy enemy;
    // 적의 게임 오브젝트 참조
    protected GameObject thisGameObject;
    // 상태가 유지된 시간, 필요에 따라 위치 변경 가능
    protected float stateTime;
    // 도착 목표 위치
    protected Vector2 ariivePosition;

    /// <summary>
    /// EnemyState 생성자
    /// 상태가 제어할 Enemy 인스턴스와 해당 오브젝트 참조 초기화
    /// </summary>
    /// <param cardName="enemy">상태가 제어할 Enemy 객체</param>
    public EnemyState(Enemy enemy)
    {
        this.enemy = enemy;
        thisGameObject = this.enemy.gameObject;
    }

    /// <summary>
    /// 상태에 진입할 때 호출되는 메서드 (상속받아 구현 가능)
    /// </summary>
    public virtual void Enter()
    {
        // 기본 동작 없음
    }

    /// <summary>
    /// 상태가 매 프레임 갱신될 때 호출되는 메서드 (상속받아 구현 가능)
    /// 상태 유지 시간(stateTime)을 감소시킴
    /// </summary>
    public virtual void Update()
    {
        stateTime -= Time.deltaTime;
    }

    /// <summary>
    /// 상태를 종료할 때 호출되는 메서드 (상속받아 구현 가능)
    /// </summary>
    public virtual void Exit() { }
}
