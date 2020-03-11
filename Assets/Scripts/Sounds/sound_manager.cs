using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sound_manager : MonoBehaviour {

    public GameObject audio;
    public int a;

   string active_scene_name;
    public GameObject SelectPicture, PanelforPass, quitt, goForQuit;
    public GameObject panel;
    public KubikRub cubeScript1;
    // Use this for initialization
    void Start () {

        if(PlayerPrefs.GetInt("info", 1) == 1)
        {
            audio.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            audio.GetComponent<AudioSource>().mute = true;
        }
        active_scene_name =  SceneManager.GetActiveScene().name;

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.Escape) && active_scene_name != "Passwordtest" && active_scene_name != "signup" && active_scene_name != "Discounts&gifts" && active_scene_name == "Myprofile")
        {

            if (active_scene_name != "rubicsCube")
            {
                if (SelectPicture.active)
                {
                    SelectPicture.SetActive(false);
                    panel.gameObject.SetActive(true);
                    //sound_setter();

                }
                else if(PanelforPass.active)
                {
                    PanelforPass.SetActive(false);
                    panel.gameObject.SetActive(true);
                }
                else if(quitt.active)
                {
                    goForQuit.GetComponent<Password>().disappear();
                    quitt.SetActive(false);
                }
                else
                {
                    GameObject cube = GameObject.Find("superCube");
                    
                    
                    SceneManager.LoadScene("Game");
                }
            }

        }

    }
}
