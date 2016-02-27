using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.

	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.

	private int collidingWith = 0;

	private bool blockedRight, blockedLeft;

	void Awake() {
		anim = GetComponent<Animator>();
	}

	void Update() {
		if (grounded) {
			gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
		} else {
			gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
		}

		if (Input.GetButtonDown("Jump") && grounded) {
			jump = true;
		}
	}

	void FixedUpdate() {
		float h = Input.GetAxis("Horizontal");

		anim.SetFloat("Speed", Mathf.Abs(h));

		if (h * GetComponent<Rigidbody>().velocity.x < maxSpeed)
			GetComponent<Rigidbody>().AddForce(Vector2.right * h * moveForce);

		if (Mathf.Abs(GetComponent<Rigidbody>().velocity.x) > maxSpeed) {
			GetComponent<Rigidbody>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody>().velocity.x) * maxSpeed, GetComponent<Rigidbody>().velocity.y);
		}

		if (h > 0 && !facingRight || h < 0 && facingRight){
			Flip();
		}

		if (jump) {
			anim.SetTrigger("Jump");
			int i = Random.Range(0, jumpClips.Length);
			GetComponent<Rigidbody>().AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}

	void OnCollisionEnter(Collision other) {
		collidingWith++;
		grounded = true;
	}

	void OnCollisionExit(Collision other) {
		collidingWith--;
		if (collidingWith == 0) {
			grounded = false;
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
