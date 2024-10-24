using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScript : MonoBehaviour
{

    public GameObject text;

    private Vector3 textScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable(){
        text.transform.localScale = new Vector3(0f,0f,0f);
        textScale = new Vector3(7.2f,4f,1f);
        
        LeanTween.scale(text, textScale, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float xScale(){
        return 7.2f*(Screen.width/1920f);
    }
    private float yScale(){
        return 4f*(Screen.width/1920f);
    }
    private float zScale(){
        return 1f;
    }

}
