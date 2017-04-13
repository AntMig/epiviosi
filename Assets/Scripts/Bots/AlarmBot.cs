using UnityEngine;
using System.Collections;

public sealed class AlarmBot : Bot {

	public override void Attack(){
		// Play sound
		// Play anim
		StartCoroutine (Alarm ());
	}

	IEnumerator Alarm(){
		yield return new WaitForSeconds (3.0f);
		// Play sound alarm
		if (!dead) {
			//anim alarm sounded
			print ("alarm ringed");
		}
	}
}
