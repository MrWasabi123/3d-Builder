using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static float gameHeight = 5.5f;
    public static int levelnumber = 0;
    public static float initHeight = 10;
    public static int currentBalance = 1000;
    public static int currentDPS = 20;
    public static int finePerCube = 50;
    public static int costPerCube = 50;
    public static bool isGameOver = false;
    public static int deleteTiles = 0;
    public static int numberOfBlocks = 0;
    public static int numberOfCollectables = 2;
    public static int rotationSpeed = 90;
    public static float heighestBlock = 0f;
    public static Vector3 direction = new Vector3(0, -1, 0);
    public static GameObject[] prefabs;
    public enum BlockType {Wood, Glass, Heavy};
    public static int BlockTypeChoice = 3;
    public static int BlockMovement = 1;
    public static int BlockMovementPlus = 0;
    public static bool BlockOneDirection = false;
    public static float ODX = 0f;
    public static float ODY = 0f;
    public static bool[] lic = {false, false, false, false, false, false, false};

    public static bool isGameStart = false;
    public static bool isGamePause = false;
    public static bool isGameTrans = false;
    public static bool isGameStory = false;

    public static bool blockReady = true;
    public static bool blockSpawned = false;

    //0=normal and +1 when rotating right, every two rotations, we change the controls
    public static int cameraRotation = 0;

    public static bool blockRotating = false;

    public static int totalCol = 0;
    public static int totalFines = 0;
    public static int totalCost = 0;
    public static int highscore = 0;
    public static int highscoreMultiplier = 100;
    public static int totalBlocksPlaced = 0;
    public static float currentDiscount = 0;
    public static int numberOfDiscounts = 0;
    public enum ValueType {Collectable, Fine, Cost, Discount};

    public static ValueTextController vtc;
    public static BalanceBarController bbc;

    public static void setDefault(){
        gameHeight = 5.5f;
        levelnumber = 0;
        initHeight = 10;
        currentBalance = 1000;
        currentDPS = 20;
        finePerCube = 50;
        costPerCube = 50;
        isGameOver = false;
        deleteTiles = 0;
        numberOfBlocks = 0;
        numberOfCollectables = 2;
        rotationSpeed = 90;
        direction = new Vector3(0, -1, 0);
        isGameStart = false;
        isGamePause = false;
        isGameTrans = false;
        blockReady = true;
        blockSpawned = false;
        cameraRotation = 0;
        blockRotating = false;
        totalCol = 0;
        totalFines = 0;
        totalCost = 0;
        highscore = 0;
        highscoreMultiplier = 100;
        totalBlocksPlaced = 0;
        currentDiscount = 0;
        numberOfDiscounts = 0;
        BlockTypeChoice = 3;
        BlockMovement = 1;
        lic = new bool[]{false, false, false, false, false, false, false};

    }
    public static void startStory(){
        currentBalance = 5000;
        currentDPS = 0;
    }

    public static Vector3 calculateDirectionVector()
    {
        float offset = 10f;
        float length = (levelnumber + 1) * (gameHeight + offset);
        Vector3 directionVector = Vector3.Normalize(direction) * length;
        return directionVector;
        
    }

    public static void calcHighscore(){
        highscore = (levelnumber + totalBlocksPlaced + totalCol) * highscoreMultiplier + totalFines;
        Debug.Log("###############################");
        Debug.Log("highscore " + highscore);
        Debug.Log("###############################");
        Debug.Log("levelnumber " + levelnumber);
        Debug.Log("totalBlocksPlaced " + totalBlocksPlaced);
        Debug.Log("totalCol " + totalCol);
        Debug.Log("totalFines " + totalFines);
        Debug.Log("totalCost "+ totalCost);
    }
}
