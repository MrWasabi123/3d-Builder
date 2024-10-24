using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnIndicator : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    private List<GameObject> indicators = new List<GameObject>();
    private Vector3 indicatorRotaionOffset = new Vector3();
    private Vector3 indicatorScaleOffset = new Vector3();
    GameObject child;


    private void Awake()
    {
        child = this.gameObject.transform.GetChild(0).gameObject;


        if (child.CompareTag("Bill"))
        {
            indicatorRotaionOffset = new Vector3(0, 90, 0);
            indicatorScaleOffset = new Vector3(0, 0, 0.12f);
        }
        if (child.CompareTag("Lawsuit"))
        {
            indicatorRotaionOffset = new Vector3(0, 90, 0);
            indicatorScaleOffset = new Vector3(0, 0, -0.12f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (gameObject != null && child != null)
        {
            spawn();
        } else
        {
            removeIndicators();
        }

    }

    private void OnDestroy()
    {
        removeIndicators();
    }

    public void spawn()
    {
        removeIndicators();


        int ColRotY = (int)transform.rotation.eulerAngles.y;

        Vector3 indicatorRotation = new Vector3(0f, ColRotY, 0f) + indicatorRotaionOffset;

        Vector3 north = new Vector3(0.4f, 0, 0.4f);
        Vector3 west = new Vector3(0.4f, 0, -0.4f);
        Vector3 south = new Vector3(-0.4f, 0, 0.4f);
        Vector3 east = new Vector3(-0.4f, 0, -0.4f);

        //Do one raycast for each corner of the cube to properly hit objects while moveing in one of the four directions. 
        Vector3 direction = new Vector3(0, -1f, 0);
        Ray rayNorth = new Ray(transform.position + north, direction);
        Ray rayWest = new Ray(transform.position + west, direction);
        Ray raySouth = new Ray(transform.position + south, direction);
        Ray rayEast = new Ray(transform.position + east, direction);


        
        Debug.DrawRay(transform.position + north, direction*10, Color.red, 10);
        Debug.DrawRay(transform.position + west, direction*10, Color.black, 10);
        Debug.DrawRay(transform.position + south, direction*10, Color.white, 10);
        Debug.DrawRay(transform.position + east, direction*10, Color.blue, 10);
        

        List<Vector3> indicatorHits = new List<Vector3>();
        indicatorHits.Add(rayCastForHighestBlock(rayNorth));
        indicatorHits.Add(rayCastForHighestBlock(rayWest));
        indicatorHits.Add(rayCastForHighestBlock(raySouth));
        indicatorHits.Add(rayCastForHighestBlock(rayEast));

        if (child.CompareTag("Bill"))
        {
            Vector3 north2 = Quaternion.Euler(indicatorRotaionOffset) * new Vector3(0.4f, 0, 0.8f);
            Vector3 west2 = Quaternion.Euler(indicatorRotaionOffset) * new Vector3(0.4f, 0, -0.8f);
            Vector3 south2 = Quaternion.Euler(indicatorRotaionOffset) * new Vector3(-0.4f, 0, 0.8f);
            Vector3 east2 = Quaternion.Euler(indicatorRotaionOffset) * new Vector3(-0.4f, 0, -0.8f);

            Ray rayNorth2 = new Ray(transform.position + north2, direction);
            Ray rayWest2 = new Ray(transform.position + west2, direction);
            Ray raySouth2 = new Ray(transform.position + south2, direction);
            Ray rayEast2 = new Ray(transform.position + east2, direction);

            indicatorHits.Add(rayCastForHighestBlock(rayNorth2));
            indicatorHits.Add(rayCastForHighestBlock(rayWest2));
            indicatorHits.Add(rayCastForHighestBlock(raySouth2));
            indicatorHits.Add(rayCastForHighestBlock(rayEast2));

            Debug.DrawRay(transform.position + north2, direction * 10, Color.red, 10);
            Debug.DrawRay(transform.position + west2, direction * 10, Color.black, 10);
            Debug.DrawRay(transform.position + south2, direction * 10, Color.white, 10);
            Debug.DrawRay(transform.position + east2, direction * 10, Color.blue, 10);
        }

        //find the highest Y position of all hits
        Vector3 highestHit = new Vector3(0, Globals.levelnumber * Globals.gameHeight, 0);

        foreach (Vector3 i in indicatorHits)
        {
            if (i.y > highestHit.y)
            {
                highestHit = i;
            }
        }

        if (highestHit.y <= Globals.levelnumber * Globals.gameHeight)
        {
            highestHit = new Vector3(0, Globals.levelnumber * Globals.gameHeight + 0.01f, 0);
        }

        GameObject currIndicator = Instantiate(indicator, new Vector3(transform.position.x, highestHit.y + 0.02f, transform.position.z), Quaternion.Euler(indicatorRotation));
        currIndicator.transform.localScale += indicatorScaleOffset;

        //Add the indicator to the scene
        indicators.Add(currIndicator);

    }

    public Vector3 rayCastForHighestBlock(Ray ray)
    {
        //Raycast for Block beneath currently active block
        Vector3 indicatorBlock = new Vector3(0, Globals.levelnumber * Globals.gameHeight, 0);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);

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
                MoveBlock script = parent.GetComponent<MoveBlock>();
                //check if hit block is not active and on the tower
                if (script.getIsOnTower() && !script.getIsActive())
                {
                    if (hit.point.y > indicatorBlock.y)
                    {
                        indicatorBlock = hit.point;
                    }
                }
            }
        }

        //indicatorBlockY != Globals.levelnumber * Globals.gameHeight when raycast hit a block
        if (indicatorBlock.y != Globals.levelnumber * Globals.gameHeight)
        {
            return indicatorBlock;
        }

        //if raycast hit no block Y pos of the current level is returned. + offset for z-fighting
        return new Vector3(0, Globals.levelnumber * Globals.gameHeight, 0);
    }

    public void removeIndicators()
    {
        foreach (GameObject indi in indicators)
        {
            if (indi != null)
            {
                Destroy(indi);
            }
        }

        indicators = new List<GameObject>();
    }
}
