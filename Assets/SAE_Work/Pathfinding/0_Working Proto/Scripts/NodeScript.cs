using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{

    [SerializeField] private float distance = int.MaxValue;
    [SerializeField] private Transform parentNode = null;
    [SerializeField] private List<Transform> neighbourNode;
    [SerializeField] private bool walkable = true;

    // Use this for initialization
    void Start()
    {
        this.resetNode();
    }

    // Reset & Set all nodes to infinity
    public void resetNode()
    {
        distance = int.MaxValue;
        parentNode = null;
    }

    //===================PARENT=NODE================
    // Set the parent node
    public void setParentNode(Transform node)
    {
        this.parentNode = node;
    }
    // Get the parent node
    public Transform getParentNode()
    {
        Transform result = this.parentNode;
        return result;
    }
    //==============================================

    //====================DISTANCE==================
    // Set distance
    public void setDistance(float value)
    {
        this.distance = value;
    }
    // Get distance
    public float getDistance()
    {
        float result = this.distance;
        return result;
    }
    //==============================================


    //===================NEIGHBOUR=NODES===========
    // Set neightbour node
    public void setNeighbourNode(Transform node)
    {
        this.neighbourNode.Add(node);
    }
    // Get neighbour node
    public List<Transform> getNeighbourNode()
    {
        List<Transform> result = this.neighbourNode;
        return result;
    }
    //==============================================

    //================WALKABLE=====================
    // Set node walkable
    public void setWalkable(bool value)
    {
        this.walkable = value;
    }
    // Get node walkable
    public bool isWalkable()
    {
        bool result = walkable;
        return result;
    }
    //==============================================
}