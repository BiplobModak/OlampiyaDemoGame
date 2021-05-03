using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
   
    //ground target and playerfullbody
    [HideInInspector]public Transform target, fullbody;

    public float playerRotationAtStart;

    public bool startScoreCount;

    /// <summary>
    /// if true Flip Score Counter will start
    /// </summary>
    public bool FlipStart, FullSpin;


    #region ScoreVariables

    public int totalScore = 0;

    public float score = 1;

    public int flipCounter = 1;

    public int medel = 0;
    /// <summary>
    /// how much gold earn in this level
    /// </summary>
    public int levelGold = 0;

    /// <summary>
    /// updating in Jump acurate or not Player drive performace if 1 = no chang if 0 = failed, if 2 = low parformance, if 5 = mid level performace if 10 = perfect 
    /// </summary>
    public int Performance = 1;
    #endregion

    #region UI
    public Text Tflip , Tscore, TtoatalScore, Tmedel;
    #endregion

    


    /// <summary>
    /// geting fullbody amd target, set fullbody rotation at start for Flip Counting
    /// </summary>
    private void Start()
    {
        fullbody = GameObject.FindGameObjectWithTag("FullBody").transform;
        target = GameObject.FindGameObjectWithTag("Target").transform;

        playerRotationAtStart = fullbody.transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        //flip count
        if (startScoreCount)
        {
            //get rotation and count flip
            if (FullSpin && fullbody.eulerAngles.x > playerRotationAtStart && fullbody.eulerAngles.x < playerRotationAtStart + 60f)
            {
                FullSpin = false;
                flipCounter += 1;
                Tflip.text = flipCounter.ToString();
            }
            if (fullbody.eulerAngles.x > playerRotationAtStart + 300f && fullbody.eulerAngles.x < 360f)
            {
                FullSpin = true;
            }
        }
        if (startScoreCount)
        {
            //update score and show
            score += .1f * (flipCounter + 1);
            Tscore.text = score.ToString("00");
            TotalScoreUpdate();
        }
    }




    /// <summary>
    /// Chaking Distance from target and give medel
    /// </summary>
    /// <param name="player"></param>
    private void DiatanceScore(GameObject player) 
    {

        float distance = Mathf.Abs( target.position.z - player.transform.position.z);


        if (distance < .5f)//in first range
        {
            medel = 3;
        }
        else if (distance < 1f && distance > .5f)//in secoend range
        {
            medel = 2;
        }
        else if (distance < 1.5f && distance > 1f) // in the last rage
        {
            medel = 1;
        }
        else //out of target
        {
            medel = 0;
        }

        Tmedel.text = medel.ToString();
    }

   IEnumerator JumpCheck(bool Spining, float DropAngle) 
    {

        JumpAcurateOrNot(Spining, DropAngle);
        yield return null;
    }


    /// <summary>
    /// checking jump is currect or not if  rotation r, r < 10 performance => 20x, 
    /// if r < 20 performance => 10x, else performance => 4x, else failed 
    /// </summary>
    /// <param name="Spining">Player touch or not </param>
    /// <param name="DropAngle"> drop angle </param>
    public void JumpAcurateOrNot(bool Spining, float DropAngle)
    {
        if (!Spining)
        {

            if (DropAngle > 330f || DropAngle < 30f)// toal 40
            {
                if (DropAngle <= 10f || DropAngle >= 350f)// if rotation is < 5  then parfect
                {
                    Performance = 10;
                    //Debug.Log("Parfect");
                }
                else if ((DropAngle <= 20f && DropAngle > 10f) || (DropAngle < 350f && DropAngle >= 340f))// if rotation is > 5 but < 10 then good
                {
                    Performance = 5;
                   // Debug.Log("Good");
                }
                else // rotation is > 10 nailed
                {
                    Performance = 2;
                    //Debug.Log("Nailed");
                }

            }
            else if (DropAngle < 200f && DropAngle > 160f)
            {
                if (DropAngle <= 190f && DropAngle >= 170f)
                {
                    Performance = 10;
                    //Debug.Log("Parfect2");
                }
                else if (DropAngle < 200f && DropAngle > 190f || DropAngle > 160f && DropAngle < 170f)
                {
                    Performance = 5;
                   // Debug.Log("Good");
                }
                else
                {
                    Performance = 2;
                    //Debug.Log("Nailed");
                }
            }
            else
            {
                Performance = 0;
                //Debug.Log("Faild");
            }

        }
    }




    /// <summary>
    /// Total score update 
    /// </summary>
    private void TotalScoreUpdate() 
    {
        totalScore = Mathf.RoundToInt(score * (flipCounter +1));
        TtoatalScore.text = totalScore.ToString();
    }

    public int MedelEarn() 
    {
        levelGold = Performance * medel; // give golds
        Debug.Log(Performance+"  "+ medel);
        switch (medel) 
        {
            case 3:        //jump at center point
                {
                    switch (Performance) 
                    {
                        case 10:
                            {
                                
                                return 3; //gold
                            }
                        case 5: 
                            {
                                return 2; //silver
                            }
                        case 2: 
                            {
                                return 1; //brons
                            }
                    }
                    break;
                }
            case 2:    //jump at 2nd Ring
                {
                    switch (Performance)
                    {
                        case 10:
                            {
                                return 2; //silver
                            }
                        case 5:
                            {
                                return 2; //silver
                            }
                        case 2:
                            {
                                return 1; //brons
                            }
                    }
                    break;
                }
            case 1:     //jump at 3rd ring
                {
                    switch (Performance)
                    {
                        case 10:
                            {
                                return 2; //silver
                            }
                        case 5:
                            {
                                return 1; //silver
                            }
                        case 2:
                            {
                                return 1; //brons
                            }
                    }
                    break;
                }
            default:
                Debug.Log("No medel");
                return 0;
        }

        return 0;

    }


    //*******************************************************************************************
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FullBody")
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            //check distance
            DiatanceScore(other.gameObject);
            //check jump
            StartCoroutine(JumpCheck(FlipStart, other.transform.localEulerAngles.x));

            //JumpAcurateOrNot();
            startScoreCount = false;
            //Debug.LogError(other.transform.eulerAngles);
            TotalScoreUpdate();
        }
    }



    //************************************ reset
    public void Retry()
    {
        startScoreCount = false;
        FlipStart = false;
        FullSpin = false;
        
        
        totalScore = 0;
        score = 1;
        flipCounter = 0;
        medel = 0;
        Performance = 1;
        levelGold = 0;

        Tscore.text = "00";
        Tflip.text = "00";
        TtoatalScore.text = "00";
        Tmedel.text = "00";

    }
}
