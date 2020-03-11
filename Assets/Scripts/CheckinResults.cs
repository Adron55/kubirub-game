using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.IO;
using System;

public  class CheckinResults : MonoBehaviour {
    // private static Player
    /*
      This class works for when move 1 scene to another,supercube gameObject must not
      destroy,and if it is exist in project dont duplicate new one again...
     
     */
    private static CheckinResults playerInstance;
    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (playerInstance == null)
        {
            playerInstance = this;
            
        }
        else
        {
            DestroyObject(gameObject);
           
        }
        
    }
    float rotationSpeed = 0.2f;
}
