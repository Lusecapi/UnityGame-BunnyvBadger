using UnityEngine;
using System.Collections;
using System.IO;

public class WorldGenerator : MonoBehaviour {

    float tamBloque = 1;
    int numberOfBadgers = 3;

    public float timePassed = 0;

	// Use this for initialization
	void Start () {

        readWorldFile("level01.txt");
        //spawnBadger();
        InvokeRepeating("spawnBadger", 1, 2);
                
        
    }
	
	// Update is called once per frame
	void Update () {

        //timePassed += Time.deltaTime;
        //if (timePassed > 5)
        //{
        //    if (numberOfBadgers > 0)
        //    {
        //        InvokeRepeating("spawnBadger", , 5);
        //        timePassed = 0;
        //    }
        //}
	}

    void spawnBadger()
    {
        numberOfBadgers--;
        GameObject badger = Resources.Load("smallBadger") as GameObject;
        Transform stP = GameObject.Find("StartPoint").transform;
        if (stP.transform.rotation.Equals(Badger.directionMoveLeft))
        {
            badger.GetComponent<Badger>().moveDirection = Badger.directionMoveLeft;
        }
        else
            if (stP.transform.rotation.Equals(Badger.directionMoveDown))
        {
            badger.GetComponent<Badger>().moveDirection = Badger.directionMoveDown;
        }
        else
            if (stP.transform.rotation.Equals(Badger.directionMoveRight))
        {
            badger.GetComponent<Badger>().moveDirection = Badger.directionMoveRight;
        }
        else
            if (stP.transform.rotation.Equals(Badger.directionMoveUp))
        {
            badger.GetComponent<Badger>().moveDirection = Badger.directionMoveUp;
        }
        Instantiate(badger, stP.position, stP.rotation);
        

        if (numberOfBadgers == 0)
        {
            CancelInvoke("spawnBadger");
        }
    }

    void readWorldFile(string file)
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/WorldFiles" + "/" + file);
        int row=0;
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            string[] vec = line.Split(';');
            for(int i=0; i < vec.Length; i++)
            {
                GameObject Tile= null;
                if (vec[i].Equals("0") || vec[i].Equals("s1") || vec[i].Equals("s2") || vec[i].Equals("s3") || vec[i].Equals("s4"))//Grass and Start Points
                {
                    Tile = Resources.Load("smallGrass") as GameObject;
                    if (!vec[i].Equals("0"))
                    {
                        setStartPoint(vec[i], i, row);
                    }
                }
                else
                    if (vec[i].Equals("1") || vec[i].Equals("p1u") || vec[i].Equals("p1d") || vec[i].Equals("p2l") || vec[i].Equals("p2r") || vec[i].Equals("p3u") || vec[i].Equals("p3d") || vec[i].Equals("p4l") || vec[i].Equals("p4r"))//Grass and Path
                {
                    Tile = Resources.Load("smallGrassnPath") as GameObject;
                    setGrassNPath(Tile, vec[i]);
                    
                }
                else
                    if (vec[i].Equals("f1") || vec[i].Equals("f2") || vec[i].Equals("f3") || vec[i].Equals("f4"))//Finish Point
                {
                    Tile = Resources.Load("smallBunnyTower") as GameObject;
                    setFinishPoint(Tile,vec[i], i, row);
                    Transform finishPoint = GameObject.Find("FinishPoint").transform;
                    Instantiate(Resources.Load("smallBunnyOfTower"), finishPoint.position, finishPoint.rotation);
                    
                }
                Instantiate(Tile, new Vector2(tamBloque * i, row * tamBloque), Tile.transform.rotation);

            }
            row--;
        }
    }

    void setGrassNPath(GameObject Tile,string grasNPathType)
    {
        if(grasNPathType.Equals("1"))
        {
            Tile.GetComponent<GrassNPath>().isLimitPoint= false;
        }
        else
        {
            if (grasNPathType.Equals("p1u"))//move to left commung from up
            {
                Tile.GetComponent<GrassNPath>().isLimitPoint = true;
                Tile.GetComponent<GrassNPath>().nextBadgerLookRotation = Badger.rotationLookLeft;
                Tile.GetComponent<GrassNPath>().nextBadgerDirection = Badger.directionMoveLeft;
                Tile.transform.eulerAngles = GrassNPath.rotationLookDown;
            }
            else
                if (grasNPathType.Equals("p1d"))//move to left comming from down
            {
                Tile.GetComponent<GrassNPath>().isLimitPoint = true;
                Tile.GetComponent<GrassNPath>().nextBadgerLookRotation = Badger.rotationLookLeft;
                Tile.GetComponent<GrassNPath>().nextBadgerDirection = Badger.directionMoveLeft;
                Tile.transform.eulerAngles = GrassNPath.rotationLookUp;
            }
            else
                if (grasNPathType.Equals("p2l"))//move to down comming from left
            {
                Tile.GetComponent<GrassNPath>().isLimitPoint = true;
                Tile.GetComponent<GrassNPath>().nextBadgerLookRotation = Badger.rotationLookDown;
                Tile.GetComponent<GrassNPath>().nextBadgerDirection = Badger.directionMoveDown;
                Tile.transform.eulerAngles = GrassNPath.rotationLookRight;
            }
            else
                if (grasNPathType.Equals("p2r"))//move to down comming from right
            {
                Tile.GetComponent<GrassNPath>().isLimitPoint = true;
                Tile.GetComponent<GrassNPath>().nextBadgerLookRotation = Badger.rotationLookDown;
                Tile.GetComponent<GrassNPath>().nextBadgerDirection = Badger.directionMoveDown;
                Tile.transform.eulerAngles = GrassNPath.rotationLookLeft;
            }
            else
                if (grasNPathType.Equals("p3u"))//move to right comming from up
            {
                Tile.GetComponent<GrassNPath>().isLimitPoint = true;
                Tile.GetComponent<GrassNPath>().nextBadgerLookRotation = Badger.rotationLookRight;
                Tile.GetComponent<GrassNPath>().nextBadgerDirection = Badger.directionMoveRight;
                Tile.transform.eulerAngles = GrassNPath.rotationLookDown;
            }
            else
                if (grasNPathType.Equals("p3d"))//moving to rigth comming from down
            {
                Tile.GetComponent<GrassNPath>().isLimitPoint = true;
                Tile.GetComponent<GrassNPath>().nextBadgerLookRotation = Badger.rotationLookRight;
                Tile.GetComponent<GrassNPath>().nextBadgerDirection = Badger.directionMoveRight;
                Tile.transform.eulerAngles = GrassNPath.rotationLookUp;
            }
            else
                if (grasNPathType.Equals("p4l"))
            {
                Tile.GetComponent<GrassNPath>().isLimitPoint = true;
                Tile.GetComponent<GrassNPath>().nextBadgerLookRotation = Badger.rotationLookUp;
                Tile.GetComponent<GrassNPath>().nextBadgerDirection = Badger.directionMoveUp;
                Tile.transform.eulerAngles = GrassNPath.rotationLookRight;
            }
            else
                if (grasNPathType.Equals("p4r"))
            {
                Tile.GetComponent<GrassNPath>().isLimitPoint = true;
                Tile.GetComponent<GrassNPath>().nextBadgerLookRotation = Badger.rotationLookUp;
                Tile.GetComponent<GrassNPath>().nextBadgerDirection = Badger.directionMoveUp;
                Tile.transform.eulerAngles = GrassNPath.rotationLookLeft;
            }
        }
    }

    void setStartPoint(string startPonintType, int i, int row)
    {
        GameObject.Find("StartPoint").transform.position = new Vector2(tamBloque * i, row * tamBloque);
        if (startPonintType.Equals("s1"))
        {
            
        }
        else
            if (startPonintType.Equals("s2"))//Start Point to Down
        {
            GameObject.Find("StartPoint").transform.eulerAngles = new Vector3(0f, 0f, 90f);
        }
        else
            if (startPonintType.Equals("s3"))//Start point to right 
        {
            GameObject.Find("StartPoint").transform.eulerAngles = new Vector3(0f, 0f, 180f);
        }
        else
            if (startPonintType.Equals("s4"))//Start point to up
        {
            GameObject.Find("StartPoint").transform.eulerAngles = new Vector3(0f, 0f, 270f);
        }
    }

    void setFinishPoint(GameObject Tile,string finishPointType, int i, int row)
    {
        GameObject.Find("FinishPoint").transform.position = new Vector2(tamBloque * i, row * tamBloque);
        if (finishPointType.Equals("f1"))
        {
            GameObject.Find("FinishPoint").transform.eulerAngles = new Vector3(0f, 0f, 180f);
            Tile.GetComponent<BunnyTower>().transform.eulerAngles = BunnyTower.rotationLookLeft;
        }
        else
            if (finishPointType.Equals("f2"))
        {
            GameObject.Find("FinishPoint").transform.eulerAngles = new Vector3(0f, 0f, 270f);
            Tile.GetComponent<BunnyTower>().transform.eulerAngles = BunnyTower.rotationLookDown;
        }
        else
            if (finishPointType.Equals("f3"))
        {
            GameObject.Find("FinishPoint").transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Tile.GetComponent<BunnyTower>().transform.eulerAngles = BunnyTower.rotationLookRight;
        }
        else
            if (finishPointType.Equals("f4"))
        {
            GameObject.Find("FinishPoint").transform.eulerAngles = new Vector3(0f, 0f, 90f);
            Tile.GetComponent<BunnyTower>().transform.eulerAngles = BunnyTower.rotationLookUp;
        }

    }
}
