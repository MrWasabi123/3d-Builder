using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] blocks;
    // square field
    GameObject test;
    private bool ready;
    GameObject camera;
    public GameObject hinge;
    public bool smooth = false;
    private int fmr = 45;
    private int fmm = 5;

    public Button upButton;
    public Button downButton;
    public Button rightButton;
    public Button leftButton;
    public Button x_plus;
    public Button x_minus;
    public Button z_plus;
    public Button z_minus;
    public Button y_plus;
    public Button y_minus;

    

    void Start()
    {
        ready = true;
        //upButton.onClick.AddListener(delegate{BlockMove("up");});
        downButton.onClick.AddListener(delegate{BlockMove("down");});
        rightButton.onClick.AddListener(delegate{BlockMove("right");});
        leftButton.onClick.AddListener(delegate{BlockMove("left");});
        x_plus.onClick.AddListener(delegate{BlockRotate("x_plus");});
        x_minus.onClick.AddListener(delegate{BlockRotate("x_minus");});
        z_plus.onClick.AddListener(delegate{BlockRotate("z_plus");});
        z_minus.onClick.AddListener(delegate{BlockRotate("z_minus");});
        y_plus.onClick.AddListener(delegate{BlockRotate("y_plus");});
        y_minus.onClick.AddListener(delegate{BlockRotate("y_minus");});

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O) && ready){
            ready = false;
            int animalIndex = Random.Range(0, blocks.Length);
            Vector3 spawnPos = new Vector3(0,8,0);
            test = Instantiate(blocks[animalIndex], spawnPos, blocks[animalIndex].transform.rotation);
            test.GetComponent<Rigidbody>().useGravity = false;
            //HingeJoint h = hinge.GetComponent<HingeJoint>();
            //h.connectedBody = test.GetComponent<Rigidbody>();
        }
        if(Input.GetKeyDown(KeyCode.P) && !ready){
            test.GetComponent<Rigidbody>().useGravity = true;
            ready = true;
        }
        if(smooth == true){
            smoothmove(KeyCode.DownArrow, new Vector3(0.1f,0,0));
            smoothmove(KeyCode.UpArrow, new Vector3(-0.1f,0,0));
            smoothmove(KeyCode.LeftArrow, new Vector3(0,0,-0.1f));
            smoothmove(KeyCode.RightArrow, new Vector3(0,0,0.1f));
            smoothrotate(KeyCode.W, new Vector3(0,0,1));
            smoothrotate(KeyCode.A, new Vector3(-1,0,0));
            smoothrotate(KeyCode.S, new Vector3(0,0,-1));
            smoothrotate(KeyCode.D, new Vector3(1,0,0));
            smoothrotate(KeyCode.Q, new Vector3(0,-1,0));
            smoothrotate(KeyCode.E, new Vector3(0,1,0));
        }else{
            fixedmove(KeyCode.DownArrow, new Vector3(0.1f,0,0));
            fixedmove(KeyCode.UpArrow, new Vector3(-0.1f,0,0));
            fixedmove(KeyCode.LeftArrow, new Vector3(0,0,-0.1f));
            fixedmove(KeyCode.RightArrow, new Vector3(0,0,0.1f));
            fixedrotate(KeyCode.W, new Vector3(0,0,1));
            fixedrotate(KeyCode.A, new Vector3(-1,0,0));
            fixedrotate(KeyCode.S, new Vector3(0,0,-1));
            fixedrotate(KeyCode.D, new Vector3(1,0,0));
            fixedrotate(KeyCode.Q, new Vector3(0,-1,0));
            fixedrotate(KeyCode.E, new Vector3(0,1,0));
        }


        
        
    }

   void smoothrotate(KeyCode key, Vector3 direction){
        if(Input.GetKey(key) && !ready){
            test.transform.Rotate(direction);
        }
    }
    void smoothmove(KeyCode key, Vector3 direction){
        if(Input.GetKey(key) && !ready){
            test.transform.position += direction;
        }
    }
    void fixedrotate(KeyCode key, Vector3 direction){
        if(Input.GetKeyDown(key) && !ready){
            test.transform.Rotate(direction*fmr);
        }
    }
    void fixedmove(KeyCode key, Vector3 direction){
        if(Input.GetKeyDown(key) && !ready){
            test.transform.position += direction*fmm;
        }
    }

    void MoveUp(){
        if(!ready){
            test.transform.position += new Vector3(-0.1f,0,0)*fmm;
        }
    }
    public void BlockMove(string d){
        if(!ready){
            if(d == "up"){
                //test.transform.position += new Vector3(-0.1f,0,0)*fmm;
            }else if(d == "down"){
                test.transform.position += new Vector3(0.1f,0,0)*fmm;
            }else if(d == "left"){
                test.transform.position += new Vector3(0,0,-0.1f)*fmm;
            }else if(d == "right"){
                test.transform.position += new Vector3(0,0,0.1f)*fmm;
            }
        }
    }
    public void BlockRotate(string d){
        if(!ready){
            if(d == "x_plus"){
                test.transform.Rotate(new Vector3(1,0,0)*fmr);
            }else if(d == "x_minus"){
                test.transform.Rotate(new Vector3(-1,0,0)*fmr);
            }else if(d == "z_plus"){
                test.transform.Rotate(new Vector3(0,0,1)*fmr);
            }else if(d == "z_minus"){
                test.transform.Rotate(new Vector3(0,0,-1)*fmr);
            }else if(d == "y_plus"){
                test.transform.Rotate(new Vector3(0,1,0)*fmr);
            }else if(d == "y_minus"){
                test.transform.Rotate(new Vector3(0,-1,0)*fmr);
            }
        }
    }

}
