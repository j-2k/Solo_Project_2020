using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += -transform.right * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += -transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += transform.right * speed;
        }
    }
}
