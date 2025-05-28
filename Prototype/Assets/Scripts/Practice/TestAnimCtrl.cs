using UnityEngine;

public class TestAnimCtrl : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        anim = GetComponent<Animator>();

    }

    void TestA()
    {
        Debug.Log("나 실행했어");

    }
    
}