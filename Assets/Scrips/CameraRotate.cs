using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class CameraRotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Vector3 d = new Vector3(0,0,0);
    public Button iso_view;
    public Button top_view;

    public Camera camera;
    public Light lightsource;

    private Vector3 dragStart;
    public int dragSpeed = 15;
    private bool dS = false;
    private bool istop = false;

    private int scrollWheel = 0;

    private float view;

    Vector3 targetPosition;
    public float smoothTime = 1f;
    private Vector3 velocity = Vector3.zero;
    private GameObject crane;

    private Coroutine RCR;
    private Coroutine SCR;

    private Coroutine Loading;
    private bool startLoading = false;

    private CameraControlls controlls;

    private void Awake()
    {
        controlls = new CameraControlls();
    }
    void Start()
    {
        //Error: not instantiated
        //iso_view.onClick.AddListener(delegate{CameraView("iso");});
        //top_view.onClick.AddListener(delegate{CameraView("top");});
        targetPosition = transform.position;
        crane = GameObject.FindWithTag("Crane");
    }

    private void OnEnable()
    {
        controlls.Enable();
    }

    private void OnDisable()
    {
        controlls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //MouseCameraMove();
        //scrollZoom();
        if(!Globals.isGameStart && !startLoading){
            IEnumerator lr = rotateLoading();
            Loading = StartCoroutine(lr);
            startLoading = true;
        }
        if(Globals.isGameStart && startLoading){
            StopCoroutine(Loading);
            CameraView("iso");
            startLoading = false;
        }

        if (transform.position.y <= targetPosition.y-0.01) {
            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }


    }

    private void OnSeeView(InputValue input){
        // Debug.Log(input);
        float v = input.Get<float>();
        print(v);
        if(v == -1){
            CameraView("iso");
            Globals.cameraRotation = 0;
        }else if(v == 1){
            CameraView("top");
        }

    }

    private void OnLook(InputValue input){
        float v = input.Get<float>();
        print(v);
        Vector3 angles = transform.localRotation.eulerAngles;
        if(v == -1){
            //rotateCon(-45);
            print(Globals.cameraRotation);
            Globals.cameraRotation -= 1;
            print(Globals.cameraRotation);
            if(Globals.cameraRotation == -1){
                Globals.cameraRotation =3;
            }
            StartCoroutine(smoothRotate(-90));
        }else if(v == 1){
            print(Globals.cameraRotation);
            Globals.cameraRotation += 1;
            print(Globals.cameraRotation);
            if(Globals.cameraRotation == 4){
                Globals.cameraRotation =0;
            }
            //rotateCon(45);
            StartCoroutine(smoothRotate(90));
        }

    }

    private void OnLookCon(InputValue input){
        float v = input.Get<float>();
        print(v);
        Vector3 angles = transform.localRotation.eulerAngles;
        IEnumerator cr = rotateConE(-45, 0.5f);
        if(v == -1){
            //print("s2");
            //InvokeRepeating("rotateCon", 0.1f, 0.25f);
            cr = rotateConE(-45, 0.5f);
            RCR = StartCoroutine(cr);
        }
        else if(v == 1){
            //print("start2");
            //InvokeRepeating("rotateCon", 0.1f, 0.25f);
            cr = rotateConE(45, 0.5f);
            RCR = StartCoroutine(cr);
        }else{
            //CancelInvoke();
            if(RCR != null){
                StopCoroutine(RCR);
            }
        }

    }

    IEnumerator rotateConE(int rd, float repeatRate){
        while(true){
            rotateCon(rd);
            yield return new WaitForSeconds(repeatRate);
        }
    }
    IEnumerator rotateLoading(){
        while(true){
            rotateCon(1);
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator smoothRotate(int angle){
        int rotateDir = -5;
        if(angle <0){
            rotateDir = 5;
            angle = Mathf.Abs(angle);
            print(angle);
        }
        int s = 0;
        while(s<angle){
            rotateCon(rotateDir);
            yield return new WaitForSeconds(0.001f);
            s+=5;
        }

    }



    private void rotateCon(int rd){
        Vector3 angles = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(angles.x, angles.y + rd, angles.z);
    }

    private void OnScrollZoom(InputValue input){
        float v = input.Get<float>();
        print(v);
        if(v>0){
            scrollCam(1);
        }else{
            scrollCam(-1);
        }

    }

     private void OnScrollZoomCon(InputValue input){
        float v = input.Get<float>();
        IEnumerator cr = scrollCon(1, 0.5f);
        print(v);

         if(v == -1){
            cr = scrollCon(-1, 0.5f);
            SCR = StartCoroutine(cr);
        }
        else if(v == 1){
            cr = scrollCon(1, 0.5f);
            SCR = StartCoroutine(cr);
        }else{
            if(SCR != null){
                StopCoroutine(SCR);
            }
        }
            
    }
    IEnumerator scrollCon(int scrollDir, float repeatRate){
        while(scrollDir !=0){
            scrollCam(scrollDir);
            yield return new WaitForSeconds(repeatRate);
        }
    }
    private void scrollCam(int scrollDir){
        Camera.main.fieldOfView += -1*scrollDir;

        if(Camera.main.fieldOfView >= 20){
            Camera.main.fieldOfView = 20;
        }
        if(Camera.main.fieldOfView <= 10){
            Camera.main.fieldOfView = 10;
        }
    }

    void CameraView(string type){
        if(type == "iso"){

            transform.rotation = Quaternion.Euler(30, 0, 0);
            transform.position = new Vector3(0,3,0);
            //Camera.main.transform.position = new Vector3(-30.6f, 28.0f, -30.6f);
            //Camera.main.transform.position -= new Vector3(0, 0, 1.6f);
            //Camera.main.orthographicSize = 10;
            lightsource.intensity = 1;
            crane.SetActive(true);

        }else if(type == "top"){

            transform.rotation = Quaternion.Euler(90, 0, 0);
            Camera.main.transform.position = new Vector3(0,45+Globals.levelnumber * Globals.gameHeight,0);
            Camera.main.orthographicSize = 10;
            //lightsource.intensity = 0.5f;
            crane.SetActive(false);

        }

    }

    public void moveUpDown(float upMovement)
    {
        targetPosition = transform.position + new Vector3(0f, upMovement, 0f);
    }
    public void moveReset(float resetNumber){
        targetPosition = transform.position = new Vector3(0f, resetNumber, 0f);
    }
    /* private void MouseCameraMove(){
        if(!istop){
            if(Input.GetMouseButtonDown(0) && !dS){
                dragStart = Input.mousePosition;
                //print(dragStart);
                dS = true;
            }
            if(dS == true){
                //if we move to the left then the value is positive, right is negative
                float tomove = dragStart.x - Input.mousePosition.x;
                Vector3 angles = transform.localRotation.eulerAngles;
                transform.localRotation = Quaternion.Euler(angles.x, angles.y + tomove*dragSpeed * Time.deltaTime, angles.z);
                //transform.Rotate(Vector3.up* tomove*dragSpeed * Time.deltaTime);
                dragStart = Input.mousePosition;
            }
            if(Input.GetMouseButtonUp(0) && dS){
                dS = false;
            }
        }
    } */

/*     //limited to 120 and 40, when using perspective view
    private void scrollZoom(){
        if(Input.mouseScrollDelta.y != 0){
            Camera.main.fieldOfView += -1 * 5*Input.mouseScrollDelta.y;
        }
        if(Camera.main.fieldOfView >= 120){
            Camera.main.fieldOfView = 120;
        }
        if(Camera.main.fieldOfView <= 40){
            Camera.main.fieldOfView = 40;
        }
        
    } */
    //when using orthographic view
/*     private void scrollZoom(){
        if(Input.mouseScrollDelta.y != 0){
            Camera.main.orthographicSize += -1 * 1*Input.mouseScrollDelta.y;
        }
        if(Camera.main.orthographicSize >= 35){
            Camera.main.orthographicSize = 35;
        }
        if(Camera.main.orthographicSize <= 5){
            Camera.main.orthographicSize = 5;
        }
        
    }
 */
}
