using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class NavigationScript : MonoBehaviour {

	public void QuitGame(){
		Application.Quit();
	}

	public void GoToScene(string sceneName){
		SceneManager.LoadScene(sceneName);
	}

	void Update () {

	}

}
