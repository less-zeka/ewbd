using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    // Using same speed reference in both, desktop and other devices
    public AudioClip shootSound;
    public float speed = 1000;
    public GameObject Explosion;
    private LevelManager levelManager;
	private Rigidbody _rigidBody;

	void Start ()
	{
		_rigidBody = GetComponent<Rigidbody> ();
		var cam = GameObject.Find("Main Camera");
		cam.GetComponent<CameraController>().Player = gameObject;

	    if (levelManager == null)
	    {
	        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
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
					//Debug.Log ("velocity: " + _rigidBody.velocity.magnitude);
	}

	void FixedUpdate ()
	{
		Vector3 movement;
		if (SystemInfo.deviceType == DeviceType.Desktop) { 
			// Player movement in desktop devices
			var moveHorizontal = Input.GetAxis ("Horizontal");
			var moveVertical = Input.GetAxis ("Vertical");
			movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        } else {
			// Player movement in mobile devices
			movement = new Vector3 (Input.acceleration.x, 0.0f, Input.acceleration.y);
		}
		_rigidBody.AddForce (movement * speed * Time.deltaTime, ForceMode.Acceleration);
	}

	void OnCollisionEnter (Collision collision)
	{				
		Debug.Log ("velONCollisionEnter: " + _rigidBody.velocity);
		_rigidBody.AddForce (_rigidBody.velocity);
		if (collision.gameObject.CompareTag ("Filler")) {
			Destroy (collision.gameObject);
		} else if (collision.gameObject.CompareTag ("Diamond")) {
			Destroy (collision.gameObject);
            levelManager.DiamondFound ();
		} else if (collision.gameObject.CompareTag ("Rock"))
		{
		    CollisionWithRock(collision);
		}
	}
		
	private void CollisionWithRock(Collision collision)
    {
		if (collision.gameObject.GetComponent<Rigidbody> ().velocity.magnitude < 1) {
			return;
		}
		foreach (var audioSource in collision.gameObject.GetComponents<AudioSource>())
        {
            audioSource.Play();
        }
        levelManager.HitRock();
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}