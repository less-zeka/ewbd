using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelConfiguration {

		public static List<Vector3> DiamondPositions
		{
				get{ 
						var list = new List<Vector3>();
						list.Add (new Vector3 (1f, 0.5f, 0f));
						list.Add (new Vector3 (4f, 0.5f, 2f));
						list.Add (new Vector3 (6f, 0.5f, 8f));
						return list;
				}
		}

	public static List<Vector3> RockPositions
	{
		get{ 
			var list = new List<Vector3>();
			list.Add (new Vector3 (2f, 0.5f, 0.95f));
			list.Add (new Vector3 (4.5f, 0.5f, 3f));
			list.Add (new Vector3 (4.5f, 0.5f, 4f));
			return list;
		}
	}
}

public class Level
{
	public int LevelNr;
	public string bar;
	public List<Vector3> DiamondPositions;
	public List<Vector3> RockPositions;
}
