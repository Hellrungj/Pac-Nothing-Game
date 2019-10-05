using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float jumpPower = 10;
    public float maxspeed = 20; //max allowed speed
    public float speed = 2.0f;  //acceleration
    public int countValue = 1;

    public int winValue = 12;
    public Text countText;
    public Text winText;


    private Vector3 startPos;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        SetCountText();
        winText.text = "";
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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            transform.position = startPos;
            winText.text = "You Lose";
        }
        if (col.gameObject.CompareTag("Food"))
        {
            col.gameObject.SetActive(false);
            count = count + countValue;
            SetCountText();
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= winValue)
        {
            winText.text = "You Win!";
        }
    }
}
