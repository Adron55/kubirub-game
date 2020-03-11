using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
   public Text timerText=null;
    float time = 0;
    int SEC = 0;
    int MIN = 0;
    int HOUR = 0;
     void Update()
    {
        time += Time.deltaTime;
        fmm();
    }
    public void fmm()
    {
        SEC = (int)(time % 60);
        MIN = (int)((time / 60) % 60);
        HOUR = (int)((int)time / 60) / 60;
        timerText.text =  HOUR + ":" + MIN + ":" + SEC;
        //print(HOUR + ":" + MIN + ":" + SEC);
    }
}