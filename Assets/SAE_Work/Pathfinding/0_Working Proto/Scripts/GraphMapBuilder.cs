using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions.Must;
using DijkstraAPI;
using JetBrains.Annotations;

public class GraphMapBuilder : MonoBehaviour
{ 
    private GameObject[] nodes;
    private float[][] connections;
    [SerializeField] LayerMask wallMaskInsp;
    //=========================================
    Dijkstra dijkstraAPIClass = new Dijkstra();
    public Vector3[] nodesPositions;
    Vector3 nodeAdder;
    int iCounter = 0;
    private void Start()
    {
        //OPTIMIZED / STATIC
        //BuildGraph2D();
        //DijkstraShortestPath();
        wallMaskInsp = LayerMask.GetMask("WallLayerMask");
        //if u want updated results must be in update func & optimized for it
        UseAPIMethod();
        dijkstraAPIClass.nodes = ConvertVectors(nodesPositions);
    }

    private void Update()
    {
        //CARE THIS IS EVERY FRAME
        BuildGraph2D();
        DrawGraphLines();
        DijkstraShortestPath();
        dijkstraAPIClass.DijkstraShortestPath();
        DebugPathsUnityANDAPI();
    }

    private void DebugPathsUnityANDAPI()
    {
        print("API VERSION");
        print("API path count = " + dijkstraAPIClass.path.Count);
        foreach (Dijkstra.Vector3 i in dijkstraAPIClass.path)
        {
            print("x = " + i.X + " y= " + i.Y + " z = " + i.Z);
        }
        print("Unity VERSION");
        print("UNITY path count = " + path.Count);
        foreach (Vector3 i in path)
        {
            print("x = " + i.x + " y= " + i.y + " z = " + i.z);
        }
    }

    private Dijkstra.Vector3[] ConvertVectors(Vector3[] vect)
    {
        Dijkstra.Vector3[] converted = new Dijkstra.Vector3[vect.Length];
        float[] vecArr = new float[vect.Length * 3];
        int iFE = 0;
        foreach (Vector3 vec in vect)
        {
            vecArr[iFE] = vec.x;
            iFE++;
            vecArr[iFE] = vec.y;
            iFE++;
            vecArr[iFE] = vec.z;
            iFE++;
        }
        for (int i = 0; i < vect.Length; i++)
        {
            converted[i] = new Dijkstra.Vector3(vect[i].x, vect[i].y, vect[i].z);// vect[i].x, vect[i].y, vect[i].z);
            //converted[i] = new DijkstraAPI.Dijkstra.Vector3(vecArr);
        }
        return converted;
    }

    private void UseAPIMethod()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");
        dijkstraAPIClass.nodes = new Dijkstra.Vector3[nodes.Length];
        dijkstraAPIClass.connections = new float[nodes.Length][];
        nodesPositions = new Vector3[nodes.Length];
        foreach (GameObject node in nodes)
        {
            nodeAdder = node.transform.position;
            nodesPositions[0 + iCounter] = nodeAdder;
            iCounter++;
        }
        //===========================================================
        for (int i = 0; i < dijkstraAPIClass.connections.Length; i++)
        {
            dijkstraAPIClass.connections[i] = new float[nodes.Length];
            for (int j = 0; j < dijkstraAPIClass.connections[i].Length; j++)
            {
                Vector3 direction;
                direction = (nodes[j].transform.position - nodes[i].transform.position);
                float distance;
                distance = Vector3.Distance(nodes[j].transform.position, nodes[i].transform.position);
                //RaycastHit hit;
                if (Physics.Raycast(nodes[i].transform.position, direction, distance, wallMaskInsp))//(nodes[i].transform.position, direction, out hit, distance))
                {
                    dijkstraAPIClass.connections[i][j] = -1;
                    Debug.DrawRay(nodes[i].transform.position, direction, Color.red);
                }
                else
                {
                    dijkstraAPIClass.connections[i][j] = distance;
                    Debug.DrawRay(nodes[i].transform.position, direction, Color.blue);
                }
            }
        }
    }

    private void BuildGraph2D()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");
        connections = new float[nodes.Length][];

        for (int i = 0; i < connections.Length; i++)
        {
            connections[i] = new float[nodes.Length];
            for (int j = 0; j < connections[i].Length; j++)
            {
                Vector3 direction;
                direction = (nodes[j].transform.position - nodes[i].transform.position);
                float distance;
                distance = Vector3.Distance(nodes[j].transform.position, nodes[i].transform.position);
                //RaycastHit hit;
                if (Physics.Raycast(nodes[i].transform.position, direction, distance, wallMaskInsp))//(nodes[i].transform.position, direction, out hit, distance))
                {
                    connections[i][j] = -1;
                    Debug.DrawRay(nodes[i].transform.position, direction, Color.red);
                }
                else
                {
                    connections[i][j] = distance;
                    Debug.DrawRay(nodes[i].transform.position, direction, Color.blue);
                }
            }
        }
    }

    private void DrawGraphLines()
    {
        for (int i = 0; i < connections.Length; i++)
        {
            for (int j = 0; j < connections[i].Length; j++)
            {
                if (connections[i][j] != -1)
                {
                    Debug.DrawLine(nodes[i].transform.position, nodes[j].transform.position, Color.blue);
                }

                if (connections[i][j] == -1)
                {
                    Debug.DrawLine(nodes[i].transform.position, nodes[j].transform.position, Color.red);
                }
            }
        }
    }
    List<Vector3> path;
    private List<Vector3> DijkstraShortestPath()//(Vector3 startPosition, Vector3 endPosition)
    {
        int startNode = 0;
        int endNode = 2;

        path = new List<Vector3>();
        List<int> unexploredNodes = new List<int>();        // creating a set of verticies for the unexploredNodes

        float[] dist = new float[nodes.Length];             //reserve 6 spaces of nodes for cost
        int[] prev = new int[nodes.Length];                 //reserve previous nodes in mem
        int current = new int();

        for (int i = 0; i < nodes.Length; i++)              //setting unknown vertex's to inf & null prev
        {
            dist[i] = float.MaxValue;
            prev[i] = -1;
            unexploredNodes.Add(i);
        }

        dist[startNode] = 0;

        while(unexploredNodes.Count > 0)
        {  
            //first find the minimum distance (indexception)
            int minNode = unexploredNodes[0];                       //set min node to the first unexplored node
            for (int i = 1; i < unexploredNodes.Count; i++)         //loop through all of the unexplored nodes
            {
                if (dist[unexploredNodes[i]] < dist[minNode])       //now check the distance of the unexplorednodes & IF IT IS LESS THAN THE DISTANCE OF OUR MIN NODE then
                {
                    minNode = unexploredNodes[i];                   //finally set min node to the unexplored node thats distance is LESS than the the unexplored node we assigned to first
                }
            }
            current = minNode;
            // end of find minimum distance

            // otherwise remove the current node from the unexplored set since we are exploring it now.
            unexploredNodes.Remove(current);

            /*
            // if the current node is our goal, stop?
            if (current == endNode)
            {
                print("Dijkstra: " + dist[endNode]);
                return path;
            }
            */

            //look at each neighbour to the node?
            for (int j = 0; j < connections[current].Length; j++)
            {
                if (connections[current][j] == -1)                              //filter out all the non connections
                {
                    continue;
                }

                float newDist = dist[current] + connections[current][j];        //get the new distances / neighbours 

                if (newDist < dist[j])                                          //if the neighbours distances is less than our distance 
                {
                    dist[j] = newDist;                                          // set the new distance
                    prev[j] = current;                                          // we set the distances to our current
                }
            }
        }

        //Initiate back track & add nodes back to the list 
        int tempTarget = endNode;

        if (prev[endNode] != -1 || endNode == startNode)                        //check if the node came from somewhere previous , if there was no previous then how did it get to the end node
        {
            while (tempTarget != -1)                                            // u = in every iteration will change so it will start from the first previous to the end previous (basically keeps pointing at different prev nodes)
            {
                path.Insert(0, nodes[tempTarget].transform.position);                           //index of the nodes is temptarget the 2 oranges theory & we insert the nodes from the end of the path to the start
                tempTarget = prev[tempTarget];                                                  //setting the temp target = prev target
            }
        }

        // DEBUG THE PATH
        for (int i = 0; i < path.Count; i++)
        {
            //Debug.Log(path[i]);
            if (i < path.Count - 1)
            {
                Debug.DrawLine(path[i] + transform.up * 5, path[i + 1] + transform.up * 5, Color.yellow);
            }
        }
        //

        return path;
    }
}