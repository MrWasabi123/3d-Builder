using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(MoveUp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MoveUp(){
        return;
    }
}
