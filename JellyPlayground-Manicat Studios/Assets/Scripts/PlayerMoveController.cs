using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveController : MonoBehaviour
{
    private Rigidbody _myRigidBody;

    [SerializeField]
    private float _moveSpeed = 1;
    public int checkpointIndex;

    private void Start()
    {
        _myRigidBody = this.GetComponent<Rigidbody>();
        InputHandler.instance.MoveInput += NewMoveInput;
        checkpointIndex = 0;
    }

    private void NewMoveInput(Vector2 moveValue)
    {
        transform.rotation = Quaternion.Euler( InputHandler.instance.MoveDirection);
        Vector3 positionModifier = new Vector3(moveValue.x, 0, moveValue.y);

        _myRigidBody.MovePosition(_myRigidBody.position + (this.transform.forward * moveValue.y * _moveSpeed * Time.deltaTime));
        _myRigidBody.MovePosition(_myRigidBody.position + (this.transform.right * moveValue.x * _moveSpeed * Time.deltaTime));
    }
}
