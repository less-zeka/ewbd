using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public static class LevelCreator
{
	public static List<Level> GetLevels(){
		var result = new List<Level> ();
		result.Add (GetLevel1());
		result.Add (GetLevel2());
		result.Add (GetLevel3());
		result.Add (GetLevel4());

		return result;
	}

	public static Level GetLevel1 ()
	{
		var level = new Level () {
			RockPositions = GetRocks (MapData.Map1),
			DiamondPositions = GetDiamonds (MapData.Map1),
			WallPositions = GetWalls (MapData.Map1),
			ExitPosition = GetExit(MapData.Map1),
			NrOfSecondsForSucceed = 150,
			LevelNr = 1
		};
				
		return level;
	}
		
	public static Level GetLevel2 ()
	{
		var level = new Level () {
			RockPositions = GetRocks (MapData.Map2),
			DiamondPositions = GetDiamonds (MapData.Map2),
			WallPositions = GetWalls (MapData.Map2),
			ExitPosition = GetExit(MapData.Map2),
			NrOfSecondsForSucceed = 150,
			LevelNr = 2
		};

		return level;
	}

	public static Level GetLevel3 ()
	{
		var level = new Level () {
			RockPositions = GetRocks (MapData.Map3),
			DiamondPositions = GetDiamonds (MapData.Map3),
			WallPositions = GetWalls (MapData.Map3),
			ExitPosition = GetExit(MapData.Map3),
			NrOfSecondsForSucceed = 150,
			LevelNr = 2
		};

		return level;
	}

	public static Level GetLevel4 ()
	{
		var level = new Level () {
			RockPositions = GetRocks (MapData.Map4),
			DiamondPositions = GetDiamonds (MapData.Map4),
			WallPositions = GetWalls (MapData.Map4),
			ExitPosition = GetExit(MapData.Map4),
			NrOfSecondsForSucceed = 150,
			LevelNr = 2
		};

		return level;
	}

	public static List<Vector3> GetElementPositions (string map, string pattern)
	{
		var elements = new List<Vector3> ();
		int lineNum = 0;
		using (var mapReader = new StringReader (map)) {
			string line;
			while ((line = mapReader.ReadLine ()) != null) {
				lineNum++;
				foreach (Match match in Regex.Matches(line, pattern)) {
					var element = new Vector3 (match.Index-20, 0.5f, 12-lineNum);
					elements.Add (element);
				}
			}
		}
		return elements;
	}

	public static List<Vector3> GetRocks (string map)
	{
		return GetElementPositions (map, "r");	
	}

	public static List<Vector3> GetDiamonds (string map)
	{
		return GetElementPositions (map, "d");	
	}

	public static List<Vector3> GetWalls (string map)
	{
		return GetElementPositions (map, "w");	
	}

	public static Vector3 GetExit (string map)
	{
		return GetElementPositions (map, "P")[0];	
	}
}
