using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpinMechanisum : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// Hole player Body it has Rigitbody Atach
    /// </summary>
    [HideInInspector] public GameObject fullBody;


    /// <summary>
    /// PlayerAnimationControlar 
    /// </summary>
    private PlayerJumpMechanism playerJumpMechanism;



    /// <summary>
    /// Score Manger Handel all Score Related Operatios Use up upade Player is rotate or not
    /// </summary>
    public ScoreManager SM;

    //player is spining or not
    bool spin = false;

    
    private Rigidbody FullBodyRigitbody;

    //all player iks Script
    private List<SingleIkMove> PlayerIK = new List<SingleIkMove>();

    

    
    void Start()
    {
        //find player jump
        playerJumpMechanism = GetComponent<PlayerJumpMechanism>();



        //upate self fullbody from playerJumpMechanism
        fullBody = playerJumpMechanism.fullBody;
        FullBodyRigitbody = fullBody.GetComponent<Rigidbody>();


        //find plyer Iks a get SingleIKMove Script
        GetSingleIKMove();
        
    }




    /// <summary>
    /// Finding PlayerIK GameObject and gating SingleIkMove Script and Adding To PlayerIK List
    /// </summary>
    private void GetSingleIKMove()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("PlayerIk");

        foreach (GameObject d in go)
        {
            SingleIkMove sm = d.GetComponent<SingleIkMove>();
            PlayerIK.Add(sm);
        }
    }




    /// <summary>
    /// Rotating body
    /// </summary>
    private void FixedUpdate()
    {

        if (spin) 
        {
            fullBody.transform.Rotate(fullBody.transform.right, -10f); // rotating main body
        }
        
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerJumpMechanism.ClickCounter == 2) 
        {
            spin = true;
            SM.FlipStart = true;
            BendPose(spin);
        }
    }




    /// <summary>
    ///  
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        if (spin &&playerJumpMechanism.ClickCounter == 2) 
        {
            FullBodyRigitbody.AddTorque(fullBody.transform.right * -3f, ForceMode.VelocityChange);//adding extar tork fter relesing button
            spin = false;
            SM.FlipStart = false;
            BendPose(spin);
        }
    }



    

    /// <summary>
    /// Giveing instruction Player Iks Go go its Bend position, 
    /// </summary>
    /// <param name="fl"> </param>
    private void BendPose(bool fl) 
    {
        foreach (SingleIkMove sm in PlayerIK)
        {
            sm.Spin = fl;
        }
    }

    //*******************************************reset

    public void Retry() 
    {
        spin = false;
        BendPose(spin);
    }
}
