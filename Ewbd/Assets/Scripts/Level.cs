using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

public class Level : MonoBehaviour
{
	public int LevelNr = 0;
	public List<Vector3> DiamondPositions;
	public List<Vector3> RockPositions;

	void Awake()
	{
		Debug.Log ("awake: " + LevelNr);
		((GameManager)GameObject.Find ("GameManager").GetComponent ("GameManager")).CurrentLevel = LevelNr;
		LoadLevel ();
	}

	private void LoadLevel(){
		var url = "http://ewbdwebapi.azurewebsites.net/api/level?levelNr=" + LevelNr;
		HttpWebRequest req = WebRequest.Create (url)
			as HttpWebRequest;
		string result = null;
		using (HttpWebResponse resp = req.GetResponse ()
			as HttpWebResponse) {
			StreamReader reader = new StreamReader (resp.GetResponseStream ());
			result = reader.ReadToEnd ();
		}
		var level = JsonUtility.FromJson<LevelData> (result);
		this.DiamondPositions = level.DiamondPositions;
		this.RockPositions = level.RockPositions;
	}
}

//just used for deserialization
public class LevelData{
	public int LevelNr ;
	public List<Vector3> DiamondPositions;
	public List<Vector3> RockPositions;
}
