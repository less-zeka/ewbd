using UnityEngine;

public class GameManager : MonoBehaviour
{
	//private int nrOfDiamonds = 3;
	public Level CurrentLevel;

	//public int CurrentLevel = 0;

	void Awake ()
	{
		DontDestroyOnLoad(transform.gameObject);
	}

	//void OnGUI ()
	//{
	//	GUI.Label (new Rect (10, 10, 100, 20), string.Format (@"Gems: {0}\{1}", nrOfDiamondsFound, nrOfDiamonds));
	//}
    



}
