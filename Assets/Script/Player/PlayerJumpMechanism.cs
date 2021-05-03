using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerJumpMechanism : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    #region Vaiables

    /// <summary>
    /// Player body with all Ik enelments apply jump force to it.
    /// </summary>
    [HideInInspector] public GameObject fullBody; //no need to apaer in editor.


    /// <summary>
    /// Rotate arouind Player Body Rotation point.
    /// </summary>
    public GameObject BodyRotationPoint;


    /// <summary>
    /// Player main body bent position. On press down player Will tress down this position.
    /// </summary>
    public Transform bendMaxPosition;

    /// <summary>
    /// Score Manager use to give a flag  for start counting
    /// </summary>
    public ScoreManager scoreManager;


    /// <summary>
    /// fullBody GameObject Rigitbody
    /// </summary>
    private Rigidbody fullBodyRigitbody;


    /// <summary>
    /// Player body shep which contains Player animatior 
    /// </summary>
    private GameObject player;


    /// <summary>
    /// called in SpinMechnisum
    /// </summary>
    public int ClickCounter{ get => clickCounter; set => clickCounter = value; }
    private int clickCounter;

    //firsttab and jump flag
    private bool tapFirst, jump;


    //Count hold time
    private float tapHoldTimer = 0f;

    //player stand position
    private Vector3 playerPositionAtStart;


    //exta jump force
    public float sencivity = 1;

    //AnimationControlar use only for hand movement
    public PlayerAnimationControlar playerAnimation;
    #endregion


    private void Start()
    {

        fullBody = GameObject.FindGameObjectWithTag("FullBody");
        fullBodyRigitbody = fullBody.GetComponent<Rigidbody>();


        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimation = player.GetComponent<PlayerAnimationControlar>();

        //get player start rotation to count Flip
        playerPositionAtStart = player.transform.position;

        scoreManager = GameObject.FindGameObjectWithTag("Water").GetComponent<ScoreManager>();

    }


    private void LateUpdate()
    {
        //check tap first time or not
        if (tapFirst) 
        {
            //count tabtime
            tapHoldTimer += Time.deltaTime;

            //check update
            //if (tapHoldTimer <= 1f)
            //{
            //    playerAnimation.IkWeight = tapHoldTimer;
            //}
            
                //RotateBody Around a Point (Backword)
            if (fullBody.transform.localEulerAngles.x > 280f || fullBody.transform.localEulerAngles.x < 1 )
            {
                player.transform.position = Vector3.Slerp(player.transform.position, bendMaxPosition.position, tapHoldTimer);
                fullBody.transform.RotateAround(BodyRotationPoint.transform.position, fullBody.transform.right, -tapHoldTimer);
            }

        }

        //Returning Stand Position
        if (jump) 
        {
            //tapHoldTimer += Time.deltaTime;
            player.transform.position = Vector3.Slerp(player.transform.position, BodyRotationPoint.transform.position, 1f);
        }
        //jump end
    }



    private void FixedUpdate() 
    {
        //jump
        if (clickCounter == 2 && jump) //adding Rigitbody and give force when jump 
        {

            tapHoldTimer = Mathf.Clamp(tapHoldTimer, 3f, 5f);

            //adding adding jump force 
            fullBodyRigitbody.AddForce(fullBody.transform.up * tapHoldTimer * sencivity , ForceMode.Impulse);
            fullBodyRigitbody.useGravity = true;

            //give a rotation force to look real
            fullBodyRigitbody.AddTorque(fullBody.transform.right * -3f, ForceMode.VelocityChange);
            jump = false;
        }
        //jump end
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        //check tap Count
        if (!tapFirst && clickCounter == 0) //avoid multiple touch
        {
            jump = false;
            tapFirst = true;
            clickCounter = 1;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (tapFirst && clickCounter == 1) //avoid multiple touch
        {
            tapFirst = false;
            clickCounter = 2;
            jump = true;
            scoreManager.startScoreCount = true;
        }
    }


    //***********************************************Reset
    public void Retry() 
    {
        clickCounter = 0;
        tapFirst = false;
        jump = false;
        tapHoldTimer = 0f;
    }

}
