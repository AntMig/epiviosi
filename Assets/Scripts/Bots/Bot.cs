using UnityEngine;
using System.Collections;

public abstract class Bot : MonoBehaviour {
	
	public float MoveSpeed = 1.0f;
	public float MoveDistance;
	protected bool MS;
	private float scaleX;

	public int Damage;
	public Sprite[] damageBot;
	public bool dead;
	public AudioClip deathSound;

	private Vector3 StartPosition;
	protected Transform Player;
	private bool PlayerSpotted;
	public AudioClip PSSound;
	private bool attackPlayer;
	public AudioClip attackSound;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player").transform;
		MS = true;
		StartPosition = transform.position;
		scaleX = transform.localScale.x;
		GetComponentInChildren<SpriteRenderer> ().sprite = damageBot [Damage];
	}

	// Update is called once per frame
	void Update () {
		GetComponentInChildren<SpriteRenderer> ().sprite = damageBot [Damage];

		if (transform.position.x >= StartPosition.x + MoveDistance) {
			MS = false;
		} else if (transform.position.x <= StartPosition.x) {
			MS = true;
		}

		if (!attackPlayer && PlayerSpotted && !dead) {
			StartCoroutine (giveUp());
		}

		if (!PlayerSpotted && !dead) {
			Patrol ();
		}

		if (attackPlayer && PlayerSpotted && !dead) {
			//anim attack not created
			Attack ();
		}

		if (dead) {
			AudioSource.PlayClipAtPoint(deathSound, transform.position);
			Damage++;
		}
	}

	void Patrol() {
		if (MS) {
			transform.Translate (Vector3.right * Time.deltaTime * MoveSpeed);
			transform.localScale = new Vector3 (scaleX, transform.localScale.y, transform.localScale.z);
		} else {
			transform.Translate (Vector3.left * Time.deltaTime * MoveSpeed);
			transform.localScale = new Vector3 (-scaleX, transform.localScale.y, transform.localScale.z);
		}
	}

	IEnumerator giveUp(){
		yield return new WaitForSeconds (2.0f);
		if (!attackPlayer) {
			PlayerSpotted = false;
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			if (!PlayerSpotted)
				AudioSource.PlayClipAtPoint(PSSound, transform.position);
			PlayerSpotted = true;
			attackPlayer = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			attackPlayer = false;
		}
	}

	public virtual void Attack() {}

}
