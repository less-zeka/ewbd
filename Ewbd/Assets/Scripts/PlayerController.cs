using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
	// Using same speed reference in both, desktop and other devices
	public float speed = 1000;
	private int count;
	public int diamondCount;
	private DateTime startTime;
	private DateTime endTime;
	public int NrOfDiamonds = 3;
	private GameManager gameManager;

	void Start ()
	{
		var cam = GameObject.Find("Main Camera");
		//cam.GetComponent(SmoothFollow).target = transform;
		cam.GetComponent<CameraController>().player = gameObject;

		startTime = DateTime.Now;
		count = 0;
		diamondCount = 0;

		if (gameManager == null) {
			gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		}
	}

	void Main ()
	{
		// Preventing mobile devices going in to sleep mode 
		//(actual problem if only accelerometer input is used)
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void Update ()
	{
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			// Exit condition for Desktop devices
			if (Input.GetKey ("escape"))
				Application.Quit ();
		} else {
			// Exit condition for mobile devices
			if (Input.GetKeyDown (KeyCode.Escape))
				Application.Quit ();            
		}
	}

	void FixedUpdate ()
	{
		//TODO!
		if (transform.position.x > 9) {
			Vector3 pos = transform.position;
			pos.x = 9;
			transform.position = pos;
		}
		if (transform.position.x < -9) {
			Vector3 pos = transform.position;
			pos.x = -9;
			transform.position = pos;
		}
		if (transform.position.z > 9) {
			Vector3 pos = transform.position;
			pos.z = 9;
			transform.position = pos;
		}
		if (transform.position.z < -9) {
			Vector3 pos = transform.position;
			pos.z = -9;
			transform.position = pos;
		}
		if (SystemInfo.deviceType == DeviceType.Desktop) { 
			// Player movement in desktop devices
			// Definition of force vector X and Y components
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			// Building of force vector
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			// Adding force to rigidbody
			GetComponent<Rigidbody> ().AddForce (movement * speed * Time.deltaTime);
		} else {
			// Player movement in mobile devices
			// Building of force vector 
			Vector3 movement = new Vector3 (Input.acceleration.x, 0.0f, Input.acceleration.y);
			// Adding force to rigidbody
			GetComponent<Rigidbody> ().AddForce (movement * speed * Time.deltaTime);
		}
	}

	//TODO check if needed
	void onCollisionEnter (Collider other)
	{
		OnTriggerEnter (other);
	}

	void OnTriggerEnter (Collider other)
	{				
		if (other.gameObject.CompareTag ("Filler")) {
			other.gameObject.SetActive (false);
			count = count + 1;
		} else if (other.gameObject.CompareTag ("Diamond")) {
			other.gameObject.SetActive (false);
			diamondCount = diamondCount + 1;
			gameManager.DiamondFound ();
			if (diamondCount >= NrOfDiamonds) {
				LevelDone ();
			}
		} else if (other.gameObject.CompareTag ("Rock")) {
			LevelFailed ();
		}
	}

	private void LevelDone ()
	{
		endTime = DateTime.Now;
		var elapsedTime = endTime - startTime;
		gameManager.LevelDone = true;
	}

	private void LevelFailed ()
	{
		endTime = DateTime.Now;
		var elapsedTime = endTime - startTime;
		gameManager.LevelFailed = true;
	}
}