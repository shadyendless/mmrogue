using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour 
{
    public float velocity = 10.0f;
    public float jumpHeight = 10.0f;
    public bool onGround = false;

	// Update is called once per frame
	void Update () 
    {
        if ( Input.GetKey(KeyCode.LeftArrow) )
            transform.Translate(-Vector3.forward * velocity * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector3.forward * velocity * Time.deltaTime);
	    if (Input.GetKey(KeyCode.UpArrow) && onGround)
	    {
	        rigidbody.AddForce(Vector3.up*jumpHeight, ForceMode.VelocityChange);
	        onGround = false;
	    }

        // Touch controls.
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).position.x < (Screen.width*0.25))
                    transform.Translate(-Vector3.forward*velocity*Time.deltaTime);
                if (Input.GetTouch(i).position.x > (Screen.width*0.75))
                    transform.Translate(Vector3.forward*velocity*Time.deltaTime);
                if (onGround &&
                    Input.GetTouch(i).position.x < (Screen.width*0.75) &&
                    Input.GetTouch(i).position.x > (Screen.width*0.25))
                {
                    rigidbody.AddForce(Vector3.up*jumpHeight, ForceMode.VelocityChange);
                    onGround = false;
                }
            }
        }

        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            rigidbody.velocity = new Vector3(
                rigidbody.velocity.x,
                0.0f,
                rigidbody.velocity.z);
            onGround = true;
        }
    }
}