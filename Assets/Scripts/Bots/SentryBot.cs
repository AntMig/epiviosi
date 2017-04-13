using UnityEngine;
using System.Collections;

public sealed class SentryBot : Bot {

	public override void Attack() {
		// Play anim
		//transform.LookAt (Player);
		if (Vector3.Distance (transform.position, Player.transform.position) >= 1) {
			//transform.position += look * MoveSpeed * Time.deltaTime;
		} else {
			//Player.GetComponent<ActionsCharacter> ().StartCoroutine (Death ());
		}
	}
}
