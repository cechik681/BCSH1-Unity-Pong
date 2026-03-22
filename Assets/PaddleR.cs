using UnityEngine;
using UnityEngine.InputSystem;

public class P2 : MonoBehaviour
{
    public float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isPressingUp = Keyboard.current.upArrowKey.isPressed;
        bool isPressingDown = Keyboard.current.downArrowKey.isPressed;

        //default coordinates
        float xCoord = transform.position.x;
        float yCoord = transform.position.y;
        float zCoord = transform.position.z;

        if (isPressingUp)
        {
            transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
            if (yCoord > 3.5)
            {
                transform.position = new Vector3(xCoord, (float)3.5, zCoord);
            }
        }
        if (isPressingDown)
        {
            transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);
            if (yCoord < -3.5)
            {
                transform.position = new Vector3(xCoord, (float)-3.5, zCoord);
            }
        }
    }
}
