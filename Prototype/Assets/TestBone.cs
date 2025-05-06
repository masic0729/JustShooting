using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBone : MonoBehaviour
{
    public GameObject test;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        bullet = Instantiate(bullet, test.transform.position, test.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        bullet.transform.Translate(Vector2.up);


    }
}
