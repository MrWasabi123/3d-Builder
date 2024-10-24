using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnBlocks : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    private SpawnControlls controlls;
    private GameObject currentBlock;
    private MoveBlock script;

    private GameObject crane;
    private HingeJoint hinge;
    private Rigidbody block;

    private int yOffset = 5;
    private bool isDroped = true;
    private bool isBlockSpawned = false;
    private bool isStuck = true;

    private GameObject lastBlock = null;

    //trying the different block types
    [SerializeField] private Material[] materials;
    [SerializeField] private float[] massNumbers = new float[]{2f,0.3f,0.5f};
    [SerializeField] private Globals.BlockType[] types = new Globals.BlockType[]{Globals.BlockType.Wood, Globals.BlockType.Glass, Globals.BlockType.Heavy};

    BalanceBarController bbc;
    ValueTextController vtc;
    private GameObject sounds;


    private void Awake()
    {


        Globals.prefabs = prefabs;
        controlls = new SpawnControlls();
        bbc = GameObject.FindObjectOfType(typeof(BalanceBarController)) as BalanceBarController;
        vtc = GameObject.Find("ValueText").GetComponent<ValueTextController>();

        Globals.vtc = vtc;
        Globals.bbc = bbc;
        //controlls.Enable();
    }

    private void OnEnable()
    {
        controlls.Enable();
    }

    private void OnDisable()
    {
        controlls.Disable();
    }

    private void Start(){
        crane = GameObject.FindWithTag("CraneBottom");
        sounds = GameObject.FindWithTag("soundsglobal");
    }

    private void Update()
    {
        if (currentBlock == null)
        {
            isDroped = true;
            isBlockSpawned = false;
        }

        if(!Globals.isGameTrans && !Globals.isGamePause && Globals.isGameStart){
            if (controlls.Player.Spawn.triggered && Globals.blockReady)
            {
                try
                {
                    hinge = crane.GetComponent<HingeJoint>();
                    Destroy(hinge);
                }catch{
                    print("no need");
                }
                sounds.GetComponent<SoundsGlobal>().playSound("spawnblock");
                GameObject prefab = pickPrefab();
                currentBlock = Instantiate(prefab, new Vector3(0.5f, Globals.heighestBlock + yOffset, 0.5f), Quaternion.identity);
                int randRot = Random.Range(0, 8);
                Vector3 Rotation = new Vector3(0, randRot, 0);
                currentBlock.transform.Rotate(Rotation* Globals.rotationSpeed);
                currentBlock.GetComponent<Rigidbody>().useGravity = false;
                currentBlock.GetComponent<Rigidbody>().isKinematic = true;
                currentBlock.GetComponent<Rigidbody>().drag = 1f;
                script = currentBlock.GetComponent<MoveBlock>();
                script.spawnIndicator();
                script.setIsAcitve(true);
                script.setIsOnTower(false);
                setMaterial(currentBlock);
                //set random rotation here

                block = currentBlock.GetComponent<Rigidbody>();
                hinge = crane.AddComponent<HingeJoint>();
                hinge.connectedBody = block;
                Globals.blockSpawned = true;
                Globals.blockReady = false;            
            }

            if ((controlls.Player.Drop.triggered && Globals.blockSpawned))
            {
                sounds.GetComponent<SoundsGlobal>().playSound("drop");
                lastBlock = currentBlock;
                script.removeIndicators();
                //currentBlock.GetComponent<Rigidbody>().useGravity = true;
                currentBlock.GetComponent<Rigidbody>().isKinematic = false;
                hinge = crane.GetComponent<HingeJoint>();
                if (hinge != null){
                hinge.connectedBody = null;
                }
                Destroy(hinge);
                script.setIsAcitve(false);
                Globals.blockSpawned = false;
                calcAndDisplayBlockprice(currentBlock, -1);
                Globals.totalBlocksPlaced++;
                Globals.ODX = 0f;
                Globals.ODY = 0f;
            }

            if (controlls.Player.Redo.triggered)
            {
                redoBlock();
            }

        }


        if (isStuck == true && Globals.isGameTrans){
        isStuck = false;
        currentBlock.GetComponent<Rigidbody>().useGravity = true;
        currentBlock.GetComponent<Rigidbody>().isKinematic = false;
        hinge = crane.GetComponent<HingeJoint>();
        if (hinge != null){
            hinge.connectedBody = null;
        }
        Destroy(hinge);
        script.setIsAcitve(false);
        Globals.blockSpawned = false;
        }


    }

    private void setMaterial(GameObject go){
        int randInt = Random.Range(0, Globals.BlockTypeChoice);
        print("choosing random");
        print(randInt);
        Material chosenmat = materials[randInt];
        float chosenweight = massNumbers[randInt];
        go.GetComponent<Rigidbody>().mass = chosenweight;
        go.GetComponent<MoveBlock>().bt = types[randInt];
        Renderer[] cubes;
        cubes = go.GetComponentsInChildren<Renderer>();
        //print(cubes);
        foreach(Renderer r in cubes){
            r.material = chosenmat;
        }

    }


    GameObject pickPrefab()
    {
        int randInt = Random.Range(1, prefabs.Length);
        return prefabs[randInt];
    }

    private void calcAndDisplayBlockprice(GameObject block, int sign){
        int price = block.transform.childCount * Globals.costPerCube * sign;
        if(Globals.numberOfDiscounts > 0){
            Globals.numberOfDiscounts--;
            price = Mathf.RoundToInt(price * (1 - Globals.currentDiscount));
        }
        bbc.changeBalance(price, Globals.ValueType.Cost);
        vtc.displayValueChange(price, Globals.ValueType.Cost);
    }

    private void redoBlock()
    {
        if (lastBlock != null)
        {
            calcAndDisplayBlockprice(lastBlock, 1);
            Destroy(lastBlock);
            lastBlock = null;
            Globals.blockReady = true;
        }
    }
}
