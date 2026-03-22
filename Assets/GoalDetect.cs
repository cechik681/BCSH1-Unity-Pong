using UnityEngine;

public class GoalDetect : MonoBehaviour
{
    public GameManager gm;
    public string wallName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D()
    {
        gm.RegisterScore(wallName);
    }
}
