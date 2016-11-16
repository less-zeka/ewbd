using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DiamondCount : MonoBehaviour {
	public Text timerLabel;

	private LevelManager levelManager;

	void Start(){
		timerLabel.text = string.Empty;
		levelManager = (LevelManager)GameObject.Find ("LevelManager").GetComponent("LevelManager");
	}
	void Update() {
		if (levelManager.GameIsRunning ()) {
			timerLabel.text = levelManager.NrOfDiamondsFound.ToString();
		} 
	}
}