using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Collections;

public class InitiateLevel : MonoBehaviour
{

    [SerializeField] private GameObject[] collectablesPrefabs;
    private HashSet<int> removedIndices = new HashSet<int>();
    private HashSet<int> posNotAllowd = new HashSet<int>();
    string mes = "";

    private int height;
    private int enums;
    private List<Vector3> collectablePositions = new List<Vector3>();

    enum Hole
    {
        cross,
        big,
        point,
        bar,
        line,
        corner,
        small,
        handy
    }


    void Start()
    {
        enums = Enum.GetNames(typeof(Hole)).Length;

        height = (int) System.Math.Sqrt(transform.childCount);

        //fillPosNotAllowd();

        //string mess = "";
        //foreach(int i in posNotAllowd)
        //{
        //    mess += ", "+i.ToString();
        //}
        //print(mess);

        //posNotAllowd = new HashSet<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 19, 20, 29, 30, 39, 40, 49,
        //    50, 59, 60, 69, 70, 79, 80, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99};

        float currentX = this.gameObject.transform.localScale.x;
        float currentZ = this.gameObject.transform.localScale.z;
        this.gameObject.transform.localScale = new Vector3(currentX, Globals.gameHeight / Globals.initHeight, currentZ);


        Transform[] allChildren = GetComponentsInChildren<Transform>();
        List<GameObject> childObjects = new List<GameObject>();
        foreach (Transform child in allChildren)
        {
            childObjects.Add(child.gameObject);
        }

        //remove first entry. Get all child transdorms also returns the root. Here we only want the plane objects not the root
        childObjects.RemoveAt(0);

 

        //pick Tiles from the ground that should get destroyed
        deleteTiles(childObjects);




        // Spwan random Blocks on tiles that will not be destroyed
        initializeBlocks(childObjects);


        initializeCollectables();




        List<int> hList = removedIndices.ToList();

        hList.Sort();
        hList.Reverse();


        for (int i = 0; i < hList.Count; i++)
        {
            mes = mes + ", " + hList[i].ToString();
        }

        //print("removedIndices: " + mes);


        //Destroy all picked tiles
        foreach (int i in hList)
        {
            Destroy(childObjects[i]);
        }

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        float heighest = 0f;

        foreach (GameObject block in blocks)
        {
            if (block.transform.position.y >= heighest)
            {
                heighest = block.transform.position.y;
            }
        }

        Globals.heighestBlock = heighest;

    }

    public void initializeBlocks(List<GameObject> childObjects)
    {
        for (int i = 0; i < Globals.numberOfBlocks; i++)
        {
            GameObject prefab = pickPrefab();
            int positionIndex = pickGroundPosition(childObjects);
            int collumn = (int) positionIndex / height;
            int row =  positionIndex % height;
            //Vector3 position = childObjects[positionIndex].transform.position;
            Vector3 position = new Vector3(-height * 0.5f + 0.5f + collumn, 0, height * 0.5f - 0.5f - row);

            GameObject currentBlock = Instantiate(prefab, position + new Vector3(0f, 5, 0f), pickRandomRotation());

            currentBlock.GetComponent<Rigidbody>().useGravity = false;
            currentBlock.GetComponent<Rigidbody>().isKinematic = true;
            
            setPosition(position, currentBlock);
            MoveBlock script = currentBlock.GetComponent<MoveBlock>();
            script.setIsAcitve(false);
            script.setIsOnTower(true);
        }
    }

    public void initializeCollectables()
    {
        for (int i = 0; i < Globals.numberOfCollectables; i++)
        {
            int randIndex = UnityEngine.Random.Range(0, collectablesPrefabs.Length);

            Vector3 currPos = pickCollectablePosition();

            while (areVectorsNear(currPos, collectablePositions))
            {
                currPos = pickCollectablePosition();
            }

            collectablePositions.Add(currPos);

            Instantiate(collectablesPrefabs[randIndex], currPos, Quaternion.identity);
        }
    }

    public Vector3 pickCollectablePosition()
    {
        int randX = UnityEngine.Random.Range(-height, height + 1);
        int randY = UnityEngine.Random.Range((int)(Globals.gameHeight * 0.5f), (int)Globals.gameHeight);
        int randZ = UnityEngine.Random.Range(-height, height + 1);

        return new Vector3(randX/2, transform.position.y + randY, randZ/2);
    }


    public Quaternion pickRandomRotation()
    {
        int rotationX = UnityEngine.Random.Range(1, 5);
        int rotationY = UnityEngine.Random.Range(1, 5);
        int rotationZ = UnityEngine.Random.Range(1, 5);
        Vector3 Rotation = new Vector3(rotationX * Globals.rotationSpeed, rotationY * Globals.rotationSpeed, rotationZ * Globals.rotationSpeed);
        return Quaternion.Euler(Rotation);
    }

    public void fillPosNotAllowd()
    {
        for (int i= 0; i < height; i++)
        {
            posNotAllowd.Add(i);
            posNotAllowd.Add(height*height -1 - i);
            posNotAllowd.Add(i * height);
            posNotAllowd.Add(i * height + (height-1));

        }
    }

    GameObject pickPrefab()
    {
        int randInt = UnityEngine.Random.Range(1, Globals.prefabs.Length);
        return Globals.prefabs[randInt];
    }

    public void setPosition(Vector3 groundPosition, GameObject currentBlock)
    {
        MoveBlock script = currentBlock.GetComponent<MoveBlock>();
        Transform[] objChild = currentBlock.transform.GetComponentsInChildren<Transform>();
        Transform[] lowestBlocks = script.getBottomBlocks(objChild);

        Vector3 positionGround = new Vector3();

        Vector3 blockHit = new Vector3(0, Globals.levelnumber * Globals.gameHeight, 0);

        List<Vector3> blockHits = new List<Vector3>();


        //make a Raycast from every bottom block of prefab
        foreach (Transform block in lowestBlocks)
        {
            Vector3 offset3D = block.rotation * new Vector3(0.5f, 0.5f, -0.5f);

            //Debug.DrawRay(block.position + offset3D, new Vector3(0, -(Globals.levelnumber + 1) * (Globals.gameHeight + 10)), Color.red, 50f);
            Ray Ray = new Ray(block.position + offset3D, new Vector3(0, -(Globals.levelnumber + 1) * (Globals.gameHeight + 10), 0));

            blockHit = script.rayCastForHighestBlock(Ray);

            int randInt = UnityEngine.Random.Range(1, 11);

            if (blockHit.y > Globals.levelnumber * Globals.gameHeight)
            {
                blockHit = new Vector3(0, Globals.levelnumber * Globals.gameHeight + randInt * 0.1f, 0);
            }

            if (blockHit.y <= Globals.levelnumber * Globals.gameHeight)
            {
                blockHit = new Vector3(0, Globals.levelnumber * Globals.gameHeight + 0.01f, 0);
            }

            blockHits.Add(blockHit);
        }

        Vector3 highest = new Vector3(0, Globals.levelnumber * Globals.gameHeight + 0.01f, 0);

        //find the indicator with the highest Y pos
        foreach (Vector3 block in blockHits)
        {
            if (block.y > highest.y)
            {
                highest = block;
            }
        }

        positionGround = new Vector3(currentBlock.transform.position.x, highest.y, currentBlock.transform.position.z);

        Vector3 positionLowest = currentBlock.transform.position;

        foreach (Transform pos in lowestBlocks)
        {
            if (positionLowest.y > pos.position.y)
            {
                positionLowest = pos.position;
            }
        }

        //positionLowest -= new Vector3(0, 0.5f, 0);



        float newBlockY = currentBlock.transform.position.y - positionLowest.y + positionGround.y + 0.01f;
        Vector3 newPos = new Vector3(currentBlock.transform.position.x, newBlockY, currentBlock.transform.position.z);

        currentBlock.transform.position = newPos;
    }

    public Transform getBottomBlock(Transform[] blocks)
    {
        Transform lowestBlock = blocks[0];
        foreach (Transform child in blocks)
        {
            if (!child.CompareTag("Block"))
            {
                if(child.transform.position.y < lowestBlock.transform.position.y)
                {
                    lowestBlock = child;
                }
            }
        }

        return lowestBlock;
    }

    
    public Transform[] getBottomBlocks(Transform[] blocks)
    {
        List<Transform> updatedBlocks = new List<Transform>();
        foreach (Transform child in blocks)
        {
            if (!child.CompareTag("Block"))
            {
                if (checkForOtherBlocks(child))
                {
                    if (!updatedBlocks.Contains(child))
                    {
                        updatedBlocks.Add(child);
                    }
                }
            }
        }

        return updatedBlocks.ToArray();
    }
    

    public bool checkForOtherBlocks(Transform block)
    {
        Ray Ray = new Ray(block.transform.position, new Vector3(0, 1, 0));
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Ray);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            Transform parent = hit.collider.gameObject.transform.parent;
            if (parent == null)
            {
                parent = hit.collider.gameObject.transform;
            }

            if (parent.CompareTag("Block"))
            {
                return false;
            }

        }
        return true;
    }


    int pickGroundPosition(List<GameObject> list)
    {
        int randInt = UnityEngine.Random.Range(0, list.Count);
        int times = 0;

        List<int> numberList = Enumerable.Range(0, height*height-1).ToList();

        String mess = "";
        List<int> temp = posNotAllowd.ToList<int>();
        for (int i = 0; i < temp.Count; i++)
        {
            if (numberList.Contains(temp[i]))
            {
                numberList.Remove(temp[i]);
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            mess = mess + ", " + list[i].name +": "+ i;
        }

        //print("indices: " + mess);

        mess = "";

        for (int i = 0; i < numberList.Count; i++)
        {
            mess = mess + ", " + list[numberList[i]].name + ": " + numberList[i].ToString();
        }

        //print("available indices: " + mess);

        List<int> blockSpace = new List<int>();
        blockSpace.Add(randInt);
        blockSpace.Add(randInt + 1);
        blockSpace.Add(randInt - 1);
        blockSpace.Add(randInt + height);
        blockSpace.Add(randInt - height);
        blockSpace.Add(randInt + height + 1);
        blockSpace.Add(randInt - height + 1);
        blockSpace.Add(randInt + height - 1);
        blockSpace.Add(randInt - height - 1);

        while (isContaind(posNotAllowd.ToList<int>(), blockSpace) || isOutOfBounds(blockSpace))
        //while (posNotAllowd.Contains(randInt))
        {
            times++;
            if(times >= height*height)
            {
                print("break while loop");
                break;
            }

            if(randInt == height * height - 1)
            {
                randInt = 0;
            }
            else
            {
                randInt++;
            }

            blockSpace = new List<int>();
            blockSpace.Add(randInt);
            blockSpace.Add(randInt + 1);
            blockSpace.Add(randInt - 1);
            blockSpace.Add(randInt + height);
            blockSpace.Add(randInt - height);
            blockSpace.Add(randInt + height + 1);
            blockSpace.Add(randInt - height + 1);
            blockSpace.Add(randInt + height - 1);
            blockSpace.Add(randInt - height - 1);

        }

        //posNotAllowd.Add(randInt);
        mess = "";
        for (int i = 0; i < blockSpace.Count; i++)
        {
            mess = mess + ", " + blockSpace[i].ToString();
        }

        //print("check indices: " + mess);

        return randInt;
    }

    HashSet<int> getNeighborIndices(int index, Hole variant)
    {
        HashSet<int> NeighborIndices = new HashSet<int>();

        switch (variant)
        {
            case Hole.bar:
                NeighborIndices.Add(index);
                NeighborIndices.Add(index + height);
                NeighborIndices.Add(index - height);
                break;
            case Hole.line:
                NeighborIndices.Add(index);
                NeighborIndices.Add(index + 1);
                NeighborIndices.Add(index - 1);
                break;
            case Hole.handy:
                NeighborIndices.Add(index);
                NeighborIndices.Add(index + 1);
                NeighborIndices.Add(index - 1);
                NeighborIndices.Add(index + height);
                NeighborIndices.Add(index + height + 1);
                break;
            case Hole.cross:
                NeighborIndices.Add(index);
                NeighborIndices.Add(index + 1);
                NeighborIndices.Add(index - 1);
                NeighborIndices.Add(index + height);
                NeighborIndices.Add(index - height);
                break;
            case Hole.corner:
                NeighborIndices.Add(index);
                NeighborIndices.Add(index + height - 1);
                NeighborIndices.Add(index - 1);
                break;
            case Hole.big:
                NeighborIndices.Add(index);
                NeighborIndices.Add(index + 1);
                NeighborIndices.Add(index - 1);
                NeighborIndices.Add(index + height);
                NeighborIndices.Add(index - height);
                NeighborIndices.Add(index + height + 1);
                NeighborIndices.Add(index - height + 1);
                NeighborIndices.Add(index + height - 1);
                NeighborIndices.Add(index - height - 1);
                break;
            case Hole.point:
                NeighborIndices.Add(index);
                break;
            case Hole.small:
                NeighborIndices.Add(index);
                NeighborIndices.Add(index + 1);
                NeighborIndices.Add(index + height);
                NeighborIndices.Add(index + height + 1);
                break;
        }

        /*
            for (int i = 0; i <= range; i++)
            {

                NeighborIndices.Add(index - hight * i);

                NeighborIndices.Add(index + hight * i);

                //if (!isOverNearestTenner(index - hight * i - 1, index))
                //{
                NeighborIndices.Add(index - hight * i - 1);
                //}
                //if (!isUnderNearestTenner(index + hight * i - 1, index))
                //{
                NeighborIndices.Add(index + hight * i - 1);
                //}

                //if (!isOverNearestTenner(index - hight * i + 1, index))
                //{
                NeighborIndices.Add(index - hight * i + 1);
                //}
                //if (!isUnderNearestTenner(index + hight * i + 1, index))
                //{
                NeighborIndices.Add(index + hight * i + 1);
                //}
            }
        */


        NeighborIndices.RemoveWhere(isNegative);
        NeighborIndices.RemoveWhere(isOverDimnesion);

        return NeighborIndices;
    }

    void deleteTiles(List<GameObject> list)
    {
        for (int i = 0; i < Globals.deleteTiles; i++)
        {
            
            int index = UnityEngine.Random.Range(0, list.Count);
            mes = mes + "Cluster: "+ index + " -- ";
            Hole randVariant = (Hole)UnityEngine.Random.Range(0, enums);
            while (isContaind(removedIndices.ToList<int>(), getNeighborIndices(index, randVariant).ToList<int>()))
            {
                index++;
            }

            foreach (int j in getNeighborIndices(index, randVariant))
            {
                mes = mes + (j).ToString() + ", ";
                removedIndices.Add(j);
            }
            mes = mes + "-----------\n";
        }

        posNotAllowd.UnionWith(removedIndices);
        
    }

    public bool areVectorsNear(Vector3 vec1, List<Vector3> vecs)
    {
        if(vecs == null)
        {
            return false;
        }

        foreach(Vector3 vec in vecs) {
            if (Math.Abs((vec1.x + height) - (vec.x + height)) < height / 2)
            {
                if (Math.Abs((vec1.z + height) - (vec.z + height)) < height / 2)
                {
                    if (Math.Abs(vec1.y - vec.y) < Globals.gameHeight / 4)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public bool isOverMax(List<int> list, int max)
    {
        foreach (int i in list)
        {
            if (i > max)
            {
                return true;
            }
        }
        return false;
    }

    private bool isOutOfBounds(List<int> list)
    {
        foreach(int i in list)
        {
            if(i < 0)
            {
                return true;
            }

            if(i >= height * height)
            {
                return true;
            }
        }
        return false;
    }

    private bool isContaind(List<int> list1, List<int> list2)
    {
        foreach(int i in list2)
        {
            if (list1.Contains(i))
            {
                return true;
            }
        }
        return false;
    }

    private bool isNegative(int i)
    {
        return i < 0;
    }

    private bool isUnderNearestTenner(int toCheck, int benchMark)
    {
        return roundDown(benchMark) > toCheck;
    }

    private bool isOverNearestTenner(int toCheck, int benchMark)
    {
        return roundUp(benchMark) < toCheck;
    }

    private bool isOverDimnesion(int i)
    {
        return i >= transform.childCount;
    }

    private int roundDown(int toRound)
    {
        return toRound - toRound % 10;
    }

    private int roundUp(int toRound)
    {
        if (toRound % 10 == 0) return toRound;
        return (10 - toRound % 10) + toRound;
    }
}
