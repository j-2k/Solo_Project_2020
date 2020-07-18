using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidFormation : MonoBehaviour
{
    [SerializeField] int boidsX = 3;
    [SerializeField] int boidsY = 3;
    public GameObject boid;
    public GameObject leadBoid;
    GameObject copyNode;
    GameObject leadNode;
    public Transform[] paths;
    Rigidbody rb;
    //public bool useFormation;
    public float reachingDistance;
    public int currentPoint = 0;
    Vector3 desiredVel; //direction
    Vector3 steering;
    Vector3 target;
    public float max_velocity;
    public float max_force;
    public float max_speed;
    public float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        bool singleRun = true;
        for (int x = 0; x < boidsX; x+=5)
        {
            for (int y = 0; y < boidsY; y+=5)
            {
                Vector3 newPosition = new Vector3((transform.position.x + x), (transform.position.y + y), transform.position.z);

                copyNode = Instantiate(boid);
                copyNode.transform.position = newPosition;
                //if (useFormation == true)
                //{
                    copyNode.transform.parent = this.transform;
                //}
                if (x == 5)
                {
                    if (singleRun == true)
                    {
                        Vector3 newLeadPos = new Vector3((transform.position.x + x), transform.position.y + (y + 15), transform.position.z);
                        leadNode = Instantiate(leadBoid);
                        leadNode.transform.position = newLeadPos;
                        singleRun = false;
                    }
                }
            }
        }
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (useFormation == true)
        //{
            desiredVel = (paths[currentPoint].position - transform.position).normalized * max_velocity;
            steering = desiredVel - rb.velocity;
            if (steering.magnitude > max_force)
            {
                steering = steering.normalized * max_force;
            }
            if (rb.velocity.magnitude > max_speed)
            {
                rb.velocity = rb.velocity.normalized * max_speed;
            }
            Debug.DrawRay(transform.position + new Vector3(0, 10, 0), rb.velocity, Color.cyan);
            // Path Following
            target = paths[currentPoint].position - transform.position;
            Debug.DrawRay(transform.position + new Vector3(0, 5, 0), target, Color.red);
            if (target.magnitude <= reachingDistance)
            {
                currentPoint++;
            }
            if (currentPoint >= paths.Length)
            {
                currentPoint = 0;
            }
            //
            rb.velocity += steering;
            //rb.rotation = Quaternion.LookRotation(rb.velocity);
        //}
    }
}
