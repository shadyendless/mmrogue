using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public Transform myProjectile;
    public float timeBetweenShots = 0.5f;
    public Transform BL;
    public Transform BR;
    public Transform CL;
    public Transform CR;
    public Transform CT;
    public Transform TL;
    public Transform TR;

    private Vector2 _fireDirection;
    private Dictionary<Vector2, Transform> _fireRotations;

    void Start()
    {
        _fireRotations = new Dictionary<Vector2, Transform>();
        _fireRotations.Add(new Vector2( 1, -1), BR);
        _fireRotations.Add(new Vector2( 1,  0), CR);
        _fireRotations.Add(new Vector2( 1,  1), TR);
        _fireRotations.Add(new Vector2( 0,  1), CT);
        _fireRotations.Add(new Vector2(-1,  1), TL);
        _fireRotations.Add(new Vector2(-1,  0), CL);
        _fireRotations.Add(new Vector2(-1, -1), BL);

        StartCoroutine(FireWeapon());
    }
	// Update is called once per frame
	void Update()
	{
	    _fireDirection = Vector2.zero;

        _fireDirection.x = (Input.GetAxis("AimHorizontal") > 0 ? 1 :
                            Input.GetAxis("AimHorizontal") < 0 ? -1 : 0);
        _fireDirection.y = (Input.GetAxis("AimVertical") > 0 ? 1 :
                            Input.GetAxis("AimVertical") < 0 ? -1 : 0);
	}

    IEnumerator FireWeapon()
    {
        while (true)
        {
            if (_fireRotations.ContainsKey(_fireDirection))
            {
                Instantiate(myProjectile, _fireRotations[_fireDirection].position, 
                                          _fireRotations[_fireDirection].rotation);
                yield return new WaitForSeconds(timeBetweenShots);
            }
            else
                yield return new WaitForEndOfFrame();
        }
    }
}