using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class HyperLinker : MonoBehaviour, IPointerClickHandler
{
    public UnityEngine.Camera cam;

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(gameObject.GetComponent<TMP_Text>(), Input.mousePosition, null);
        if (linkIndex != -1)
        { // was a link clicked?
            TMP_LinkInfo linkInfo = gameObject.GetComponent<TMP_Text>().textInfo.linkInfo[linkIndex];

            // open the link id as a url, which is the metadata we added in the text field
            Application.OpenURL(linkInfo.GetLinkID());
        }
    }
}

