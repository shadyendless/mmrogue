using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public bool moveX = false;
    public bool moveY = false;
    public Vector3 endPos;

    public float velocity = 5.0f;

    private bool goingRight = true;
    private bool goingUp = true;

    private Vector3 startPos;
    private GameObject player = null;

    void Awake()
    {
        startPos = transform.position;
    }

	// Update is called once per frame
	void Update ()
	{
	    Vector3 translateVector = Vector3.zero;
	    if (moveX)
	    {
	        if (goingRight)
	        {
	            if (transform.position.x < endPos.x)
	                translateVector = Vector3.right*velocity*Time.deltaTime;
	            else
	                goingRight = false;
	        }
	        else
	        {
	            if (transform.position.x > startPos.x)
	                translateVector = -Vector3.right*velocity*Time.deltaTime;
	            else
	                goingRight = true;
	        }
	    }

	    if (moveY)
	    {
	        if (goingUp)
	        {
	            if (transform.position.y < endPos.y)
	                translateVector = Vector3.up*velocity*Time.deltaTime;
	            else
	                goingUp = false;
	        }
	        else
	        {
	            if (transform.position.y > startPos.y)
	                translateVector = -Vector3.up*velocity*Time.deltaTime;
	            else
	                goingUp = true;
	        }
	    }

	    transform.Translate(translateVector);
        if (player != null) player.transform.Translate(translateVector);
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            player = collision.gameObject;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            player = null;
    }
}