using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour {

	private List<Level> levels;
	private int currentLevel = 0;

	void Awake(){
	    levels = new List<Level>
	    {
	        new Level
	        {
	            LevelNr = 1
	        },
	        new Level
	        {
	            LevelNr = 2
	        },
	        new Level
	        {
	            LevelNr = 3
	        },
	        new Level
	        {
	            LevelNr = 4
	        },
	        new Level
	        {
	            LevelNr = 5
	        },
	        new Level
	        {
	            LevelNr = 6
	        }
	    };
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
