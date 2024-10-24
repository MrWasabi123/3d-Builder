using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueTextController : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    RectTransform rt;
    Camera cam;
    private bool moveUp;
    private float currentHight;
    // Start is called before the first frame update
    void Start()
    {
        valueText = GameObject.Find("ValueText").GetComponent<TextMeshProUGUI>();
        rt = GetComponent<RectTransform>();
        gameObject.SetActive(false);        
        setText("Hallo");
        setColor(0.9f, 0.1f, 0.1f);
        resetPosition();
        moveUp = false;

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveUp){
            setPosition(790, currentHight);
            currentHight += 2f;
        }
    }

    void setText(string text)
    {
        valueText.text = text;
    }

    void setColor(float r, float g, float b)
    {
        valueText.color = new Color(r, g, b);        
    }

    void setPosition(float x, float y)
    {
        valueText.rectTransform.position= new Vector2(x, y);
    }

    void resetPosition(){
        currentHight = 70f;
        setPosition(790f,currentHight);
    }

    public void displayValueChange(int value, Globals.ValueType type)
    {
        gameObject.SetActive(true);
        moveUp = true;
        resetPosition();
        StartCoroutine(displayWait(value, type));        
    }

    IEnumerator displayWait(int value, Globals.ValueType type)
    {       
        // int x = Random.Range(100, Screen.width - 100);
        // int y = Random.Range(100, Screen.height - 100);
        // setPosition(x, y);
        switch(type)    
        {
            case Globals.ValueType.Collectable:
                setColor(0.286f, 0.82f, 0.098f);
                setText("+" + value.ToString("C"));        
                break;
            case Globals.ValueType.Fine:
                setColor(0.8f, 0.1f, 0.1f);
                setText(value.ToString("C"));                
                break;
            case Globals.ValueType.Cost:
                setColor(0.9f, 0.5f, 0.1f);
                setText(value.ToString("C"));                
                break;
            case Globals.ValueType.Discount:
                setColor(0.286f, 0.82f, 0.098f);
                setText(value.ToString() + "%");                
                break;
            default:
                break;
        }

        

        yield return new WaitForSeconds(2);

        gameObject.SetActive(false);
        setText("");
        resetPosition();
    }


}
