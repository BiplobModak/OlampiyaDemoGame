using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// if true Flip Score Counter will start
    /// </summary>
    public bool FlipStart, flipcalculatorHelper;


    public Transform target, fullbody;

    public float fliptimer = 0f;
    public int flipCounter = 0;


    public int TotalScore = 0;
    public int medel = 0;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (FlipStart)
        {
            //fliptimer += Time.deltaTime;
            //if (fliptimer > .42f)
            //{
            //    fliptimer -= .42f;
            //    flipCounter += 1;
            //}
            Debug.Log(fullbody.eulerAngles);
            if (flipcalculatorHelper && fullbody.eulerAngles.x > 0f && fullbody.eulerAngles.x < 10f) 
            {
                flipcalculatorHelper = false;
                flipCounter += 1;
            }
            if (fullbody.eulerAngles.x > 300f && fullbody.eulerAngles.x < 360f) 
            {
                flipcalculatorHelper = true;
            }
        }
    }


    private void FlipScore() 
    {
        TotalScore = 10 * flipCounter;

    }


    private void DiatanceScore(GameObject player) 
    {
        float distance = Mathf.Abs( target.position.z - player.transform.position.z);

        if (distance < .5f)
        {
            medel = 3;
        }
        else if (distance < 1f && distance > .5f)
        {
            medel = 2;
        }
        else if (distance < 1.5f && distance > 1f)
        {
            medel = 1;
        }
        else 
        {
            medel = 0;
        }
       // Debug.Log(distance);
        
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FullBody") 
        {
            DiatanceScore(other.gameObject);
            Debug.Log(other.transform.localEulerAngles);
            Debug.Log(other.transform.rotation);
            Debug.LogError(other.transform.eulerAngles);
        }
    }

    
}
