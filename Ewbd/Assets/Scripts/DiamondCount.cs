using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DiamondCount : MonoBehaviour {
	public Text timerLabel;

	private float time;
	private LevelManager levelManager;

	void Start(){
		timerLabel.text = string.Empty;
		levelManager = (LevelManager)GameObject.Find ("LevelManager").GetComponent("LevelManager");
	}
	void Update() {
		if (levelManager.GameIsRunning ()) {
			var elapsedTime = DateTime.Now - levelManager.StartTime;

			string elapsedTimeString = String.Format("{0:00}:{1:00}.{2:00}",
				elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds / 10);
			
			timerLabel.text = elapsedTimeString;
		} 
	}
}