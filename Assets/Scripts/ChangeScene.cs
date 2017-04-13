 using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class ChangeScene : MonoBehaviour {

	public string sceneName;

	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Player") {
			SceneManager.LoadScene(sceneName);
		}
	}

	void Update () {
		// réservé aux tests

		if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad0))
			SceneManager.LoadScene("Menu");

		if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad1))
			SceneManager.LoadScene("Start");

		if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad2))
			SceneManager.LoadScene("Infiltration");

		if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad3))
			SceneManager.LoadScene("mainLevel");

		if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad4))
			SceneManager.LoadScene("bossLevel");
	}

}
