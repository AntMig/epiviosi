using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class Dialogues : MonoBehaviour {

	public GameObject HUD;
	public GameObject coolEa;
	public GameObject angryEa;
	public GameObject sadEa;
	public GameObject HalID;
	public GameObject EaID;
	public GameObject AliID;

	public string sceneName;

	[SerializeField]
	private Text txtRef;

	private string str;
	private int nbDial;
	private bool skipText;
	private bool isDial;
	private bool endDial;
	private GameObject Player;

	void Start () {
		Player = GameObject.Find ("Player");
	}

	void Update(){
		if (Input.GetKeyDown ("space") && !endDial) {
			skipText = true;
		}

		if (nbDial == 1 && !isDial) {
			SetDial ("ali", 1, "sad");
		}
		if (nbDial == 2 && !isDial) {
			SetDial ("ea", 1, "angry");
		}
		if (nbDial == 3 && !isDial) {
			SetDial ("ali", 2, "angry");
		}
		if (nbDial == 4 && !isDial) {
			SetDial ("hal", 1, "angry");
		}
		if (nbDial == 5 && !isDial) {
			SetDial ("ali", 3, "angry");
		}
		if (nbDial == 6 && !isDial) {
			SetDial ("hal", 2, "angry");
		}
		if (nbDial == 7) {
			Player.GetComponent<MovingCharacter> ().enabled = true;
			Player.GetComponent<ActionsCharacter> ().enabled = true;
			if (sceneName != "") {
				SceneManager.LoadScene(sceneName);
			} else {
				Destroy (gameObject);
			}
		}

		if (endDial) {
			if (Input.GetKeyDown ("space")) {
				endDial = false;
				isDial = false;
				nbDial++;
				HUD.SetActive (false);
			}
		}
	}

	void SetDial(string speaker, int idDial, string emotion){

		isDial = true;
		skipText = false;
		
		HUD.SetActive(true);

		if (emotion == "anger") {
			angryEa.SetActive (true);
			sadEa.SetActive (false);
			coolEa.SetActive (false);
		}
		if (emotion == "joy") {
			coolEa.SetActive (true);
			angryEa.SetActive (false);
			sadEa.SetActive (false);
		}
		if (emotion == "sad") {
			sadEa.SetActive (true);
			angryEa.SetActive(false);
			coolEa.SetActive (false);
		}

		if (speaker == "ea")
			EaID.SetActive (true);
		else
			EaID.SetActive (false);

		if (speaker == "hal") {
			AliID.SetActive (false);
			HalID.SetActive (true);
		} else if (speaker == "ali") {
			HalID.SetActive (false);
			AliID.SetActive (true);
		}

		StartCoroutine( AnimateText(CSVLoader.instance.GetString("d3_" + speaker + idDial)) );
	}

	IEnumerator AnimateText(string strComplete){
		int i = 0;
		str = "";
		while( i < strComplete.Length ){
			str += strComplete[i++];
			yield return new WaitForSeconds(0.05F);
			txtRef.text = str;
			if (skipText) {
				txtRef.text = strComplete;
				skipText = false;
				i = strComplete.Length;
			}
		}
		if (i == strComplete.Length && txtRef.text == strComplete){
			endDial = true;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Player") {
			Player.GetComponent<MovingCharacter> ().enabled = false;
			Player.GetComponent<ActionsCharacter> ().enabled = false;
			nbDial = 1;
		}
	}

}
