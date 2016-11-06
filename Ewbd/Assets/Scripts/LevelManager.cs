using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Filler;
    public GameObject Diamond;
    public GameObject Rock;
	public GameObject Wall;
    public bool LevelFailed;

    private int _nrOfDiamondsFound;
    private Level _level;

    // Use this for initialization
    void Start()
    {
        _level = ((GameManager)GameObject.Find("GameManager").GetComponent("GameManager")).CurrentLevel;
        StartCoroutine(GameLoop());
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
            SceneManager.LoadScene(Constants.Scene_LevelSelection);
        }

        else if (LevelFailed)
        {
            SceneManager.LoadScene(Constants.Scene_RestartLevel);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
        SetUpLevel();
        var delay = 1.0f;
        yield return new WaitForSeconds(delay);
    }

    private IEnumerator RoundPlaying()
    {
        while (GameIsRunning())
        {
            // ... return on the next frame.
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        var delay = 3.0f;
        yield return new WaitForSeconds(delay);
    }

    private bool GameIsRunning()
    {
        return !LevelDone && !LevelFailed;
    }

    private void SetUpLevel()
    {
        //order is important! Fillers will be only created if nothing is there
        SetUpDiamonds();
        SetUpRocks();
		SetUpWalls ();
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
                    myGameObject.transform.parent = GameObject.Find("Fillers").transform;
                    myGameObject.transform.localScale = new Vector3(scale, transform.localScale.y, scale);
                }
                
            }
        }
    }

    private void SetUpDiamonds()
    {
        foreach (var position in _level.DiamondPositions)
        {
            var rotation = Quaternion.Euler(new Vector3(45, 45f, 45f));
            var myGameObject = Instantiate(Diamond, position, rotation);
            myGameObject.transform.parent = GameObject.Find("Diamonds").transform;
        }
    }

    private void SetUpPlayer()
    {
        var myGameObject = Instantiate(Player, new Vector3(-18.5f, 0.5f, 9.5f), Quaternion.identity);
        myGameObject.transform.parent = GameObject.Find("Player").transform;
    }

    private void SetUpRocks()
    {
        foreach (var position in _level.RockPositions)
        {
            var myGameObject = Instantiate(Rock, position, Quaternion.identity);
            myGameObject.transform.parent = GameObject.Find("Rocks").transform;
        }
    }

	private void SetUpWalls(){
		foreach (var position in _level.WallPositions)
		{
			var myGameObject = Instantiate(Wall, position, Quaternion.identity);
			myGameObject.tag = "Wall";
			myGameObject.transform.parent = GameObject.Find("Walls").transform;
		}
	}

    public void DiamondFound()
    {
        _nrOfDiamondsFound++;
    }

    public void HitRock()
    {
        LevelFailed = true;
    }

    private bool LevelDone
    {
        get { return _nrOfDiamondsFound >= _level.DiamondPositions.Count; }
    }
}