using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

// ����Ƽ�� Animator ���¿� ���� Spine �ִϸ��̼��� ����ϱ� ���� StateMachineBehaviour Ȯ�� Ŭ����
public class SpineAnimationBehavior : StateMachineBehaviour
{
    public AnimationClip motion; // ������ �ִϸ��̼� Ŭ�� (Unity AnimationClip ����)
    string animationClip; // Spine �ִϸ��̼� �̸�

    [Header("SpineCustumOption")]
    int layer = 0; // Spine �ִϸ��̼� Ʈ�� �ε���
    public float timeScale = 1.0f; // ��� �ӵ� ����
    public bool loop; // �ݺ� ����

    private SkeletonAnimation skeletonAnim; // Spine SkeletonAnimation ������Ʈ ����
    private Spine.AnimationState spineAnimationState; // Spine �ִϸ��̼� ����
    private Spine.TrackEntry trackEntity; // ���� ��� ���� Spine �ִϸ��̼� Ʈ��

    private void Awake()
    {
        if (motion != null)
        {
            animationClip = motion.name; // AnimationClip���� Spine �ִϸ��̼� �̸� ����
        }
    }

    // �ִϸ����� ���� ���� �� ȣ���
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        // Spine ������Ʈ �ʱ�ȭ (���� �� ���� ����)
        if (skeletonAnim == null)
        {
            skeletonAnim = animator.GetComponent<SkeletonAnimation>();
            spineAnimationState = skeletonAnim.state;
        }

        // Spine �ִϸ��̼� �̸��� ��ȿ�� ��� �ִϸ��̼� ���
        if (animationClip != null)
        {
            loop = stateInfo.loop; // Unity �ִϸ������� �ݺ� ���θ� Spine���� ����
            trackEntity = spineAnimationState.SetAnimation(layer, animationClip, loop); // �ִϸ��̼� ��� ����
            trackEntity.TimeScale = timeScale; // ��� �ӵ� ����
        }
        else
        {
            Debug.Log(animationClip + "is not own"); // �ִϸ��̼� �̸��� ���� ��� ����� ���
        }
    }
}
