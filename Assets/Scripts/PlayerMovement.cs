using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            rb.AddForce(Vector3.right * movementSpeed, ForceMode.VelocityChange);
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            rb.AddForce(Vector3.left * movementSpeed, ForceMode.VelocityChange);
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            rb.AddForce(Vector3.forward * movementSpeed, ForceMode.VelocityChange);
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            rb.AddForce(Vector3.back * movementSpeed, ForceMode.VelocityChange);
        }
    }
}
