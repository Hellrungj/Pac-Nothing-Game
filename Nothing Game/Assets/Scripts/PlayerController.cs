using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameController GameManager;
    Rigidbody rb;
    public float jumpPower = 10;
    public float maxspeed = 20; //max allowed speed
    public float speed = 2.0f;  //acceleration

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
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
        if (col.gameObject.CompareTag("Enemy") && !GameManager.PowerUpActive)
        {
            //transform.position = startPos; Debug
            GameManager.GameOver();
        }
    }
}
