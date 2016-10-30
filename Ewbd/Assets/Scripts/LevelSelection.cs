using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour {

	private List<Level> levels;
	private int currentLevel = 0;

	void Awake(){
		levels = new List<Level> ();
		levels.Add (new Level 
			{
				LevelNr = 1
			});
		levels.Add (new Level 
			{
				LevelNr = 2
			});
		levels.Add (new Level 
			{
				LevelNr = 3
			});
		levels.Add (new Level 
			{
				LevelNr = 4
			});
		levels.Add (new Level 
			{
				LevelNr = 5
			});
		levels.Add (new Level 
			{
				LevelNr = 6
			});
	}

	void OnGUI () {
		foreach (var level in levels) {
			if (GUILayout.Button ("Level "+ level.LevelNr)) {
				currentLevel = level.LevelNr;
				SceneManager.LoadScene (level.LevelNr);
			}
		}
	}
}
