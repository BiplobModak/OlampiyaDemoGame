using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class overView 

/// <summary>
///                                                                Awake:
///                                                                    don't destroy on load         prevent load once
///                                                                    getplayerStatus()
///                                                               
///                                                                Update data:
///                                                                        get update and save 
///                                                                        savePlayerstatus()
///                                                                        
///                                                                Getplayerstaus:
///                                                                        check player satus exgist or not
///                                                                      
///                                                                        if true
///                                                                            update player staus
///                                                                        else 
///                                                                            save all data to 0
///                                                                            saveplayerStus()
///                                                                            
///                                                                savePlayerStatus:
///                                                                        convert player status to json stign
///                                                                        save date
///                                                                        
/// 
///                                                                private class PlayerStatus:
///                                                                                Variabls:
///                                                                                        lastLevelplay
///                                                                                        totalGold
///                                                                                        totalGoldMedel
///                                                                                        totalSilverMedel
///                                                                                        totalBronzgold
/// </summary>
#endregion
public class GameManager : MonoBehaviour
{
    public string playerStatus;

    public int TotalGold { get => totalGold;}
    public int CurrentLevel { get => currentlevel;}
    public int TotalGoldMedel { get => goldMedel;}
    public int TotalSilverMedel { get => silverMedel;}
    public int TotalBornzeMedel { get => bornzeMedel; }

    private int totalGold, currentlevel, goldMedel, silverMedel, bornzeMedel;
    [SerializeField] PlayerStatus ps = new PlayerStatus();


    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        GetPlayarStatus();

        totalGold = ps.playerTotalGold;
        currentlevel = ps.lastLevelPlayerd + 1;
        goldMedel = ps.PlayerGoldMadel;
        silverMedel = ps.PlayerGoldMadel;
        bornzeMedel = ps.playerBronzMedel;
        Debug.Log(playerStatus);
        
    }
    

    /// <summary>
    /// Return ths last level 
    /// </summary>
    /// <returns>last level</returns>
    public int GetLastLevel()
    {
        GetPlayarStatus();
        return ps.lastLevelPlayerd;
    }


    /// <summary>
    /// updateing Player statuss like which level playing, how many gold earn in this , which medel get 
    /// </summary>
    /// <param name="currentlevel">Which level Currently Playing</param>
    /// <param name="goldEarn">how many gold earn</param>
    /// <param name="medelEarn">Which medel earn if Gold =>3, if Silver => 2, if bronez => 1</param>
    public void UpdateDataAndSave(int currentlevel, int goldEarn, int medelEarn)
    {
        Debug.Log("PlayerStatus");
        ps.lastLevelPlayerd = currentlevel;
        ps.playerTotalGold += goldEarn;
        if (medelEarn == 3)
        {
            ps.PlayerGoldMadel += 1; //make sure ps is updated before add 
        }
        else if (medelEarn == 2)
        {
            ps.PlayerSilverMedel += 1;
        }
        else 
        {
            ps.playerBronzMedel += 1;
        }
        SavePlayerStatus();
    }
    



    /// <summary>
    /// get player status from memory, check Playerstatus before call
    /// </summary>
    public void GetPlayarStatus()
    {

        if (PlayerPrefs.HasKey("PlayerStatus"))
        {
            playerStatus = PlayerPrefs.GetString("PlayerStatus");
            ps = JsonUtility.FromJson<PlayerStatus>(playerStatus);
            

        }
        else 
        {
            ps = new PlayerStatus()
            {
                lastLevelPlayerd = 0,
                playerTotalGold = 0,
                PlayerGoldMadel = 0,
                playerBronzMedel = 0,
                PlayerSilverMedel = 0
            };
            //playerStatus = JsonUtility.ToJson(ps);  //updat once in save player status
            //to save first update
            SavePlayerStatus();
        }
    }




    /// <summary>
    /// Set player status to memory  Update Player status befor call
    /// </summary>
    public void SavePlayerStatus()
    {
        playerStatus = JsonUtility.ToJson(ps);
        PlayerPrefs.SetString("PlayerStatus", playerStatus);
        PlayerPrefs.Save();
    }




    private class PlayerStatus
    {
        public int lastLevelPlayerd;
        public int playerTotalGold;
        public int PlayerGoldMadel;
        public int PlayerSilverMedel;
        public int playerBronzMedel;
    }

}
