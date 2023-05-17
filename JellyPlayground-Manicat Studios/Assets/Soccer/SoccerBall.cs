using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : MonoBehaviour
{
    public GameObject goal;
    public GameObject goal2;
    public ParticleSystem goalParticle;
    public ParticleSystem goalParticle2;
    public float kickForce;
    public BoxCollider targetCollider;
    public Transform teleportLocation;
    public float dampingForce = 1f;

    private Rigidbody ballRigidbody;
    private bool isTeleporting = false;

    private void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
        if (other.gameObject == goal2)
        {
            goalParticle2.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<BoxCollider>() == targetCollider && !isTeleporting)
        {
            isTeleporting = true;
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
            Invoke(nameof(TeleportBall), 0.1f);
        }
    }

    private void TeleportBall()
    {
        transform.position = teleportLocation.position;
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
        isTeleporting = false;
        ApplyDampingForce();
    }

    private void ApplyDampingForce()
    {
        if (dampingForce > 0f)
        {
            ballRigidbody.AddForce(-ballRigidbody.velocity * dampingForce, ForceMode.Force);
        }
    }
}