using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBounce : MonoBehaviour
{
    public int minJumpHeight = 1;        // Minimum jump height
    public int maxJumpHeight = 2;        // Maximum jump height
    public float minJumpDuration = 1f; // Minimum jump duration
    public float maxJumpDuration = 3f; // Maximum jump duration
    public float minInterval = 0.2f;       // Minimum time interval between jumps
    public float maxInterval = 2f;       // Maximum time interval between jumps

    private float originalY;             // Original Y position of the object
    private float targetY;               // Target Y position of the jump
    private float jumpTimer;             // Timer for tracking the jump duration
    private float intervalTimer;         // Timer for tracking the time intervals between jumps

    private void Start()
    {
        originalY = transform.position.y;
        CalculateJump();
    }

    private void Update()
    {
        if (jumpTimer < maxJumpDuration)
        {
            // Perform the jump
            jumpTimer += Time.deltaTime;
            float t = jumpTimer / maxJumpDuration;
            float newY = Mathf.Lerp(originalY, targetY, TriangleWave(t));
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        else if (intervalTimer < maxInterval)
        {
            // Wait for the next jump
            intervalTimer += Time.deltaTime;
        }
        else
        {
            // Trigger a new jump
            CalculateJump();
        }
    }

    private void CalculateJump()
    {
        targetY = originalY + Random.Range(minJumpHeight, maxJumpHeight + 1);
        jumpTimer = 0f;
        intervalTimer = 0f;
        maxJumpDuration = Random.Range(minJumpDuration, maxJumpDuration);
        maxInterval = Random.Range(minInterval, 3f);  // Randomize the time interval between jumps
    }

    private float TriangleWave(float t)
    {
        // Triangle waveform function
        return Mathf.Abs(2f * (t - Mathf.Floor(t + 0.5f)));
    }
}