using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public LayerMask collisionMask;
    public bool moveX = false;
    public bool moveY = false;
    public Vector3 endPos;

    public float velocity = 5.0f;

    private bool goingRight = true;
    private bool goingUp = true;

    private BoxCollider _collider;
    private Vector3 _s;
    private Vector3 _c;

    private Ray _ray;
    private RaycastHit _hit;

    private Vector3 startPos;
    private PlayerPhysics player = null;
    private PlayerPhysics prevPlayer = null;

    private GameObject ___player;
    private bool movePlayer = false;

    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _s = _collider.size;
        _s.x *= transform.localScale.x;
        _s.y *= transform.localScale.y;
        _c = _collider.center;
    }

    void Awake()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (___player == null)
            ___player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) prevPlayer = player;


        movePlayer = true;
        if (___player != null)
        {
            Bounds smallBounds = ___player.collider.bounds;
            smallBounds.size = new Vector3(smallBounds.size.x, smallBounds.size.y * 0.90f, smallBounds.size.z);
            if (collider.bounds.Intersects(smallBounds))
            {
                ___player.GetComponent<PlayerPhysics>().Move(new Vector3(0.0f, 0.25f + velocity * Time.deltaTime, 0.0f));
                movePlayer = false;
            }
        }

        player = null;
        for (int i = 0; i < 4; ++i)
        {
            float dir = 1;
            Vector2 p = transform.position;
            float x = (p.x + _c.x - _s.x / 2.0f) + _s.x / 3.0f * i;
            float y = p.y + _c.y + _s.y / 2.0f * dir;

            _ray = new Ray(new Vector2(x, y), new Vector2(0, dir));
            Debug.DrawRay(_ray.origin, _ray.direction, Color.white, 0.15f);

            if (Physics.Raycast(_ray, out _hit, 0.15f, collisionMask))
            {
                player = _hit.collider.gameObject.GetComponent<PlayerPhysics>();
                break;
            }
        }

        if (player == null && prevPlayer != null)
        {
            prevPlayer.onMovingPlatform = false;
        }

        Vector3 translateVector = Vector3.zero;
        if (moveX)
        {
            if (goingRight)
            {
                if (transform.position.x < endPos.x)
                    translateVector += Vector3.right * velocity * Time.deltaTime;
                else
                    goingRight = false;
            }
            else
            {
                if (transform.position.x > startPos.x)
                    translateVector += -Vector3.right * velocity * Time.deltaTime;
                else
                    goingRight = true;
            }
        }

        if (moveY)
        {
            if (goingUp)
            {
                if (transform.position.y < endPos.y)
                    translateVector += Vector3.up * velocity * Time.deltaTime;
                else
                    goingUp = false;
            }
            else
            {
                if (player != null) player.onMovingPlatform = false;
                if (transform.position.y > startPos.y)
                    translateVector += -Vector3.up * velocity * Time.deltaTime;
                else
                    goingUp = true;
            }
        }

        if (goingUp)
        {
            if (movePlayer)
            {
                if (player != null) player.Move(translateVector);
                transform.Translate(translateVector, Space.World);
            }
        }
        else
        {
            if (movePlayer)
            {
                transform.Translate(translateVector, Space.World);
                if (player != null) player.Move(translateVector + new Vector3(0.0f, 0.001f));
            }
        }

        prevPlayer = null;
    }
}