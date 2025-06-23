using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            anim.SetTrigger("TestTrigger");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            anim.SetTrigger("TestTrigger2");
        }
    }

    public void Test()
    {
        Debug.Log("ÀÀ ³Í ¹Ùº¸µÆ¾î. ½Ã°£¸¸ ¹ö·È¾î ¤»¤»");
    }
}
