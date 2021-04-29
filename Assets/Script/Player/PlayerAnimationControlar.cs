using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControlar : MonoBehaviour
{

    Animator PlayerAnimator;
    Avatar avatar;

    public bool ikActivate;


    [Header("Head Look Ik Object")]
    public Transform HeadIK;



    [Header("LeftHand Ik ")]
    public Transform LeftHandIKTarget;
    [Header("LeftElbo Ik")]
    public Transform LeftElboIKTarget;



    [Header("RightHand Ik ")]
    public Transform RightHandIKTarget;
    [Header("RightElbo Ik ")]
    public Transform RightElboIKTarget;



    [Header("Rightfoot Ik ")]
    public Transform RightFootIKTarget;
    [Header("RightKneel Ik ")]
    public Transform RightKneelIKTarget;



    [Header("leftFoot Ik ")]
    public Transform LeftFootIKTarget;
    [Header("leftKneelIk ")]
    public Transform LeftKneelIKTarget;


    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        avatar = PlayerAnimator.avatar;
    }


    /// <summary>
    /// Activate all ik add weiight, set poristio and rotation
    /// </summary>
    /// <param name="layerIndex"></param>
    private void OnAnimatorIK(int layerIndex)
    {
        if (ikActivate)
        {
            //head Rotation
            if (HeadIK != null)
            {
                PlayerAnimator.SetLookAtWeight(1);
                PlayerAnimator.SetLookAtPosition(HeadIK.position);
            }

            if (RightHandIKTarget != null)
            {
                PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                PlayerAnimator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1f);
                PlayerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

                //hand
                PlayerAnimator.SetIKRotation(AvatarIKGoal.RightHand, RightHandIKTarget.rotation);
                PlayerAnimator.SetIKPosition(AvatarIKGoal.RightHand, RightHandIKTarget.position);

                //elbo
                PlayerAnimator.SetIKHintPosition(AvatarIKHint.RightElbow, RightElboIKTarget.position);
            }
            if (LeftHandIKTarget != null)
            {

                PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                PlayerAnimator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1f);
                PlayerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

                //hand
                PlayerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandIKTarget.rotation);
                PlayerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandIKTarget.position);

                //elbo
                PlayerAnimator.SetIKHintPosition(AvatarIKHint.LeftElbow, LeftElboIKTarget.position);
            }
            if (LeftFootIKTarget != null) 
            {
                PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                PlayerAnimator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, 1f);
                PlayerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);

                PlayerAnimator.SetIKRotation(AvatarIKGoal.LeftFoot, LeftFootIKTarget.rotation);
                PlayerAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, LeftFootIKTarget.position);

                PlayerAnimator.SetIKHintPosition(AvatarIKHint.LeftKnee, LeftKneelIKTarget.position);

            }
            if (RightFootIKTarget != null)
            {
                PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                PlayerAnimator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, 1f);
                PlayerAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

                PlayerAnimator.SetIKRotation(AvatarIKGoal.RightFoot, RightFootIKTarget.rotation);
                PlayerAnimator.SetIKPosition(AvatarIKGoal.RightFoot, RightFootIKTarget.position);

                PlayerAnimator.SetIKHintPosition(AvatarIKHint.RightKnee, RightKneelIKTarget.position);
            }
        }
        else 
        {
            PlayerAnimator.SetLookAtWeight(0f);
            PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            
        }
    }
}
