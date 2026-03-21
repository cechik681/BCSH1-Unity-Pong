using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int LeftPlayerScore;
    private int RightPlayerScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LeftPlayerScore = 0;
        RightPlayerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterScore(string wallHit)
    {
        //Debug.Log($"wall that was hit: {wallHit}");
    }
}
