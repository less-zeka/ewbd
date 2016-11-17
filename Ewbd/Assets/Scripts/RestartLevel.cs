//using UnityEngine;
//using UnityEngine.SceneManagement;
//
//public class RestartLevel : MonoBehaviour
//{
//	void OnGUI ()
//	{
//        GUILayout.BeginArea (new Rect((Screen.width/2)-100, (Screen.height/2)-100 , 200, 200));
//
//		if (GUILayout.Button ("Restart", GUILayout.Width(200), GUILayout.Height(200))) {
//			var level = ((GameManager)GameObject.Find ("GameManager").GetComponent ("GameManager")).CurrentLevel;
//			SceneManager.LoadScene (level.LevelNr);
//		}
//
//		if (GUILayout.Button ("Select Scene")) {
//			SceneManager.LoadScene ("LevelSelection");
//		}
//
//        GUILayout.EndArea();
//	}
//}
