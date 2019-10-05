using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float jumpPower = 10;
    public float maxspeed = 20; //max allowed speed
    public float speed = 2.0f;  //acceleration

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (rb.velocity.magnitude < maxspeed)
        {
            rb.AddForce(new Vector3(moveHorizontal, 0.0f, moveVertical) * speed);
            
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(0, 150 + (rb.velocity.magnitude * jumpPower), 0);
        }


    }
}
