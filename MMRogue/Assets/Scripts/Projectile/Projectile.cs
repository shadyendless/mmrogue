using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float velocity = 10.0f;
    public LayerMask collisionMask;

    private float _timeBorn = 0.0f;
    private Ray _ray;
    private RaycastHit _hit;

    void Awake()
    {
        _timeBorn = Time.time;
    }

	void Update() 
	{
        transform.Translate(Vector2.right * velocity * Time.deltaTime, Space.Self);

        _ray = new Ray(transform.position, transform.right);
        Debug.DrawRay(_ray.origin, _ray.direction);

        if (Physics.Raycast(_ray, out _hit, velocity * Time.deltaTime, collisionMask))
        {
            DestroyImmediate(gameObject);
        }

        if (Time.time - _timeBorn >= 1)
            DestroyImmediate(gameObject);
	}
}