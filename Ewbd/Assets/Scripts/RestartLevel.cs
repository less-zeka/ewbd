using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour {
	void OnGUI () {
			if (GUILayout.Button ("Restart")) {
			var a = GameObject.Find ("GameManager");
			var b = GameObject.Find ("GameManager").GetComponent ("GameManager");
			var c = (GameManager)GameObject.Find ("GameManager").GetComponent ("GameManager");
			var x = ((GameManager)GameObject.Find ("GameManager").GetComponent ("GameManager")).CurrentLevel;


			var levelSelected = ((GameManager)GameObject.Find ("GameManager").GetComponent ("GameManager")).CurrentLevel;

			SceneManager.LoadScene(levelSelected);
			}
		if (GUILayout.Button ("Select Scene")) {
			SceneManager.LoadScene ("LevelSelection");
		}
	}
}
