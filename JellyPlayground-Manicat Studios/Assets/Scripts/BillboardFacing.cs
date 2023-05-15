using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFacing : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(CameraController.mainCameraPosition);
    }
}
