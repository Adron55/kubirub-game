using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionError : MonoBehaviour
{
    Image nese;
    Canvas canvas;
    GameObject error;

    /// <summary>
    /// Creates an error with the 'text' on the 'errorPrefab', as a child of the 'canvas'
    /// </summary>
    /// <param name="text"></param>
    /// <param name="canvas"></param>
    public ConnectionError(GameObject errorPrefab, string text, Canvas canvas)
    {
        this.canvas = canvas;
        this.error = Instantiate(errorPrefab, canvas.transform);
        this.error.SetActive(false);
        this.error.transform.GetChild(0).GetComponent<Text>().text = text;       
    }

    public void displayInternetConnectionError()
    {
        this.error.SetActive(true);
    }
    /// <summary>
    /// Deletes all instances of connection error
    /// </summary>
    public static void deleteInternetConnectionError()
    {
        GameObject[] errors;
        errors = GameObject.FindGameObjectsWithTag("Error");
        print(errors);
        for(int i = errors.Length-1; i > -1; --i)
        {
            Destroy(errors[i]);
       }
    }
}
