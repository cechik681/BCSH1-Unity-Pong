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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftPlayerScore = 0;
        rightPlayerScore = 0;
        
        Debug.Log("x: " + paddleL.transform.localScale.x);
        Debug.Log("y: " + paddleL.transform.localScale.y);
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
        paddleL.ResetPositionL();
        paddleR.ResetPositionR();
    }
}
