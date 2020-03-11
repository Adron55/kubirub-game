using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
public class PauseGame : MonoBehaviour {
    public GameObject internet_connectionError;
    //public Text test;
    //bool isPa=false;
    private void Start()
    {
        StartCoroutine(internetChecker());
    }
    //private void Update()
    //{
    //   // StartCoroutine(internetChecker());
    //}
    IEnumerator internetChecker()
    {
        if (HasConnection() == false)
        {
            internet_connectionError.SetActive(true);
        }
        else
        {
            internet_connectionError.SetActive(false);
        }
        yield return new WaitForSeconds(3);
        internet_connectionError.SetActive(false);
    }

    public static bool HasConnection()
    {
        try
        {
            using (var client = new WebClient()) ;
            using (var stream = new WebClient().OpenRead("http://www.google.com")) ;
            {
                return true;
            }
        }

        catch
        {
            return false;
        }
    }
}
