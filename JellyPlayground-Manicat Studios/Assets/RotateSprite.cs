using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    [SerializeField] float speed = 10f; // Rotation speed

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }


}


