using UnityEngine;
using UnityEngine.InputSystem;

public class P1 : MonoBehaviour
{
    public float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isPressingUp = Keyboard.current.wKey.isPressed;
        bool isPressingDown = Keyboard.current.sKey.isPressed;

        if (isPressingUp)
        {
            transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
        }
        if (isPressingDown)
        {
            transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);
        }
    }
}
