using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	public int LevelNr = 0;
	public List<Vector3> DiamondPositions;
	public List<Vector3> RockPositions;

	void Start(){
		((GameManager)GameObject.Find ("GameManager").GetComponent ("GameManager")).CurrentLevel = LevelNr;
	}
}
