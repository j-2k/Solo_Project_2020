using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public Vector3 sphereVelocity;

    // Start is called before the first frame update
    void Start()
    {

        sphereVelocity = transform.forward * 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += sphereVelocity * Time.deltaTime;
        Debug.DrawRay(transform.position,sphereVelocity,Color.white);
    }
}
