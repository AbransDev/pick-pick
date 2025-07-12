using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float hiz = 1.0f; 

    void Update()
    {
        
        transform.position += new Vector3(0, -hiz * Time.deltaTime, 0);

        
        if (transform.position.y < -5f) 
        {
            float arkaplanYukseklik = 5f; 
            transform.position = new Vector3(0, arkaplanYukseklik, 0);
        }
    }
}

