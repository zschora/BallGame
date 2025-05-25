using UnityEngine;

public class BallController : MonoBehaviour
{
    
    public Rigidbody2D myBall;

    public void StartMoving()
    {
        if (myBall.bodyType == RigidbodyType2D.Kinematic) {
            myBall.bodyType = RigidbodyType2D.Dynamic;
            myBall.linearVelocity = new Vector2 (0f, -2f);
        }
    }

}
