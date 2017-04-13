using UnityEngine;
using System.Collections;

public class FallingSpike : MonoBehaviour {

	public AudioClip hitSound;
	public AudioClip impactSound;

	void OnTriggerEnter(Collider other){

		if(other.transform.tag == "LightShot"){
			AudioSource.PlayClipAtPoint(hitSound, transform.position);
			GetComponent<Rigidbody> ().useGravity = true;
		}

		if(other.transform.name == "Boss"){
			AudioSource.PlayClipAtPoint(impactSound, transform.position);
			gameObject.SetActive (false);
		}

		if(other.transform.name == "GroundCollider"){
			GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			StartCoroutine (Respawn ());
		}
	}

	IEnumerator Respawn(){
		transform.position = new Vector3 (transform.position.x, -11.5f, 0);
		yield return new WaitForSeconds (2.0f);
		transform.position = new Vector3 (transform.position.x, -14f, 0);
	}
}
