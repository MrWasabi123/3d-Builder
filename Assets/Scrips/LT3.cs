using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LT3 : MonoBehaviour
{
    // Start is called before the first frame update
    private float xStart;
    private float yStart;
    void Start()
    {
        xStart = gameObject.transform.position.x;
        yStart = gameObject.transform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable(){
        StartCoroutine(hoverMove());
    }

    private void randmove(){
        float directionX = Random.Range(-10f, 10f);
        float directionY = Random.Range(-10f,10f);
        LeanTween.move(gameObject, new Vector3(xStart + directionX,yStart+ directionY,0), 0.3f);
    }

    IEnumerator hoverMove(){
        while(true){
            randmove();
            yield return new WaitForSeconds(0.3f);
        }

    }
}
