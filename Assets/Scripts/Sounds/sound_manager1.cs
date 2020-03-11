using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sound_manager1 : MonoBehaviour {

    public GameObject audioo1, gameobjectt;
    AudioSource[] audii;
    string active_scene_name;
    public GameObject OldCouponsPanel;
    public KubikRub cubeScript1;
    
    void Start() {

        if (PlayerPrefs.GetInt("info", 1) == 1)
        {           
           
            audioo1.GetComponent<AudioSource>().mute = false;
            
            audii = gameobjectt.GetComponents<AudioSource>();
            for(int i = 0; i < audii.Length; i++)
            {
                //Debug.Log(i);
                audii[i].mute = false;
            }
        }
        else
        {
           
            audioo1.GetComponent<AudioSource>().mute = true;
            
            audii = gameobjectt.GetComponents<AudioSource>();
            for (int i = 0; i < audii.Length; i++)
            {
                //Debug.Log(i);
                audii[i].mute = true;
            }
        }

        active_scene_name = SceneManager.GetActiveScene().name;

    }
	
	
    
}
