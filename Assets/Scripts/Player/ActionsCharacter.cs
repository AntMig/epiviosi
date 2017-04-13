using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class ActionsCharacter : MonoBehaviour
{

	public GameObject lightPrefab;
	private GameObject Cam;
	public AudioClip shotSound;
	public AudioClip deathSound;

	private int lightSpeed = 10;
	private bool cooldown;

	private Animator anim;

	void Start ()
	{
		Cam = GameObject.Find ("Camera");
		anim = gameObject.GetComponentInChildren<Animator>();
	}

	void Update ()
	{
		if (Input.GetKeyDown ("e") && !cooldown) {
			ShootLight ();
			cooldown = true;
			StartCoroutine (StartCooldown ());
		}
	}

	IEnumerator StartCooldown ()
	{
		yield return new WaitForSeconds (1.0f);
		cooldown = false;
	}

	void ShootLight ()
	{
		GameObject LS = (GameObject)GameObject.Instantiate (lightPrefab, transform.position, transform.rotation); //On crée un gameobject qui venant du modele de lavaPrefab
		AudioSource.PlayClipAtPoint(shotSound, transform.position);

		if (!MovingCharacter.goingLeft)
			LS.GetComponent<Rigidbody> ().velocity = new Vector3 (lightSpeed, 0, 0);
		else
			LS.GetComponent<Rigidbody> ().velocity = new Vector3 (-lightSpeed, 0, 0);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Spike") {
			StartCoroutine (Death ());
		}

		if (other.tag == "EnemyShot") {
			StartCoroutine (Death ());
		}
	}

	IEnumerator Death ()
	{
		anim.SetTrigger("death");
		GetComponent<Rigidbody> ().isKinematic = true;
		GetComponent<Collider>().enabled = false;
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Cam.GetComponent<SmoothCamera2D> ().enabled = false;
		GetComponent<MovingCharacter> ().enabled = false;
		gameObject.GetComponent<MovingCharacter> ().enabled = false;
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex) ;
	}
}
