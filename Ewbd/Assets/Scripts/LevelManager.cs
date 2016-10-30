using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

public class LevelManager : MonoBehaviour
{
	private bool roundFinished = false;
	public int CurrentLevel = 0;

	// Use this for initialization
	void Start ()
	{
		((Level)GameObject.Find("Level").GetComponent("Level")).LevelNr = 3;
		StartCoroutine (GameLoop ());
	}

	void OnGUI ()
	{
		//makes a GUI button at coordinates 10, 100, and a size of 200x40						
		if (GUI.Button (new Rect (10, 400, 100, 50), "LevelSelection")) {
			//Loads a level
			SceneManager.LoadScene ("LevelSelection");
		}
	}

	private IEnumerator GameLoop ()
	{
		// Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
		yield return StartCoroutine (RoundStarting ());

		// Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
		yield return StartCoroutine (RoundPlaying ());

		// Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
		yield return StartCoroutine (RoundEnding ());

		if (roundFinished) {
			SceneManager.LoadScene ("RestartLevel");
		}
		StartCoroutine (GameLoop ());
	}

	private IEnumerator RoundStarting ()
	{
		var delay = 1.0f;
		yield return new WaitForSeconds (delay);
	}

	private IEnumerator RoundPlaying ()
	{
		var delay = 1.0f;
		yield return new WaitForSeconds (delay);
	}

	private IEnumerator RoundEnding ()
	{
		var delay = 1.0f;
		roundFinished = true;
		yield return new WaitForSeconds (delay);
	}
}
