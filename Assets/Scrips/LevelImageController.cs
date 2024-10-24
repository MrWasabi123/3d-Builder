using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelImageController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> levelInd = new List<GameObject>();
    public List<GameObject> levelImage = new List<GameObject>();

    void Start()
    {
        levelInd[0].GetComponent<LT2>().goin();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
