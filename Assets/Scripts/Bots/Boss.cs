using UnityEngine;
using System.Collections;

public sealed class Boss : Bot {
	
	public GameObject projectile;

	private int lightSpeed = 12;
	private bool canShoot = true;

	public override void Attack() {
		if (canShoot) {
			StartCoroutine (Shoot ());
			canShoot = false;
		}
	}

	IEnumerator Shoot(){
		GameObject LS = (GameObject)GameObject.Instantiate (projectile, transform.position, transform.rotation);
		AudioSource.PlayClipAtPoint(attackSound, transform.position);

		if (MS)
			LS.GetComponent<Rigidbody> ().velocity = new Vector3 (lightSpeed, 0, 0);
		else
			LS.GetComponent<Rigidbody> ().velocity = new Vector3 (-lightSpeed, 0, 0);
		yield return new WaitForSeconds (1.5f);
		canShoot = true;
	}

	void OnTriggerEnter(Collider other){

		if(other.transform.tag == "Spike"){
			Damage++;
			MoveSpeed -= 0.75f;

			if (Damage == 2) {
				transform.tag = "Enemy";
			}
		}
	}
}
