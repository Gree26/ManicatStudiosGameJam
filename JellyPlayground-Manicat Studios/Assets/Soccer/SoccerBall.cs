using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : MonoBehaviour
{

    public GameObject goal;
    public ParticleSystem goalParticle;
    public float kickForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody ballRigidbody = GetComponent<Rigidbody>();
            Vector3 kickDirection = collision.contacts[0].normal;
            ballRigidbody.AddForce(kickDirection * kickForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == goal)
        {
            goalParticle.Play();
        }
    }
}