using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform myTarget = null;

    void LateUpdate()
    {
        if (myTarget != null)
            transform.position = new Vector3(
                myTarget.position.x,
                transform.position.y,
                transform.position.z);
    }
}