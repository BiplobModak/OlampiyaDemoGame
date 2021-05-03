using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Class OverView
/// <summary>
///                                     start:
///                                         get ScoreManger, PlayerMovement, GameManager, target point, jumpfloar, jumppoint
///                                     
///                                     startLevel:
///                                         check GameManage lastlevel status
///                                         Starlevel()  lastlevel + 1
///                                         set goal()
///                                         
/// 
///                                     setGoal:
///                                        set gola with last level, show target result
///                                        
/// 
/// 
///                                       checkStarus:
///                                         check flipcount, score, performance, medel
///                                         
///                                       
///                                     
///                                     
///                                     
/// </summary>
#endregion


public class LevelManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public PlayerMovemet playerMovemet;
    public GameManager gameManager;

    #region UI Variables (text and gameCompletePanal)
    [Header("Goal UI")]
    public Text TeTotalScore, TeScore, TeFlip, TeMedel;

    public GameObject gameCompletePanal, nextlevelButton;
    #endregion

    #region GoalVariables 
    private int targetMadel = 0; 
    private int targetScore = 0;
    private int targetPerformance  = 0;
    private int targetFlip = 0;
    #endregion

    //progress variable
    bool flip = false, score = false, performance = false, medel = false;
    int lastlevel = 0;
    public bool triggerEnterCheck = false;

    [HideInInspector]public GameObject targetPoint, Jumpflor, playercreatpoint, fullBoddy;



    //***********************************start
    private void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("Water").GetComponent<ScoreManager>();
        playerMovemet = GameObject.FindGameObjectWithTag("FullBody").GetComponent<PlayerMovemet>();
        fullBoddy = playerMovemet.gameObject;

        gameManager = FindObjectOfType<GameManager>();

        targetPoint = GameObject.FindGameObjectWithTag("Target");
        Jumpflor = GameObject.Find("JumpFlor");
        playercreatpoint = GameObject.FindGameObjectWithTag("PlayerCreatpoint"); ;

        
        //Startlevel(); //call form button PlayAndExit
    }




    /// <summary>
    /// game entry porint  *************************************************************
    /// </summary>
    public void Startlevel()
    {
        // check if level hase update 
        lastlevel =  gameManager.GetLastLevel();
        SetGoal(lastlevel+1);
        Debug.Log(lastlevel + 1);
    }




    //*************************************************************target goal
    // Lastlevel play  will come form game Manager

    private void SetGoal(int LevelPlaying)
    {
        Jumpflor.transform.position = new Vector3(Jumpflor.transform.position.x, LevelPlaying, Jumpflor.transform.position.z);
        fullBoddy.transform.position = playercreatpoint.transform.position;
        //call at starta
        if (LevelPlaying < 4)
        {
            targetMadel = 2; //silver medel
            targetPerformance = 2; // jump in 3 ring
            targetScore = 100 * LevelPlaying;
            targetFlip = 1;

        }
        else if (LevelPlaying >= 4 && LevelPlaying <= 7)
        {
            targetMadel = 2;
            targetPerformance = 5;
            targetScore = 100 * LevelPlaying;
            targetFlip = 2;
        }
        else 
        {
            targetMadel = 3;
            targetPerformance = 10;
            targetScore = 120 * LevelPlaying;
            targetFlip = 3;
        }
        

        TeFlip.text = targetFlip.ToString();
        TeMedel.text = targetMadel.ToString();
        TeTotalScore.text = targetScore.ToString();

        TeScore.text = targetPerformance.ToString();
    }



    // all task is completed or not
    public void CheckStatus() 
    {
        scoreManager.totalScore = scoreManager.totalScore * scoreManager.Performance;
        //Debug.Log("Check status");
        //save data 
        if (scoreManager.flipCounter >= targetFlip)
        {
            //Debug.Log("FlipDone");

            //TeFlip.gameObject.SetActive(false);
            flip = true;
        }
        else 
        {
            flip = false;
        }
        if (scoreManager.totalScore >= targetScore)
        {
            //Debug.Log("TargetScore done");
            score = true;

        }
        else 
        {
            score = false;
        }
        if (scoreManager.Performance >= targetPerformance)
        {
            // show drive performanve 
            //Debug.Log("TargetPerformance");
            performance = true;
        }
        else 
        {
            performance = false;
        }
        if (scoreManager.medel >= targetMadel)
        {
            // Debug.Log("targetmedelIs pass");
            medel = true;
        }
        else 
        {
            medel = false;
        }
        
        if (flip && score && performance && medel)
        {
            //Debug.LogError("Complete");
            gameCompletePanal.SetActive(true);
            nextlevelButton.SetActive(true);


            int medel = scoreManager.MedelEarn();
            int goldearn = scoreManager.levelGold;
            
            gameManager.UpdateDataAndSave(lastlevel+1, goldearn, medel); //

            gameCompletePanal.GetComponent<GameCompletePanal>().ShowResult(goldearn, scoreManager.totalScore, medel);

            //Debug.Log(gameManager.playerStatus);

        }
        else 
        {
            //failed //
            gameCompletePanal.SetActive(true);
            nextlevelButton.SetActive(false);
            gameCompletePanal.GetComponent<GameCompletePanal>().ShowResult(scoreManager.levelGold, scoreManager.totalScore, 0);
            ReStart();
        }


    }


    // NEED TO CHECK PLAYER SCORE IF SCORE 1/3 THEN GIVE A CHANCE(FIRST TIME) IF PLAYE SCORE 2/3 GIVE SECOEND CHANCE ELSE FAILD   

    /// <summary>
    ///  Reset score manager and Player Positions 
    /// </summary>
    public void ReStart() 
    {
        // teset all again;
        // call player movemnt 
        triggerEnterCheck = false; // avoid 2 time triger enter

        flip = score = performance = medel = false;
        scoreManager.Retry();
        playerMovemet.Retry();
        // Debug.Log("restart");

    }



    private bool dathlockflag = false;
    /// <summary>
    /// Checking performace of player jump 
    /// </summary>
    private void ParformanceStatusCheck() 
    {
        if (scoreManager.Performance == 0)
        {

            //GameCompletePanal.SetActive(true);
            //nextlevelButton.SetActive(false);
            // faild but check status for and possible gole updated
            CheckStatus();
            Debug.Log("Retake");
        }
        else if (!dathlockflag && scoreManager.Performance == 1)
        {
            // Not updated check once again;

            dathlockflag = true; //avoid dath lock
            ParformanceStatusCheck();// dath lock

        }
        else
        {
            //Task is Completd check status

            
            CheckStatus();
            //gameCompletePanal.SetActive(true);
            //nextlevelButton.SetActive(false);
            //Debug.Log("Done");
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        //rDebug.LogError("colide");
        if (!triggerEnterCheck && other.gameObject.tag == "FullBody")
        {
            triggerEnterCheck = true;
            //Debug.Log(other.gameObject.name);
            ParformanceStatusCheck();
        }
    }

}

