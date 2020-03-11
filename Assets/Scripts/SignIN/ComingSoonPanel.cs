using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComingSoonPanel : MonoBehaviour {

	public GameObject comSo;
	private bool f;

	public void comSoonStart(){
        this.GetComponent<AudioSource>().Play();

        comSo.SetActive (true);
		f = false;
		StartCoroutine (closePopUp());
        ;
	}

	IEnumerator closePopUp(){
	
		yield return new WaitForSeconds (1f);
		comSo.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
