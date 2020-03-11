using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontdestroy : MonoBehaviour {

    private static GameObject instance;

    // Use this for initialization
    private void Awake()
    {

        DontDestroyOnLoad(gameObject);

    }


}
