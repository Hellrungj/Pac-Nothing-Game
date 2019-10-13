using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameController GameManager;
    Rigidbody rb;

    public float maxspeed = 20; //max allowed speed
    public float speed = 10.0f;  //acceleration

    private Vector3 startPos;

    public float jumpForce = 1.0f;
    public LayerMask GroundLayers;
    public SphereCollider col;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
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

        if (isGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Jump"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, GroundLayers);   
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
