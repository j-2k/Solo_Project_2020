using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

//==========================================================================//
//DISTANCE FORMULA 3D SPACE                                                 //
//f(A, B) = (Ax - Bx)^2 + (Ay - By)^2 + (Az - Bz)^2 <= Aradius + Bradius    //
//https://www.youtube.com/watch?v=0IOEPcAHgi4 2D                            //
//https://www.youtube.com/watch?v=HASYgn1Q6Fc 3D                            //
//https://www.mathsisfun.com/pythagoras.html                                //
//https://www.mathsisfun.com/algebra/distance-2-points.html                 //
//==========================================================================//

public class SphereColliderCode : MonoBehaviour
{
    [SerializeField] GameObject sphere1;
    [SerializeField] GameObject sphere2;
    [SerializeField] GameObject sphere3;
    [SerializeField] GameObject point1;
    [SerializeField] GameObject point2;
    [SerializeField] GameObject ground;
    float radius;
    [SerializeField] float speed = 2;
    [SerializeField] float gravityMultiplier;
    Vector3 velocity = Vector3.zero;
    Vector3 gravity;
    [SerializeField] bool isGrounded = false;
    [SerializeField] bool bounce = false;

    void Start()
    {
        gravity = new Vector3(0, -9.8f * Time.deltaTime, 0);
        gravityMultiplier = 1;
        radius = (sphere1.transform.localScale.x + sphere2.transform.localScale.x) / 2;
        point1.transform.position = new Vector3(sphere1.transform.position.x, sphere1.transform.position.y - sphere1.transform.localScale.y / 2, sphere1.transform.position.z);
        point2.transform.position = new Vector3(sphere2.transform.position.x, sphere2.transform.position.y - sphere2.transform.localScale.y / 2, sphere2.transform.position.z);
    }
    
    void Update()
    {
        /*
        if (Vector3.Distance(sphere1.transform.position, sphere2.transform.position) <= 2)
        {
            Debug.Log("coll");
        }
        else
        {
            Debug.Log("no coll");
        }
        */
        //point1.transform.position = new Vector3(sphere1.transform.position.x, sphere1.transform.position.y - sphere1.transform.localScale.y / 2, sphere1.transform.position.z);
        //point2.transform.position = new Vector3(sphere2.transform.position.x, sphere2.transform.position.y - sphere2.transform.localScale.y / 2, sphere2.transform.position.z);
        SphereCollisionMethod(sphere2, sphere3);
        GroundCollisionHandler();
        DebugLines(sphere2,sphere3);
        //SphereVelo(sphere2,sphere3);
    }

    double SphereCollisionMethod(GameObject sphere, GameObject otherSphere)
    {
        sphere = sphere2;
        otherSphere = sphere3;
        int sphereMass = 1;
        int otherSphereMass = 1;

        Vector3 otherSphereDirMIRROR = (otherSphere.transform.position - sphere.transform.position).normalized;
        Vector3 sphereDirMIRROR = (sphere.transform.position - otherSphere.transform.position).normalized;
        Vector3 otherSphereDirSTRAIGHT = transform.forward;
        Vector3 sphereDirSTRAIGHT = transform.forward;
        //sphere.transform.position += otherSphereDir * speed * Time.deltaTime;
        //otherSphere.transform.position += sphereDir * speed * Time.deltaTime;

        double distance = Math.Sqrt((sphere.transform.position.x - otherSphere.transform.position.x) * (sphere.transform.position.x - otherSphere.transform.position.x) +
                                    (sphere.transform.position.y - otherSphere.transform.position.y) * (sphere.transform.position.y - otherSphere.transform.position.y) +
                                    (sphere.transform.position.z - otherSphere.transform.position.z) * (sphere.transform.position.z - otherSphere.transform.position.z));

        
        //int cDistance = (int)distance;
        //Debug.Log(cDistance);
        

        if (distance <= radius) //<= raidus
        {
            Debug.Log("Collision Detected");
            return distance;
        }
        else
        {
            Debug.Log("No Collision");
            sphere.transform.Translate(otherSphereDirSTRAIGHT * speed * Time.deltaTime);
            //Quaternion rot = Quaternion.LookRotation(otherSphereDirMIRROR, Vector3.up);
            //sphere.transform.rotation = rot;
            otherSphere.transform.Translate(-sphereDirSTRAIGHT * speed * Time.deltaTime);
            //Quaternion rot2 = Quaternion.LookRotation(sphereDirMIRROR, Vector3.up);
            //otherSphere.transform.rotation = rot2;
            return distance;
        }
    }

    void SphereVelo(GameObject sphere, GameObject otherSphere)
    {
        sphere = sphere2;
        otherSphere = sphere3;

        Vector3 VelocitySphere = Vector3.zero;
        VelocitySphere.Normalize();
        Vector3 NewVelo = VelocitySphere * 100;

        Debug.Log(NewVelo + "this is pos");
        Debug.Log(NewVelo.magnitude + "this is mag");

        Debug.DrawLine(VelocitySphere, new Vector3 (VelocitySphere.x, VelocitySphere.y, (VelocitySphere.z * 10)),Color.red);
        sphere.transform.position += VelocitySphere;

    }

    void DebugLines(GameObject sphere, GameObject otherSphere)
    {
        sphere = sphere2;
        otherSphere = sphere3;

        Vector3 otherSphereDir = (otherSphere.transform.position - sphere.transform.position).normalized;
        Vector3 sphereDir = (sphere.transform.position - otherSphere.transform.position).normalized;
        Debug.DrawRay(sphere.transform.position, otherSphereDir * 4f, Color.red);
        Debug.DrawRay(otherSphere.transform.position, sphereDir * 4f, Color.green);
        Debug.DrawRay(sphere.transform.position, transform.forward * 4f, Color.red);
        Debug.DrawRay(otherSphere.transform.position, -transform.forward * 4f, Color.green);
    }

    void GroundCollisionHandler()
    {

        /*
        if (isGrounded == true)
        {
            if (gravityMultiplier > 1)
            {
                gravityMultiplier = gravityMultiplier / 2;
                if (gravityMultiplier <= 1)
                {
                    gravityMultiplier = 1;
                    return;
                }
                if (gravityMultiplier >= 2)
                {
                    bounce = true;
                    if (bounce == true)
                    {
                        isGrounded = false;
                    }
                }
            }
        }
        
        if (!isGrounded && bounce)
        {
            //gravityMultiplier -= 2 * Time.deltaTime;
            sphere1.transform.position += new Vector3(0, 2 * Time.deltaTime, 0);
        }
        */
        if (sphere1.transform.position.y - sphere1.transform.localScale.y/2 <= ground.transform.position.y + ground.transform.localScale.y/2)
        {
            isGrounded = true;
            print("sphere1 is inside the ground");
        }
        else
        {
            isGrounded = false;
            /*
            if (bounce == false)
            {
                sphere1.transform.position += new Vector3(0, (-9.8f * gravityMultiplier * Time.deltaTime), 0);
            }
            */
        }

        if (isGrounded == false)//&& bounce == false
        {
            gravityMultiplier += 2 * Time.deltaTime;
            sphere1.transform.position += new Vector3(0, (-9.8f * gravityMultiplier * Time.deltaTime), 0);
        }
        else //(isGrounded == true)
        {
            gravityMultiplier = 1;
            sphere1.transform.position = new Vector3(sphere1.transform.position.x, sphere1.transform.position.y, sphere1.transform.position.z);
        }
    }
}
