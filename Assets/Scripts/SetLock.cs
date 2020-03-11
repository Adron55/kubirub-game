using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ToastPlugin;
public class SetLock : MonoBehaviour {
    public GameObject camMain;
    public bool locked = true;
    public AudioSource clickForToggleCam;
    private string infoText;
    public GameObject backgroundImage;
    public void Lock(bool togg)
    {
        camMain.GetComponent<Cam1_1>().enabled = togg;
        camMain.GetComponent<Cam1_1>().enabled = !togg;
        backgroundImage.GetComponent<Image>().enabled = togg;
        backgroundImage.GetComponent<Image>().enabled = !togg;
        if (togg) {
            locked = false;
			StartCoroutine (showPopUp());
		} else {
            locked = true;
			clickForToggleCam.Play ();
		}
    }
    IEnumerator showPopUp() {
        int l = PlayerPrefs.GetInt("Language");
        switch (l) {
            case 0:
                infoText= "Camera is locked";
                break;
            case 1:
                infoText = "Камера заблокирована";
                break;
            case 2:
                infoText = "Kamera kapatıldı";
                break;
            case 3:
                infoText = "Kamera bağlandı";
                break;
            default:
                infoText = "Camera is locked";
                break;
        }
        ToastHelper.ShowToast(infoText);
        yield return null;
    }
	IEnumerator lockPopUp(){
        int l = PlayerPrefs.GetInt("Language");
        switch (l)
        {
            case 0:
                infoText = "Camera is unlocked";
                break;
            case 1:
                infoText = "Камера разблокирована";
                break;
            case 2:
                infoText = "Kamera açık";
                break;
            case 3:
                infoText = "Kamera açıldı";
                break;
            default:
                infoText = "Camera is locked";
                break;
        }
        ToastHelper.ShowToast(infoText);
        yield return null;
    }
    public bool getLocked()
    {
        return locked;
    }
}
