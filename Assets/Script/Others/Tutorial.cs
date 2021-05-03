using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour, IPointerDownHandler
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("Tutorial"))
        {
            int get = PlayerPrefs.GetInt("Tutorial");
            if (get == 1)
            {
                transform.gameObject.SetActive(false);
            }
            else 
            {
                PlayerPrefs.SetInt("Tutorial", 1);
            }
        }
        else 
        {
            PlayerPrefs.SetInt("Tutorial", 1);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //if (!PlayerPrefs.HasKey("Tutorial")) 
        //{
        //    PlayerPrefs.SetInt("Turorial", 1);
        //}
        PlayerPrefs.SetInt("Tutorial", 1);
        transform.gameObject.SetActive(false);
        
    }
}
