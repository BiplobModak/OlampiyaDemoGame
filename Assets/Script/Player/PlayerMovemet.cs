using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemet : MonoBehaviour
{
    public GameObject playerCreatPoint;
    public PlayerJumpMechanism pjm;
    public SpinMechanisum sm;

    Vector3 positionAtStart;
    Quaternion rotationAtstart;
    Rigidbody selfRigitbody;


    // Start is called before the first frame update
    void Start()
    {
        playerCreatPoint = GameObject.FindGameObjectWithTag("PlayerCreatpoint");
        //get start rotation and position
        //positionAtStart = transform.position;
        rotationAtstart = transform.rotation;


        selfRigitbody = GetComponent<Rigidbody>();


        GameObject go = GameObject.FindGameObjectWithTag("PlayerMovementControlar");
        pjm = go.GetComponent<PlayerJumpMechanism>();
        sm = go.GetComponent<SpinMechanisum>();
    }


    /// <summary>
    /// if faild to complet task rest player postion at start, and reset player jump Mechanism and spin Mechanism
    /// </summary>
    public void Retry()
    {
        transform.position = playerCreatPoint.transform.position;
        transform.rotation = rotationAtstart;

        selfRigitbody.isKinematic = false;
        selfRigitbody.useGravity = false;

        pjm.Retry();
        sm.Retry();        
    }
}
