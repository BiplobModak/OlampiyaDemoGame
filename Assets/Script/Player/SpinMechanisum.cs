using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpinMechanisum : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// PlayerAnimationControlar 
    /// </summary>
    public PlayerJumpMechanism pJM;
    /// <summary>
    /// Score Manger Handel all Score Related Operatios
    /// </summary>
    public ScoreManager SM;

    public GameObject fullBody,roatemain;
    bool flip = false;
    private float tapHoldTimer = 0;

    List<SingleIkMove> PlayerIK = new List<SingleIkMove>();
    Rigidbody rb;

    void Start()
    {
        pJM = GetComponent<PlayerJumpMechanism>();
        fullBody = pJM.fullbody;
        rb = fullBody.GetComponent<Rigidbody>();
        GameObject[] go = GameObject.FindGameObjectsWithTag("PlayerIk");

        foreach (GameObject d in go) 
        {
            SingleIkMove sm = d.GetComponent<SingleIkMove>();
            PlayerIK.Add(sm);
        }
    }
    private void FixedUpdate()
    {
        if (flip) 
        {
            //tapHoldTimer += Time.deltaTime * 2f;
            
            fullBody.transform.Rotate(roatemain.transform.right, -10f);
            
        }
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (pJM.ClickCounter == 2) 
        {
            flip = true;
            SM.FlipStart = true;
            //Rigidbody rb = fullBody.GetComponent<Rigidbody>(); //no need get referance in start
            //rb.AddTorque(fullBody.transform.right * .1f, ForceMode.Acceleration);
            if (rb != null)
            {
                rb.freezeRotation = false;
            }
            BendPose(flip);
            //StartCoroutine(errordebug());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (pJM.ClickCounter == 2) 
        {
            flip = false;
            SM.FlipStart = false;
            tapHoldTimer = 0f;
            //rb.AddTorque(fullBody.transform.right * -.1f, ForceMode.Acceleration);
            //Rigidbody rb = fullBody.GetComponent<Rigidbody>();
            //if (rb != null)
            //{
            //    rb.freezeRotation = true;
            //}
            BendPose(flip);
        }
    }

    IEnumerator errordebug() 
    {
        yield return new WaitForSeconds(1f);
        //Debug.LogError("stp[");
    }
    private void BendPose(bool fl) 
    {
        foreach (SingleIkMove sm in PlayerIK)
        {
            sm.Spin = fl;
        }
    }

}
