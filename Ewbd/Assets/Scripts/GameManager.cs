using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Level CurrentLevel;

	void Awake ()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
}
