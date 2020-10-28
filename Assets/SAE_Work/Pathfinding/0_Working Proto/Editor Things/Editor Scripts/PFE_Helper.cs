using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PFE_Helper : MonoBehaviour
{
    GameObject[] nodes;

    void OnDrawGizmos()
    {
        GameObject[] nodes;
        nodes = GameObject.FindGameObjectsWithTag("Node");
        for (int i = 0; i < nodes.Length; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(nodes[i].transform.position,0.5f);
        }
    }
}
