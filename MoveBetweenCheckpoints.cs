using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenCheckpoints : MonoBehaviour
{
    public Transform checkpoint1;
    public Transform checkpoint2;
    public float moveDuration = 3f;
    //private Vector3 originalPlayerScale;
    

    private void Start()
    {
        
        StartCoroutine(MoveToNextCheckpoint());
    }

    private IEnumerator MoveToNextCheckpoint()
    {
        Vector3 startPosition = checkpoint1.position;
        Vector3 targetPosition = checkpoint2.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        // Swap the checkpoints and move back to the first checkpoint
        Transform temp = checkpoint1;
        checkpoint1 = checkpoint2;
        checkpoint2 = temp;

        // Start the coroutine again for the reverse movement
        StartCoroutine(MoveToNextCheckpoint());
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    } 
}