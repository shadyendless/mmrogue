using UnityEngine;
using System.Collections;

public class HookShot : MonoBehaviour {

    public GameObject _player;
    public float velocity = 1.2f;
    public LayerMask collisionMask;

    private float _timeBorn;
    public float _timeAlive = 1.2f;
    private Ray _ray;
    private RaycastHit _hit;
    private Vector2 _point;
    private PlayerPhysics _physics;
    private bool _hooked = false;
    private LineRenderer _hookLine;

    void Awake()
    {
        _hookLine = GetComponentInChildren<LineRenderer>();
        _timeBorn = Time.time;
        _player = GameObject.FindGameObjectWithTag("Player");
        _physics = _player.GetComponent<PlayerPhysics>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log(_player.transform.position);
        if(_hooked == false)
            transform.Translate(Vector2.right * velocity * Time.deltaTime, Space.Self);

        _ray = new Ray(transform.position, transform.right);
        Debug.DrawRay(_ray.origin, _ray.direction);

        if (Physics.Raycast(_ray, out _hit, velocity * Time.deltaTime, collisionMask))
        {
            _point =_hit.transform.position;
            _hooked = true;;
            _player.GetComponent<PlayerController>()._canMove = false;
            //DestroyImmediate(gameObject);
        }

        if (Time.time - _timeBorn >= _timeAlive && _hooked == false)
        {
            Destroy(gameObject);
        }

        if (GenericInput.GetButtonDown(GenericButton.O) && _hooked == true)
        {
            Destroy(gameObject);
            _hooked = false;
            _player.GetComponent<PlayerController>()._canMove = true;
            _physics.grounded = true;
        }

        _hookLine.SetPosition(0, _player.transform.position + new Vector3(0,1,0));
        _hookLine.SetPosition(1, transform.position);

        Debug.Log(_hookLine.transform.position);

        if (_hooked)
        {
            PullPlayerToMe();
            //Debug.Break();
        }
	}

    void PullPlayerToMe()
    {
        _physics.Move((transform.position - _player.transform.position) * Time.deltaTime * velocity);
    }
}
