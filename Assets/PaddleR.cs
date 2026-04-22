using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleR : MonoBehaviour
{
    public float moveSpeed;

    private float xCoord;
    private float yCoord;
    private float zCoord;

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
        xCoord = transform.position.x;
        yCoord = transform.position.y;
        zCoord = transform.position.z;

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

    public void ResetPositionR()
    {
        transform.position = new Vector3(xCoord, 0, zCoord);
    }

    public void ChangeSize(float newSize)
    {
        Vector3 currentScale = transform.localScale;
        currentScale.y = newSize;
        transform.localScale = currentScale;
    }
}
