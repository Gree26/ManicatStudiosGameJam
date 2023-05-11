using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    
    public List<Checkpoint> checkpoints;

    private void OnTriggerEnter(Collider collision)
    {
        PlayerMoveController player = collision.gameObject.GetComponent<PlayerMoveController>();

        if (player)
        {
            if(player.checkpointIndex == checkpoints.Count)
            {
                player.checkpointIndex = 0;
                Debug.Log("Got to the finish line, index reset!");
            }
        }    
    }
}
