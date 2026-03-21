using UnityEngine;

public class GoalDetect : MonoBehaviour
{
    public string wallName;
    public GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        gm.RegisterScore(wallName);
        if (wallName == "LeftDetectionWall")
        {
            Debug.Log("left wall detected touch");
        }
        else
        {
            Debug.Log("right wall detected touch");
        }
    }
}
