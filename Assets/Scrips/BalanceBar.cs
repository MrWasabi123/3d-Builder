using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceBar : MonoBehaviour
{
    private Slider slider;
    private float maxValue;    

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        maxValue = 10000f;
        slider.value = Globals.currentBalance / maxValue;
    }

    // Update is called once per frame
    void Update()
    {        
    }

    public void setBalance(int newBalance)
    {        
        float newFloatBalance = newBalance;  
        slider.value = newFloatBalance / maxValue;
    }
}
