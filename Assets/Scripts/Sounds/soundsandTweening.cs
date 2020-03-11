using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class soundsandTweening : MonoBehaviour {

    public Toggle sound_Toggle;
    public GameObject check_First;
    public AudioSource audio;

    private void Start()
    {
        audio.mute = audio.mute;
        audio = GetComponent<AudioSource>();
        check_First.gameObject.SetActive(true);

        // check_Second.gameObject.SetActive(false);
        //    Anime();
    }
    public void Anime()
    {
        if (sound_Toggle.isOn)
        {
            audio.mute = false;
            check_First.gameObject.SetActive(true);
         //   PlayerPrefs.SetInt("info", 1);
        }
        else
        {
            audio.mute = true;
            check_First.gameObject.SetActive(false);
        //    PlayerPrefs.SetInt("info", 0);
        }

    }
}
