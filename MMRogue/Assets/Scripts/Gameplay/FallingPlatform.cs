using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public LayerMask collisionMask;
    public float timeToFall = 1.0f;
    public float timeToRespawn = 5.0f;

    public float gravity = 30.0f;

    private bool useGravity = false;

    private BoxCollider _collider;
    private Vector3 _s;
    private Vector3 _c;

    private Ray _ray;
    private RaycastHit _hit;

    private Vector3 startPos;

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
        for (int i = 0; i < 4; ++i)
        {
            float dir = 1;
            Vector2 p = transform.position;
            float x = (p.x + _c.x - _s.x / 2.0f) + _s.x / 3.0f * i;
            float y = p.y + _c.y + _s.y / 2.0f * dir;

            _ray = new Ray(new Vector2(x, y), new Vector2(0, dir));
            Debug.DrawRay(_ray.origin, _ray.direction);

            if (Physics.Raycast(_ray, out _hit, 1, collisionMask))
            {
                StartCoroutine(PlatformFall());
                break;
            }
        }

        if (useGravity) transform.Translate(Vector3.down * gravity * Time.deltaTime);
    }

    IEnumerator PlatformFall()
    {
        yield return new WaitForSeconds(timeToFall);
        useGravity = true;
        StartCoroutine(PlatformRespawn());
    }

    IEnumerator PlatformRespawn()
    {
        yield return new WaitForSeconds(timeToRespawn);
        useGravity = false;
        transform.position = startPos;
    }
}