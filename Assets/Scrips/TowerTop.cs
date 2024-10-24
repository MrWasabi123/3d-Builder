using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTop : MonoBehaviour
{
    private bool isHidden;
    private List<GameObject> children = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        float currentX = this.gameObject.transform.localScale.x;
        float currentZ = this.gameObject.transform.localScale.z;
        //this.gameObject.transform.localScale = new Vector3(currentX, Globals.gameHeight / Globals.initHeight, currentZ);

       
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }

        hideTop();
    }

    private void Update()
    {
        hideTop();

        if (Globals.levelnumber != 0 && isHidden)
        {
            isHidden = false;
            foreach (GameObject g in children)
            {
                print("child: " + g);
                g.SetActive(true);
            }
        }
    }

    private void hideTop()
    {
    
        if (Globals.levelnumber == 0)
        {
            isHidden = true;
            foreach (GameObject g in children)
            {
                g.SetActive(false);
            }
        }
    }
}
