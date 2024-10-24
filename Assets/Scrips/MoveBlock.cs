using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveBlock : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    private GameObject cameraContainer;
    private Vector3[] lastRotation = new Vector3[2];

    public float speed = 0.5f;
    private bool isActive = false;
    private float movementX;
    private float movementY;
    private float rotationX;
    private float rotationY;
    private float rotationZ;
    private float rot;

    private float movepercent = 0.5f;
    float time;

    private Vector3 margin = new Vector3(0.00025f, 0.00025f, 0.00025f);
    private Vector3 indicatorMargin = new Vector3(0.1f, 0f, -0.1f);
    Vector3 lastPosition;
    Transform myTransform;
    Rigidbody rb;

    private GameObject craneBottom;
    private HingeJoint hinge;
    private Rigidbody block;

    
    bool isMoving;
    bool isOnTower;
    bool blockCollisionDetected;
    bool velocitySet;

    //trying the different block types
    Color heavy = new Color(0.415f, 0.415f, 0.415f, 1f);
    Color glass = new Color(0.7f, 1f, 1f, 0.4f);
    Color wood = new Color(1f, 0.8f, 0.6f, 1f);

    private ParticleSystem part;

    private Coroutine MP;
    private Coroutine RP;

    private List<GameObject> indicators = new List<GameObject>();

    public Globals.BlockType bt;

    private bool hasCollided = false;

    private bool rotStart = false;

    private int[] mulMove = new int[]{1,2,3,4};

    private GameObject sounds;


    private void Awake()
    {
        myTransform = transform;
        lastPosition = myTransform.position;
        isMoving = false;
        blockCollisionDetected = false;
        isOnTower = false;
        velocitySet = false;
        time = 0f;
        rb = this.GetComponent<Rigidbody>();
        part = GameObject.FindWithTag("GlassPart").GetComponent<ParticleSystem>();
        craneBottom = GameObject.FindWithTag("CraneBottom");
        sounds = GameObject.FindWithTag("soundsglobal");

        cameraContainer = GameObject.FindWithTag("CameraContainer");

    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.useGravity = true;

        //print(bt);
        if(!hasCollided){
            Globals.blockReady = true;
            hasCollided = true;
        }
        GameObject currentObject;
        if (collision.gameObject.transform.parent != null)
        {
            currentObject = collision.gameObject.transform.parent.gameObject;
        } else
        {
            currentObject = collision.gameObject;
        }
        
        //print(this.name + ": " + currentObject.name);
        if (currentObject.CompareTag("Block") || currentObject.CompareTag("Level"))
        {
            
            if (!blockCollisionDetected)
            {
                blockCollisionDetected = true;
                // print(this.name + ": " + rb.velocity.ToString());
                rb.velocity = rb.velocity * 0.1f;
                //rb.AddForce(new Vector3(0f,5f,0f));
            }
            
            
        }

        if (currentObject.CompareTag("Block"))
        {
            collisionTypeCheckEnum(currentObject);
        }


    }

    private void OnMove(InputValue input)
    {
        Vector2 movement = input.Get<Vector2>();

        if (movement != new Vector2(0f, 0f))
        {
            movementX = movement.x;
            movementY = movement.y;

            
            if(Globals.BlockOneDirection){
                if(Globals.ODX == 0){
                    Globals.ODX = movementX;
                }else{
                    if(movementX != Globals.ODX){
                        movementX = 0;
                    }
                }
                if(Globals.ODY == 0){
                    Globals.ODY = movementY;
                }else{
                    if(movementY != Globals.ODY){
                        movementY = 0;
                    }
                }
            } 
            

            MovePiece(movementX, movementY);
        }
    }

    /* private void OnMoveCon(InputValue input)
    {
        Vector2 movement = input.Get<Vector2>();
        //print(movement);
        if (movement != new Vector2(0f, 0f))
        {
            movementX = movement.x;
            movementY = movement.y;
            if (movementX != 0 || movementY != 0)
            {
                IEnumerator movec = MovePieceCon(movementX, movementY, 0.5f);
                MP = StartCoroutine(movec);
            }
            else
            {
                if (MP != null)
                {
                    StopCoroutine(MP);
                }
            }
        }
    } */


    private void MovePiece(float movementX, float movementY){
        sounds.GetComponent<SoundsGlobal>().playSound("click");
        if (this.isActive) {
            if(Globals.BlockMovementPlus == 0){
                
                if(mulMove[Globals.cameraRotation] == 1){
                    MoveUp(movementX, movementY);
                }else if(mulMove[Globals.cameraRotation] == 2){
                    MoveLeft(movementX, movementY);
                }else if(mulMove[Globals.cameraRotation] == 3){
                    MoveDown(movementX, movementY);
                }else if(mulMove[Globals.cameraRotation] == 4){
                    MoveRight(movementX, movementY);
                }
                /*
                Vector3 zAxis = new Vector3((float)Math.Round(cameraContainer.transform.forward.x, 0), 0, (float)Math.Round(cameraContainer.transform.forward.z, 0));
                Vector3 xAxis = new Vector3((float)Math.Round(cameraContainer.transform.right.x, 0), 0, (float)Math.Round(cameraContainer.transform.right.z, 0));


                this.transform.position += (xAxis * movementX + zAxis * movementY) * movepercent * Globals.BlockMovement;
                */
            }
            else{
                randomMove(Globals.BlockMovementPlus, movementX, movementY);
            }


            spawnIndicator();
        }

    }
    private void MoveUp(float movementX, float movementY){
        Vector3 Movement = new Vector3(movementX, 0, movementY);
        this.transform.position += Movement * movepercent*Globals.BlockMovement;
    }
    private void MoveLeft(float movementX, float movementY){
        Vector3 Movement = new Vector3(-movementY, 0, movementX);
        this.transform.position += Movement * movepercent*Globals.BlockMovement;
    }
    private void MoveDown(float movementX, float movementY){
        Vector3 Movement = new Vector3(-movementX, 0, -movementY);
        this.transform.position += Movement * movepercent*Globals.BlockMovement;
    }
    private void MoveRight(float movementX, float movementY){
        Vector3 Movement = new Vector3(movementY, 0, -movementX);
        this.transform.position += Movement * movepercent*Globals.BlockMovement;
    }
    private void randomMove(int x,float movementX, float movementY){
        print(x);
        if (x==1){
            /*
            Vector3 zAxis = new Vector3((float)Math.Round(cameraContainer.transform.forward.x, 0), 0, (float)Math.Round(cameraContainer.transform.forward.z, 0));
            Vector3 xAxis = new Vector3((float)Math.Round(cameraContainer.transform.right.x, 0), 0, (float)Math.Round(cameraContainer.transform.right.z, 0));

            this.transform.position += (-xAxis * movementX + zAxis * movementY) * movepercent * Globals.BlockMovement;
            */
            if(mulMove[Globals.cameraRotation] == 1){
                MoveLeft(movementX, movementY);
            }else if(mulMove[Globals.cameraRotation] == 2){
                MoveDown(movementX, movementY);
            }else if(mulMove[Globals.cameraRotation] == 3){
                MoveRight(movementX, movementY);
            }else if(mulMove[Globals.cameraRotation] == 4){
                MoveUp(movementX, movementY);
            }
        }
        else if(x==2){
            
            if(mulMove[Globals.cameraRotation] == 1){
                MoveDown(movementX, movementY);
            }else if(mulMove[Globals.cameraRotation] == 2){
                MoveRight(movementX, movementY);
            }else if(mulMove[Globals.cameraRotation] == 3){
                MoveUp(movementX, movementY);
            }else if(mulMove[Globals.cameraRotation] == 4){
                MoveLeft(movementX, movementY);
            }
            /*
            Vector3 zAxis = new Vector3((float)Math.Round(cameraContainer.transform.forward.x, 0), 0, (float)Math.Round(cameraContainer.transform.forward.z, 0));
            Vector3 xAxis = new Vector3((float)Math.Round(cameraContainer.transform.right.x, 0), 0, (float)Math.Round(cameraContainer.transform.right.z, 0));

            this.transform.position += (xAxis * movementX - zAxis * movementY) * movepercent * Globals.BlockMovement;
            */
        }
        else if(x==3){
            
            if(mulMove[Globals.cameraRotation] == 1){
                MoveRight(movementX, movementY);
            }else if(mulMove[Globals.cameraRotation] == 2){
                MoveUp(movementX, movementY);
            }else if(mulMove[Globals.cameraRotation] == 3){
                MoveLeft(movementX, movementY);
            }else if(mulMove[Globals.cameraRotation] == 4){
                MoveDown(movementX, movementY);
            }
            /*
            Vector3 zAxis = new Vector3((float)Math.Round(cameraContainer.transform.forward.x, 0), 0, (float)Math.Round(cameraContainer.transform.forward.z, 0));
            Vector3 xAxis = new Vector3((float)Math.Round(cameraContainer.transform.right.x, 0), 0, (float)Math.Round(cameraContainer.transform.right.z, 0));

            this.transform.position += (-xAxis * movementX - zAxis * movementY) * movepercent * Globals.BlockMovement;
            */
        }
    }

    IEnumerator MovePieceCon(float mx, float my, float rr){
        while(true){
            MovePiece(mx,my);
            yield return new WaitForSeconds(rr);
        }

    }

    private void OnDrag(InputValue input){
        Vector2 rotation = input.Get<Vector2>();
        // print(rotation);
        rotationX = rotation.x;
        rotationZ = rotation.y;
        //rotatePiece(rotationX,0f,rotationZ);
        StartCoroutine(rotateDelay(rotationX,0f,rotationZ));

    }
    private void OnYRot(InputValue input){
        float yrot = input.Get<float>();
        // print(yrot);
        rotationY = yrot;
        //rotatePiece(0f,rotationY,0f);
        StartCoroutine(rotateDelay(0f,rotationY,0f));
    }

/*     private void rotatePiece(float rotationX, float rotationY, float rotationZ){
        if (this.isActive) { 
            hinge = craneBottom.GetComponent<HingeJoint>();
            Destroy(hinge);
            Vector3 Rotation = new Vector3(rotationX, rotationY, rotationZ);
            this.transform.Rotate(Rotation*Globals.rotationSpeed);
            hinge = craneBottom.AddComponent<HingeJoint>();
            hinge.connectedBody = this.GetComponent<Rigidbody>();
            spawnIndicator();
        }
    } */
    private bool checkBool(float a, float b, float c){
        float test = Mathf.Abs(a) + Mathf.Abs(b) + Mathf.Abs(c);
        if(test == 1f){
            return true;
        }else{
            return false;
        }
    }
    IEnumerator rotateDelay(float rotationX, float rotationY, float rotationZ){
        //yield return new WaitForSeconds(0.1f);
        if (checkBool(rotationX,rotationY,rotationZ))
        {
            if (this.isActive && !rotStart && !Globals.blockRotating)
            {
                sounds.GetComponent<SoundsGlobal>().playSound("click");
                rotStart = true;
                Globals.blockRotating = true;
                hinge = craneBottom.GetComponent<HingeJoint>();
                Destroy(hinge);
                yield return new WaitForSeconds(0.01f);

                Vector3 xAxis = new Vector3((float)Math.Round(cameraContainer.transform.forward.x, 0), 0, (float)Math.Round(cameraContainer.transform.forward.z, 0));
                Vector3 yAxis = Vector3.up;
                Vector3 zAxis = new Vector3((float)Math.Round(cameraContainer.transform.right.x, 0), 0, (float)Math.Round(cameraContainer.transform.right.z, 0));

                for (int i = 0; i <Globals.rotationSpeed/5;i++){
                    transform.RotateAround(transform.position, xAxis*-1, rotationX * 5);
                    transform.RotateAround(transform.position, yAxis*-1, rotationY * 5);
                    transform.RotateAround(transform.position, zAxis, rotationZ * 5);
                    yield return new WaitForSeconds(0.01f);
                }
                yield return new WaitForSeconds(0.01f);
                Globals.blockRotating = false;
                hinge = craneBottom.AddComponent<HingeJoint>();
                hinge.connectedBody = this.GetComponent<Rigidbody>();
                rotStart = false;
                spawnIndicator();
            }
        }
        //yield return new WaitForSeconds(0.1f);
    }

    public void spawnIndicator()
    {
        removeIndicators();
        Transform[] objChildren = gameObject.transform.GetComponentsInChildren<Transform>();
        objChildren = getBottomBlocks(objChildren);

        List<Vector3> blockCenters = new List<Vector3>();
        Vector3 blockPlane = objChildren[0].position + objChildren[0].transform.rotation * new Vector3(0.5f, 0.5f, -0.5f);

        foreach (Transform child in objChildren)
        {
            //calculate offset3D to change the origin of the cube to the middle of the 3d object
            Vector3 pos = child.position + child.transform.rotation * new Vector3(0.5f, 0.5f, -0.5f);
            if(pos.y < blockPlane.y)
            {
                blockPlane = pos;
            }
        }

            
        foreach (Transform child in objChildren){

            int blockRotY = (int)child.transform.rotation.eulerAngles.y;

            //calculate offset3D to change the origin of the cube to the middle of the 3d object
            Vector3 offset3D = child.transform.rotation * new Vector3(0.5f, 0.5f, -0.5f);

            //calculate offsetIndicator2D to change the origin of the indicator to the middle of the 2d object
            Vector3 offsetIndicator2D = Vector3.ProjectOnPlane(offset3D, new Vector3(0, 1, 0));


            Vector3 indicatorRotation = new Vector3(0f, blockRotY, 0f);

            Vector3 north = new Vector3(0.4f, 0, 0.4f);
            Vector3 west = new Vector3(0.4f, 0, -0.4f);
            Vector3 south = new Vector3(-0.4f, 0, 0.4f);
            Vector3 east = new Vector3(-0.4f, 0, -0.4f);

            Vector3 childCenter = child.position + offset3D;
            childCenter = new Vector3(childCenter.x, blockPlane.y, childCenter.z) - new Vector3(0, 0.5f, 0);

            blockCenters.Add(childCenter);

            //Do one raycast for each corner of the cube to properly hit objects while moveing in one of the four directions. 
            Ray rayNorth = new Ray(childCenter + north, Globals.calculateDirectionVector());
            Ray rayWest = new Ray(childCenter + west, Globals.calculateDirectionVector());
            Ray raySouth = new Ray(childCenter + south, Globals.calculateDirectionVector());
            Ray rayEast = new Ray(childCenter + east, Globals.calculateDirectionVector());

            

            /*
            Debug.DrawRay(child.position + west + offset, Globals.calculateDirectionVector(), Color.white, 10);
            Debug.DrawRay(child.position + south + offset, Globals.calculateDirectionVector(), Color.cyan, 10);
            Debug.DrawRay(child.position + east + offset, Globals.calculateDirectionVector(), Color.blue, 10);
            */

            List<Vector3> indicatorHits = new List<Vector3>();
            indicatorHits.Add(rayCastForHighestBlock(rayNorth));
            indicatorHits.Add(rayCastForHighestBlock(rayWest));
            indicatorHits.Add(rayCastForHighestBlock(raySouth));
            indicatorHits.Add(rayCastForHighestBlock(rayEast));

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

            //Add the indicator to the scene
            indicators.Add(Instantiate(indicator, new Vector3(childCenter.x, highestHit.y + 0.02f, childCenter.z), Quaternion.Euler(indicatorRotation)));
        }

        float highest = Globals.levelnumber * Globals.gameHeight + 0.01f;


        //find the indicator with the highest Y pos
        foreach (GameObject indicator in indicators)
        {
            if (indicator.transform.position.y > highest)
            {
                highest = indicator.transform.position.y;
            }
        }

        Plane plane = new Plane(new Vector3(0, 1, 0), new Vector3(0, highest, 0));

        int index = 0;
        //set Y pos of each indicator to the highest
        foreach (GameObject indicator in indicators)
        {
            
            Ray rayCenter = new Ray(blockCenters[index], Globals.calculateDirectionVector());

            float enter = 0.0f;
            Vector3 hitPoint = new Vector3(0, 0, 0);

            if (plane.Raycast(rayCenter, out enter))
            {
                //Get the point that is clicked
                hitPoint = rayCenter.GetPoint(enter);

            }

            Vector3 position = new Vector3(hitPoint.x, highest, hitPoint.z);
            indicator.transform.position = position;
            index++;
        }
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
                if (script.isOnTower && !script.isActive)
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


    public Transform[] getBottomBlocks(Transform[] blocks)
    {
        List<Transform> updatedBlocks = new List<Transform>();
        foreach (Transform child in blocks)
        {
            if (!child.CompareTag("Block"))
            {
                if (isLowestAlt(child) && !updatedBlocks.Contains(child))
                {
                    updatedBlocks.Add(child);
                }
            }
        }
        return updatedBlocks.ToArray();
    }


    public bool isLowest(Transform block)
    {
        Vector3 offset = block.rotation * new Vector3(0.5f, 0, -0.5f);

        Ray ray = new Ray(block.position + offset, new Vector3(0, -1, 0));

        //Raycast beneath current cube of block to check if there is another cube.
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

            if (parent == block.parent)
            {
                //if raycast hits cube, current cube is not lowest
                return false;
            }

        }
        //if raycast hits no cube, current block is lowest
        return true;
    }

    public bool isLowestAlt(Transform cube)
    {
        Vector3 offset = cube.rotation * new Vector3(0.5f, 0.5f, -0.5f) - new Vector3(0, 0.5f, 0);

        Ray ray = new Ray(cube.position + offset, new Vector3(0, -1, 0));

        Debug.DrawRay(cube.position + offset, new Vector3(0, -0.25f, 0), Color.red, 10);

        RaycastHit hitSingle;

        if (Physics.Raycast(ray, out hitSingle, 0.25f))
        {

            Transform parent = hitSingle.collider.gameObject.transform.parent;
            if (parent == null)
            {
                parent = hitSingle.collider.gameObject.transform;
            }


            if (parent == cube.parent)
            {
                //if raycast hits cube, current cube is not lowest
                return false;
            }
        }

        //if raycast hits no cube, current block is lowest
        return true;
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

    private void OnDestroy()
    {
        removeIndicators();
    }

    void FixedUpdate()
    {
        /* if (this.isActive) { 
            Vector3 Movement = new Vector3(movementX, 0, movementY);
            print(Movement);
            this.transform.position += Movement;
            Vector3 Rotation = new Vector3(rotationX, rotationY, rotationZ);
            this.transform.Rotate(Rotation*rotationSpeed);
        } */

        //stopMovementOnTower();

        

        if(!isActive && !hasCollided)
        {
            time += Time.deltaTime;

            rb.velocity = Globals.direction.normalized * 0.5f * time * 9.81f;
        }

        if (!isInThreshhold(myTransform.position, lastPosition))
            isMoving = true;
        else
            isMoving = false;

        if (!isMoving && blockCollisionDetected)
        {
            this.isOnTower = true;
        }
        /*         if(!this.craneActive && craneTop.transform.position != cranePos + new Vector3(0,3*Globals.levelnumber,0)){
                    print(cranePos + new Vector3(0,3*Globals.levelnumber,0));
                    craneTop.transform.position = Vector3.MoveTowards(craneTop.transform.position, cranePos + new Vector3(0,3*Globals.levelnumber,0), Time.deltaTime *1);
                    craneBottom.transform.position = Vector3.MoveTowards(craneBottom.transform.position, craneBotPos + new Vector3(0,3*Globals.levelnumber,0), Time.deltaTime *1);
                } */

        lastPosition = myTransform.position;

        if(!isActive && isOnTower)
        {
            if(transform.position.y > Globals.heighestBlock)
            {
                Globals.heighestBlock = transform.position.y;
            }
        }
    }

    public void setIsAcitve(bool boo)
    {
        this.isActive = boo;
    }

    public bool getIsActive()
    {
        return this.isActive;
    }

    public bool getIsMoving()
    {
        return this.isMoving;
    }

    public bool getIsOnTower()
    {
        return this.isOnTower;
    }

    public void setIsOnTower(bool v)
    {
        this.isOnTower = v;
    }

    public bool getHasCollided()
    {
        return this.hasCollided;
    }

    public void setHasCollided(bool v)
    {
        this.hasCollided = v;
    }

    public bool isInThreshhold(Vector3 vec1, Vector3 vec2)
    {
        if (vec2.x - margin.x <= vec1.x && vec1.x <= vec2.x + margin.x)
        {
            if (vec2.y - margin.y <= vec1.y && vec1.y <= vec2.y + margin.y)
            {
                if (vec2.z - margin.z <= vec1.z && vec1.z <= vec2.z + margin.z)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void collisionTypeCheck(GameObject cObject){
        Color blockColor = this.GetComponentInChildren<Renderer>().material.color;
        if(blockColor == heavy && !getIsOnTower()){
            Color collidedColor = cObject.GetComponentInChildren<Renderer>().material.color;
            if(collidedColor == glass){
                Destroy(cObject);
                Instantiate(part, cObject.transform.position, part.transform.rotation);
                var main = part.main;
                main.stopAction = ParticleSystemStopAction.Destroy;
                part.Play();
            }else if(collidedColor == wood){
                //print("wood");
            }else{
                //print("test2");
            }
        }

    }
    private void collisionTypeCheckEnum(GameObject cObject){

        Globals.BlockType b = bt;
        if (b == Globals.BlockType.Heavy && !getIsOnTower())
        {
            Globals.BlockType cb = cObject.GetComponent<MoveBlock>().bt;
            float thisy = this.transform.position.y;
            float othery = cObject.transform.position.y;
            //print(thisy);
            //print(othery);
            if (cb == Globals.BlockType.Glass && thisy>othery)
            {
                Destroy(cObject);
                Instantiate(part, cObject.transform.position, part.transform.rotation);
                var main = part.main;
                main.stopAction = ParticleSystemStopAction.Destroy;
                part.Play();
            }
            else if (cb == Globals.BlockType.Wood)
            {
                //print("wood");
            }
            else
            {
                //print("test2");
            }
        }

    }
}
