using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DiamondCount : MonoBehaviour {
	public Text nrDiamonds;
	public Text nrLivesLabel;
	public Text levelNrLabel;

	private LevelManager levelManager;
	private GameManager gameManager;

	void Start(){
		nrDiamonds.text = string.Empty;
		nrLivesLabel.text = string.Empty;
		levelNrLabel.text = string.Empty;

		levelManager = (LevelManager)GameObject.Find ("LevelManager").GetComponent("LevelManager");
		gameManager = (GameManager)GameObject.Find ("GameManager").GetComponent("GameManager");
	}
	void Update() {
		if (levelManager != null && levelManager.GameIsRunning ()) {
			nrDiamonds.text = "Diamonds: "+levelManager.NrOfDiamondsFound.ToString ();
			nrLivesLabel.text = "Lives: "+gameManager.NrOfLivesLeft.ToString();
			levelNrLabel.text = "Level: "+gameManager.CurrentLevelNr.ToString ();
		} else {
			nrDiamonds.text = string.Empty;
			nrLivesLabel.text = string.Empty;
			levelNrLabel.text = string.Empty;
		}
	}
}