using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public Transform myProjectile;
    public float timeBetweenShots = 0.5f;
    public float angleOfFire = 90.0f;
    public int numProjectiles = 3;
    public Transform BL;
    public Transform BR;
    public Transform CL;
    public Transform CR;
    public Transform CT;
    public Transform TL;
    public Transform TR;

    private Vector2 _fireDirection;
    private Dictionary<Vector2, Transform> _fireRotations;
    private float _angleBetweenProjectiles;

    void Start()
    {
        if (numProjectiles == 1) _angleBetweenProjectiles = 0.0f;
        else _angleBetweenProjectiles = angleOfFire / (numProjectiles - 1);
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

        _fireDirection.x = (GenericInput.GetAxis(GenericAxis.RX) > 0 ? 1 :
                            GenericInput.GetAxis(GenericAxis.RX) < 0 ? -1 : 0);
        _fireDirection.y = (GenericInput.GetAxis(GenericAxis.RY) > 0 ? 1 :
                            GenericInput.GetAxis(GenericAxis.RY) < 0 ? -1 : 0);
	}

    IEnumerator FireWeapon()
    {
        while (true)
        {
            if (_fireRotations.ContainsKey(_fireDirection))
            {
                float currentRotation = 0.0f;

                for (int i = 0; i < numProjectiles; ++i)
                {
                    currentRotation = (angleOfFire / 2) - _angleBetweenProjectiles * i;
                    //rotModifier.y += currentRotation;
                    Instantiate(myProjectile, _fireRotations[_fireDirection].position,
                                            Quaternion.Euler(new Vector3(0f, 0f, currentRotation) + _fireRotations[_fireDirection].rotation.eulerAngles));
                }

                yield return new WaitForSeconds(timeBetweenShots);
            }
            else
                yield return new WaitForEndOfFrame();
        }
    }
}