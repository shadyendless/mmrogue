using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour
{
    // Player Handling
    public float gravity = 20.0f;
    public float speed = 8.0f;
    public float acceleration = 12.0f;
    public float jumpHeight = 8.0f;
    public float timeToJump = 0.01f;
    public bool canMove = true;
    public GameObject suit;
    public int extraJumps = 1;
    public float fallSpeed = -10.0f;

    private float _currentSpeed;
    private float _targetSpeed;
    private bool _canJump = true;
    public Vector2 _amountToMove;
    private float _jumpStart;
    private PlayerPhysics _physics;
    private Vector3 _restartPoint;
    private tk2dSprite _sprite;
    private int _numJumps;

    // Use this for initialization
    void Start()
    {
        _numJumps = extraJumps;
        _physics = GetComponent<PlayerPhysics>();
        _restartPoint = transform.position;
        _sprite = GetComponent<tk2dSprite>();
        OuyaInput.SetContinuousScanning(true);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!canMove)
            return;

        OuyaInput.UpdateControllers();
        if (_physics.stopMovement)
            _targetSpeed = _currentSpeed = 0.0f;

        _targetSpeed = GenericInput.GetAxis(GenericAxis.LX) * speed;
        _currentSpeed = IncrementTowards(_currentSpeed, _targetSpeed, acceleration);

        if (GenericInput.GetButtonDown(GenericButton.SELECT))
            transform.position = _restartPoint;

        if (GenericInput.GetButtonDown(GenericButton.START))
            Debug.Break();

        _amountToMove.x = _currentSpeed;

        Jump();

        if (!_physics.onMovingPlatform && !_physics.grounded)
            _amountToMove.y -= gravity * Time.deltaTime;

        if (_amountToMove.y < fallSpeed)
            _amountToMove.y = fallSpeed;

        if (GenericInput.GetAxis(GenericAxis.LX) < 0) _sprite.FlipX = true;
        else if (GenericInput.GetAxis(GenericAxis.LX) > 0) _sprite.FlipX = false;

        if (_amountToMove != Vector2.zero)
        {
           _physics.Move(_amountToMove * Time.deltaTime);
        }

    }

    private void Jump()
    {
 
        if (_physics.grounded)
        {
            _numJumps = extraJumps;
            _canJump = true;
            //Debug.Log("Grounded");
        }


        if ((GenericInput.GetButtonDown(GenericButton.O) || GenericInput.GetButtonDown(GenericButton.R3)) && _canJump)
        {

            if (!_physics.grounded && _numJumps == extraJumps)
            {
                --_numJumps;
                Debug.Log("I Fell!");
            }

            if (_numJumps < 0)
            {
                _canJump = false;
                _amountToMove.y = 0;
                Debug.Log("Out of Jumps!");
            }

            else if (_numJumps >= 0)
            {
                --_numJumps;
                _amountToMove.y = jumpHeight;
                _physics.grounded = false;
                Debug.Log("Jump " + jumpHeight);
                //Debug.Break();
            }
        }

    }

    private float IncrementTowards(float n, float target, float a)
    {
        if (n == target) return n;
        float dir = Mathf.Sign(target - n);
        n += a * Time.deltaTime * dir;
        return (dir == Mathf.Sign(target - n) ? n : target);
    }
}