using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAndExit : MonoBehaviour
{
    public GameObject PlayPanal;
    public LevelManager levelManager;

    public Text TotalGold, Currentlevel, goldMedel, SilverMedel, BronxeMedel; 


    private void Start()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        TotalGold.text = gm.TotalGold.ToString();
        Currentlevel.text = gm.CurrentLevel.ToString();
        goldMedel.text = gm.TotalGoldMedel.ToString();
        SilverMedel.text = gm.TotalSilverMedel.ToString();
        BronxeMedel.text = gm.TotalBornzeMedel.ToString();
    }
    public void OnPlayButtonPressed() 
    {
        levelManager.Startlevel();
        StartCoroutine(DeactivatePanl());
    }
    public void OnExitButtonPressed() 
    {
        Application.Quit();
    }
    IEnumerator DeactivatePanl() 
    {
        yield return new WaitForSeconds(.1f);
        transform.gameObject.SetActive(false);
    }
}
