﻿using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

public class Level : MonoBehaviour
{
    public int LevelNr;
    public List<Vector3> DiamondPositions;
    public List<Vector3> RockPositions;
	public List<Vector3> WallPositions;
	public Vector3 ExitPosition;
	public int NrOfSecondsForSucceed;

	//currently unused. remains as template
//	private void LoadLevelWithApi(){
//		var url = "http://ewbdwebapi.azurewebsites.net/api/level?levelNr=" + LevelNr;
//		var req = WebRequest.Create(url)
//			as HttpWebRequest;
//		string result;
//		using (var resp = req.GetResponse() as HttpWebResponse)
//		{
//			var reader = new StreamReader(resp.GetResponseStream());
//			result = reader.ReadToEnd();
//		}
//		var level = JsonUtility.FromJson<LevelData>(result);
//
//		this.DiamondPositions = level.DiamondPositions;
//		this.RockPositions = level.RockPositions;
//		//TODO 
//		//this.WallPositions = level.WallPositions;	
//	}
//
//    private void LoadLevel()
//    {
//		var level = LevelCreator.GetLevel1 ();
//		this.DiamondPositions = level.DiamondPositions;
//		this.RockPositions = level.RockPositions;
//		this.WallPositions = level.WallPositions;
//		this.ExitPosition = level.ExitPosition;
//		this.NrOfSecondsForSucceed = level.NrOfSecondsForSucceed;
//	}
}

//just used for deserialization
public class LevelData
{
    public int LevelNr;
    public List<Vector3> DiamondPositions;
    public List<Vector3> RockPositions;
}