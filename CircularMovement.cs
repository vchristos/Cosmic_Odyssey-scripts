using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CircularMovement : MonoBehaviour
{
    public float radius = 2f;          // Radius of the circular path
    public float speed = 2f;           // Speed of rotation in degrees per second
    public bool isClockwise = false;   

    private Vector2 centerPosition;   
    private float currentAngle = 0f;   // Current angle in degrees

    private void Start()
    {
        
        centerPosition = transform.position;
    }

    private void Update()
    {
        
        float angleInRadians = currentAngle * Mathf.Deg2Rad;
        float x = centerPosition.x + radius * Mathf.Cos(angleInRadians);
        float y = centerPosition.y + radius * Mathf.Sin(angleInRadians);

        // Update the object's position
        transform.position = new Vector2(x, y);

        // Update the angle for the next frame
        if (isClockwise)
        {
            currentAngle -= speed * Time.deltaTime;
        }
        else
        {
            currentAngle += speed * Time.deltaTime;
        }
    }
}
