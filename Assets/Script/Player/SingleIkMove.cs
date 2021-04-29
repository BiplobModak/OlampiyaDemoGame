using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleIkMove : MonoBehaviour
{
    public Transform MaxIKPosition, IkStartPosition;
    Vector3 startpositio;
    public bool Spin;
    float flippoint = 0f;
    int touchpoint;
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (Spin) 
        {
            flippoint += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, MaxIKPosition.position, flippoint);
        }
        if (!Spin) 
        {
            
            transform.position = Vector3.Lerp(transform.position, IkStartPosition.position, 1f);
            flippoint = 0f;
        }
    }
}
