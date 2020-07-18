using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoidScript : MonoBehaviour
{
    public GameObject[] paths;
    Rigidbody rb;
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
        rb = GetComponent<Rigidbody>();
        paths = GameObject.FindGameObjectsWithTag("Node");
    }

    // Update is called once per frame
    void Update()
    {
        desiredVel = (paths[currentPoint].gameObject.transform.position - transform.position).normalized * max_velocity;
        steering = desiredVel - rb.velocity;
        if (steering.magnitude > max_force)
        {
            steering = steering.normalized * max_force;
        }
        if (rb.velocity.magnitude > max_speed)
        {
            rb.velocity = rb.velocity.normalized * max_speed;
        }
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), rb.velocity, Color.cyan); //+new Vector3(0, 10, 0)
        // Path Following
        target = paths[currentPoint].gameObject.transform.position - transform.position;
        Debug.DrawRay(transform.position + new Vector3(0, 0, 0), target, Color.red); //+ new Vector3(0, 5, 0)
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
        //

        //rb.velocity = Vector3.up * 1f;

    }
}
