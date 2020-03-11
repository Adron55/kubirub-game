using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//<summary>
//in this script there is scene names which is choosed by index or name,
//be careful.
//
//</summary>

public class test : MonoBehaviour
{

    public GameObject levelmanager, wholeMenu, panel, toggle, homeButton, soundd,cameraObject;
    public GameObject[] arrGm, panels;
    private Animator anim, anmaa;
    private Animation animationClip, animColora, animToggle;
    public AnimationClip clip, colorChange, toggleClip;
    public GameObject settingPanel;
    private Vector3 initPosOfMenu;
    private Quaternion quat;
    public float speed = 0.3f;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    public void Start()
    {
        anim = panel.GetComponent<Animator>();
        animToggle = toggle.GetComponent<Animation>();
        initPosOfMenu = new Vector3(0, -400, 0);
        quat = panel.transform.rotation;
        if (PlayerPrefs.GetInt("info") == 1)
        {
            toggle.GetComponent<Toggle>().isOn = true;
            playToggle();
        }
    }
    public void show()
    {
        anim.SetTrigger("open");
        // opener();
        homeButton.gameObject.SetActive(false);
    }
    public void opener(bool f = true)
    {
        float y = panel.transform.position.y;
        float x = panel.transform.position.x;
        float z = panel.transform.position.z;
        if (f)
            speed = Mathf.Abs(speed);
        else
            speed = -speed;
        while ((f && y <= 8) || (!f && y >= -483.4))
        {
            Vector3 vecE = new Vector3(x, 8, z);
            Vector3 targetPosition = panel.transform.TransformPoint(vecE);
            Vector3 vecI = new Vector3(x, -483.4f, z);

            //panel.transform.position = Vector3.Lerp(vecI,vecE,speed);
            panel.transform.position = Vector3.SmoothDamp(panel.transform.position, targetPosition, ref velocity, smoothTime);

            y = panel.transform.position.y;
        }
    }
    public void close(int id = -1)
    {

        //panel.GetComponent<Animator>().enabled = false;
        //panel.transform.SetPositionAndRotation(initPosOfMenu,quat);
        if (!homeButton.active)
        {
            anim.SetTrigger("close");
            //opener(false);
            if (id != -1)
            {
                arrGm[id].transform.GetChild(0).GetComponent<Animation>().enabled = false;
                arrGm[id].transform.GetChild(0).transform.GetChild(0).GetComponent<Animation>().enabled = false;

                arrGm[1].transform.GetChild(0).GetComponent<Image>().sprite = null;
                arrGm[1].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color32(0, 60, 219, 255);

            }

            homeButton.gameObject.SetActive(true);
        }
        //arrGm[id].transform.GetChild(0).GetComponent<Animation>().enabled = true;
        //arrGm[id].transform.GetChild(0).transform.GetChild(0).GetComponent<Animation>().enabled = true;
    }
    public void Playy(int id)
    {
        animationClip = arrGm[id].transform.GetChild(0).GetComponent<Animation>();
        animColora = arrGm[id].transform.GetChild(0).transform.GetChild(0).GetComponent<Animation>();
        animationClip.clip = clip;
        animColora.clip = colorChange;
        animColora.Play();
        animationClip.Play("click");

        // wholeMenu.transform.position =initPosOfMenu;
        close(id);
        if (id == 1 || id == 3)
            openPanel(id);
        else
            openScene(id);
    }
    public void playToggle()
    {
        AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();
        bool f = toggle.GetComponent<Toggle>().isOn;
        animToggle.clip = toggleClip;
        if (f)
        {
            animToggle.clip = toggleClip;
            animToggle["Toggle"].speed = 1;
            animToggle.Play("Toggle");
            PlayerPrefs.SetInt("info", 1);
            audioSource.mute = false;
        }
        else
        {

            animToggle["Toggle"].speed = -1;
            animToggle["Toggle"].time = animToggle["Toggle"].length;
            animToggle.Play("Toggle");
            PlayerPrefs.SetInt("info", -1);
            audioSource.mute = true;
        }
        soundd.GetComponent<sounddd>().sound_setter();
    }
    public void openPanel(int id)
    {//bu kodu duzelt
        if (id == 1)
            id = 2;
        panels[Mathf.Abs(id - 3)].SetActive(true);//because in array settings panel put on 0th element
        if (cameraObject.GetComponent<Cam1_1>().enabled)
            cameraObject.GetComponent<Cam1_1>().enabled=false;
    }
    public void openScene(int id)
    {
        string name;
        switch (id)
        {
            case 0:
                name = "My Profile";
                break;
            case 2:
                name = "Discounts";
                break;
            default:
                name = "My Profile";
                break;
        }
        levelmanager.GetComponent<LevelManager>().openScenes(name);
    }
    public void quit()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && !homeButton.active && Input.mousePosition.y > (Screen.height * 11.75 / 100))
        {
            close();
        }
    }

}
