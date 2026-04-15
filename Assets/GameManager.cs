using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public TextMeshProUGUI leftTextScore;
    public TextMeshProUGUI rightTextScore;
    public PaddleL paddleL;
    public PaddleR paddleR;
    
    private int leftPlayerScore;
    private int rightPlayerScore;
    private float paddleLHeight;
    private float paddleRHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftPlayerScore = 0;
        rightPlayerScore = 0;
        
        paddleLHeight = paddleL.GetComponent<BoxCollider2D>().bounds.size.y;
        paddleRHeight = paddleR.GetComponent<BoxCollider2D>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterScore(string wallHit)
    {
        if (wallHit == "LeftDetectionWall")
        {
            rightPlayerScore += 1;
            rightTextScore.text = rightPlayerScore.ToString();
        }
        else if (wallHit == "RightDetectionWall")
        {
            leftPlayerScore += 1;
            leftTextScore.text = leftPlayerScore.ToString();
        }
        //timeout so the ball doesn't spawn at the moment
        ball.ResetPosition();
        //paddleL.ResetPositionL();
        //paddleR.ResetPositionR();
    }

    public void BallHit(Collision2D collision, Vector2 ballCoords)
    {
        string objectHitName = collision.gameObject.name;

        float ballX = ballCoords.x;
        float ballY = ballCoords.y;
        
        Vector3 paddleCoords = collision.transform.position;
        float paddleY = paddleCoords.y; //I won't need X coordinate
        float VSizePaddle = collision.collider.bounds.size.y;

        Debug.Log($"\nBall: {ballX}, {ballY};;; PaddleY: {paddleY} and {VSizePaddle}");

        if (objectHitName == "PaddleL")
        {
            //Debug.Log("Lpaddle hit");

            Vector2 v = new Vector2(1f*5, 1f);
            ball.ChangeTrajectory(v);
        }
        else if (objectHitName == "PaddleR")
        {
            //Debug.Log("Rpaddle hit");
            Vector2 v = new Vector2(-1f*5, -1f);
            ball.ChangeTrajectory(v);
        }
        else
        {
            /*do nothing, possible other collisions:
            top/bottom wall,
            detectionWall
            */
        }
    }
}
