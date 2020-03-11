using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class sceneloader : MonoBehaviour {




    private void Start()
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
    public void LoadScene2()
    {
        SceneManager.LoadScene("Game");
    }
}
