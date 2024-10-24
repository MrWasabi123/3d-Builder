using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalanceBarController : MonoBehaviour
{
    TextMeshProUGUI balanceText;
    BalanceBar bb;
    Canvas canvas;

    private float decreaseSmoothness = 0.05f;
    private float decreaseSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        balanceText = GameObject.Find("CurrentBalanceText").GetComponent<TextMeshProUGUI>();
        bb = GameObject.Find("CurrentBalanceSlider").GetComponent<BalanceBar>();
        canvas = GetComponentInParent<Canvas>();

        balanceText.text = Globals.currentBalance.ToString();

        InvokeRepeating("decreaseBalance", decreaseSmoothness, decreaseSpeed);

        setCountText(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCountText(bool isInital)
    {
        //Error instantiate balanceText
        if(!isInital)
        {
            bb.setBalance(Globals.currentBalance);
        }
        balanceText.text = Globals.currentBalance.ToString("C");
    }

    private void decreaseBalance()
    {
        if(!Globals.isGameOver && Globals.isGameStart && !Globals.isGamePause)
        {
            //print("Balance: "+Globals.currentBalance);
            if (Globals.currentBalance - Globals.currentDPS <= 0)
            {
                Globals.currentBalance = 0;
                Globals.isGameOver = true;
                Globals.calcHighscore();
                //displayGameOverText();
            } else {
               Globals.currentBalance -= (int) (Globals.currentDPS * decreaseSmoothness);
            }
            setCountText(false);
        }
    }

    public void changeBalance(int value, Globals.ValueType type)
    {
        Globals.currentBalance += value;
        switch(type)
        {
            case Globals.ValueType.Collectable:
                Globals.totalCol += value;
                break;
            case Globals.ValueType.Fine:
                 Globals.totalFines += value;
                break;
            case Globals.ValueType.Cost:
                Globals.totalCost += value;
                break;
            default:
                break;
        }
    }

    public void displayValueChange(int value, Globals.ValueType type, GameObject targetGO)
    {
        StartCoroutine(displayWait(value, type, targetGO));
    }

    IEnumerator displayWait(int value, Globals.ValueType type, GameObject targetGO)
    {
        GameObject ngo = new GameObject("colValueText");
        ngo.transform.SetParent(canvas.transform);
        TextMeshProUGUI valueText = ngo.AddComponent<TextMeshProUGUI>();
        // valueText.rectTransform.position = new Vector2(Screen.width/2,Screen.height/2);


        // RectTransform rectTransform = new RectTransform();
        // print(rectTransform);
        Vector2 pos = targetGO.transform.position;  // get the game object position
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);  //convert game object position to VievportPoint
 
        valueText.rectTransform.anchorMin = viewportPoint;  
        valueText.rectTransform.anchorMax = viewportPoint; 

        valueText.rectTransform.position = new Vector2(valueText.rectTransform.position.x + Screen.width/2, valueText.rectTransform.position.y + Screen.height/2);

        print(valueText.rectTransform.position);




        switch(type)    
        {
            case Globals.ValueType.Collectable:
                valueText.outlineColor = new Color(0.286f, 0.82f, 0.098f);
                valueText.color = new Color(0.286f, 0.82f, 0.098f);
                valueText.text = "+" + value.ToString("C");                
                break;
            case Globals.ValueType.Fine:
                valueText.outlineColor = new Color(0.8f, 0.1f, 0.1f);
                valueText.color = new Color(0.8f, 0.1f, 0.1f);
                valueText.text = value.ToString("C");
                break;
            case Globals.ValueType.Cost:
                valueText.outlineColor = new Color(0.9f, 0.5f, 0.1f);
                valueText.color = new Color(0.9f, 0.5f, 0.1f);
                valueText.text = value.ToString("C");
                break;
            case Globals.ValueType.Discount:
                valueText.outlineColor = new Color(0.286f, 0.82f, 0.098f);
                valueText.color = new Color(0.286f, 0.82f, 0.098f);
                valueText.text = value.ToString() + "%";
                break;
            default:
                break;
        }

        if(value > 0)
        {
            valueText.outlineColor = new Color(0.286f, 0.82f, 0.098f);
             valueText.color = new Color(0.286f, 0.82f, 0.098f);
             valueText.text = "+" + value.ToString("C");
        } else if(value < 0){
             valueText.outlineColor = new Color(0.8f, 0.1f, 0.1f);
             valueText.color = new Color(0.8f, 0.1f, 0.1f);
             valueText.text = value.ToString("C");
        }       

        yield return new WaitForSeconds(3);

        Destroy(ngo);
    }

    private void displayGameOverText(){
        GameObject ngo = new GameObject("gameOverText");
        ngo.transform.SetParent(canvas.transform);
        TextMeshProUGUI gameOverText = ngo.AddComponent<TextMeshProUGUI>();
        gameOverText.rectTransform.position = new Vector2(Screen.width / 2, Screen.height / 2 - 200);
        gameOverText.rectTransform.sizeDelta = new Vector2(261,100);
        // gameOverText.alignment = AlignmentTypes.Left;
        gameOverText.outlineColor = new Color(1f,1f,1f);
        gameOverText.fontSize = 50;
        gameOverText.text = "Game Over";
    }
}
