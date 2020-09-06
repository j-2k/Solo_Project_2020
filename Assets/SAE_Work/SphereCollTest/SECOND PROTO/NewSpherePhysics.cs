using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpherePhysics : MonoBehaviour
{
    //SPHERE 1 BLUE PROPERTIES ARE = a1 / b1 = A1/B1 ARE BLUE
    [SerializeField] GameObject sphere1B;
    [SerializeField] SphereCollider sphere1Coll;
    [SerializeField] float massOfSphere1B;
    [SerializeField] Vector3 a1; // float not vec
    [SerializeField] Vector3 b1; // float not vec
    [SerializeField] float a1f; // float not vec
    [SerializeField] float b1f; // float not vec
    [SerializeField] Vector3 initialVelocityOfBlueSphere;
    //SPHERE 2 RED PROPERTIES ARE = a2 / b2 = A2/B2 ARE RED
    [SerializeField] GameObject sphere2R;
    [SerializeField] SphereCollider sphere2Coll;
    [SerializeField] float massOfSphere2R;
    [SerializeField] Vector3 a2; // float not vec
    [SerializeField] Vector3 b2; // float not vec
    [SerializeField] float a2f; // float not vec
    [SerializeField] float b2f; // float not vec
    [SerializeField] Vector3 initialVelocityOfRedSphere;
    //==============================================
    [SerializeField] GameObject testSphere3G;
    [SerializeField] Vector3 prevPosition;
    [SerializeField] Vector3 newPosition;
    [SerializeField] Vector3 sphereVelocity;
    [SerializeField] float speed;
    [SerializeField] float radius;
    [SerializeField] bool collisionBR;
    Vector3 fakePosRed;
    Vector3 fakePosBlue;

    // Start is called before the first frame update
    void Start()
    {
        sphere1Coll = sphere1B.GetComponent<SphereCollider>();
        sphere2Coll = sphere2R.GetComponent<SphereCollider>();
        massOfSphere1B = 1;
        massOfSphere2R = 1;
        fakePosRed = (sphere2R.transform.position + transform.right * -8);
        fakePosBlue = (sphere1B.transform.position + transform.right * -8);
        radius = (sphere1B.transform.localScale.x + sphere2R.transform.localScale.x) / 2;
        prevPosition = testSphere3G.transform.position;
    }

    void FixedUpdate()
    {
        DebugInfo();
        GreenSphereVelocityTest();
        RedBlueSphereCollisionHandler(sphere1B,sphere2R);
        MovementVectors(sphere1B, sphere2R);
        if (collisionBR == true)
        {
            CollisionBR();
        }
        CollisionBR();
    }


    void CollisionBR()
    {
        //====================================================================================================
        //line going to blue sphere first contact point
        a1 = sphere1B.transform.position - sphere2Coll.ClosestPointOnBounds(sphere1B.transform.position);
        a1.Normalize();
        Debug.DrawRay(sphere2R.transform.position, a1, Color.white);
        //line going to red sphere first contact point
        a2 = sphere2R.transform.position - sphere1Coll.ClosestPointOnBounds(sphere2R.transform.position);
        a2.Normalize();
        Debug.DrawRay(sphere1B.transform.position, a2, Color.white);
        //====================================================================================================
        //line going to blue sphere
        Vector3 n1 = sphere1B.transform.position - sphere2R.transform.position;
        n1.Normalize();
        Debug.DrawRay(sphere2R.transform.position + transform.up * 4, n1, Color.white);
        //line going to red sphere
        Vector3 n2 = sphere2R.transform.position - sphere1B.transform.position;
        n2.Normalize();
        Debug.DrawRay(sphere1B.transform.position + transform.up * 3, n2, Color.white);
        //====================================================================================================
        float a11 = Vector3.Dot(initialVelocityOfBlueSphere, n2);
        float a22 = Vector3.Dot(initialVelocityOfRedSphere, n1);
        //====================================================================================================

        double P = (2.0 * (a11 - a22)) / (massOfSphere1B + massOfSphere2R);

        //Vector3 v1AFTER = initialVelocityOfBlueSphere.x - P * massOfSphere2R * n1,initialVelocityOfBlueSphere.y - P * massOfSphere2R * n1 , initialVelocityOfBlueSphere.z - P * massOfSphere2R * n1);
        //current vel is going to be total then divide into 2
        //quantitiy a amount effected by coll
        //quant b prependicular to a will take all the collision
    }
    //1:00:40
    void BounceCalculation()
    {
        // FIRST P CALCULATION
        //line going to blue sphere first contact point
        Vector3 pointingToBlueSphere = sphere1B.transform.position - sphere2Coll.ClosestPointOnBounds(sphere1B.transform.position);
        pointingToBlueSphere.Normalize();


    }

    void MovementVectors(GameObject blueSphere, GameObject redSphere)
    {
        blueSphere = sphere1B;
        redSphere = sphere2R;
        //blueSphere.transform.forward * speed IS OUR HYPO
        //pointingToRedSphere IS DIR/D/ NORMALIZED DIRECTION
        initialVelocityOfBlueSphere = blueSphere.transform.forward * speed * Time.deltaTime;
        initialVelocityOfRedSphere = redSphere.transform.forward * speed * Time.deltaTime;
        if (collisionBR == false)
        {
            //Default vectors/movement
            blueSphere.transform.LookAt(fakePosRed);// + transform.right * 2);
            blueSphere.transform.position += initialVelocityOfBlueSphere;
            redSphere.transform.LookAt(fakePosBlue);// + transform.right * 2);
            redSphere.transform.position += initialVelocityOfRedSphere;
        }
        if (collisionBR == true)
        {
            //line going to red sphere
            Vector3 pointingToRedSphere = sphere2R.transform.position - sphere1B.transform.position;
            pointingToRedSphere.Normalize();
            float H_dot_D = Vector3.Dot(initialVelocityOfBlueSphere, pointingToRedSphere);
            float blueSphereHypotenuse = blueSphere.transform.forward.z * speed;
            float cosHypoMagLength = Mathf.Cos(Vector3.Angle(initialVelocityOfBlueSphere, pointingToRedSphere)) * blueSphereHypotenuse;
            Debug.DrawRay(blueSphere.transform.position + transform.up * 1.5f, pointingToRedSphere * cosHypoMagLength, Color.cyan);
            float sinHypoMagLength = Mathf.Sin(Vector3.Angle(initialVelocityOfBlueSphere, pointingToRedSphere)) * blueSphereHypotenuse;
            Vector3 pointLeft = Quaternion.Euler(0,-90,0) * pointingToRedSphere; 
            Debug.DrawRay(blueSphere.transform.position + transform.up * 1.5f, pointLeft * sinHypoMagLength, Color.green);
            Debug.DrawRay(blueSphere.transform.position + transform.up * 1.5f,  blueSphere.transform.forward * blueSphereHypotenuse, Color.blue);
            print(H_dot_D + " < dot | cos * hypo > " + cosHypoMagLength + " sin * hypo > " + sinHypoMagLength);
            print("angle is" + Vector3.Angle(initialVelocityOfBlueSphere, pointingToRedSphere));
        }
        Debug.DrawRay(redSphere.transform.position, redSphere.transform.forward * speed, Color.red);
        Debug.DrawRay(blueSphere.transform.position, blueSphere.transform.forward * speed, Color.blue);
    }

    double RedBlueSphereCollisionHandler(GameObject blueSphere, GameObject redSphere)
    {
        blueSphere = sphere1B;
        redSphere = sphere2R;

        double distance = Math.Sqrt((blueSphere.transform.position.x - redSphere.transform.position.x) * (blueSphere.transform.position.x - redSphere.transform.position.x) +
                            (blueSphere.transform.position.y - redSphere.transform.position.y) * (blueSphere.transform.position.y - redSphere.transform.position.y) +
                            (blueSphere.transform.position.z - redSphere.transform.position.z) * (blueSphere.transform.position.z - redSphere.transform.position.z));

        if (distance <= radius)
        {
            collisionBR = true;
            Debug.Log("Collision Detected");

            return distance;
        }
        else
        {
            collisionBR = false;
            Debug.Log("No Collision Detected");
            return distance;
        }
    }

    void GreenSphereVelocityTest()
    {
        newPosition = testSphere3G.transform.position;

        sphereVelocity = (newPosition - prevPosition) / Time.deltaTime;

        prevPosition = newPosition;

        testSphere3G.transform.position += transform.forward * speed * Time.deltaTime;

        Debug.DrawRay(testSphere3G.transform.position, sphereVelocity, Color.cyan);
        Debug.Log("Sphere's Linear Velocity " + sphereVelocity);
    }

    void DebugInfo()
    {
        //Debug.Log(sphere1Coll.ClosestPointOnBounds(sphere2R.transform.position));
        //Vector3 B2R_N = sphere1Coll.ClosestPointOnBounds(sphere2R.transform.position);
        //B2R_N.Normalize();
        //Vector3 R2B_N = sphere2Coll.ClosestPointOnBounds(sphere1B.transform.position);
        //R2B_N.Normalize();
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Vector3 vectorN_B2R = (sphere2Coll.ClosestPointOnBounds(sphere1B.transform.position) - sphere1B.transform.position);                                                            //
        Debug.DrawRay(sphere1B.transform.position, vectorN_B2R.normalized, Color.green);                                                                                                //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Vector3 vectorN_R2B = (sphere1B.transform.position - sphere2Coll.ClosestPointOnBounds(sphere1B.transform.position));                                                            //
        Debug.DrawRay(sphere2R.transform.position, vectorN_R2B.normalized, Color.green);                                                                                                //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Debug.DrawLine(sphere1B.transform.position + transform.up * 1, sphere2Coll.ClosestPointOnBounds(sphere1B.transform.position) + transform.up * 1, Color.blue);                   //
        Debug.DrawLine(sphere1B.transform.position, new Vector3(sphere1B.transform.position.x, sphere1B.transform.position.y + 1, sphere1B.transform.position.z), Color.blue);          //
        Debug.DrawLine(sphere2Coll.ClosestPointOnBounds(sphere1B.transform.position) + transform.up * 1, sphere2Coll.ClosestPointOnBounds(sphere1B.transform.position), Color.blue);    //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Debug.DrawLine(sphere2R.transform.position - transform.up * 1, sphere1Coll.ClosestPointOnBounds(sphere2R.transform.position) - transform.up * 1, Color.red);                    //
        Debug.DrawLine(sphere2R.transform.position, new Vector3(sphere2R.transform.position.x,sphere2R.transform.position.y -  1, sphere2R.transform.position.z), Color.red);           //
        Debug.DrawLine(sphere1Coll.ClosestPointOnBounds(sphere2R.transform.position) - transform.up * 1, sphere1Coll.ClosestPointOnBounds(sphere2R.transform.position), Color.red);     //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    private void OnDrawGizmos()
    {
        if (collisionBR == false)
        {
            Gizmos.color = (Color.red);
            //Gizmos.DrawWireSphere(sphere1B.transform.position + transform.right * 2, sphere1B.transform.localScale.z / 2);
            Gizmos.DrawWireSphere(fakePosBlue, sphere1B.transform.localScale.z / 2);
            Gizmos.color = (Color.blue);
            //Gizmos.DrawWireSphere(sphere2R.transform.position + transform.right * 2, sphere2R.transform.localScale.z / 2);
            Gizmos.DrawWireSphere(fakePosRed, sphere1B.transform.localScale.z / 2);
        }
        else
        {

        }
    }
}
