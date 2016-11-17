using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
	private GameObject MyPlayer;
    public GameObject Player;
    public GameObject Filler;
    public GameObject Diamond;
    public GameObject Rock;
	public GameObject Wall;
	public GameObject Exit;
    public bool LevelFailed;
	public bool LevelDone;

    public int NrOfDiamondsFound;
    private List<Level> _levels;
	private GameManager _gameManager;
	public DateTime StartTime;

	private bool showCountdown;
	private string countdown = "";    

    // Use this for initialization
    void Start()
    {
		_gameManager = ((GameManager)GameObject.Find("GameManager").GetComponent("GameManager"));
		_levels = LevelCreator.GetLevels();
        StartCoroutine(GameLoop());
    }

	void OnGUI ()
	{
		if (showCountdown) {    
			// display countdown    
			GUI.color = Color.white;    
			GUI.Box (new Rect (Screen.width / 2, 75, 180, 140), countdown);
		} else if (LevelFailed) {
			if (_gameManager.NrOfLivesLeft > 0) {
				GUILayout.BeginArea (new Rect ((Screen.width / 2) - 100, (Screen.height / 2) - 100, 150, 100));

				var myButtonStyle = new GUIStyle (GUI.skin.button) { fontSize = 50 };
				if (GUILayout.Button ("Retry", myButtonStyle)) {
					//TOOD levelnr
					_gameManager.NrOfLivesLeft--;
					SceneManager.LoadScene (1);
				}

				GUILayout.EndArea ();
			} else {
				GUI.color = Color.white;    
				GUI.Box (new Rect (Screen.width / 2, 75, 180, 140), "you're dead :-(");
				GUILayout.BeginArea (new Rect ((Screen.width / 2) - 100, (Screen.height / 2) - 300, 150, 100));
				var myButtonStyle = new GUIStyle (GUI.skin.button) { fontSize = 40 };
				if (GUILayout.Button ("Restart", myButtonStyle)) {
					_gameManager.Initialize ();
					SceneManager.LoadScene (Constants.Scene_Level);
				}
				GUILayout.EndArea ();
			}
		} else if (LevelDone) {
			_gameManager.CurrentLevelNr++;
			LevelDone = false;
			SceneManager.LoadScene (Constants.Scene_Level);
		}
	}

    private IEnumerator GameLoop()
    {
        // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundStarting());

        // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundPlaying());

        // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
        yield return StartCoroutine(RoundEnding());

        if (LevelDone)
        {
            //SceneManager.LoadScene(Constants.Scene_LevelSelection);
        }

        else if (LevelFailed)
        {
            //SceneManager.LoadScene(Constants.Scene_RestartLevel);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
		showCountdown = true;    
        SetUpLevel();

        var delay = 1f;
		countdown = "3";
		yield return null;
		MyPlayer.GetComponent<PlayerController> ().enabled = false;
		yield return new WaitForSeconds (delay);

		countdown = "2";
        yield return new WaitForSeconds(delay);
		countdown = "1";
		yield return new WaitForSeconds(delay);
		showCountdown = false;
		MyPlayer.GetComponent<PlayerController> ().enabled = true;

    }

    private IEnumerator RoundPlaying()
    {
		StartTime = DateTime.Now;
        while (GameIsRunning())
        {
            // ... return on the next frame.
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        var delay = 3.0f;
		if (LevelDone) {
			Debug.Log ("yeahh you did it!");
		}
		else {
			Debug.Log ("NOOOO you failed!");
		}
        yield return new WaitForSeconds(delay);
    }

    public bool GameIsRunning()
    {
        return !LevelDone && !LevelFailed;
    }

    private void SetUpLevel()
    {
        //order is important! Fillers will be only created if nothing is there
        SetUpDiamonds();
        SetUpRocks();
		SetUpWalls ();
		SetUpExit ();
        SetUpFillers();
        SetUpPlayer();
    }
		
    private void SetUpFillers()
    {
        // rows and columns
		var nrFillerColumns = 38;
        var nrFillerRows = 22.0f;
        var absoluteMaxX = 19.0f;
        var absoluteMaxZ = 11.0f;
		var deltaX = 2.0f*absoluteMaxX/(nrFillerColumns - 1);
		var deltaZ = 2.0f*absoluteMaxZ/(nrFillerRows - 1);
        var scale = 1f;
		Debug.Log ("deltaX: " + deltaX);
		Debug.Log ("deltaZ: " + deltaZ);
		for (int x = 0; x < nrFillerColumns; x++)
        {
            for (int y = 0; y < nrFillerRows; y++)
            {
                var position = new Vector3(-absoluteMaxX + x*deltaX, 0.5f, -absoluteMaxZ + y*deltaZ);
                //put a filler if nothing at position
                if (!Physics.CheckBox(position, new Vector3(0.24f, 0.24f, 0.24f)))
               {
                    var myGameObject = Instantiate(Filler, position, Quaternion.identity);
					myGameObject.layer = Constants.Layer_NonCollision;
					var myGameObjectCollider = new GameObject ();
					myGameObjectCollider.AddComponent<BoxCollider> ();
					myGameObjectCollider.layer = Constants.Layer_Collision;
					myGameObjectCollider.transform.position = myGameObject.transform.position;

					myGameObjectCollider.transform.parent = myGameObject.transform;
                    myGameObject.transform.parent = GameObject.Find("Fillers").transform;
                    myGameObject.transform.localScale = new Vector3(scale, transform.localScale.y, scale);
                }
                
            }
        }
    }

    private void SetUpDiamonds()
    {
		foreach (var position in _levels[_gameManager.CurrentLevelNr-1].DiamondPositions)
        {
            var rotation = Quaternion.Euler(new Vector3(45, 45f, 45f));
            var myGameObject = Instantiate(Diamond, position, rotation);
            myGameObject.transform.parent = GameObject.Find("Diamonds").transform;
        }
    }

    private void SetUpPlayer()
    {
        MyPlayer = Instantiate(Player, new Vector3(-18.0f, 0.5f, 9.5f), Quaternion.identity);
		MyPlayer.transform.parent = GameObject.Find("Player").transform;
    }

    private void SetUpRocks()
    {
		foreach (var position in _levels[_gameManager.CurrentLevelNr-1].RockPositions)
        {
            var myGameObject = Instantiate(Rock, position, Quaternion.identity);
            myGameObject.transform.parent = GameObject.Find("Rocks").transform;
        }
    }

	private void SetUpWalls(){
		foreach (var position in _levels[_gameManager.CurrentLevelNr-1].WallPositions)
		{
			var pos = new Vector3 (position.x, position.y, position.z+0.5f);
			var myGameObject = Instantiate(Wall, pos, Quaternion.identity);
			myGameObject.tag = "Wall";
			myGameObject.transform.parent = GameObject.Find("Walls").transform;
		}
	}

	private void SetUpExit()
	{
		var myGameObject = Instantiate(Exit, _levels[_gameManager.CurrentLevelNr-1].ExitPosition, Quaternion.identity);
		myGameObject.transform.parent = GameObject.Find("Exit").transform;
	}

    public void DiamondFound()
    {
        NrOfDiamondsFound++;
    }

    public void HitRock()
    {
        LevelFailed = true;
    }

	public int NrOfSecondsForSucceed {
		get{
			return _levels[_gameManager.CurrentLevelNr-1].NrOfSecondsForSucceed;
		}
	}
}