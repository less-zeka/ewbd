using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
	void OnGUI ()
	{
		if (GUILayout.Button ("Restart")) {
			var levelSelected = ((GameManager)GameObject.Find ("GameManager").GetComponent ("GameManager")).CurrentLevel;
			SceneManager.LoadScene (levelSelected);
		}

		if (GUILayout.Button ("Select Scene")) {
			SceneManager.LoadScene ("LevelSelection");
		}
	}
}
