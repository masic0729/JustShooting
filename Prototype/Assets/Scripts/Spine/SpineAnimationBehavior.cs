using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SpineAnimationBehavior : StateMachineBehaviour
{
    public AnimationClip motion;
    string animationClip;

    [Header("SpineCustumOption")]
    int layer = 0;
    public float timeScale = 1.0f;
    public bool loop;

    private SkeletonAnimation skeletonAnim;
    private Spine.AnimationState spineAnimationState;
    private Spine.TrackEntry trackEntity;

    private void Awake()
    {
        if (motion != null)
        {
            animationClip = motion.name;
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (skeletonAnim == null)
        {
            skeletonAnim = animator.GetComponent<SkeletonAnimation>();
            spineAnimationState = skeletonAnim.state;
        }

        if (animationClip != null)
        {
            loop = stateInfo.loop;
            trackEntity = spineAnimationState.SetAnimation(layer, animationClip, loop);
            trackEntity.TimeScale = timeScale;
        }
        else
        {
            Debug.Log(animationClip + "is not own");
        }
    }

}
