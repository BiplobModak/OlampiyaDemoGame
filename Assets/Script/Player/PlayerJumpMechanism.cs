using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerJumpMechanism : MonoBehaviour, IPointerDownHandler, IPointerUpHandler //, IBeginDragHandler
{
    /// <summary>
    /// Player body
    /// </summary>
    [SerializeField ]GameObject player;

    /// <summary>
    /// Player body with all Ik enelments 
    /// </summary>
    public GameObject fullbody;
    /// <summary>
    /// PlayerBody Rotation Point
    /// </summary>
    public GameObject BodyRotationPoint;
    /// <summary>
    /// MaxBand Transform
    /// </summary>
    public Transform bendMaxPosition;

    public int ClickCounter{ get => clickCounter; }

    //jump
    private bool tapFirst, jump;



    private int clickCounter;
    private float tapHoldTimer = 0f;
    private Vector3 playerPositionAtStart;
    private Rigidbody rb;
    public float sencivity = 1;
    //jump end


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPositionAtStart = player.transform.position;
    }
    private void LateUpdate()
    {
        //jump
        if (tapFirst) 
        {
            tapHoldTimer += Time.deltaTime;
            //Debug.Log(Time.deltaTime);
            
            if (tapHoldTimer < 1f) 
            {
                
                player.transform.position = Vector3.Slerp(player.transform.position, bendMaxPosition.position, tapHoldTimer);

                //float angel = Mathf.Clamp(tapHoldTimer, 0f, 1.1f);
                //RotateBody Around a Point
                fullbody.transform.RotateAround(BodyRotationPoint.transform.position, fullbody.transform.right, -tapHoldTimer);
                

            }
        }
        if (jump) 
        {
            player.transform.position = Vector3.Slerp(player.transform.position, playerPositionAtStart, .9f);
        }
        //jump end
        
    }
    private void FixedUpdate()
    {
        //jump
        if (clickCounter == 2 && jump) //adding Rigitbody and give force when jump 
        {
            rb = fullbody.GetComponent<Rigidbody>();
            tapHoldTimer = Mathf.Clamp(tapHoldTimer, 3f, 100f);
            rb.AddForce(fullbody.transform.up * tapHoldTimer * sencivity , ForceMode.Impulse);
            rb.useGravity = true;


            //rb.AddTorque(fullbody.transform.right * -3f, ForceMode.Impulse);

            //rb.constraints = RigidbodyConstraints.FreezeRotation;
            //rb.constraints = RigidbodyConstraints.u;

            jump = false;
            
        }
        //jump end
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //check tap Count
        if (!tapFirst && clickCounter == 0) //codition for avoid multiple touch
        {
            jump = false;
            tapFirst = true;
            clickCounter = 1;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (tapFirst && clickCounter == 1) //codition for avoid multiple touch
        {
            tapFirst = false;
            clickCounter = 2;
            jump = true;
        }
    }
}
