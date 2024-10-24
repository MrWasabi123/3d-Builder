using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showWinTxt : MonoBehaviour
{
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        text.SetText("");
    }

    void Update()
    {

    }

    public void showText()
    {
        text.SetText("You WIN!");
    }

}
