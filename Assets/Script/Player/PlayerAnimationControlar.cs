using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControlar : MonoBehaviour
{

    Animator PlayerAnimator;
    Avatar avatar;

    public bool ikActivate;
    
    [Range(0f,1f)]
    public float IkWeight = 1f;

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
                PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, IkWeight);
                PlayerAnimator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, IkWeight);
                PlayerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, IkWeight);

                //hand
                PlayerAnimator.SetIKRotation(AvatarIKGoal.RightHand, RightHandIKTarget.rotation);
                PlayerAnimator.SetIKPosition(AvatarIKGoal.RightHand, RightHandIKTarget.position);

                //elbo
                PlayerAnimator.SetIKHintPosition(AvatarIKHint.RightElbow, RightElboIKTarget.position);
            }
            if (LeftHandIKTarget != null)
            {

                PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, IkWeight);
                PlayerAnimator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, IkWeight);
                PlayerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, IkWeight);

                //hand
                PlayerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandIKTarget.rotation);
                PlayerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandIKTarget.position);

                //elbo
                PlayerAnimator.SetIKHintPosition(AvatarIKHint.LeftElbow, LeftElboIKTarget.position);
            }
            if (LeftFootIKTarget != null) 
            {
                PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, IkWeight);
                PlayerAnimator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, IkWeight);
                PlayerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, IkWeight);

                PlayerAnimator.SetIKRotation(AvatarIKGoal.LeftFoot, LeftFootIKTarget.rotation);
                PlayerAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, LeftFootIKTarget.position);

                PlayerAnimator.SetIKHintPosition(AvatarIKHint.LeftKnee, LeftKneelIKTarget.position);

            }
            if (RightFootIKTarget != null)
            {
                PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, IkWeight);
                PlayerAnimator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, IkWeight);
                PlayerAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, IkWeight);

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
