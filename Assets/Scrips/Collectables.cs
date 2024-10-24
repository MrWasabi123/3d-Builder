using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectables : MonoBehaviour
{
    Rigidbody rb;
    MoveBlock mb;
    BalanceBarController bbc;
    ValueTextController vtc;

    // public TextMeshProUGUI balanceText;
 
    int[] coinValues = {10, 20, 50, 100, 200};
    int[] billValues = {500, 1000, 2000, 5000};
    int[] lawsuitValues = {-100, -500, -1000};
    // int[] billValues = {100};

    private bool isOnTrigger = false;
    private List<GameObject> currentTriggers = new List<GameObject>();
    private GameObject sounds;

    // Start is called before the first frame update
    void Awake()
    {
        //bbc = GameObject.FindObjectOfType(typeof(BalanceBarController)) as BalanceBarController;
        //vtc = GameObject.FindObjectOfType(typeof(ValueTextController)) as ValueTextController;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mb = gameObject.GetComponent<MoveBlock>();      
        //vtc = GetComponent<ValueTextController>();
        sounds = GameObject.FindWithTag("soundsglobal");

        vtc = Globals.vtc;
        bbc = Globals.bbc;
    }

    // Update is called once per frame
    void Update()
    {
    }   

    void FixedUpdate()
    {
       if(isOnTrigger && mb.getIsOnTower() && !mb.getIsActive())
       //if(isOnTrigger && !mb.getIsMoving())
       {
           int rand;
           float frand;
           int value;

           List<GameObject> deletList = new List<GameObject>();

           foreach (GameObject go in currentTriggers)
           {
                if (go != null)
                {
                    switch (go.tag)
                    {
                        case "Coin":
                            sounds.GetComponent<SoundsGlobal>().playSound("coin");
                            deletList.Add(go);
                            Destroy(go);
                            rand = Random.Range(0, coinValues.Length - 1);
                            bbc.changeBalance(coinValues[rand], Globals.ValueType.Collectable);
                            //vtc = GameObject.FindObjectOfType(typeof(ValueTextController)) as ValueTextController;
                            vtc.displayValueChange(coinValues[rand], Globals.ValueType.Collectable);
                            bbc.setCountText(false);
                            break;
                        case "Bill":
                            sounds.GetComponent<SoundsGlobal>().playSound("bill");
                            deletList.Add(go);
                            Destroy(go);
                            rand = Random.Range(0, billValues.Length - 1);
                            bbc.changeBalance(billValues[rand], Globals.ValueType.Collectable);
                            //vtc = GameObject.FindObjectOfType(typeof(ValueTextController)) as ValueTextController;
                            vtc.displayValueChange(billValues[rand], Globals.ValueType.Collectable);
                            bbc.setCountText(false);
                            break;
                        case "Stock":
                            sounds.GetComponent<SoundsGlobal>().playSound("stock");
                            deletList.Add(go);
                            Destroy(go);
                            frand = Random.Range(-0.2f, 0.2f);
                            value = Mathf.RoundToInt(Globals.currentBalance * frand);
                            bbc.changeBalance(value, Globals.ValueType.Collectable);
                            //vtc = GameObject.FindObjectOfType(typeof(ValueTextController)) as ValueTextController;
                            if(value > 0)
                            {
                                vtc.displayValueChange(value, Globals.ValueType.Collectable);
                            } else if(value < 0)
                            {
                                vtc.displayValueChange(value, Globals.ValueType.Fine);
                            }
                            bbc.setCountText(false);
                            break;
                        case "Discount":
                            sounds.GetComponent<SoundsGlobal>().playSound("discount");
                            deletList.Add(go);
                            Destroy(go);
                            Globals.numberOfDiscounts += 5;
                            frand = Random.Range(0.05f, 0.5f);
                            Globals.currentDiscount = frand;
                            value = Mathf.RoundToInt(frand * 100);
                            //vtc = GameObject.FindObjectOfType(typeof(ValueTextController)) as ValueTextController;
                            vtc.displayValueChange(value, Globals.ValueType.Discount);
                            // bbc.setCountText(false);
                            break;
                    	case "Lawsuit":
                            //sounds.GetComponent<SoundsGlobal>().playSound("bill"); TODO: neuer sound
                            deletList.Add(go);
                            Destroy(go);
                            rand = Random.Range(0, lawsuitValues.Length - 1);
                            bbc.changeBalance(lawsuitValues[rand], Globals.ValueType.Fine);
                            //vtc = GameObject.FindObjectOfType(typeof(ValueTextController)) as ValueTextController;
                            vtc.displayValueChange(lawsuitValues[rand], Globals.ValueType.Fine);
                            bbc.setCountText(false);
                            break;              
                        default:
                            break;
                    }
                }
           }

           foreach (GameObject gobj in deletList)
           {
               currentTriggers.Remove(gobj);
           }        

            if(currentTriggers.Count < 1){
                isOnTrigger = false;    
            }
       }

       float lowerBound = Globals.levelnumber * Globals.gameHeight -5;
    
       if(gameObject.transform.position.y < lowerBound && CompareTag("Block") && gameObject.activeSelf)
       {
            doBlockDropFine();
       }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Grass")
        {
            doBlockDropFine();
            return;
        }
        isOnTrigger = true;

        GameObject otherBlock = other.gameObject;

        if(!currentTriggers.Contains(otherBlock))
        {
            currentTriggers.Add(otherBlock);
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        isOnTrigger = false;

        GameObject otherBlock = other.gameObject;

        if(currentTriggers.Contains(otherBlock)){
            currentTriggers.Remove(otherBlock);
        }
    }  


    private void doBlockDropFine()
    {
        if (bbc != null && vtc != null)
        {
            Globals.blockReady = true;
            int fine = gameObject.transform.childCount * -1 * Globals.finePerCube;
            bbc.changeBalance(fine, Globals.ValueType.Fine);
            Destroy(gameObject);
            gameObject.SetActive(false);
            //vtc = GameObject.FindObjectOfType(typeof(ValueTextController)) as ValueTextController;
            vtc.displayValueChange(fine, Globals.ValueType.Fine);
        }
    }    
}
