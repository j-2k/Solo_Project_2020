using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;

public class FlockTesting : MonoBehaviour
{

    static public List<FlockTesting> neighbours = new List<FlockTesting>();

    [SerializeField] float cohesionWeight = 0.2f;
    [SerializeField] float seperationWeight = 0.2f;
    [SerializeField] float alignmentWeight = 0.2f;

    [SerializeField] float cohesionDistance = 5f;
    [SerializeField] float seperationDistance = 5f;
    [SerializeField] float alignmentDistance = 5f;

    [SerializeField] float maxSpeed;
    [SerializeField] float forwardSpeed;
    [SerializeField] bool flocking;

    Vector3 velocity;
    Rigidbody rb;
    GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        neighbours.Add(this);
    }

    void Update()
    {
        if (flocking == true)
        {
            this.rb.velocity += ((Alignment() * alignmentWeight) + (Cohesion() * cohesionWeight) + (Seperation() * seperationWeight)) * maxSpeed;
            transform.LookAt(player.transform);
            rb.position += transform.forward * forwardSpeed;
        } 
        else
        {
            rb.velocity = transform.forward * maxSpeed;
        }

        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 8, transform.position.z), Alignment(),Color.blue);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 4, transform.position.z), Cohesion(),Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 6, transform.position.z), Seperation(),Color.white);
    }

    Vector3 Alignment()
    {
        Vector3 velocity = Vector3.zero;
        int neighbourCount = 0;
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (this.transform.gameObject != neighbours[i].gameObject)
            {
                if (Vector3.Distance(this.transform.position, neighbours[i].transform.position) < alignmentDistance)
                {
                    velocity += neighbours[i].velocity.normalized;
                    //===============
                    neighbourCount++;
                }
            }
        }

        velocity /= neighbourCount;
        Vector3 vAlignment = velocity.normalized; //* alignmentWeight;
        return vAlignment;
    }

    Vector3 Cohesion()
    {
        Vector3 centerVelocity = Vector3.zero;
        int neighbourCount = 0;
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (this.transform.gameObject != neighbours[i].gameObject)
            {
                if (Vector3.Distance(this.transform.position, neighbours[i].transform.position) < cohesionDistance)
                {
                    Debug.Log("cohesion work?");
                    centerVelocity += neighbours[i].transform.position;
                    //===============
                    neighbourCount++;
                }
            }
        }

        centerVelocity /= neighbourCount;
        Vector3 vCohesion = (centerVelocity - this.transform.position).normalized;//* cohesionWeight //center direction
        return vCohesion;
    }

    Vector3 Seperation()
    {
        Vector3 seperationCompVec = Vector3.zero;
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (this.transform.gameObject != neighbours[i].gameObject)
            {
                float distance = Vector3.Distance(this.transform.position, neighbours[i].transform.position);
                if (Vector3.Distance(this.transform.position, neighbours[i].transform.position) < seperationDistance)
                {
                    seperationCompVec += (neighbours[i].transform.position - this.transform.position).normalized * (-1 + (distance / seperationDistance));
                }
            }
        }

        Vector3 vSeperation = seperationCompVec.normalized; // * seperationWeight;
        return vSeperation;
    }
}
