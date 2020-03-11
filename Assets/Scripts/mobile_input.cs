using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class mobile_input : MonoBehaviour {
    public GameObject input_mobile;
    public InputField inputfield;

    //public Toggle togg11;

	// Use this for initialization
	void Start () {
        TouchScreenKeyboard.hideInput = true;

    }
	public void ssler()
    {
        inputfield.contentType = InputField.ContentType.Password;
        TouchScreenKeyboard.hideInput = true;
    }
    public void sssss()
    {
        inputfield.contentType = InputField.ContentType.Standard;
        TouchScreenKeyboard.hideInput = true;
    }
    public void visible(bool tog)
    {
        if (tog)
        {
            inputfield.contentType = InputField.ContentType.Password;
        }
        else
        {
            inputfield.contentType = InputField.ContentType.Standard;
        }
    }
}
