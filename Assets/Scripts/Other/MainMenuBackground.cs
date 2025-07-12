using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackground : MonoBehaviour
{
    public float hiz = 250f; 

    void Update()
    {
        
        transform.position += new Vector3(0, -hiz * Time.deltaTime, 0);

        
        if (transform.position.y < -500f) 
        {
            float arkaplanYukseklik = 0; 
            transform.position = new Vector3(0, arkaplanYukseklik, 0);
        }
    }
}

