using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraorbit1 : MonoBehaviour {
    public GameObject tutorialpanel;
    public Transform target;
    public float distance = 5;
    public bool dragControl = false;

    public float sensitivityX = 1.0f;
    public float sensitivityY = 1.8f;
    //c#..1
    public float viewerYmin = 1.0f;
    private float yMinLimit = 10;
    public float yMaxLimit = 10;

    public float distanceMin = 3;
    public float distanceMax = 15;

    private float x = 0;
    private float y = 0;
    private float y0 = 0;

    //<--descend var
    private float startTime = 0;
    private float startAngle = 0;
    private bool descending = false;
    //descend var-->
    private float i1 = 0;
    public GameObject tutorialPanel;
    // c#..2
    //@script AddComponentMenu("Camera-Control/Mouse Orbit")

    void Start() {
            //Init();
            descending = false;
            // Make the rigid body not change rotation
            if (GetComponent<Rigidbody>())
                GetComponent<Rigidbody>().freezeRotation = true;
      //  Debug.Log(distance);
    }
        
    // Update is called once per frame
    public void LateUpdate()
    {
        if (target)
        {
            if (!dragControl || (dragControl && Input.GetMouseButton(1)))
            {
                x += Input.GetAxis("Mouse X") * sensitivityX*distance;
                y -= Input.GetAxis("Mouse Y") * sensitivityY;
                //Debug.Log(distance);
            }
            yMinLimit = -yMaxLimit;
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            var localrotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 4, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position))
            {
                // distance =  hit.distance;
            }

            var position = localrotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = localrotation;
            transform.position = position;

        }
        tutorialPanel.SetActive(false);
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
        
    }
}
