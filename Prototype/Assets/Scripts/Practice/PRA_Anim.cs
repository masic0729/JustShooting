using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PRA_Anim : MonoBehaviour
{
    Animator anim;
    public AnimatorController[] animCtrl;
    /*
    �� ��ũ��Ʈ���� �� �� �ִ� ��.
    �켱, ��������Ʈ�� ���ؼ� ���� ������ ��ü(���� ��� �Ѿ�, ĳ���� ��� �پ��ϰ� ���� �迭��)���� ȿ�������� ���谡 ������ Ȯ���� ���Ե�.
    �� ����, ���� ���ҽ��� ��Ȱ�ϰ� ������ �� �ִ°�?�� �߿��ѵ�, �̸� �ذ��� �� �ְ� �ȴ�. �׷��� �����ؾ��� �κ���, �ִϸ��̼��� ������ ��,
    ���� �ִϸ��̼ǰ� ���� �Ķ���� ���� ��� �����ؾ� �Ѵٴ� ���̴�. �̸� ����Ͽ� ��������Ʈ�� ����Ѵ�.
    ��, �������� ����ü�� ��ü(���� ��� �Ѿ˰� �ٸ��� ũ�� ���� ��ų�� ����ϴ� ��ü��, �Ϲݸ��Ϳ� ������ �԰� ���)
    
    */
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.runtimeAnimatorController = animCtrl[0];
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            anim.runtimeAnimatorController = animCtrl[1];
        }
    }
}
