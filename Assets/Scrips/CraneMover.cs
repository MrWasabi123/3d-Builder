using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class CraneMover : MonoBehaviour
{
    private GameObject craneTop;
    private Vector3 cranePos;
    private bool craneActive = false;

    private GameObject crane;
    private Vector3 craneOrigin;

    private GameObject craneBottom;
    private Vector3 craneBotPos;

    private GameObject craneBody;
    private Vector3 craneBodyPos;

    private GameObject craneAxis;
    private Vector3 craneAxisPos;

    private float movementX;
    private float movementY;
    private float movepercent = 0.5f;
    private int level = Globals.levelnumber;
    private int rise = 4;
    private int moveSpeed = 5;

    private Coroutine MC;
    private float offset = 13;


    // Start is called before the first frame update
    void Start()
    {
        crane = GameObject.FindWithTag("Crane");
        craneOrigin = crane.transform.position;
        float yPos = Globals.heighestBlock - offset;
        crane.transform.position = new Vector3(crane.transform.position.x, yPos, crane.transform.position.z);

        craneTop = GameObject.FindWithTag("CraneTop");
        cranePos = craneTop.transform.position;

        craneBottom = GameObject.FindWithTag("CraneBottom");
        craneBotPos = craneBottom.transform.position;
        
        //craneBody = GameObject.FindWithTag("CraneBody");
        //craneBodyPos = craneBody.transform.position;
        
        craneAxis = GameObject.FindWithTag("CraneAxis");
        craneAxisPos = craneAxis.transform.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float yPos = Globals.heighestBlock - offset;
        crane.transform.position = Vector3.MoveTowards(crane.transform.position, new Vector3(crane.transform.position.x, yPos, crane.transform.position.z), Time.deltaTime * moveSpeed);

        if (Globals.levelnumber > level){
            level = Globals.levelnumber;
            CraneReset();
            //rise*Globals.levelnumber+ Globals.levelnumber

        }
        if(!this.craneActive && craneTop.transform.position != cranePos + new Vector3(0,3*Globals.levelnumber,0)){
            //print(cranePos + new Vector3(0,3*Globals.levelnumber,0));
            CraneReset();

        }
        //idea to have a more smooth crane movement is to move it up (so y +=10) and then have a variable to check if it is up or not. If craneTop.transform.position > 9 then set the variable to true and
        //then movetowards the original spot and if the craneTop <cranePos + new Vector3() then set the variable to false and stop movement?
        //this could then be done for spwaning a block so that the crane goes up, block gets spawned with the crane and then smoothly comes down (so the blocks don't appear out of nowhere?)

    }
    private void OnMove(InputValue input)
    {
        Vector2 movement = input.Get<Vector2>();
        //print(movement);

        movementX = movement.x;
        movementY = movement.y;
        MoveCrane(movementX, movementY);
    }

    private void OnMoveCon(InputValue input){
        Vector2 movement = input.Get<Vector2>();
        movementX = movement.x;
        movementY = movement.y;
        if(movementX !=0 || movementY !=0){
            IEnumerator movec = MoveCraneCon(movementX, movementY, 0.5f);
            MC = StartCoroutine(movec);
        }else{
            if(MC != null){
                StopCoroutine(MC);
            }
        }
    }

    private void MoveCrane(float movementX, float movementY){
        if (this.craneActive) { 
            Vector3 Movement = new Vector3(movementX, 0, movementY);
            craneBottom.transform.position += Movement * movepercent;
            craneTop.transform.position += Movement * movepercent;
            Vector3 MovementAxis = new Vector3(movementX, 0, 0);
            craneAxis.transform.position += MovementAxis * movepercent;
        }
    }
    IEnumerator MoveCraneCon(float mx, float my, float rr){
        while(true){
            MoveCrane(mx,my);
            yield return new WaitForSeconds(rr);
        }
    }

    private void OnSpawn(InputValue input){
        this.craneActive = true;
    }
    private void OnDrop(InputValue input){
        this.craneActive = false;
    }

    private void CraneReset()
    {
        /*
        craneTop.transform.position = Vector3.MoveTowards(craneTop.transform.position, cranePos + new Vector3(0,rise*Globals.levelnumber+ Globals.levelnumber,0), Time.deltaTime *moveSpeed);
        craneBottom.transform.position = Vector3.MoveTowards(craneBottom.transform.position, craneBotPos + new Vector3(0,rise*Globals.levelnumber+ Globals.levelnumber,0), Time.deltaTime *moveSpeed);
        craneAxis.transform.position = Vector3.MoveTowards(craneAxis.transform.position, craneAxisPos + new Vector3(0,rise*Globals.levelnumber+ Globals.levelnumber,0), Time.deltaTime *moveSpeed);
        */

        craneTop.transform.position = Vector3.MoveTowards(craneTop.transform.position, new Vector3(cranePos.x, craneTop.transform.position.y, cranePos.z), Time.deltaTime * moveSpeed);
        craneBottom.transform.position = Vector3.MoveTowards(craneBottom.transform.position, new Vector3(craneBotPos.x, craneBottom.transform.position.y, craneBotPos.z), Time.deltaTime * moveSpeed);
        //craneAxis.transform.position = Vector3.MoveTowards(craneAxis.transform.position, new Vector3(craneAxisPos.x, craneAxis.transform.position.y, craneAxisPos.z), Time.deltaTime * moveSpeed);

    }
}
