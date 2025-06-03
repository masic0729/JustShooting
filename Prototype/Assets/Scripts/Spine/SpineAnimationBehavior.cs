using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

// 유니티의 Animator 상태에 따라 Spine 애니메이션을 재생하기 위한 StateMachineBehaviour 확장 클래스
public class SpineAnimationBehavior : StateMachineBehaviour
{
    public AnimationClip motion; // 연결할 애니메이션 클립 (Unity AnimationClip 기준)
    string animationClip; // Spine 애니메이션 이름

    [Header("SpineCustumOption")]
    int layer = 0; // Spine 애니메이션 트랙 인덱스
    public float timeScale = 1.0f; // 재생 속도 배율
    public bool loop; // 반복 여부

    private SkeletonAnimation skeletonAnim; // Spine SkeletonAnimation 컴포넌트 참조
    private Spine.AnimationState spineAnimationState; // Spine 애니메이션 상태
    private Spine.TrackEntry trackEntity; // 현재 재생 중인 Spine 애니메이션 트랙

    private void Awake()
    {
        if (motion != null)
        {
            animationClip = motion.name; // AnimationClip에서 Spine 애니메이션 이름 추출
        }
    }

    // 애니메이터 상태 진입 시 호출됨
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        // Spine 컴포넌트 초기화 (최초 한 번만 실행)
        if (skeletonAnim == null)
        {
            skeletonAnim = animator.GetComponent<SkeletonAnimation>();
            spineAnimationState = skeletonAnim.state;
        }

        // Spine 애니메이션 이름이 유효할 경우 애니메이션 재생
        if (animationClip != null)
        {
            loop = stateInfo.loop; // Unity 애니메이터의 반복 여부를 Spine에도 적용
            trackEntity = spineAnimationState.SetAnimation(layer, animationClip, loop); // 애니메이션 재생 설정
            trackEntity.TimeScale = timeScale; // 재생 속도 적용
        }
        else
        {
            Debug.Log(animationClip + "is not own"); // 애니메이션 이름이 없을 경우 디버그 출력
        }
    }
}
