using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNewLevel : MonoBehaviour
{
    [SerializeField] private GameObject cameraContainer;
    [SerializeField] private GameObject[] levelPrefabs;
    [SerializeField] private GameObject Outline;
    [SerializeField] private GameObject Ground;
    [SerializeField] private GameObject Tower;
    [SerializeField] private GameObject[] levelInd;
    [SerializeField] private GameObject[] levelImages;
    private int[] numbers = new int[] { 0,1,2,3 }; 
    private GameObject sounds;
    

    public GameObject levelEnd;

    void OnEnable()
    {
        Outline.GetComponent<CheckIfLevelEnded>().OnVariableChange += startNewLevelWait;
    }

    private void Awake()
    {
        adjustDificulty();
    }
    private void Start()
    {
        sounds = GameObject.FindWithTag("soundsglobal");
    }


    private void startNewLevelWait(bool levelFinished)
    {
        print("level Finished: " + levelFinished);
        if (levelFinished)
        {
            StartCoroutine(LevelMoveAnimation());
        }
    }
    IEnumerator LevelMoveAnimation()
    {
        levelEnd.SetActive(true);
        Globals.isGamePause = true;
        Globals.isGameTrans= true;
        //RemoveCraneItem();
        yield return new WaitForSeconds(0.1f);
        startNewLevel();
        levelEnd.SetActive(false);
        Globals.isGamePause = false;
        Globals.isGameTrans= false;
    }

    public void startNewLevel(bool reset = false)
    {

        removePastLevel();

        if (!reset)
        {
            adjustDificulty();
            Globals.levelnumber += 1;
        }
        else
        {
            Globals.levelnumber = 0;
            adjustDificulty();
        }

        if (Globals.isGameStory)
        {
            Globals.currentBalance += 1000;
        }

        Vector3 newLevel = new Vector3(0f, Globals.levelnumber * Globals.gameHeight, 0f);
        print(Globals.gameHeight);

        CheckIfLevelEnded outlineScript = Outline.GetComponent<CheckIfLevelEnded>();
        outlineScript.hideHeightIndicator();
        outlineScript.reset();

        Outline.transform.position = newLevel + new Vector3(-3.5f, 0f, -3.5f);
            
        /*
        try
        {
            Instantiate(levelPrefabs[Globals.levelnumber], newLevel + new Vector3(5f, 0f, 5f), Quaternion.identity);
        }
        catch
        {
            Instantiate(levelPrefabs[0], newLevel + new Vector3(5f, 0f, 5f), Quaternion.identity);
        }*/

        Instantiate(Ground, newLevel, Quaternion.identity);

        if (reset)
        {
            cameraContainer.GetComponent<CameraRotate>().moveReset(3);
            Tower.GetComponent<TowerMover>().moveReset(-17);
        }
        else
        {
            cameraContainer.GetComponent<CameraRotate>().moveUpDown(Globals.gameHeight);
            if(Globals.levelnumber != 1)
            {
                Tower.GetComponent<TowerMover>().moveUpDown(Globals.gameHeight);
            } else
            {
                Tower.GetComponent<TowerMover>().moveUpDown(Globals.gameHeight * 0.4f);
            }
        }
    }

    private void removePastLevel()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        //GameObject[] blocksLastLevel = GameObject.FindGameObjectsWithTag("BlockLastLevel");
        GameObject[] levels = GameObject.FindGameObjectsWithTag("Level");

        List<GameObject> collectables = new List<GameObject>();
        collectables.AddRange(GameObject.FindGameObjectsWithTag("Bill"));
        collectables.AddRange(GameObject.FindGameObjectsWithTag("Coin"));
        collectables.AddRange(GameObject.FindGameObjectsWithTag("Discount"));
        collectables.AddRange(GameObject.FindGameObjectsWithTag("Lawsuit"));
        collectables.AddRange(GameObject.FindGameObjectsWithTag("Stock"));

        foreach (GameObject coll in collectables)
        {
            Transform parent = coll.transform.parent;
            if (parent != null)
            {
                spawnIndicator script = parent.gameObject.GetComponent<spawnIndicator>();
                if (script != null)
                {
                    script.removeIndicators();
                }
                Destroy(parent.gameObject);
            }
            else
            {
                spawnIndicator script = coll.GetComponent<spawnIndicator>();
                if (script != null)
                {
                    script.removeIndicators();
                }
                Destroy(coll);
            }
        }

        foreach (GameObject level in levels)
        {
            //Destroy(level);
            foreach (var comp in level.GetComponents<Component>())
            {
                if (!(comp is Transform))
                {
                    Destroy(comp);
                }
            }
            level.transform.position += new Vector3(100f, 0f, 0f);
            level.tag = "LevelM";
        }

        foreach (GameObject block in blocks)
        {
            foreach (var comp in block.GetComponents<Component>())
            {
                if (!(comp is Transform))
                {
                    Destroy(comp);
                }
            }
            //Destroy(block);
            //block.transform.position += new Vector3(100f, 0f, 0f);
            //block.tag = "BlockM";

            block.transform.position += new Vector3(100f, 0f, 0f);
            block.tag = "BlockM";
            //block.tag = "BlockLastLevel";
        }

        /*
        foreach (GameObject block in blocksLastLevel)
        {
            block.transform.position += new Vector3(100f, 0f, 0f);
            block.tag = "BlockM";
        }
        */
    }

    private void randomAction(int c){
        if(c == 0){
            moveIn(3);
            Globals.BlockMovement = -1;
        }
        if(c==1){
            float directionX = Random.Range(0f, 1f);
            float directionZ = Random.Range(0f, 1f);
            Globals.direction = new Vector3(directionX, -1, directionZ);
            moveIn(4);
        }
        if(c==2){
            int movementDirection = Random.Range(0,4);
            Globals.BlockMovementPlus = movementDirection;
            moveIn(5);
        }
        if(c==3){
            moveIn(6);
            Globals.BlockOneDirection = true;
        }

    }
    private void resetAction(int c){
        if(c == 0){
            moveOut(3);
            Globals.BlockMovement = 1;
        }
        if(c==1){
            Globals.direction = new Vector3(0, -1, 0);
            moveOut(4);
        }
        if(c==2){
            moveOut(5);
            Globals.BlockMovementPlus = 0;
        }
        if(c==3){
            moveOut(6);
            Globals.BlockOneDirection = false;
        }
    }


    private void adjustDificulty()
    {
        Globals.numberOfBlocks =  Random.Range(1, 4 - (int)System.Math.Log(Globals.levelnumber+1, 2) + 1);
        Globals.numberOfCollectables = Random.Range(1, 4 - (int)System.Math.Log(Globals.levelnumber+1, 2) + 1);
        Globals.deleteTiles =  (int)System.Math.Log(Globals.levelnumber+1, 2) + 1;
       

        print("DPS: " + Globals.currentDPS + "init: NumB=" + Globals.numberOfBlocks.ToString() + " - NumC="+Globals.numberOfCollectables.ToString() + " - Holes=" +Globals.deleteTiles.ToString());

        //here the level system for freeplay
        if(!Globals.isGameStory && Globals.isGameStart ){
            Globals.currentDPS = (int)System.Math.Log((Globals.levelnumber + 1), 1.5) + 20;

            Globals.currentBalance += 500;

            sounds.GetComponent<SoundsGlobal>().playSound("leveldone");
            print("THE LEVEL IS " + Globals.levelnumber);
            levelImages[0].GetComponent<LT>().goin();
            if(Globals.levelnumber == 0){
                Globals.BlockTypeChoice = 3;
                moveIn(0);
                moveIn(1);
                moveIn(2);
            }
            if(Globals.levelnumber ==1 ||Globals.levelnumber ==2){
                int choice = Random.Range(0,4);
                print("THE CHOICE IS " + choice);
                for(int i=0; i<numbers.Length;i++){
                    resetAction(i);
                }
                randomAction(choice);
            }
            if(Globals.levelnumber ==3 ||Globals.levelnumber ==4){
                int choice1 = Random.Range(0,4);
                int choice2 = Random.Range(0,4);
                while(choice1 == choice2){
                    choice2 = Random.Range(0,4);
                }
                print("THE CHOICE1 IS " + choice1);
                print("THE CHOICE2 IS " + choice2);
                for(int i=0; i<numbers.Length;i++){
                    resetAction(i);
                }
                randomAction(choice1);
                randomAction(choice2);


            }
            if(Globals.levelnumber ==5 ||Globals.levelnumber ==6){
                int nochoice = Random.Range(0,4);
                print("THE NOCHOICE IS " + nochoice);
                for(int i=0; i<numbers.Length;i++){
                    resetAction(i);
                }
                for(int i=0; i<numbers.Length;i++){
                    if(i != nochoice){
                        randomAction(i);
                }
                }

            }
            if(Globals.levelnumber >6){
                for(int i=0; i<numbers.Length;i++){
                    resetAction(i);
                }
                 for(int i=0; i<numbers.Length;i++){
                     print("THE THING IS " + i);
                        randomAction(i);
                }

            }

        }
        
        //here the level system for story mode
        if(Globals.isGameStory){
            sounds.GetComponent<SoundsGlobal>().playSound("leveldone");
            //start with only wood blocks
            if(Globals.levelnumber == 0){
                moveIn(0);
                levelImages[0].GetComponent<LT>().goin();
                Globals.BlockTypeChoice = 1;
            }
            //add glass blocks
            if(Globals.levelnumber == 1){
                moveIn(1);
                levelImages[1].GetComponent<LT>().goin();
                Globals.BlockTypeChoice = 2;
            }
            //add stone blocks
            if(Globals.levelnumber == 2){
                moveIn(2);
                levelImages[2].GetComponent<LT>().goin();
                Globals.BlockTypeChoice = 3;
            }
            //movement is opposite of click
            if(Globals.levelnumber == 3){
                moveIn(3);
                levelImages[3].GetComponent<LT>().goin();
                Globals.BlockMovement = -1;
            }else{
                moveOut(3);
                Globals.BlockMovement = 1;
            }
            //wind takes place
            if(Globals.levelnumber == 4){
                moveIn(4);
                levelImages[4].GetComponent<LT>().goin();
                Globals.direction = new Vector3(1, -1, 0);
            }else{
                moveOut(4);
                Globals.direction = new Vector3(0, -1, 0);
            }
            //movement is randomly chosen
            if(Globals.levelnumber == 5){
                moveIn(5);
                levelImages[5].GetComponent<LT>().goin();
                int randMove = Random.Range(0, 3) +1;
                Globals.BlockMovementPlus = randMove;
            }else{
                moveOut(5);
                Globals.BlockMovementPlus = 0;
            }
            //Player can only move in one direction once they chose one
            if(Globals.levelnumber == 6){
                moveIn(6);
                levelImages[6].GetComponent<LT>().goin();
                Globals.BlockOneDirection = true;
            }else{
                moveOut(6);
                Globals.ODX = 0f;
                Globals.ODY = 0f;
                Globals.BlockOneDirection = false;
            }
            //mix of all previous levels
            if(Globals.levelnumber == 7){
                levelImages[7].GetComponent<LT>().goin();
            }
            if(Globals.levelnumber >6){
                if(Globals.levelnumber != 7){
                    levelImages[0].GetComponent<LT>().goin();
                }
                //random wind direction
                int ifWind = Random.Range(0,2);
                float directionX = Random.Range(0f, 1f);
                float directionZ = Random.Range(0f, 1f);
                if(ifWind == 1){
                    Globals.direction = new Vector3(directionX, -1, directionZ);
                    if(directionX==0f && directionZ==0f){
                        moveOut(4);
                    }else{
                        moveIn(4);
                    }
                }else{
                    Globals.direction = new Vector3(0, -1, 0);
                    moveOut(4);
                }
                //randomly give BlockMovement
                int minusDirection = Random.Range(0, 2);
                if(minusDirection==0){
                    moveIn(3);
                    Globals.BlockMovement = -1;
                }else{
                    moveOut(3);
                    Globals.BlockMovement = 1;
                }
                //random Block Direction
                int movementDirection = Random.Range(0,4);
                Globals.BlockMovementPlus = movementDirection;
                if(movementDirection==0){
                    moveOut(5);
                }else{
                    moveIn(5);
                }
                
                //Lock Movement randomly
                int lockMovement = Random.Range(0, 2);
                if(lockMovement==0){
                    moveIn(6);
                    Globals.BlockOneDirection = true;
                }else{
                    moveOut(6);
                    Globals.ODX = 0f;
                    Globals.ODY = 0f;
                    Globals.BlockOneDirection = false;
                }
            }
        }
    }
    private void moveIn(int x){
            levelInd[x].GetComponent<LT2>().goin();
    }
    private void moveOut(int x){
            levelInd[x].GetComponent<LT2>().goback();
    }

    private void RemoveCraneItem(){
        Globals.isGameTrans= true;

    }
}
