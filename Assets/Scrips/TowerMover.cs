using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMover : MonoBehaviour
{
    Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 1f;
    private bool possitionSet = false;

    // Start is called before the first frame update
    void Start()
    {

        targetPosition = transform.position;
        Debug.Log(targetPosition);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y <= targetPosition.y-0.01) {
            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

    }

    public void moveUpDown(float upMovement)
    {
        targetPosition = transform.position + new Vector3(0f, upMovement, 0f);
        Debug.Log(targetPosition);
    }
    public void moveReset(float resetNumber){
        targetPosition = transform.position = new Vector3(0f, resetNumber, 0f);
    }
}
