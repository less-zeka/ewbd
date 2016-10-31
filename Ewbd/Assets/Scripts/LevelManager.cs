using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int nrOfDiamondsFound = 0;
    public GameObject Player;
    public GameObject Filler;
    public GameObject Diamond;
    public GameObject Rock;
    public bool LevelDone = false;
    public bool LevelFailed = false;

    private Level level;

    private bool roundFinished = false;

    // Use this for initialization
    void Start()
    {
        level = ((GameManager)GameObject.Find("GameManager").GetComponent("GameManager")).CurrentLevel;
        StartCoroutine(GameLoop());
    }

    void OnGUI()
    {
        //makes a GUI button at coordinates 10, 100, and a size of 200x40						
        if (GUI.Button(new Rect(10, 400, 100, 50), "LevelSelection"))
        {
            //Loads a level
            SceneManager.LoadScene("LevelSelection");
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
        var delay = 1.0f;
        roundFinished = true;
        yield return new WaitForSeconds(delay);
    }

    private bool GameIsRunning()
    {
        return !LevelDone && !LevelFailed;
    }

    private void SetUpLevel()
    {
        //level = GetLevelConfiguration ();
        SetUpFillers();
        SetUpRocks();
        SetUpDiamonds();
        SetUpPlayer();
    }

    private void SetUpFillers()
    {
        // rows and columns
        var nrFillerRows = 15.0f;
        var absoluteMaxX = 9.0f;
        var absoluteMaxZ = 9.0f;
        var deltaX = 2.0f*absoluteMaxX/(nrFillerRows - 1);
        var deltaZ = 2.0f*absoluteMaxZ/(nrFillerRows - 1);
        var scale = 0.96f;

        for (int x = 0; x < nrFillerRows; x++)
        {
            for (int y = 0; y < nrFillerRows; y++)
            {
                var gameObject = Instantiate(Filler,
                    new Vector3(-absoluteMaxX + x*deltaX, 0.25f, -absoluteMaxZ + y*deltaZ), Quaternion.identity);
                gameObject.transform.parent = GameObject.Find("Fillers").transform;
                gameObject.transform.localScale = new Vector3(scale, transform.localScale.y, scale);
            }
        }
    }

    private void SetUpDiamonds()
    {
        foreach (var position in level.DiamondPositions)
        {
            var rotation = Quaternion.Euler(new Vector3(45, 45f, 45f));
            var gameObject = Instantiate(Diamond, position, rotation);
            gameObject.transform.parent = GameObject.Find("Diamonds").transform;
        }
    }

    private void SetUpPlayer()
    {
        var gameObject = Instantiate(Player, new Vector3(-3.96f, 0.5f, 0f), Quaternion.identity);
        gameObject.transform.parent = GameObject.Find("Player").transform;
    }

    private void SetUpRocks()
    {
        foreach (var position in level.RockPositions)
        {
            var gameObject = Instantiate(Rock, position, Quaternion.identity);
            gameObject.transform.parent = GameObject.Find("Rocks").transform;
        }
    }

    public void DiamondFound()
    {
        nrOfDiamondsFound++;
    }
}