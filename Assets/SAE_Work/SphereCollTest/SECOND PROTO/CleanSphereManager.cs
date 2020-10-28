using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CleanSphereManager : MonoBehaviour
{
    //Blue Sphere Properties
    [SerializeField] GameObject sphere1Blue;
    [SerializeField] Vector3 sphere1BlueInitialVelo;

    //Red Sphere Properties
    [SerializeField] GameObject sphere2Red;
    [SerializeField] Vector3 sphere2RedInitialVelo;

    //Mutual Properties
    [SerializeField] float speed;
    [SerializeField] float radiusCalculation;
    //Vector3 sphereVelocity;
    Vector3 fakePosRed;
    Vector3 fakePosBlue;
    Vector3 prevPosition;
    Vector3 newPosition;
    [SerializeField] Vector3 sphereVelocity;
    public SphereMovement sphereRedVeloAccess;
    public SphereMovement sphereBlueVeloAccess;
    bool collided;
    // N / Q / P / A1` / B1`
    //[SerializeField] float mass = 1; // dont need this

    // Start is called before the first frame update
    void Start()
    {
        prevPosition = sphere1Blue.transform.position;
        fakePosRed = (sphere2Red.transform.position + transform.right * -8);
        fakePosBlue = (sphere1Blue.transform.position + transform.right * -8);
        speed = 2;
        radiusCalculation = (sphere1Blue.transform.localScale.x + sphere2Red.transform.localScale.x) / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SphereCollisionHandeler(sphere1Blue, sphere2Red) && !collided)
        {
            collided = true;
            SphereMovementVectors();
            Debug.Log("colided");
            collided = false;
        }
    }

    void VelocityHandler()
    {
        newPosition = sphere1Blue.transform.position;

        sphereVelocity = (newPosition - prevPosition) / Time.deltaTime;

        prevPosition = newPosition;

        Debug.Log(sphereVelocity);
    }
    void SphereMovementVectors()
    {
        //THESE VECTORS LENGTH ISNT  2 (SPEED)!
        sphere1BlueInitialVelo = sphereBlueVeloAccess.sphereVelocity;//sphere1Blue.transform.forward * speed * Time.deltaTime;
        Debug.DrawRay(sphere1Blue.transform.position, sphere1Blue.transform.forward * speed, Color.blue);
        sphere2RedInitialVelo = sphereRedVeloAccess.sphereVelocity;//sphere2Red.transform.forward * speed * Time.deltaTime;
        Debug.DrawRay(sphere2Red.transform.position, sphere2Red.transform.forward * speed, Color.red);
        //Dir = target.Pos - current.Pos
        Vector3 dir2BlueS = (sphere1Blue.transform.position - sphere2Red.transform.position).normalized;   //normalize?
        Debug.DrawRay(sphere2Red.transform.position, dir2BlueS.normalized * 1, Color.red);
        Vector3 dir2RedS = (sphere2Red.transform.position - sphere1Blue.transform.position).normalized;    //normalize?
        Debug.DrawRay(sphere1Blue.transform.position, dir2RedS.normalized * 1, Color.blue);
        //======================================================================================================
        Vector3 blueSphereHypo = sphere1BlueInitialVelo;
        Vector3 collisionDir = dir2RedS.normalized;
        float blueA1 = Vector3.Dot(blueSphereHypo, collisionDir);               // length of the component of each of the movement vectors along the dir
        Vector3 redSphereHypo = sphere2RedInitialVelo;
        //Vector3 redSphereDir = dir2BlueS.normalized;
        float redA2 = Vector3.Dot(redSphereHypo, collisionDir);//redSphereDir); 
        float P = (2 * (blueA1 - redA2)) / (1 + 1);
        Vector3 blueNewV1 = blueSphereHypo - P * 1 * collisionDir;//redSphereDir;
        Vector3 redNewV1 = redSphereHypo + P * 1 * collisionDir;
        //======================================================================================================
        sphereRedVeloAccess.sphereVelocity = redNewV1;
        sphereBlueVeloAccess.sphereVelocity = blueNewV1;
    }
    
    bool SphereCollisionHandeler(GameObject blueSphere, GameObject redSphere)
    {
        blueSphere = sphere1Blue;
        redSphere = sphere2Red;

        double distance = Math.Sqrt((blueSphere.transform.position.x - redSphere.transform.position.x) * (blueSphere.transform.position.x - redSphere.transform.position.x) +
                            (blueSphere.transform.position.y - redSphere.transform.position.y) * (blueSphere.transform.position.y - redSphere.transform.position.y) +
                            (blueSphere.transform.position.z - redSphere.transform.position.z) * (blueSphere.transform.position.z - redSphere.transform.position.z));

        if (distance <= radiusCalculation)
        {
            Debug.Log("Collision Detected");
            return true;
        }
        else
        {
            Debug.Log("No Collision Detected");
            return false;
        }
    }

    private void OnDrawGizmos()
    {
            Gizmos.color = (Color.red);
            Gizmos.DrawWireSphere(fakePosBlue, sphere1Blue.transform.localScale.z / 2);
            Gizmos.color = (Color.blue);
            Gizmos.DrawWireSphere(fakePosRed, sphere2Red.transform.localScale.z / 2);
    }
}
