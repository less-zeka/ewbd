using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 100, 150, 100));

        var myButtonStyle = new GUIStyle(GUI.skin.button) {fontSize = 50};
        if (GUILayout.Button("Play", myButtonStyle))
        {
			SceneManager.LoadScene(Constants.Scene_Level);
        }

        GUILayout.EndArea();
    }
}