using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingStarter : MonoBehaviour
{
    [SerializeField] GameObject pfStarter;
    [SerializeField] GameObject flatNodes;
    GameObject emptyNodesHolder;

    public void SpawnPathfindingResources()
    {
        if (GameObject.FindGameObjectWithTag("PFManager") == null)
        {
            Instantiate(pfStarter, Camera.main.transform.position + new Vector3(0, -2, 5), Quaternion.identity);
        }
        else
        {
            Debug.Log("You already have a pathfinding manager in your scene!");
        }
    }

    public void SpawnPathfindingNodes()
    {
        if (GameObject.Find("NodesHolder") == null)
        {
            emptyNodesHolder = new GameObject();
            emptyNodesHolder.name = "NodesHolder";
        }
        else
        {
            Debug.Log("You already have a Nodes Holder in your scene! Great!");
        }

        Instantiate(flatNodes, Camera.main.transform.position + new Vector3(0, -2, 5), Quaternion.identity);

        GameObject[] nodes;
        nodes = GameObject.FindGameObjectsWithTag("Node");

        foreach (GameObject node in nodes)
        {
            node.transform.SetParent(emptyNodesHolder.transform);
        }
    }

    public void DeletePathfindingNodes()
    {
        if (emptyNodesHolder == null)
        { }else { Destroy(emptyNodesHolder); }
    }
}
