using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : StateMachineBehaviour
{
    EndBoss enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EndBoss>();
        enemy.StopAllCoroutines();
        enemy.ChangeState(new BossNullState(enemy));
        //클리어 했으니 충돌 판정 삭제
        if(enemy.GetIsFinalBoss() == true)
        {
            GameObject.Find("Player").GetComponent<Collider2D>().enabled = false;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //보스의 사망 애니메이션이 종료되면 카드가 생성된다.
        //CardManager.instance.ShowCards();
        enemy.OnCharacterDeath?.Invoke();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
