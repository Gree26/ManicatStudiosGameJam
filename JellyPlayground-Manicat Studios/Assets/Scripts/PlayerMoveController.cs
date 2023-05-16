using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveController : MonoBehaviour
{
    private static float _currentTime = 0;
    public static float CurrentTime
    {
        get
        {
            return _currentTime;
        }
    }

    private Rigidbody _myRigidBody;

    private float _maxMoveSpeed = 10;

    [SerializeField]
    [Min(0)]
    private float _maxWalkSpeed = 10;

    [SerializeField]
    [Min(0)]
    private float _slowMoveSpeed = 5;

    [SerializeField]
    [Min(0)]
    private float _capSpeed = 15;

    private float _moveSpeed = 1;
    public int checkpointIndex;

    [SerializeField]
    private float _moveSpeedDrag = -.01f;

    [SerializeField] [Min(0)]
    private float _moveSpeedAcceleration = .05f;
    private float _moveSpeedDeceleration = -.075f;


    Vector2 positionModifier = Vector2.zero;

    private bool _stunned = false;

    private Vector3 _scale = Vector3.one;

    //Berries Collected
    [SerializeField] int berriesCollected;
    [SerializeField] List<GameObject> berries;

    [SerializeField] public List<GameObject> berriesLap0;
    [SerializeField] public List<GameObject> berriesLap1;
    [SerializeField] public List<GameObject> berriesLap2;

    //Sound------------------------------------------------
    [SerializeField] AK.Wwise.Event jumpEvent;
    [SerializeField] AK.Wwise.Event crouchEvent;
    [SerializeField] AK.Wwise.Event collisionEvent;
    [SerializeField] AK.Wwise.Event accelerationEvent;
    [SerializeField] AK.Wwise.RTPC RTPC_Acceleration;

    [SerializeField] private float _brakeForce = 0.001f;
    [SerializeField] private bool _isBraking = false;

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
        _maxMoveSpeed = _maxWalkSpeed;
        _myRigidBody = this.GetComponent<Rigidbody>();
        InputHandler.instance.MoveInput += NewMoveInput;
        InputHandler.instance.Jump += Jump;
        InputHandler.instance.Slide += Slide;
        InputHandler.instance.BrakeInput += Brake;

        _scale = this.transform.localScale;
        checkpointIndex = 0;

        accelerationEvent.Post(this.gameObject);

        _currentTime = 0;
    }

    private void Update()
    {
        if (_stunned)
        {
            return;
        }

        ModifyMoveSpeed(_moveSpeedDrag, 0, _capSpeed);

        _myRigidBody.MovePosition(_myRigidBody.position + (this.transform.forward * Mathf.Abs( positionModifier.y) * _moveSpeed * Time.deltaTime));
        _myRigidBody.MovePosition(_myRigidBody.position + (this.transform.right * positionModifier.x * _maxMoveSpeed / 2 * Time.deltaTime));

        if (_isBraking)
        {
            ModifyMoveSpeed(-_brakeForce, 0, _maxMoveSpeed);
        }

        if (DetectCollision())
        {
            _myRigidBody.velocity = new Vector3(-this.transform.forward.x * 10, 3, -this.transform.forward.y * 10) * (_moveSpeed/ _capSpeed);
            _moveSpeed = 0;
            GameDataManager.isSpeed = false;
            isStunned = true;
        }
        RTPC_Acceleration.SetValue(this.gameObject,_moveSpeed);
        _currentTime += Time.deltaTime;
    }

    private void Jump()
    {
        if (DetectStanding())
        {
            _myRigidBody.velocity = Vector3.up * 4;
            jumpEvent.Post(this.gameObject);
            
        }
    }

    private void NewMoveInput(Vector2 moveValue)
    {
        ModifyMoveSpeed(_moveSpeedAcceleration * moveValue.y, -_maxMoveSpeed, _maxMoveSpeed);

        transform.rotation = Quaternion.Euler( InputHandler.instance.MoveDirection);
        positionModifier = moveValue;
    }

    /// <summary>
    /// Adjust Speed by the given parameters.
    /// </summary>
    /// <param name="newValue"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    private void ModifyMoveSpeed(float newValue, float minValue, float maxValue)
    {
        bool isGreater = _moveSpeed > _maxWalkSpeed;

        if (isGreater && newValue >= 0)
            return;


        GameDataManager.isSpeed = (isGreater)? true:false;

        _moveSpeed = (isGreater)? Mathf.Clamp(_moveSpeed + newValue, minValue, _capSpeed) : Mathf.Clamp(_moveSpeed + newValue, minValue, maxValue);
        


    }

    private void Slide(bool isSlidiing)
    {
        this.transform.localScale = (isSlidiing) ? new Vector3(_scale.x, _scale.y / 2, _scale.z) : _scale;
        this.transform.localPosition = (isSlidiing) ? new Vector3(transform.localPosition.x, transform.localPosition.y - _scale.y / 4, transform.localPosition.z) : new Vector3(transform.localPosition.x, transform.localPosition.y + _scale.y / 4, transform.localPosition.z);
        crouchEvent.Post(this.gameObject);
        if (!DetectStanding())
            return;
        if(_moveSpeed<0)
            ModifyMoveSpeed(-_moveSpeedDeceleration, -_maxMoveSpeed, 0);
        else
            ModifyMoveSpeed(_moveSpeedDeceleration, 0, _maxMoveSpeed);
    }

    private void Boost()
    {
        GameDataManager.isSpeed = true;
        _moveSpeed = _capSpeed;
        jumpEvent.Post(this.gameObject);

    }

    private bool DetectCollision()
    {
        int layerMask = LayerMask.GetMask("Obstacle");


        if (Physics.Raycast(transform.position, transform.forward*1.1f, .55f, layerMask, QueryTriggerInteraction.Ignore))
        {
            collisionEvent.Post(this.gameObject);
            return true;
        }
        return false;
    }

    private bool DetectStanding()
    {
        int layerMask = Physics.AllLayers;

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

    private void Brake(bool isBraking)
    {
        _isBraking = isBraking;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(LayerMask.LayerToName( other.gameObject.layer) == "Berry")
            Boost();  

            //EDITED BY MIKOANGELO
        //BOOSTS WHEN COLLIDES WITH "SPEED BOOST" TAG
        if (other.CompareTag("SpeedBoost"))
        {
            Boost();
        }

        if (other.CompareTag("Checkpoint"))
        {
            checkpointIndex++;
            switch (checkpointIndex)
            {
                case 1:
                    foreach (var berry in berriesLap0)
                    {
                        berry.SetActive(true);
                    }
                    break;
                case 2:
                    foreach (var berries in berriesLap1)
                    {
                        berries.SetActive(true);
                    }
                    break;
                case 3:
                    foreach (var berries in berriesLap2)
                    {
                        berries.SetActive(true);
                    }
                    break;
                default:
                    Debug.Log("You Won!");
                    break;
            }


        }

        if (other.CompareTag("Berry"))
        {
            
            foreach (var berry in berries)
            {
                if (other.gameObject == berry)
                {
                    berriesCollected++;
                    Destroy(berry);
                    break; // exit the loop once the object is destroyed
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Slow")
        {
            _maxMoveSpeed = _slowMoveSpeed;
            Debug.Log("Start Slow");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Slow")
        {
            _maxMoveSpeed = _maxWalkSpeed;
            Debug.Log("Stop Slow");
        }
    }
}
