/// <summary>
/// 상태머신의 기본 틀
/// </summary>
public class StateMachine
{
    // 현재 활성화된 상태를 저장하는 변수
    public EnemyState currentState;

    /// <summary>
    /// 상태 변경 메서드
    /// 기존 상태가 있으면 종료(Exit)시키고,
    /// 새로운 상태로 변경 후 진입(Enter) 메서드 호출
    /// </summary>
    /// <param cardName="newState">변경할 새로운 상태</param>
    public void ChangeState(EnemyState newState)
    {
        // 현재 상태가 존재하면 종료 처리
        currentState?.Exit();

        // 상태 변경
        currentState = newState;

        // 새 상태 진입 처리
        currentState.Enter();
    }

    /// <summary>
    /// 상태 머신의 업데이트 메서드
    /// MonoBehaviour의 Update가 아닌, 상태별 Update 호출용
    /// </summary>
    public void Update()
    {
        // 현재 상태가 존재하면 상태별 업데이트 실행
        currentState?.Update();
    }
}
