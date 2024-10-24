using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfLevelEnded: MonoBehaviour
{
    private bool levelFinished;

    public delegate void OnLevelFinish(bool newVal);
    public event OnLevelFinish OnVariableChange;
    private List<Transform> children = new List<Transform>();
    private GameObject topIndicator;
    private bool indicatorShown;
    private GameObject currentObjectInIndicator;


    // Start is called before the first frame update
    private void Awake()
    {
        currentObjectInIndicator = null;
        indicatorShown = false;
        levelFinished = false;
        float currentX = this.gameObject.transform.localScale.x;
        float currentZ = this.gameObject.transform.localScale.z;
        this.gameObject.transform.localScale = new Vector3(currentX, Globals.gameHeight / Globals.initHeight, currentZ);

        children.AddRange(this.gameObject.transform.GetComponentsInChildren<Transform>());

        foreach (Transform child in children)
        {
            if (child.gameObject.CompareTag("OutlineTop"))
            {
                topIndicator = child.gameObject;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject currentObject = other.gameObject.transform.parent.gameObject;
        if (currentObject.CompareTag("Block") && !levelFinished)
        {
            MoveBlock script = currentObject.GetComponent<MoveBlock>();
            if (!script.getIsActive() && !indicatorShown && script.getHasCollided())
            {
                currentObjectInIndicator = currentObject;
                showHeightIndicator();
            }

            if (!script.getIsActive() && script.getIsOnTower() && Globals.blockReady)
            {
                print("Game Won");
                levelFinished = true;
                OnVariableChange(levelFinished);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject currentObject = other.gameObject.transform.parent.gameObject;
        if(currentObjectInIndicator == currentObject)
        {
            hideHeightIndicator();
        }
    }

    public void reset()
    {
        levelFinished = false;
    }

    public void showHeightIndicator()
    {
        if (topIndicator != null)
        {
            topIndicator.GetComponent<MeshRenderer>().enabled = true;
            indicatorShown = true;
        }
    }

    public void hideHeightIndicator()
    {
        if (topIndicator != null)
        {
            topIndicator.GetComponent<MeshRenderer>().enabled = false;
            indicatorShown = false;
        }
    }
}
