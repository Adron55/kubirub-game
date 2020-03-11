using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class shareScript : MonoBehaviour {

    private bool isProcessing = false;
    private bool isFocus = false;

    public void ShareBtnPress()
    {
        if (!isProcessing)
        {
#if UNITY_ANDROID
            StartCoroutine(ShareScreenshot());
#endif
        }
    }

    IEnumerator ShareScreenshot()
    {
        isProcessing = true;

        yield return new WaitForEndOfFrame();
        yield return new WaitForSecondsRealtime(0.3f);
        
        
            if (!Application.isEditor)
            {
                AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
                AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
                intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
                AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "https://goo.gl/3tRHjm");
                intentObject.Call<AndroidJavaObject>("setType", "text/plain");
                AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Kubirub download link");
                currentActivity.Call("startActivity", chooser);
                yield return new WaitForSecondsRealtime(1);
            }
        yield return new WaitUntil(() => isFocus);
        isProcessing = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }
}
