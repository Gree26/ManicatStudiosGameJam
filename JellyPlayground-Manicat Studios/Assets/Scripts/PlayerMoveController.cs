using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveController : MonoBehaviour
{
    private Rigidbody _myRigidBody;

    [SerializeField] [Min(0)]
    private float _maxMoveSpeed = 10;

    [SerializeField]
    [Min(0)]
    private float _capSpeed = 15;

    private float _moveSpeed = 1;
    public int checkpointIndex;

    private float _moveSpeedDrag = -.01f;

    [SerializeField] [Min(0)]
    private float _moveSpeedAcceleration = .02f;
    private float _moveSpeedDeceleration = -.1f;


    Vector2 positionModifier = Vector2.zero;

    private void Start()
    {
        _myRigidBody = this.GetComponent<Rigidbody>();
        InputHandler.instance.MoveInput += NewMoveInput;
        checkpointIndex = 0;
    }

    private void Update()
    {
        ModifyMoveSpeed(_moveSpeedDrag);

        _myRigidBody.MovePosition(_myRigidBody.position + (this.transform.forward * positionModifier.y * _moveSpeed * Time.deltaTime));
        _myRigidBody.MovePosition(_myRigidBody.position + (this.transform.right * positionModifier.x * _moveSpeed * Time.deltaTime));

        if (DetectCollision())
        {
            _moveSpeed = 0;
        }

        Debug.Log("Movement Speed: " + _moveSpeed);
    }

    private void NewMoveInput(Vector2 moveValue)
    {
        ModifyMoveSpeed(_moveSpeedAcceleration);

        transform.rotation = Quaternion.Euler( InputHandler.instance.MoveDirection);
        positionModifier = moveValue;
    }

    private void ModifyMoveSpeed(float newValue)
    {
        bool isGreater = _moveSpeed > _maxMoveSpeed;

        if (isGreater && newValue >= 0)
            return;


        GameDataManager.isSpeed = (isGreater)? true:false;

        _moveSpeed = (isGreater)? Mathf.Clamp(_moveSpeed + newValue, 0, _capSpeed) : Mathf.Clamp(_moveSpeed + newValue, 0, _maxMoveSpeed);
    }

    private void Boost()
    {
        GameDataManager.isSpeed = true;
        _moveSpeed = _capSpeed;
    }

    private bool DetectCollision()
    {
        if (Physics.Raycast(transform.position, transform.forward, .55f, Physics.AllLayers ,QueryTriggerInteraction.Ignore))
        {
            Debug.Log("STOP");
            GameDataManager.isSpeed = false;
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Boost();
    }
}
