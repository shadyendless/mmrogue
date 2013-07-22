using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform myTarget = null;
    public Transform parallax = null;
    private Transform _leftBoundary = null;
    private Transform _rightBoundary = null;

    void Awake()
    {
        _leftBoundary  = GameObject.Find("CameraBoundaryLeft").transform;
        _rightBoundary = GameObject.Find("CameraBoundaryRight").transform;

        if (_leftBoundary == null || _rightBoundary == null)
            Debug.LogError("CameraBoundaryLeft and CameraBoundaryRight are required!");
    }

    void LateUpdate()
    {
        int left = -1;
        if (myTarget == null)
            myTarget = GameObject.FindGameObjectWithTag("Player").transform;

        if (myTarget != null)
        {
            if (myTarget.position.x == transform.position.x) return;
            if (myTarget.position.x < transform.position.x) left = -1;
            else left = 1;

            if (myTarget.position.x < _leftBoundary.position.x)
                transform.position = new Vector3(
                    _leftBoundary.position.x,
                    transform.position.y,
                    transform.position.z);
            else if (myTarget.position.x > _rightBoundary.position.x)
                transform.position = new Vector3(
                    _rightBoundary.position.x,
                    transform.position.y,
                    transform.position.z);
            else
            {
                transform.position = new Vector3(
                    myTarget.position.x,
                    transform.position.y,
                    transform.position.z);

                if (parallax != null) parallax.transform.Translate(Vector3.right * left * Time.deltaTime);
            }
        }
    }
}