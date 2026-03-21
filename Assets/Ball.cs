using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rb;
    public float startingSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //QualitySettings.vSyncCount = 0;

        bool isRight = UnityEngine.Random.value >= 0.5;
        float xVelocity = -1f;
        if(isRight == true)
        {
            xVelocity = 1f;
        }
        float yVelocity = UnityEngine.Random.Range(-1, 1);
        //rb.linearVelocity = new Vector2(xVelocity * startingSpeed, yVelocity * startingSpeed);
        rb.linearVelocity = new Vector2(-1f*startingSpeed, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
