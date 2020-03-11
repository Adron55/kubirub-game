using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bringpanel : MonoBehaviour {

    public GameObject narbutton;
    public GameObject inputfieldsforcanvas;
	void Awake(){
		Screen.orientation = ScreenOrientation.Portrait;
	}
    public void mypanel()
    {
        narbutton.SetActive(true);
        inputfieldsforcanvas.SetActive(false);
    }
    public void cancelbutton()
    {
        narbutton.SetActive(false);
        inputfieldsforcanvas.SetActive(true);
    }

}
