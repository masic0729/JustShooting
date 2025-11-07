using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyBulletTransColor : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Vector4[] colorData = {
        new Vector4(0.2314f, 0.4471f, 1f, 1f),
        new Vector4(0.4235f, 1f, 0.4353f, 1f) 
    };

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colorData[GameManager.instance.GetStage()];
    }

}
