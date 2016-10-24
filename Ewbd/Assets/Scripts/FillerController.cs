using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillerController : MonoBehaviour
{
	public GameObject filler;
	
	// Use this for initialization
	void Start ()
	{
		for (int x = 0; x < 5; x++) {
			for (int y = 0; y < 5; y++) {
				GameObject go = Instantiate (filler, new Vector3 (x, 0, y), Quaternion.identity);

				go.transform.parent = GameObject.Find ("Fillers").transform;
			}
		}
	}
}
