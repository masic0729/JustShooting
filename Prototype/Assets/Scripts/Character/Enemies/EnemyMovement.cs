using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement
{
    public void MoveToPointNormal(ref GameObject enemy, ref Vector2 targetPos, ref float speed)
    {
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, targetPos, speed * Time.deltaTime);
    }

    

    public void MoveToPointLerp(ref GameObject enemy, ref Vector2 targetPos, ref float speed)
    {
        enemy.transform.position = Vector2.Lerp(enemy.transform.position, targetPos, speed);
    }

    
}
