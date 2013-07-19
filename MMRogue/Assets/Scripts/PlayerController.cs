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

    private float _currentSpeed;
    private float _targetSpeed;
    private bool _canJump;
    private Vector2 _amountToMove;

    private float _jumpStart;

    private PlayerPhysics _physics;
    private Vector3 _restartPoint;

	// Use this for initialization
	void Start()
	{
	    _physics = GetComponent<PlayerPhysics>();
	    _restartPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update()
	{
	    _targetSpeed = Input.GetAxis("Horizontal")*speed;
	    _currentSpeed = _targetSpeed;//IncrementTowards(_currentSpeed, _targetSpeed, acceleration);

        if (_physics.grounded || _canJump)
        {
            _amountToMove.y = Jump();// +(gravity * Time.deltaTime);
        }
	    if (Input.GetButtonDown("Back"))
	        transform.position = _restartPoint;

        _amountToMove.x = _currentSpeed;
        _amountToMove.y -= gravity * Time.deltaTime;
        _physics.Move(_amountToMove * Time.deltaTime);
	}

    private float Jump()
    {
        float currentHeight = 0.0f;
        float jumpRatio = 0.0f;
        if (Input.GetButtonDown("Jump") && _physics.grounded)
        {
            _jumpStart = Time.time;
            _canJump = true;
            _physics.grounded = false;
        }

        if (Input.GetButton("Jump") && _canJump)
        {
            jumpRatio = (Time.time - _jumpStart) / timeToJump;
            if (jumpRatio > 0.6f) jumpRatio = 1.0f;
            currentHeight = jumpHeight * jumpRatio;
            if (currentHeight >= jumpHeight)
            {
                currentHeight = jumpHeight;
                _canJump = false;
            }
            
            if (Time.time - _jumpStart >= timeToJump)
            {
                _canJump = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            _physics.grounded = false;
            currentHeight = 0.0f;
            _canJump = false;
        }

        return currentHeight;
    }

    private float IncrementTowards(float n, float target, float a)
    {
        if (n == target) return n;
        float dir = Mathf.Sign(target - n);
        n += speed * Time.deltaTime * dir;
        return (dir == Mathf.Sign(target - n) ? n : target);
    }
}