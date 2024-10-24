using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LT2 : MonoBehaviour
{
    // Start is called before the first frame update
    public LeanTweenType easeType;
    private Vector3 startPos;
    private float height;
    void Start()
    {
        startPos = gameObject.transform.position;
        print(startPos);
        var rectTransform = GetComponent<RectTransform>();
        height = rectTransform.sizeDelta.y;
        print(height);
        Debug.Log("Screen Height : " + Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void goback(){
        LeanTween.move(gameObject, new Vector3(gameObject.transform.position.x,startPos.y,gameObject.transform.position.z), 1f);
    }

    public void goin(){
       LeanTween.move(gameObject, new Vector3(gameObject.transform.position.x,startPos.y,gameObject.transform.position.z), 0.1f).setOnComplete(goin2);
    }

    private void goback2(){
        LeanTween.move(gameObject, gameObject.transform.position+new Vector3(0f,downCal(),0f), 1f);
    }

    private void goin2(){
        LeanTween.move(gameObject, gameObject.transform.position-new Vector3(0f,downCal(),0f), 1f);
    }
    public void reset(){
        LeanTween.move(gameObject, new Vector3(gameObject.transform.position.x,startPos.y,gameObject.transform.position.z), 1f);
    }

    private float downCal(){
        //100 when 1080 so
        //100 * newscreen/1080
        //print(Screen.height/1080f);
        //print(100f*(Screen.height/1080f));
        //print(100f*(Screen.width/1920f));
        return 100f*(Screen.width/1920f);
    }
}
