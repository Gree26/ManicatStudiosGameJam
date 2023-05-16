using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : MonoBehaviour
{
    public float kickForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody ballRigidbody = GetComponent<Rigidbody>();

            // Calculate the kick direction based on the collision normal
            Vector3 kickDirection = collision.contacts[0].normal;

            // Apply the kick force to the ball in the kick direction
            ballRigidbody.AddForce(kickDirection * kickForce, ForceMode.Impulse);
        }
    }
}