using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using System.Runtime.InteropServices;

public class PF_Editor : EditorWindow
{
    [MenuItem("Window/Pathfinding Editor Window")]
    public static void ShowEditorWindow()
    {
        EditorWindow.GetWindow<PF_Editor>("Pathfinding Editor");
    }

    void OnGUI()
    {
        GUILayout.Label("This is the Pathfinding Editor for Clients", EditorStyles.boldLabel);

        if (GUILayout.Button("Implement Pathfinding Base Resources"))
        {
            if (GameObject.FindGameObjectWithTag("PFManager") == null)
            {
                GameObject PathfindingImplementer = new GameObject();
                PathfindingImplementer.tag = "PFManager";
                PathfindingImplementer.name = "PathfindingImplementer";
                PathfindingImplementer.AddComponent<GraphMapBuilder>();
                PathfindingImplementer.AddComponent<PFE_Helper>();
                Debug.Log("Incase you delete a component you can click the same button to recreate lost components!");
            }
            if (GameObject.FindGameObjectWithTag("PFManager") != null)
            {
                GameObject PathfindingImplementer = GameObject.FindGameObjectWithTag("PFManager");
                PathfindingImplementer.tag = "PFManager";
                PathfindingImplementer.name = "PathfindingImplementer";
                if (PathfindingImplementer.GetComponent<GraphMapBuilder>() == null)
                {
                    PathfindingImplementer.AddComponent<GraphMapBuilder>();
                }
                if (PathfindingImplementer.GetComponent<PFE_Helper>() == null)
                {
                    PathfindingImplementer.AddComponent<PFE_Helper>();
                }
                Debug.Log("Recreating / Checking on lost components...");
            }
        }

        if (GUILayout.Button("Add A New Node"))
        {
            if (GameObject.Find("NodesHolder") == null)
            {
                GameObject emptyNodesHolder = new GameObject();
                emptyNodesHolder.name = "NodesHolder";
            }
            else
            {
                Debug.Log("You already have a Nodes Holder in your scene! Great!");
            }

            GameObject Node = new GameObject();
            Node.tag = "Node";
            Node.name = "Node";
            Node.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Node.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3 + Camera.main.transform.up * -3;

            GameObject[] nodes;
            nodes = GameObject.FindGameObjectsWithTag("Node");

            foreach (GameObject node in nodes)
            {
                GameObject emptyNodesHolder = GameObject.Find("NodesHolder");
                node.transform.SetParent(emptyNodesHolder.transform);
            }
        }

        if (GUILayout.Button("Test Pathfinding"))
        {
            Debug.Log("Checking if you have all pathfinding tools ready & initialized!");
            if (GameObject.FindGameObjectWithTag("PFManager") != null)
            {
                /*if (PathfindingImplementer.GetComponent<GraphMapBuilder>() == null)
                {
                    PathfindingImplementer.AddComponent<GraphMapBuilder>();
                }
                if (PathfindingImplementer.GetComponent<PFE_Helper>() == null)
                {
                    PathfindingImplementer.AddComponent<PFE_Helper>();
                }*/
            }
            EditorApplication.isPlaying = true;
            Debug.Log("Please pay attention to the yellow line displayed at the top of the nodes will be your shortest path!");
        }

        if (GUILayout.Button("Delete All Pathfinding Nodes"))
        {
            GameObject NodeHolder = GameObject.Find("NodesHolder");
            DestroyImmediate(NodeHolder);
        }

        if (GUILayout.Button("Delete All Pathfinding Related Objects"))
        {
            GameObject NodeHolder = GameObject.Find("NodesHolder");
            GameObject PathfindingImplementer = GameObject.FindGameObjectWithTag("PFManager");
            DestroyImmediate(NodeHolder);
            DestroyImmediate(PathfindingImplementer);
        }
    }
}

