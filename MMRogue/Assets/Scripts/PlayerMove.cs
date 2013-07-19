using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour 
{
    public float velocity = 10.0f;
    public float jumpSpeed = 2.0f;
    public float maxJumpHeight = 10.0f;
    public float minimumHangTime = 0.2f;
    private float currentJumpHeight = 0.0f;
    private bool canJump = false;
    private Transform myGrounder;

    void Awake()
    {
        myGrounder = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Grounder");
    }

	// Update is called once per frame
	void Update ()
	{
        if (Input.GetButton("Back"))
        {
            transform.position = new Vector3(-18.0f, -3.5f, 0.0f);
            rigidbody.velocity = Vector3.zero;
        }

	    if (Input.GetAxis("Horizontal") != 0.0f)
	    {
            if (!Physics.Linecast(transform.position,
                new Vector3(transform.position.x +
                    ((Input.GetAxis("Horizontal") > 0.0f) ?
                        collider.bounds.extents.x :
                        -1 * collider.bounds.extents.x),
                    transform.position.y,
                    0.0f), LayerMask.NameToLayer("Player")))
	            transform.Translate(Vector3.right*Input.GetAxis("Horizontal")*velocity*Time.deltaTime);
	    }

	    if (canJump)
	    {
	        Jump();
	    }

        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Ground" &&
            (collision.gameObject.name == "MovingPlatform" || 
            collision.collider.bounds.Contains(myGrounder.position)))
        {
            rigidbody.velocity = Vector3.zero;
            canJump = true;
            currentJumpHeight = 0.0f;
        }
        if (collision.collider.tag == "Ceiling" || collision.collider.tag == "Wall")
        {
            canJump = false;
            currentJumpHeight = 0.0f;
        }
    }

    void Jump()
    {
        float yMovementModifier = 0.0f;

        if (Physics.Linecast(transform.position,
                                 new Vector3(transform.position.x,
                                             transform.position.y + collider.bounds.extents.y,
                                             0.0f), LayerMask.NameToLayer("Units")))
        {
            rigidbody.velocity = new Vector3(
                rigidbody.velocity.x,
                0.0f,
                rigidbody.velocity.z);
        }

        if (Input.GetButton("Jump"))
        {
            if (currentJumpHeight < (maxJumpHeight - (jumpSpeed * Time.deltaTime)))
            {
                currentJumpHeight += jumpSpeed * Time.deltaTime;
                yMovementModifier = jumpSpeed * Time.deltaTime;
                rigidbody.useGravity = false;
            }
            else
            {
                currentJumpHeight = maxJumpHeight;
                yMovementModifier = 0.0f;
                canJump = false;
                StartCoroutine(ProcessHangTime());
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            canJump = false;
            StartCoroutine(ProcessHangTime());
        }

        transform.Translate(Vector3.up * yMovementModifier * (1 - Mathf.Pow(currentJumpHeight/maxJumpHeight, 2.0f)));
    }

    IEnumerator ProcessHangTime()
    {
        yield return new WaitForSeconds(
            minimumHangTime * (currentJumpHeight/maxJumpHeight));
        rigidbody.useGravity = true;
    }
}