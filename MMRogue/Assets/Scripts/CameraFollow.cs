using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform myTarget = null;

    void LateUpdate()
    {
        if (myTarget != null)
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                myTarget.position.z);
    }
}