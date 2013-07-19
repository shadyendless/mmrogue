using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour
{
    public LayerMask collisionMask;

    [HideInInspector] public bool grounded;
    public bool isOnWall;

    private BoxCollider _collider;
    private Vector3 _s;
    private Vector3 _c;

    private float _skin = 0.001f;

    private Ray _ray;
    private RaycastHit _hit;

    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _s = _collider.size;
        _c = _collider.center;
    }

    public void Move(Vector2 moveAmount)
    {
        float deltaX = moveAmount.x;
        float deltaY = moveAmount.y;
        float x = 0.0f;
        float y = 0.0f;
        float dir = 0.0f;
        Vector2 p = transform.position;

        // Up and down collisions.
        grounded = false;
        for (int i = 0; i < 3; ++i)
        {
            dir = Mathf.Sign(deltaY);
            x = (p.x + _c.x  - _s.x / 2.0f) + _s.x / 2.0f * i;
            y = p.y + _c.y + _s.y / 2.0f * dir;

            _ray = new Ray(new Vector2(x, y), new Vector2(0, dir));
            Debug.DrawRay(_ray.origin, _ray.direction);

            if (Physics.Raycast(_ray, out _hit, Mathf.Abs(deltaY) + _skin, collisionMask))
            {
                float dist = Vector3.Distance(_ray.origin, _hit.point);

                if (dist > _skin)
                    deltaY = dist*dir + _skin*dir*-1;
                else
                    deltaY = 0;

                grounded = true;
                isOnWall = false;
                break;
            }
        }

        isOnWall = false;
        // Left and right collisions
        for (int i = 0; i < 3; ++i)
        {
            dir = Mathf.Sign(deltaX);
            x = p.x + _c.x + _s.x / 2.0f * dir;
            y = (p.y + _c.y - _s.y / 2.0f) + _s.y / 2.0f * i;

            _ray = new Ray(new Vector2(x, y), new Vector2(dir, 0));
            Debug.DrawRay(_ray.origin, _ray.direction);

            if (Physics.Raycast(_ray, out _hit, Mathf.Abs(deltaX) + _skin, collisionMask))
            {
                float dist = Vector3.Distance(_ray.origin, _hit.point);

                if (dist > _skin)
                    deltaX = dist * dir + _skin * dir * -1;
                else
                    deltaX = 0;
                isOnWall = true;
                grounded = false;
                break;
            }
        }

        // Check the direction of the player.
        Vector3 playerDir = new Vector3(deltaX, deltaY);
        Vector3 o = new Vector3(p.x + _c.x + _s.x / 2.0f * Mathf.Sign(deltaX), 
                                p.y + _c.y + _s.y / 2.0f * Mathf.Sign(deltaY));
        _ray = new Ray(o, playerDir.normalized);
        if (!grounded && !isOnWall)
        {
            if (Physics.Raycast(_ray, out _hit, Mathf.Sqrt(deltaX*deltaX + deltaY*deltaY), collisionMask))
            {
                grounded = true;
                deltaY = 0.0f;
            }
        }

        Vector2 finalTranslate = new Vector2(deltaX, deltaY);

        transform.Translate(finalTranslate);
    }
}