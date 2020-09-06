using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class UOGraphBuilder : MonoBehaviour
{
    GameObject[] nodes;
    float[][] connections;
    [SerializeField] LayerMask wallMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node"); 
        connections = new float[nodes.Length][];                            //connections i will get all the nodes which is 6 leave j empty

        for (int i = 0; i < connections.Length; i++)
        {
            connections[i] = new float[nodes.Length];                       //connections i = length of nodes set before now we are setting j 
                                                                            //j now is equal to the nodes.length which is again 6 so all we do is set first connections array to i then we set j's
                                                                            //=============================================================================
            for (int j = 0; j < connections[i].Length; j++)                 //j will scroll through all the "columes array / elements" in the rows array
            {
                Vector3 direction;
                direction = (nodes[j].transform.position - nodes[i].transform.position);
                float distance;
                distance = Vector3.Distance(nodes[j].transform.position, nodes[i].transform.position); 
                //RaycastHit hit;
                if (Physics.Raycast(nodes[i].transform.position, direction, distance, wallMask))        //(nodes[i].transform.position, direction, out hit, distance))
                {
                    connections[i][j] = -1;                                                             //i & j are a,a b,b c,c and they r equal to -1
                    Debug.DrawRay(nodes[i].transform.position, direction, Color.red);
                }
                else
                {
                    connections[i][j] = distance;                                                       //i & j are having connections so set it to the distance
                    Debug.DrawRay(nodes[i].transform.position, direction, Color.blue);
                }
            }
        }
    }
}
