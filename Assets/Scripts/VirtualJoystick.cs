using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    //public Camera camera;
    private Image bgImg;
    private Image JoystickImg;
    private Vector3 inputVector;
 //   private float x = 0;
 //   private float y = 0;

    private void Start()
    {
        bgImg = GetComponent<Image>();
        JoystickImg = transform.GetChild(0).GetComponent<Image>();

    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            //Move Joystick Img
            JoystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 4), inputVector.z * (bgImg.rectTransform.sizeDelta.y / 4));

        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        JoystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }
    
/*
    public void Init()
    {
        // kameranin baxmasi
        var angles = transform.eulerAngles;
        x = angles.y;
        y = 30;
    }
    */

}
