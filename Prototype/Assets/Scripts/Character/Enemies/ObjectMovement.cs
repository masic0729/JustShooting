using UnityEngine;

/// <summary>
/// 객체 이동 관련 기능을 제공하는 클래스.
/// 일반 이동, 선형 보간 이동, 사인 파형 기반 Y 이동을 지원한다.
/// </summary>
public class ObjectMovement
{
    /// <summary>
    /// 대상 위치까지 일정 속도로 직선 이동 (프레임마다 동일한 거리 이동).
    /// </summary>
    /// <param cardName="instance">이동할 객체</param>
    /// <param cardName="targetPos">이동할 목표 위치</param>
    /// <param cardName="speed">이동 속도</param>
    public void MoveToPointNormal(ref GameObject instance, Vector2 targetPos, float speed)
    {
        instance.transform.position = Vector2.MoveTowards(instance.transform.position, targetPos, speed * Time.deltaTime);
    }

    /// <summary>
    /// 대상 위치까지 선형 보간 이동 (점점 느려지는 형태로 부드럽게 접근).
    /// </summary>
    /// <param cardName="instance">이동할 객체</param>
    /// <param cardName="targetPos">이동할 목표 위치</param>
    /// <param cardName="ratio">Lerp 기능 경과 시간값. 추후 본래의 기능대로 수정할 예정</param>
    public void MoveToPointLerp(ref GameObject instance, Vector2 targetPos, float ratio)
    {
        instance.transform.position = Vector2.Lerp(instance.transform.position, targetPos, ratio * Time.deltaTime);
    }

    /// <summary>
    /// 사인 파형을 기반으로 Y축 이동을 수행 (주기적인 위아래 움직임).
    /// </summary>
    /// <param cardName="instance">이동할 객체</param>
    /// <param cardName="runningTime">시간 누적값 (ref로 받아 계속 증가)</param>
    /// <param cardName="yValue">사인 곡선의 진폭 (움직임 높이)</param>
    /// <param cardName="speed">움직임 속도</param>
    public void MoveToSinY(ref GameObject instance, ref float runningTime, float yValue, float speed)
    {
        runningTime += Time.deltaTime; // 시간 누적, 객체 작동 시간 관리용
        float posY = Mathf.Sin(runningTime) * yValue;
        instance.transform.position = new Vector2(instance.transform.position.x, posY);
    }
}
