using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRA_Player : MonoBehaviour
{
    //public Dictionary<string, GameObject> CommonBulletResources;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.AddComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PoolManager.Instance.Pools["PlayerCommonBullet"].Get();
            PoolManager.Instance.Pools["TestSkillBullet"].Get();
            //GameObject instanceProjectile = PoolManager.Instance.Pools["PlayerCommonBullet"].Get();
        }
    }
}