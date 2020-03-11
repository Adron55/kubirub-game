using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cam1_1 : MonoBehaviour {
    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;
    public Camera mainCamera;
    Vector2 firstTouchPrevPos, secondTouchPrevPos;
    [SerializeField]
    float zoomModifierSpeed = 0.5f;
    public int direction = 1;
    public Slider fred;
    public GameObject Target;
    public GameObject Camera;
    public Transform target;
    public float distance;
    public float xSpeed = 90.0f;
    public float ySpeed = 90.0f;
    public float yMinLimit = 60f;
    public float yMaxLimit = 60f;
    public float distanceMin = 4f;
    public float distanceMax = 15f;
    int counter = 0;
    public float dist;
    public bool dragControl = false;
    private bool f = false, camDirec = true, xPos = true,zPos=true, aniRev = false,doneIn=true;
    public GameObject tutorialPanel;
    public Image camTutorial;
    float x = 0.0f;
    float y = 0.0f;
    private float pX = 4.4f, pY=3.75f;
    Quaternion rotation;
    private void Awake()
    {
        distance = fred.value;
    }
    void Start()
    {
        
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        distance = fred.value;
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
        
        dist = Mathf.Abs(Target.transform.position.z - Camera.transform.position.z);
       transform.position = new Vector3(0,0, Target.transform.position.z - fred.value);
        if (PlayerPrefs.GetInt("tutorial") == 1 && f == false)
        {
            showTutorialCam();
        }


    }
  
    private void showTutorialCam()
    {
       // tutorialPanel.SetActive(true);
        f = true;
        PlayerPrefs.SetInt("tutorial",2);
        StartCoroutine(finishTutorial());
    }
    void Update()
    {
        if (target && Input.touchCount == 1 &&  Input.GetTouch(0).phase == TouchPhase.Moved) //Just orbit touch without movement
        {
            Orbit(Input.GetTouch(0));
            //x += Input.GetTouch(0).deltaPosition.x * xSpeed * 0.02f/*distance*/ ;
            //y -= Input.GetTouch(0).deltaPosition.y * ySpeed * 0.02f;

        }
         else if (Input.touchCount == 2)

            {
               // Debug.Log("Ikinci if ishleyir!");
                if (Input.GetTouch(0).position.x > Screen.height/4 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {

                     Orbit(Input.GetTouch(0)); //Movement was touched second
                  //  Debug.Log(Input.GetTouch(0));
                    //x += Input.GetTouch(0).deltaPosition.x * xSpeed * 0.02f/*distance*/ ;
                    //y -= Input.GetTouch(0).deltaPosition.y * ySpeed * 0.02f;
                }
                else if (Input.GetTouch(1).position.x > Screen.height*2 && Input.GetTouch(1).phase == TouchPhase.Moved)
                {

                     Orbit(Input.GetTouch(1)); //Movement was touched first
                    //x += Input.GetTouch(1).deltaPosition.x * xSpeed * 0.02f/*distance*/ ;
                    //y -= Input.GetTouch(1).deltaPosition.y * ySpeed * 0.02f;
                }
            }
        //if (Input.touchCount == 2)
        //{
        //    Touch firstTouch = Input.GetTouch(0);
        //    Touch secondTouch = Input.GetTouch(1);

        //    firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
        //    secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

        //    touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
        //    touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

        //    zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;
        //    //if (touchesPrevPosDifference > touchesCurPosDifference)
        //    //    Camera.GetComponent<Camera>()
        //    //    mainCamera.orthographicSize += zoomModifier;
        //    //if (touchesPrevPosDifference < touchesCurPosDifference)
        //    //    distance += zoomModifier;
        //    //mainCamera.orthographicSize -= zoomModifier;
        //    Debug.Log("Zoom "+zoomModifier);
        //}

    }
    void Orbit(Touch touch)
    {
        x += (direction * touch.deltaPosition.x) * (xSpeed) * 0.004f/*distance*/ ;//eger touch.deltaPosition.x menfi olsa onda ters firlanar
        y -= (touch.deltaPosition.y) * (ySpeed) * 0.004f;


        if (transform.GetChild(0).position.y < transform.position.y)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }


     
        yMinLimit = -yMaxLimit;

        y = ClampAngle(y, yMinLimit, yMaxLimit);


        rotation = Quaternion.Euler(y, x, 0);

        Vector3 negDistance = new Vector3(0, 0, -distance);
        Vector3 position = rotation * negDistance + target.position;
        transform.rotation = rotation;
        transform.position = position;

    }

    public static float ClampAngle(float angle, float min, float max)
    {
        
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
    IEnumerator finishTutorial() {
        //yield return new WaitForSeconds(1);
        //camTutorial.gameObject.SetActive(false);
      //  tutorialPanel.SetActive(false);
        yield return null;

    }
    
}
