using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PRA_Anim : MonoBehaviour
{
    Animator anim;
    public AnimatorController[] animCtrl;
    /*
    이 스크립트에서 알 수 있는 것.
    우선, 델리게이트를 통해서 같은 형태의 객체(예를 들어 총알, 캐릭터 등등 다양하고 같은 계열들)들을 효율적으로 설계가 가능할 확률이 높게됨.
    이 뜻은, 원래 리소스를 원활하게 변경할 수 있는가?가 중요한데, 이를 해결할 수 있게 된다. 그러나 참고해야할 부분은, 애니메이션을 편집할 때,
    같은 애니메이션과 같은 파라미터 값이 모두 동일해야 한다는 점이다. 이를 고려하여 델리게이트를 사용한다.
    단, 예외적인 투사체나 객체(예를 들어 총알과 다르게 크고 작은 스킬로 사용하는 객체나, 일반몬스터와 보스의 규격 등등)
    
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
