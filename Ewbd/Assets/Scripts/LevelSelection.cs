using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour {

	void Awake(){
		SceneManager.LoadScene (1);
	}
}
