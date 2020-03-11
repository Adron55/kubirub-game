using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingScreen : MonoBehaviour {
    public float say;
    public Text loadingScreen,dots,dots12;
    public GameObject imgLoad,screen,mainMenu;
    public Transform Cube;
    public KubikRub cubeScript_load;
    // Use this for initialization
    void Start () {
        cubeScript_load = Cube.GetComponent<KubikRub>();
    }
	
	// Update is called once per frame
	void Update () {
        if (say < 100)
        {
       //     mainMenu.SetActive(false);
            say += Time.deltaTime*3;
            screen.SetActive(true);
            loadingScreen.text = "" + (int)say+"%";
            cubeScript_load.gameObject.SetActive(false);
        }
        else if (say >= 100)
        {
         //   mainMenu.SetActive(true);
            say = 100;
            screen.SetActive(false);
            loadingScreen.text = "" + (int)say+"%";
            cubeScript_load.gameObject.SetActive(true);
        }
        dots12.text = "Loading";
        if ((say > 0 && say < 4)|| (say > 12 && say < 16) || (say > 24 && say < 28) || (say > 36 && say < 40) || (say > 48 && say < 52) || (say > 60 && say < 64)|| (say > 72 && say < 76)|| (say > 84 && say < 88)|| (say>96&&say<98))
        {
            dots.text =  ".  ";
        }
        else if ((say > 4 && say < 8)|| (say > 16 && say < 20)|| (say > 28 && say < 32)|| (say > 40 && say < 44)|| (say > 52 && say < 56)|| (say > 64 && say < 68)|| (say > 76 && say < 80)|| (say > 88 && say < 92)||(say>98&&say<=100))
        {
            dots.text = ".. ";
        }
        else if ((say > 8 && say < 12)|| (say > 20 && say < 24)|| (say > 32 && say < 36)|| (say > 44 && say < 48)|| (say > 56 && say < 60)|| (say > 68 && say < 72)|| (say > 80 && say < 84) || (say > 92 && say < 96))
        {
            dots.text = "...";
        }
        imgLoad.transform.localScale = new Vector3(say / 100, 1, 1);
    }
}
