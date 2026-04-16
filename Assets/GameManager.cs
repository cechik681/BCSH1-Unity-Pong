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

    public void BallHit(Collision2D collision, Vector2 ballCoords, float ballSpeed)
    {
        string objectHitName = collision.gameObject.name;

        float ballX = ballCoords.x;
        float ballY = ballCoords.y;

        Vector3 paddleCoords = collision.transform.position;
        float paddleY = paddleCoords.y; //I won't need X coordinate
        float VSizePaddle = collision.collider.bounds.size.y;
        string name = collision.gameObject.name;

        //Debug.Log($"\nBall: {ballX}, {ballY};;; PaddleY: {paddleY} and {VSizePaddle}");

        float relativeHit = ballY - paddleY;
        //percentage hit from center of paddle
        //(VSizePaddle / 2f) - we care only from center to edge NOT top/bottom edge
        float normalizedHit = relativeHit / (VSizePaddle / 2f);

        //bounce angle (degrees)
        float maxBounceAngle = 60f;
        float bounceAngle = normalizedHit * maxBounceAngle;
        Debug.Log($"bounce angle: {bounceAngle}deg");

        //convert to radians (because Unity)
        float bounceAngleRad = bounceAngle * Mathf.Deg2Rad;

        float dirX = Mathf.Cos(bounceAngleRad); //cos - X direction
        float dirY = Mathf.Sin(bounceAngleRad); //sin - Y direction

        if (objectHitName == "PaddleL")
        {
            //paddleL bounce -> => dirX+
            Vector2 v = new Vector2(dirX, dirY) * ballSpeed;
            ball.ChangeTrajectory(v);
        }
        else if (objectHitName == "PaddleR")
        {
            //paddleR bounce <- => dirX-
            Vector2 v = new Vector2(-dirX, dirY) * ballSpeed;
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
