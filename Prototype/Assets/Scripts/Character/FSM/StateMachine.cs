/// <summary>
/// ���¸ӽ��� �⺻ Ʋ
/// </summary>
public class StateMachine
{
    public EnemyState currentState;

    /// <summary>
    /// ���� ���Ľ��� ������ �����ϰ�,
    /// ���Ľ��� �ٲٴ� ����̱⿡ ���Ľ��� �ְ�,
    /// �־����� �����Ѵ�
    /// </summary>
    /// <param name="newState">���氪�� ���� ������</param>
    public void ChangeState(EnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    /// <summary>
    /// monobehavior�� Update�� �ƴ�, ���Ľ��ӽ��� ���� ������Ʈ
    /// </summary>
    public void Update()
    {
        //���� ���ıװ� ������ ������Ʈ ����.
        currentState?.Update();
    }
}
