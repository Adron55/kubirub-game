using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class Manager_beforeLoginAndRegister : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt("info", 1) == 1)
        {
            GetComponent<AudioSource>().mute = false;
        }
        else
        {
            GetComponent<AudioSource>().mute = true;
        }
    }


    public void openScene_Login()
    {
        SceneManager.LoadScene("Login");
    }

    public void openScene_Register()
    {
        SceneManager.LoadScene("Register");
    }
    
}
