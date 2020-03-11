using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startthegame : MonoBehaviour {

    public GameObject CubeSelf;
    public GameObject Button;

    private KubikRub Cubescript;

	// Use this for initialization
    public void open()
    {
        DontDestroyOnLoad(gameObject);
        CubeSelf.SetActive(true);
        Button.SetActive(false);
    }
}
