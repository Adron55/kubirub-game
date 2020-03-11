using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rubbishbin : MonoBehaviour
{
    private int count = 0;
    public void closeCam1()
    {

        if (count % 2 != 0)
        {
            this.GetComponent<Cam1_1>().enabled = false;
            this.GetComponent<Cameraorbit1>().enabled = true;
        }
        else
        {
            this.GetComponent<Cam1_1>().enabled = true;
            this.GetComponent<Cameraorbit1>().enabled = false;
        }
        Debug.Log(count);
        count++;
    }
}
