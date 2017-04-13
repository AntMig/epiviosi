using UnityEngine;
using System.Collections;

public class MovingPlatforms : MonoBehaviour {
	
	public float MoveSpeed = 1.0f;
	public float MoveDistance;
	public bool verticalMove;
	private bool MS;

	// Use this for initialization
	void Start () {
		MS = true;
		StartCoroutine (Move ());
	}

	// Update is called once per frame
	void Update () {

		if (!verticalMove) {
			if (MS)
				transform.Translate (Vector3.right * Time.deltaTime * MoveSpeed);
			else
				transform.Translate (Vector3.left * Time.deltaTime * MoveSpeed);
		} else {
			if (MS)
				transform.Translate (Vector3.up * Time.deltaTime * MoveSpeed);
			else
				transform.Translate (Vector3.down * Time.deltaTime * MoveSpeed);
		}
	}

	IEnumerator Move(){
		yield return new WaitForSeconds(MoveDistance/Mathf.Abs(MoveSpeed));
		MS = false;
		yield return new WaitForSeconds(MoveDistance/Mathf.Abs(MoveSpeed));
		MS = true;
		StartCoroutine (Move ());
	}  

	void OnCollisionEnter(Collision other){

		if(other.transform.tag == "Player"){
			other.transform.parent = transform;
		}
	}

	void OnCollisionExit(Collision other){
		if(other.transform.tag == "Player"){
			other.transform.parent = null;
		}
	} 
}
