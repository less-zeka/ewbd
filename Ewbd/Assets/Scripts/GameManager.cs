using UnityEngine;

public class GameManager : MonoBehaviour
{
	public int NrOfLivesLeft;
	public int CurrentLevelNr;

	void Awake ()
	{
		Initialize ();
		DontDestroyOnLoad(transform.gameObject);
	}

	public void Initialize(){
		NrOfLivesLeft = 3;
		CurrentLevelNr = 1;
	}
}
