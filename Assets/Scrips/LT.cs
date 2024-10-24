using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LT : MonoBehaviour
{
    public LeanTweenType easeType;
    // Start is called before the first frame update
    private Vector3 startPos;
    private bool moved = false;
    private bool active = false;
    void Start()
    {
        //LeanTween.scale(gameObject, new Vector3(0,0,0), 5f).setOnComplete(dme);
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
/*         if(Globals.isGameTrans && !moved && active){
            moved = true;
            active = false;
            goin()
        }
        if(!Globals.isGameTrans && moved){
            moved = false;
        } */
        
    }
    public void setActive(){
        active = true;
    }
    public void goin(){
        LeanTween.move(gameObject, gameObject.transform.position+new Vector3(downCal(),0f,0f), 1f).setOnComplete(wait);
    }
    void wait(){
        LeanTween.move(gameObject,gameObject.transform.position, 3f).setOnComplete(goback);
    }

    void goback(){
        LeanTween.move(gameObject, gameObject.transform.position-new Vector3(downCal(),0f,0f), 1f);
    }
    void dme(){
        Destroy(gameObject);
    }

    private float downCal(){
        return 600f*(Screen.width/1920f);
    }
}
