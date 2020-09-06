using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDeform : MonoBehaviour
{
    public enum PlaneStates
    {
        STATIC,
        DYNAMIC,
    };
    public PlaneStates currentState;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    public Material waterTexture;
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<MeshFilter>() == null)
        {
            gameObject.AddComponent<MeshFilter>();
            meshFilter = gameObject.GetComponent<MeshFilter>();
        }
        if (gameObject.GetComponent<MeshRenderer>() == null)
        {
            gameObject.AddComponent<MeshRenderer>();
            meshRenderer = gameObject.GetComponent<MeshRenderer>();
        }

        if (currentState == PlaneStates.STATIC)
        {
            Vector3[] Verticies = new Vector3[]
            {
            //new Vector3(0,0,0),
            //new Vector3(1,0,0),
            //new Vector3(0,1,0),
            //new Vector3(1,1,0)
            //0 Y   X Y
            //0 0   X 0
            //
            //0 0   0 0   1 0   0 1
            //1 0   0 1   0 0   0 0
            //
            point1.transform.position,
            point2.transform.position,
            point3.transform.position,
            point4.transform.position
            };

            int[] Triangles = new int[]
            {
            //desktop/tris.png
            //0,3,1,
            //3,0,2
            0,2,1
            ,
            3,1,2
            };
            Vector2[] UVs = new Vector2[]
            {
                point1.transform.position,
                point2.transform.position,
                point3.transform.position,
                point4.transform.position
                /*new Vector2(0,0),
                new Vector2(1,0),
                new Vector2(0,1),
                new Vector2(1,1),*/
            };
            Vector3[] Normals = new Vector3[]
            {
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up,
            };
            Mesh waterMesh = new Mesh();
            waterMesh.vertices = Verticies;
            waterMesh.normals = Normals;
            waterMesh.uv = UVs;
            waterMesh.triangles = Triangles;
            waterMesh.Optimize();

            meshFilter.mesh = waterMesh;
            meshRenderer.material = waterTexture;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (currentState == PlaneStates.DYNAMIC)
        {
            Vector3[] Verticies = new Vector3[]
            {
                //new Vector3(0,0,0),
                //new Vector3(1,0,0),
                //new Vector3(0,1,0),
                //new Vector3(1,1,0)
                //0 Y   X Y
                //0 0   X 0
                //
                //0 0   0 0   1 0   0 1
                //1 0   0 1   0 0   0 0
                //
                point1.transform.position,
                point2.transform.position,
                point3.transform.position,
                point4.transform.position
            };

            int[] Triangles = new int[]
            {
                //desktop/tris.png
                //0,3,1,
                //3,0,2
                0,2,1
                ,
                3,1,2
            };
            Vector2[] UVs = new Vector2[]
            {
                point1.transform.position,
                point2.transform.position,
                point3.transform.position,
                point4.transform.position
                /*new Vector2(0,0),
                new Vector2(1,0),
                new Vector2(0,1),
                new Vector2(1,1),*/
            };
            Vector3[] Normals = new Vector3[]
            {
                Vector3.up,
                Vector3.up,
                Vector3.up,
                Vector3.up,
            };
            Mesh waterMesh = new Mesh();
            waterMesh.vertices = Verticies;
            waterMesh.normals = Normals;
            waterMesh.uv = UVs;
            waterMesh.triangles = Triangles;
            waterMesh.Optimize();

            meshFilter.mesh = waterMesh;
            meshRenderer.material = waterTexture;
        }
    }
}
