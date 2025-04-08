using UnityEngine;

public class EnemyMovement
{
    public void MoveToPointNormal(ref GameObject enemy, Vector2 targetPos, float speed)
    {
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, targetPos, speed);
    }

    

    

    public void MoveToPointLerp(ref GameObject enemy, Vector2 targetPos, float ratio)
    {
        enemy.transform.position = Vector2.Lerp(enemy.transform.position, targetPos, ratio);
    }

    /*public void MoveToPointLerp(ref GameObject enemy, Vector2 targetPos, float ratio)
    {
        enemy.transform.position = Vector2.Lerp(enemy.transform.position, targetPos, ratio);
    }*/

}
