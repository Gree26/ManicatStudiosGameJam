using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    private int _totalLaps = 3;
    private int _currentLap = 1;

    public List<Checkpoint> checkpoints;

    private void OnTriggerEnter(Collider collision)
    {
        PlayerMoveController player = collision.gameObject.GetComponent<PlayerMoveController>();

        if (player)
        {
            if(player.checkpointIndex == checkpoints.Count)
            {
                player.checkpointIndex = 0;
                _currentLap++;
                Debug.Log("Got to the finish line, index reset!");

                if (_currentLap >= _totalLaps)
                {
                    GameDataManager.RaceCompleted();
                }
                else
                {
                    GameDataManager.LapFinished(_currentLap);
                }
            }
        }    
    }
}
