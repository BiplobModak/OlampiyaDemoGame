using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCompletePanal : MonoBehaviour
{
    public LevelManager levelManager;
    public Text Medel,  Gold , Score;
    public GameObject NextButton;
    private void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("Water").GetComponent<LevelManager>();
    }


    public void ShowResult(int gold, int score, int medel) 
    {
        Gold.text = gold.ToString();
        Score.text = score.ToString();
        // Debug.Log(medel);
        switch(medel)
        {
            case 3:
                Medel.text = "Gold Medel";
                break;
            case 2:
                Medel.text = "Silver Medel";
                break;
            case 1:
                Medel.text = "Bronze Medel";
                break;
            default:
                Medel.text = "No Medel Earn";
                break;
        }

    }







    public void OnNextLevelPlayButtonPressed() 
    {
        //Call nextLevel from level Manager

        levelManager.Startlevel();
        levelManager.ReStart();

        NextButton.SetActive(false);
        transform.gameObject.SetActive(false);

        //SceneManager.LoadScene("Demo");

    }
    public void OnRetryButtonPressed() 
    {
        //Retry
        levelManager.ReStart();
        transform.gameObject.SetActive(false);
    }
    public void QuitGamePlay() 
    {
        Application.Quit();
    }
}
