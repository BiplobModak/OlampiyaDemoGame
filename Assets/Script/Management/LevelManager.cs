using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {

        PlayerStatus playerStatus = new PlayerStatus
        {
            Lastlevel = 5,
            playerPoints = 150,
            playerGold = 15,
            GoldMadel = 0,
            BronzeMedel = 0,
            SilvetMadel = 0  };
        

        string playerStatusJason = JsonUtility.ToJson(playerStatus);

        SaveData(playerStatusJason);

        string playerSaveData = GetSavedDate();

        //Debug.Log(playerStatus);

    }

    private string GetSavedDate() 
    {
        string playerData;
        if (PlayerPrefs.HasKey("PlayerData")) 
        {
            playerData = PlayerPrefs.GetString("PlayerData");
            PlayerStatus ps = JsonUtility.FromJson<PlayerStatus>(playerData);
            //Debug.Log(ps.GoldMadel);
            //Debug.Log(playerData);
            return playerData;
        }
        return null;
    }

    private void SaveData(string playerStatusJson) 
    {
        if (PlayerPrefs.HasKey("PlayerData"))
        {
            string Befordata = PlayerPrefs.GetString("PlayerData");
            PlayerPrefs.SetString("PlayerData", playerStatusJson);
            //Debug.Log("Befor" + Befordata);

        }
        else
        {
            PlayerPrefs.SetString("PlayerData", playerStatusJson);
            //Debug.Log(playerStatusJson);
            PlayerPrefs.Save();
        }
    }

    
    
    private class PlayerStatus 
    {
        /// <summary>
        /// Last level Player Completed
        /// </summary>
        public int Lastlevel;
        /// <summary>
        /// Player total point
        /// </summary>
        public int playerPoints;
        /// <summary>
        /// Total goldPlayer have
        /// </summary>
        public int playerGold;
        /// <summary>


        ///  Totalmedel Player have, There is 3 kind of medel Gold, Silver, Bronze.  
        /// </summary>
        public int GoldMadel;
        public int SilvetMadel;
        public int BronzeMedel;


    }
}

