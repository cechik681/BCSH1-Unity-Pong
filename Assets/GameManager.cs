using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public TextMeshProUGUI leftTextScore;
    public TextMeshProUGUI rightTextScore;
    public PaddleL paddleL;
    public PaddleR paddleR;
    public GameObject winningPanel;
    public TextMeshProUGUI winningText;

    private int leftPlayerScore;
    private int rightPlayerScore;
    private float paddleLHeight;
    private float paddleRHeight;
    private float playtimeCounter = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f; //temporary

        leftPlayerScore = 0;
        rightPlayerScore = 0;

        paddleLHeight = paddleL.GetComponent<BoxCollider2D>().bounds.size.y;
        paddleRHeight = paddleR.GetComponent<BoxCollider2D>().bounds.size.y;

        int levelToLoad = MenuManager.chosenLevel;

        //-------- default settings is level 1:
        //BallMovingSpeed(5);
        //PaddlesMovingSpeed(5);
        //PaddlesSize(2);
        if (levelToLoad == 2)
        {
            BallMovingSpeed(10);
            PaddlesMovingSpeed(7);
            PaddlesSize(2);
        }else if (levelToLoad == 3)
        {
            BallMovingSpeed(15);
            PaddlesMovingSpeed(12);
            PaddlesSize(1.5f);
        }
    }
    void BallMovingSpeed(float newSpeed)
    {
        ball.startingSpeed = newSpeed;
    }
    void PaddlesMovingSpeed(float newSpeed)
    {
        paddleL.moveSpeed = newSpeed;
        paddleR.moveSpeed = newSpeed;
    }
    void PaddlesSize(float newSize)
    {
        paddleL.ChangeSize(newSize);
        paddleR.ChangeSize(newSize);
    }

    // Update is called once per frame
    void Update()
    {
        playtimeCounter += Time.deltaTime;
    }

    public void RegisterScore(string wallHit)
    {
        float timeout = 1f;
        if (wallHit == "LeftDetectionWall")
        {
            rightPlayerScore += 1;
            rightTextScore.text = rightPlayerScore.ToString();
            ball.StopMoving();
            Invoke("ResetBallRight", timeout);
        }
        else if (wallHit == "RightDetectionWall")
        {
            leftPlayerScore += 1;
            leftTextScore.text = leftPlayerScore.ToString();
            ball.StopMoving();
            Invoke("ResetBallLeft", timeout);
        }
        //possible paddles position reset after goal
        //paddleL.ResetPositionL();
        //paddleR.ResetPositionR();

        //winning
        int maxPointsToWin = 5;
        if (leftPlayerScore == maxPointsToWin)
        {
            //left one wins
            PlayerWins("L");
        }
        else if (rightPlayerScore == maxPointsToWin)
        {
            //right one wins
            PlayerWins("R");
        }
    }
    //functions to make timeout, so the ball wouldn't respawn instantly
    void ResetBallLeft()
    {
        ball.ResetPosition("LeftScored");
    }
    void ResetBallRight()
    {
        ball.ResetPosition("RightScored");
    }

    void PlayerWins(string whoWin)
    {
        winningPanel.SetActive(true);
        winningText.text = whoWin + " wins!";
        SaveMatchTxt(whoWin);
        //slows the game before moving to MainMenu
        Time.timeScale = 0.25f;

        Invoke("RestartGame", 0.5f);
    }
    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
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

        float relativeHit = ballY - paddleY;
        //percentage hit from center of paddle
        //(VSizePaddle / 2f) - distance from center to edge NOT from top/bottom edgeß
        float normalizedHit = relativeHit / (VSizePaddle / 2f);

        //bounce angle (degrees)
        float maxBounceAngle = 50f;
        float bounceAngle = normalizedHit * maxBounceAngle;

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

    public void SaveMatchTxt(string whoWin)
    {
        int min = Mathf.FloorToInt(playtimeCounter / 60f);
        int sec = Mathf.FloorToInt(playtimeCounter % 60f);
        string niceTime = min.ToString("00") + ":" + sec.ToString("00");

        string path = Application.persistentDataPath + "/MatchLog.txt";

        string text = whoWin + " won! Score: " +
        leftPlayerScore + ":" + rightPlayerScore +
        " Total playtime: " + niceTime + "\n";

        File.AppendAllText(path, text);
    }
}
