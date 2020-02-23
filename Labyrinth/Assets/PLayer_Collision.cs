using UnityEngine;

public class PLayer_Collision : MonoBehaviour
{
    public Player_Movement movement;
    private void OnCollisionEnter(Collision collisioninfo)
    {
        if (collisioninfo.collider.tag == "Obstacle")
        {
            movement.enabled = false;
        }
    }
}
