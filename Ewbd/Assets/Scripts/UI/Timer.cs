using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour {
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

			var timeLeft = levelManager.NrOfSecondsForSucceed - (int)elapsedTime.TotalSeconds;

			timerLabel.text = "Time: "+timeLeft.ToString ();
		} else {
			timerLabel.text = string.Empty;
		}
	}
}