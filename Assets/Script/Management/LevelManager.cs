using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public PlayerMovemet playerMovemet;

    #region UI
    [Header("Goal UI")]
    public Text TeTotalScore, TeScore, TeFlip, TeMedel;

    #endregion

    #region GoalVariables 
    private int targetMadel = 0; 
    private int targetScore = 0;
    private int targetPerformance  = 0;
    private int targetFlip = 0;
    #endregion
    //progress variable
    bool flip = false, score = false, performance = false, medel = false;

    [HideInInspector]public GameObject targetPoint, Jumpflor, jumpPotion;

    private void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("Water").GetComponent<ScoreManager>();
        playerMovemet = GameObject.FindGameObjectWithTag("FullBody").GetComponent<PlayerMovemet>();

        targetPoint = GameObject.FindGameObjectWithTag("Target");
        Jumpflor = GameObject.Find("JumpFlor");
        jumpPotion = GameObject.FindGameObjectWithTag("PlayerCreatpoint"); ;

        Startlevel();
    }

    private void Startlevel()
    {
        // check if level hase update 
        SetGoal(1);
         
    }


    // Lastlevel play  will come form game Manager
    private void SetGoal(int LevelPlaying)
    {
        //call at starta

        targetMadel = 3;
        targetPerformance = 5;
        targetScore = 300;
        targetFlip = 1;

        TeFlip.text = targetFlip.ToString();
        TeMedel.text = targetMadel.ToString();
        TeTotalScore.text = targetScore.ToString();

        TeScore.text = targetPerformance.ToString();
    }




    public void CheckStatus() 
    {
        
        //save data 
        if (scoreManager.flipCounter >= targetFlip) 
        {
            Debug.Log("FlipDone");
            TeFlip.gameObject.SetActive(false);
            flip = true;

        }
        if (scoreManager.totalScore >= targetScore) 
        {
            Debug.Log("TargetScore done");

            score = true;

        }
        if (scoreManager.Performance >= targetPerformance) 
        {
            // show drive performanve 
            Debug.Log("TargetPerformance");
            performance = true;
        }
        if (scoreManager.medel >= targetMadel) 
        {
            Debug.Log("targetmedelIs pass");
            medel = true;
        }
        if (flip && score && performance && medel)
        {
            Debug.LogError("Complete");
        }
        else 
        {
            ReStart();
        }


    }




    /// <summary>
    /// Reset score manager and Player Positions 
    /// </summary>
    private void ReStart() 
    {
        // teset all again;
        // call player movemnt 
        scoreManager.Retry();
        playerMovemet.Retry();
        // Debug.Log("restart");

    }



    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogError("colide");
        if (other.gameObject.tag == "FullBody") 
        {
            StartCoroutine(LevelCompletCheck());
        }
    }



    IEnumerator LevelCompletCheck() 
    {
        yield return new WaitForSeconds(.1f);
        //check sm status;
        ParformanceStatusCheck();
    }




    private bool ricCheck = false;
    private void ParformanceStatusCheck() 
    {
        if (scoreManager.Performance == 0)
        {
            //restart Game
            Debug.Log("get");
            //ReStart();
            CheckStatus();
        }
        else if (!ricCheck && scoreManager.Performance == 1)
        {
            // score 
            ricCheck = true; //avoid dath lock
            Debug.Log("Not get");
            ParformanceStatusCheck();// dath lock

        }
        else
        {
            //Complet task
            CheckStatus();

            Debug.Log("Notfkfk get");
        }
    }

}

