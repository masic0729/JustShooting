using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyer : MonoBehaviour
{
    public float destroyTime;

    private void OnEnable()
    {
        Destroy(this.gameObject, destroyTime);
    }
}
