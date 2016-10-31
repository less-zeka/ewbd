using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
	void OnGUI ()
	{
		if (GUILayout.Button ("Restart")) {
			var level = ((GameManager)GameObject.Find ("GameManager").GetComponent ("GameManager")).CurrentLevel;
			SceneManager.LoadScene (level.LevelNr);
		}

		if (GUILayout.Button ("Select Scene")) {
			SceneManager.LoadScene ("LevelSelection");
		}
	}
}
