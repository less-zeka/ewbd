using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour
{
	public GameObject Player;
	public GameObject Filler;
	public GameObject Diamond;
	public GameObject Rock;
	public bool LevelDone = false;
	public bool LevelFailed = false;

	private int nrOfDiamondsFound = 0;
	private int nrOfDiamonds = 3;
	private Level level;

	public int CurrentLevel;

	private static GameManager instance = null;

//	public static GameManager Instance { get { return _instance; } }

	void Awake ()
	{
//		if (_instance != null && _instance != this) {
//			Destroy (this.gameObject);
//		} else {
//			_instance = this;
			CurrentLevel = 1;
//			DontDestroyOnLoad (this.gameObject);
//		}
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}else if(instance != this){
			Destroy(this.gameObject);
			return;
		}

		//DontDestroyOnLoad(transform.root.gameObject);
	}

	// Use this for initialization
	void Start ()
	{
		level = GetLevelConfiguration ();
		StartCoroutine (GameLoop ());
	}

	private void SetUpFillers ()
	{
		// rows and columns
		var nrFillerRows = 15.0f;
		var absoluteMaxX = 9.0f;
		var absoluteMaxZ = 9.0f;
		var deltaX = 2.0f * absoluteMaxX / (nrFillerRows - 1);
		var deltaZ = 2.0f * absoluteMaxZ / (nrFillerRows - 1);
		var scale = 0.96f;

		for (int x = 0; x < nrFillerRows; x++) {
			for (int y = 0; y < nrFillerRows; y++) {
				var gameObject = Instantiate (Filler, new Vector3 (-absoluteMaxX + x * deltaX, 0.25f, -absoluteMaxZ + y * deltaZ), Quaternion.identity);
				gameObject.transform.parent = GameObject.Find ("Fillers").transform;
				gameObject.transform.localScale = new Vector3 (scale, transform.localScale.y, scale);
			}
		}
	}

	private void SetUpDiamonds ()
	{
		foreach (var position in level.DiamondPositions) {
			var rotation = Quaternion.Euler (new Vector3 (45, 45f, 45f));
			var gameObject = Instantiate (Diamond, position, rotation);
			gameObject.transform.parent = GameObject.Find ("Diamonds").transform;
		}
	}

	private void SetUpPlayer ()
	{ 
		var gameObject = Instantiate (Player, new Vector3 (-3.96f, 0.5f, 0f), Quaternion.identity);
		gameObject.transform.parent = GameObject.Find ("Player").transform;
	}

	private void SetUpRocks ()
	{
		foreach (var position in level.RockPositions) {
			var gameObject = Instantiate (Rock, position, Quaternion.identity);
			gameObject.transform.parent = GameObject.Find ("Rocks").transform;

		}
	}

	void OnGUI ()
	{
		//makes a GUI button at coordinates 10, 100, and a size of 200x40						
		if (GUI.Button (new Rect (10, 400, 100, 50), "Restart")) {
			//Loads a level
			SceneManager.LoadScene ("Level" + CurrentLevel);
		}
		GUI.Label (new Rect (10, 10, 100, 20), string.Format (@"Gems: {0}\{1}", nrOfDiamondsFound, nrOfDiamonds));
	}

	private Level GetLevelConfiguration ()
	{
		var url = "http://ewbdwebapi.azurewebsites.net/api/level?levelNr=" + CurrentLevel;
		HttpWebRequest req = WebRequest.Create (url)
			as HttpWebRequest;
		string result = null;
		using (HttpWebResponse resp = req.GetResponse ()
			as HttpWebResponse) {
			StreamReader reader = new StreamReader (resp.GetResponseStream ());
			result = reader.ReadToEnd ();
		}
		var level = JsonUtility.FromJson<Level> (result);
		return level;
	}

	private IEnumerator GameLoop ()
	{
		// Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
		yield return StartCoroutine (RoundStarting ());

		// Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
		yield return StartCoroutine (RoundPlaying ());

		// Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
		yield return StartCoroutine (RoundEnding ());

		// This code is not run until 'RoundEnding' has finished.  At which point, check if a game winner has been found.
		if (LevelDone || LevelFailed) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		// If there isn't a winner yet, restart this coroutine so the loop continues.
		// Note that this coroutine doesn't yield.  This means that the current version of the GameLoop will end.
		else {
			StartCoroutine (GameLoop ());
		}
	}

	private IEnumerator RoundStarting ()
	{
		// As soon as the round starts reset the tanks and make sure they can't move.
		SetUpLevel ();

		// Snap the camera's zoom and position to something appropriate for the reset tanks.
		//m_CameraControl.SetStartPositionAndSize ();

		// Increment the round number and display text showing the players what round it is.
		//m_RoundNumber++;
		//m_MessageText.text = "ROUND " + m_RoundNumber;

		// Wait for the specified length of time until yielding control back to the game loop.
		var delay = 3.0f;
		yield return new WaitForSeconds (delay);
	}

	private IEnumerator RoundPlaying ()
	{
		// Clear the text from the screen.
		//m_MessageText.text = string.Empty;

		// While not gameOver...
		while (GameIsRunning ()) {
			// ... return on the next frame.
			yield return null;
		}
	}

	private IEnumerator RoundEnding ()
	{
		// Stop tanks from moving.
		//destroy player?

		// Clear the winner from the previous round.
		//m_RoundWinner = null;

		// See if there is a winner now the round is over.
		//m_RoundWinner = GetRoundWinner ();

		// If there is a winner, increment their score.
		//if (m_RoundWinner != null)
		//	m_RoundWinner.m_Wins++;

		if (LevelDone) {
			CurrentLevel++;
		}
		// Now the winner's score has been incremented, see if someone has one the game.
//		m_GameWinner = GetGameWinner ();

		// Get a message based on the scores and whether or not there is a game winner and display it.
//		string message = EndMessage ();
//		m_MessageText.text = message;

		// Wait for the specified length of time until yielding control back to the game loop.
		//yield return m_EndWait;
		var delay = 3.0f;
		yield return new WaitForSeconds (delay);
	}

	private void SetUpLevel ()
	{
		//player, diamonds, rocks, fillers
		SetUpFillers ();
		SetUpRocks ();
		SetUpDiamonds ();
		SetUpPlayer ();
	}

	private Boolean GameIsRunning ()
	{
		return nrOfDiamondsFound < nrOfDiamonds && !LevelDone && !LevelFailed;
	}

	public void DiamondFound ()
	{
		nrOfDiamondsFound++;
	}
}
