using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class newline : MonoBehaviour {

    EventSystem system;
    public Text textt;

    void Start()
    {
        //system = EventSystemManager.currentSystem;

    }
    // Use this for initialization
    public void Update()
    {
        if (Input.anyKeyDown)
        {
            //Debug.Log(Input.anyKeyDown.ToString());
            //textt.text = Input.inputString;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (EventSystem.current.currentSelectedGameObject != null)
                {
                    //Debug.Log("up");
                    Selectable selectable = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
                    if (selectable != null)
                        selectable.Select();
                }
            }
            else
            {
                
                if (EventSystem.current.currentSelectedGameObject != null)
                {
                    //Debug.Log("down");
                    Selectable selectable = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
                    if (selectable != null)
                        selectable.Select();
                }
            }
        }
    }

    public void down()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                Selectable selectable = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
                if (selectable != null)
                    selectable.Select();
            }
        }
    }

    public void test(GameObject texts)
    {
        texts.gameObject.SetActive(true);
    }
    public void testoff(GameObject texts)
    {
        texts.gameObject.SetActive(false);
    }

}
