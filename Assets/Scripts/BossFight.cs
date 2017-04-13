using UnityEngine;
using System.Collections;

public class BossFight : MonoBehaviour {

	public GameObject bossFight;

	// Use this for initialization
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			bossFight.SetActive (true);
		}
	}

	void Update () {
		GameObject boss = GameObject.Find ("Boss");
		if (boss.GetComponent<Bot> ().dead) {
			bossFight.SetActive (false);
			Destroy (gameObject);
		}
	}
}
