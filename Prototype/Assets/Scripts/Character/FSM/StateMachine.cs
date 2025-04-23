/// <summary>
/// 상태머신의 기본 틀
/// </summary>
public class StateMachine
{
    public EnemyState currentState;

    /// <summary>
    /// 현재 스탠스가 있으면 종료하고,
    /// 스탠스를 바꾸는 기능이기에 스탠스를 넣고,
    /// 넣었으니 실행한다
    /// </summary>
    /// <param name="newState">변경값을 넣을 데이터</param>
    public void ChangeState(EnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    /// <summary>
    /// monobehavior의 Update가 아닌, 스탠스머신의 고유 업데이트
    /// </summary>
    public void Update()
    {
        //현재 스탠그가 있으면 업데이트 구동.
        currentState?.Update();
    }
}
