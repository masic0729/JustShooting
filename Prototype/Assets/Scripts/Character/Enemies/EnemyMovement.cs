using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement
{
    public void MoveToPointNormal(ref GameObject enemy, ref Vector2 targetPos, ref float ratio)
    {
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, targetPos, ratio * Time.deltaTime);
    }

    

    

    public void MoveToPointLerp(ref GameObject enemy, ref Vector2 targetPos, ref float ratio)
    {
        enemy.transform.position = Vector2.Lerp(enemy.transform.position, targetPos, ratio);
    }

    

}
