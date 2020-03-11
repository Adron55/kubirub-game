using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    #region singaltonStuff
    private static TouchManager _instance;
    public static TouchManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<TouchManager>();
            return _instance;
        }
    }
    #endregion

    public delegate void TouchDelegate(Touch eventData);
    public static event TouchDelegate OnTouchDown;
    public static event TouchDelegate OnTouchUp;
    public static event TouchDelegate OnTouchDrag;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (OnTouchDown != null)
                    OnTouchDown(touch);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (OnTouchUp != null)
                    OnTouchUp(touch);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (OnTouchDrag != null)
                    OnTouchDrag(touch);
            }
        }
    }
}
