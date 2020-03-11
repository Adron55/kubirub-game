using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sounddd : MonoBehaviour {

    public Toggle sound;
    public AudioSource audio;

    public GameObject audio1, audio2, audio3;

    public GameObject optionsMenu, levelmanager;
    public GameObject forBackButton;

    // Use this for initialization
    void Start()
    {

        if (PlayerPrefs.GetInt("info", 0) == 1)
        {
            audio.mute = false;
        }
        else
        {
            audio.mute = true;
        }

        sound_setter();


    }

    public void sound_setter()
    {
        if (PlayerPrefs.GetInt("info", 0) == 1)
        {
            audio1.GetComponent<AudioSource>().mute = false;
            audio2.GetComponent<AudioSource>().mute = false;
            audio3.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            audio1.GetComponent<AudioSource>().mute = true;
            audio2.GetComponent<AudioSource>().mute = true;
            audio3.GetComponent<AudioSource>().mute = true;
        }

    }
}
