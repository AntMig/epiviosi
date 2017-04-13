using UnityEngine;
using System.Collections;

public class MovingCharacter : MonoBehaviour
{
	private bool canJump = true;
	public float JumpForce;
	public float MoveSpeed = 5.0f;

	public Rigidbody rb;
	private float scaleX;

	public static bool goingLeft;

	private Animator anim;

	// Use this for initialization
	void Start ()
	{
		anim = gameObject.GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody> ();
		scaleX = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float h = Input.GetAxis("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (h));

		if (Input.GetKey ("space") && canJump && rb.velocity.y == 0.0f) {
			MoveSpeed = 2.5f;
			//canJump = false;
			rb.velocity = new Vector3 (0, JumpForce, 0);
			anim.SetBool ("idle", false);
			anim.SetBool("ground", false);
			anim.SetBool ("jump", true);
		} else if (Input.GetKey ("q") || Input.GetKey ("left")) {
			transform.Translate (Vector3.left * Time.deltaTime * MoveSpeed);
			goingLeft = true;
			transform.localScale = new Vector3 (-scaleX, transform.localScale.y, transform.localScale.z);
			anim.SetBool ("idle", false);
			anim.SetBool("ground", false);
			anim.SetBool("jump", false);
			anim.SetFloat("Speed", 1.0f) ;
		} else if (Input.GetKey ("d") || Input.GetKey ("right")) {
			transform.Translate (Vector3.right * Time.deltaTime * MoveSpeed);
			goingLeft = false;
			transform.localScale = new Vector3 (scaleX, transform.localScale.y, transform.localScale.z);
			anim.SetBool ("idle", false);
			anim.SetBool("jump", false);
			anim.SetBool("ground", false);
			anim.SetFloat ("Speed", 1.0f);
		} else {
			anim.SetBool("jump", false);
			anim.SetBool ("idle", true);
			anim.SetFloat("Speed", 0.0f);
		}

		if (rb.velocity.y < -1) {
			// animation de chute
			GetComponent<Renderer> ().material.color = Color.blue;
			anim.SetBool("fall", true);
			anim.SetBool("idle", false);
			anim.SetFloat ("Speed", Mathf.Abs (h));
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.tag == "GroundCollider") {
			MoveSpeed = 2.5f;
			canJump = true;
			anim.SetBool("idle", true);
			anim.SetBool("fall", false);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "GroundCollider") {
			MoveSpeed = 2.5f;
			canJump = false;
			anim.SetBool("jump", true);
		}
	}
}
