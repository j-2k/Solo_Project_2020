using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFE_Helper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
