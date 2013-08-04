using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour
{
    public LayerMask collisionMask;

    public bool grounded;
    [HideInInspector]
    public bool stopMovement;
    public bool onMovingPlatform;

    private BoxCollider _collider;
    private Vector3 _s;
    private Vector3 _c;

    private float _skin = 0.005f;

    private Ray _ray;
    private RaycastHit _hit;

    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _s = _collider.size;
        _c = _collider.center;
    }

   public Vector2 Move(Vector2 moveAmount)
    {
        float deltaX = moveAmount.x;
        float deltaY = moveAmount.y;
        float x = 0.0f;
        float y = 0.0f;
        float dir = 0.0f;
        Vector2 p = transform.position;
        int i = 0;

      
        // Up and down collisions.
        for (i = 0; i < 4; ++i)
        {
            dir = Mathf.Sign(deltaY);
            x = (p.x + _c.x - _s.x / 2.0f) + _s.x / 3.0f * i;
            y = p.y + _c.y + _s.y / 2.0f * dir;

            _ray = new Ray(new Vector2(x, y), new Vector2(0, dir));
            Debug.DrawRay(_ray.origin, _ray.direction);
            if (Physics.Raycast(_ray, out _hit, Mathf.Abs(deltaY) + _skin, collisionMask))
            {
                float dist = Vector3.Distance(_ray.origin, _hit.point);

                if (dist > _skin)
                    deltaY = dist * dir + _skin * dir * -1;
                else
                {
                    deltaY = 0.0f;
                    Debug.Log("Collided Vertical 1 " + deltaY);
                }

                if (dir < 0.0f)
                {
                    if (_hit.collider.gameObject.tag == "MovingPlatform")
                        onMovingPlatform = true;
                    else
                        onMovingPlatform = false;
                }

                if (dir < 0)
                {
                    grounded = true;
                    Debug.Log("grounded " + deltaY);
                    break;
                }
            }
        }

        grounded = i < 4;

        if (grounded == false)
        {
            Debug.Log("not grounded " + deltaY);
            //Debug.Break();
        }

        stopMovement = false;
        // Left and right collisions
        for (i = 0; i < 4; ++i)
        {
            dir = Mathf.Sign(deltaX);
            x = p.x + _c.x + _s.x / 2.0f * dir;
            y = (p.y + _c.y - _s.y / 2.0f) + _s.y / 3.0f * i;

            _ray = new Ray(new Vector2(x, y), new Vector2(dir, 0));
            Debug.DrawRay(_ray.origin, _ray.direction);

            if (Physics.Raycast(_ray, out _hit, Mathf.Abs(deltaX) + _skin, collisionMask))
            {
                float dist = Vector3.Distance(_ray.origin, _hit.point);

                if (dist > _skin)
                    deltaX = dist * dir + _skin * dir * -1;
                else
                {
                    deltaX = 0;
                    Debug.Log("Collided Horizontal 1 " + deltaX);
                }

                stopMovement = true;
                break;
            }
        }

        // Check the direction of the player.
        Vector3 playerDir = new Vector3(deltaX, deltaY);
        Vector3 o = new Vector3(p.x + _c.x + _s.x / 2.0f * Mathf.Sign(deltaX),
                                p.y + _c.y + _s.y / 2.0f * Mathf.Sign(deltaY));
        _ray = new Ray(o, playerDir.normalized);
        if (!stopMovement)
        {
            if (Physics.Raycast(_ray, out _hit, Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY), collisionMask))
            {
                grounded = true;
                deltaY = 0.0f;
                Debug.Log("Collided Vertical 2 " + deltaY);
            }
        }

        Vector2 finalTranslate = new Vector2(deltaX, deltaY);
        //onMovingPlatform = false;
        transform.Translate(finalTranslate);

        return finalTranslate;
    }
}