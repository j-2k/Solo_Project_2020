using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    public GameObject gNode;
    public GameObject gWall;
    public GameObject gGoal;
    public int xWidth;
    public int yHeight;
    bool spawnWalls = false;
    //=================================================================================================
    //public IDictionary<Vector3, bool> walkableNodes = new Dictionary<Vector3, bool>();
    //public IDictionary<Vector3, GameObject> nodeReference = new Dictionary<Vector3, GameObject>();
    //=================================================================================================


    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x <= xWidth; x+=5)
        {
            for (int y = 0; y <= yHeight; y+=5)
            {
                Vector3 newPosition = new Vector3((transform.position.x + x), (transform.position.y + y), transform.position.z);
                if (x == xWidth && y == yHeight)
                {
                    //GameObject goalNode = GameObject.FindGameObjectWithTag("Goal");
                    GameObject goalNode;
                    goalNode = Instantiate(gGoal);
                    goalNode.transform.position = newPosition;
                    goalNode.transform.parent = this.transform;
                    //=================================================================================================
                    //walkableNodes.Add(new KeyValuePair<Vector3, bool>(newPosition, true));
                    //nodeReference.Add(new KeyValuePair<Vector3, GameObject>(newPosition, goalNode));
                    //=================================================================================================
                    break;
                }
                if (spawnWalls == false) //1st Node must always be walkable
                {
                    GameObject copyNode;
                    copyNode = Instantiate(gNode);
                    copyNode.transform.position = newPosition;
                    copyNode.transform.parent = this.transform;
                    //walkableNodes.Add(new KeyValuePair<Vector3, bool>(newPosition, true));
                }
                Debug.DrawLine(newPosition,Camera.main.transform.position, UnityEngine.Color.yellow);
                if (spawnWalls == true)
                {
                    int rng = Random.Range(1, 101); //1-100
                    if (rng <= 75) //gNode = Walkable
                    {
                        GameObject copyNode;
                        copyNode = Instantiate(gNode);
                        copyNode.transform.position = newPosition;
                        copyNode.transform.parent = this.transform;
                        //walkableNodes.Add(new KeyValuePair<Vector3, bool>(newPosition, true));
                    }
                    else //if (rng > 80) // gWall = Unwalkable
                    {
                        GameObject copyWall;
                        copyWall = Instantiate(gWall);
                        copyWall.transform.position = newPosition;
                        copyWall.transform.parent = this.transform;
                        //walkableNodes.Add(new KeyValuePair<Vector3, bool>(newPosition, false));
                    }
                }
                spawnWalls = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
