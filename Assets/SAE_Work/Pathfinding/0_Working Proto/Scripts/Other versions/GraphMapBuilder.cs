using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GraphMapBuilder : MonoBehaviour
{// Chris > //https://hastebin.com/elacoxerat.cs // Juma > //https://hastebin.com/uzusakofil.cs
    private GameObject[] nodes;
    private float[][] connections;

    [SerializeField] LayerMask wallMaskInsp;

    private void Start()
    {
        BuildGraph2D();
    }

    private void Update()
    {
        DrawGraphLines();
        //DijkstraShortestPath();
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

    private List<Vector3> DijkstraShortestPath()//(Vector3 startPosition, Vector3 endPosition)
    {
        int startNode = 0;
        int endNode = 4;


        List<Vector3> path = new List<Vector3>();
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
            int minNode = 0;
            for (int i = 0; i < unexploredNodes.Count; i++)
            {
                if (dist[unexploredNodes[i]] < unexploredNodes[minNode])
                {
                    minNode = unexploredNodes[i];
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

        int tempTarget = endNode;

        if (prev[endNode] != -1 || endNode == startNode)                        //check if the node came from somewhere previous , if there was no previous then how did it get to the end node
        {
            while (tempTarget != -1)                                            // u = in every iteration will change so it will start from the first previous to the end previous (basically keeps pointing at different prev nodes)
            {
                path.Insert(0, nodes[tempTarget].transform.position);                           //index of the nodes is temptarget the 2 oranges theory & we insert the nodes from the start of the path to the end
                tempTarget = prev[tempTarget];                                                  //setting the temp target = prev target
                //Debug.DrawLine((transform.position + transform.up * 5), (nodes[tempTarget].transform.position + transform.up * 5));
            }
        }

        return path;
    }

}