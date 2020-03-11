using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class backgroundForButtons : MonoBehaviour {
	void Awake(){
		MainFunction.ClearNotifications ();
	}

	public void OneTime(){
		MainFunction.SendNotification (1,5000,"Fred","My Long Things",new Color32(0xff,0x44,0x44,255));
	}
}
