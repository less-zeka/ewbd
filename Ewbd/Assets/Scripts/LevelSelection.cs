using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	// Use this for initialization
	void Start () {
		Debug.Log("level: "+currentLevel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		foreach (var level in levels) {
			if (GUILayout.Button ("Level "+ level.LevelNr)) {
				currentLevel = level.LevelNr;
				Application.LoadLevel (level.LevelNr);
			}
		}
	}
}
