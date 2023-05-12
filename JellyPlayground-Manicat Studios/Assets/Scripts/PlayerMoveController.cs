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

    private float _moveSpeedDrag = -.01f;

    [SerializeField] [Min(0)]
    private float _moveSpeedAcceleration = .02f;
    private float _moveSpeedDeceleration = -.1f;


    Vector2 positionModifier = Vector2.zero;

    private bool _stunned = false;

    private bool isStunned
    {
        set
        {
            _stunned = value;
            StartCoroutine(StunTime());
        }
    }

    private void Start()
    {
        _myRigidBody = this.GetComponent<Rigidbody>();
        InputHandler.instance.MoveInput += NewMoveInput;
        InputHandler.instance.Jump += Jump;
    }

    private void Update()
    {
        if (_stunned)
        {
            return;
        }

        ModifyMoveSpeed(_moveSpeedDrag);

        _myRigidBody.MovePosition(_myRigidBody.position + (this.transform.forward * positionModifier.y * _moveSpeed * Time.deltaTime));
        _myRigidBody.MovePosition(_myRigidBody.position + (this.transform.right * positionModifier.x * _moveSpeed * Time.deltaTime));

        if (DetectCollision())
        {
            _myRigidBody.velocity = new Vector3(-this.transform.forward.x * 10, 3, -this.transform.forward.y * 10) * (_moveSpeed/ _capSpeed);
            _moveSpeed = 0;
            GameDataManager.isSpeed = false;
            isStunned = true;
        }
    }

    private void Jump()
    {
        if (DetectStanding())
        {
            _myRigidBody.velocity = Vector3.up * 4;
        }
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
        int layerMask = ~LayerMask.GetMask("Berries");

        if (Physics.Raycast(transform.position, transform.forward*1.1f, .55f, layerMask, QueryTriggerInteraction.Ignore))
        {
            return true;
        }
        return false;
    }

    private bool DetectStanding()
    {
        int layerMask = ~LayerMask.GetMask("Berries");

        if (Physics.Raycast(transform.position, Vector3.down * 1.1f, .55f, layerMask, QueryTriggerInteraction.Ignore))
        {
            return true;
        }
        return false;
    }

    private IEnumerator StunTime()
    {
        int stunnedTimer = 50;
        while (stunnedTimer > 0)
        {
            stunnedTimer--;
            yield return new WaitForEndOfFrame();
        }
        _stunned = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Boost();
    }
}
