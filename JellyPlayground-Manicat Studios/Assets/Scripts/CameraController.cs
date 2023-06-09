using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _followTransform;

    [SerializeField]
    private Vector3 _offset = Vector3.zero;

    [SerializeField]
    private float _verticalSpeed = 1f;

    [SerializeField]
    private float _horizontalSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        InputHandler.instance.MouseInput += NewMouseInput;
    }

    private void Update()
    {
        this.transform.position = _followTransform.position + _offset;
    }

    private void NewMouseInput(Vector2 mouseValue)
    {
        Vector3 newRotationValue = new Vector3(mouseValue.y, mouseValue.x * -1, 0);
        float newXRotation = transform.eulerAngles.x - (newRotationValue.x * _verticalSpeed);
        transform.eulerAngles = new Vector3(newXRotation, transform.eulerAngles.y - newRotationValue.y * _horizontalSpeed , 0) * Time.timeScale;

        InputHandler.instance.MoveDirection = new Vector3(0, this.transform.eulerAngles.y, 0);
    }


}
