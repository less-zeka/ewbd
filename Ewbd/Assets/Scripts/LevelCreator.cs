using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public static class LevelCreator
{

	public static Level GetLevel1 ()
	{
		var mapData = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
W.........d.r......r.r............r....W
W.rXr...... .........rd..r.... ..... ..W
W.......... ..r.....r.r..r........r....W
Wr.rr.........r......r..r....r...r.....W
Wr. r......... r..r........r......r.rr.W
W... ..r........r.....r. r........r.rr.W
Wwwwwwwwwwwwwwwwwwwwwwwwwwwwwww...r..r.W
W. ...r..d. ..r.r..........d.rd...... .W
W..d.....r..... ........rr r..r....r...W
W...r..r.r..............r .r..r........W
W.r.....r........rrr.......r.. .d....r.W
W.d.. ..r. .....r.rd..d....r...r..d. .W
W. r..............r r..r........d.....rW
W........wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwW
W r.........r...d....r.....r...r.......W
W r......... r..r........r......r.rr..PW
W. ..r........r.....r. ....d...r.rr...W
W....rd..r........r......r.rd......r...W
W... ..r. ..r.rr.........r.rd...... ..rW
W.d.... ..... ......... .r..r....r...r.W
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

		//easy
		mapData = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
W.........d.r......r.r............r....W
W.rXr...... .........rd..r.... ..... ..W
W.......... ..r...................r....W
Wr.rr............................r.....W
Wr. r.............................r.rr.W
W... ..r........r.....r. r..........rr.W
Wwwwwwwwwwwwwwwwwwwwwwwwwwwwwww......r.W
W. ...r..d. ..r.r..........d.dd...... .W
W..d........... ........rr r.......r...W
W......r................r .............W
W.d..............r.........r.. ......r.W
W.d.. .... .......rd..d....r...r..d. .W
W. r..............r r..r........d.....rW
W........wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwW
W r.........r...d....r.................W
W r......... r..r...............r.rr..PW
W. ................... ....d...r.rr...W
W....rd...........r......r.rd......r...W
W... ..r. ..r.rr.........r.rd...... ..rW
W.d.... ..... ......... .r..r....r...r.W
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

		Level level = new Level () {
			RockPositions = GetRocks (mapData),
			DiamondPositions = GetDiamonds (mapData),
			WallPositions = GetWalls (mapData)
		};
				
		return level;
	}

	public static List<Vector3> GetRocks (string map)
	{
		return GetElementPositions (map, "r");	
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

	public static List<Vector3> GetDiamonds (string map)
	{
		return GetElementPositions (map, "d");	
	}

	public static List<Vector3> GetWalls (string map)
	{
		return GetElementPositions (map, "w");	
	}

}
