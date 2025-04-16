using UnityEngine;

public class ObjectMovement
{
    public void MoveToPointNormal(ref GameObject instance, Vector2 targetPos, float speed)
    {
        instance.transform.position = Vector2.MoveTowards(instance.transform.position, targetPos, speed * Time.fixedDeltaTime);
    }

    public void MoveToPointLerp(ref GameObject instance, Vector2 targetPos, float ratio)
    {
        instance.transform.position = Vector2.Lerp(instance.transform.position, targetPos, ratio * Time.fixedDeltaTime);
    }

    public void MoveToSinY(ref GameObject instance, ref float runningTime, float yValue, float speed)
    {
        runningTime += Time.deltaTime; //�� �κ��� ���� Ŭ���� ���迡�� �ʾ��� ���� ����. �ܼ� ��ü�� �۵� �ð��̱� ����
        float posY = Mathf.Sin(runningTime) * yValue;
        instance.transform.position = new Vector2(instance.transform.position.x, posY);
    }
}