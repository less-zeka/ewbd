using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float scaleX = 10.050f;//Mathf.Cos(Time.time) * 0.5F + 1;
		float scaleY = 10.050f;//Mathf.Sin(Time.time) * 0.5F + 1;
		transform.GetComponent<Renderer>().material.mainTextureScale = new Vector2(scaleX , scaleY );

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
