using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int index;

    private void OnTriggerEnter(Collider collision)
    {
        PlayerMoveController player = collision.gameObject.GetComponent<PlayerMoveController>();
        if (player)
        {
            if(player.checkpointIndex == index - 1)
            {
                player.checkpointIndex = index;
                Debug.Log("You just passed a checkpoint successfully");
            }
            else
            {
                Debug.Log("You missed a checkpoint, your last checkpoint is " + player.checkpointIndex);
            }
        }
    }
}
