using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CircularMovement : MonoBehaviour
{
    public float radius = 2f;          // Radius of the circular path
    public float speed = 2f;           // Speed of rotation in degrees per second
    public bool isClockwise = false;   // Set this to true for clockwise motion

    private Vector2 centerPosition;    // Center of the circular path
    private float currentAngle = 0f;   // Current angle in degrees

    private void Start()
    {
        // Store the initial position as the center of the circular path
        centerPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the new position based on the current angle and radius
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
