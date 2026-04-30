using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rb;
    public float startingSpeed;
    public GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = new Vector2(-1f*startingSpeed, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPosition(string sideScored)
    {
        rb.MovePosition(new Vector2(0, 0));
        rb.linearVelocityY = 0f;
        if (sideScored == "LeftScored")
        {
            //after score fly to RIGHT
            rb.linearVelocityX = startingSpeed;
        }
        else if (sideScored == "RightScored")
        {
            //after score fly to LEFT
            rb.linearVelocityX = -startingSpeed;
        }
    }
    public void StopMoving()
    {
        rb.linearVelocity = new Vector2(0,0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 ballCoords = transform.position;
        gm.BallHit(collision, ballCoords, startingSpeed);
    }

    public void ChangeTrajectory(Vector2 newVector)
    {
        rb.linearVelocity = newVector;
    }
}
