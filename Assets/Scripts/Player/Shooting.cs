using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
	float timeLeft = 32.0f;

	// Update is called once per frame
	void Update ()
	{
		timeLeft--;
		if (timeLeft < 0f) {
			Destroy (gameObject);
		}
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Enemy") {
			Destroy (gameObject);
			other.transform.position += new Vector3(0, 0.5f, 1.5f);
			other.GetComponent<Bot> ().dead = true;
		}

		if (other.tag == "Boss") {
			Destroy (gameObject);
		}
	}


}