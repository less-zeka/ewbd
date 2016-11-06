using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    // Using same speed reference in both, desktop and other devices
    public AudioClip shootSound;
    public float speed = 1000;
    public GameObject Explosion;
	private int count;
	private DateTime startTime;
	private DateTime endTime;
    private LevelManager levelManager;

	void Start ()
	{
		var cam = GameObject.Find("Main Camera");
		cam.GetComponent<CameraController>().Player = gameObject;

		startTime = DateTime.Now;
		count = 0;
		//diamondCount = 0;

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
	}

	void FixedUpdate ()
	{
		if (SystemInfo.deviceType == DeviceType.Desktop) { 
			// Player movement in desktop devices
			// Definition of force vector X and Y components
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			// Building of force vector
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			// Adding force to rigidbody
			GetComponent<Rigidbody> ().AddForce (movement * speed * Time.deltaTime);
			//GetComponent<Rigidbody> ().velocity = movement;

            //Physics.gravity = movement;
        } else {
			// Player movement in mobile devices
			// Building of force vector 
			Vector3 movement = new Vector3 (Input.acceleration.x, 0.0f, Input.acceleration.y);
			// Adding force to rigidbody
			GetComponent<Rigidbody> ().AddForce (movement * speed * Time.deltaTime);
			//GetComponent<Rigidbody> ().velocity = movement;

		}
	}

	void OnTriggerEnter (Collider other)
	{				
		if (other.gameObject.CompareTag ("Filler")) {
			other.gameObject.SetActive (false);
			count = count + 1;
		} else if (other.gameObject.CompareTag ("Diamond")) {
			other.gameObject.SetActive (false);
            levelManager.DiamondFound ();
		} else if (other.gameObject.CompareTag ("Rock"))
		{
		    HitRock(other);
		}
		else if (other.gameObject.CompareTag ("Wall"))
		{
			HitWall(other);
		}
	}

	private void HitWall(Collider other){
		var velocity = GetComponent<Rigidbody> ().velocity;
		if(other.name == "NorthWall" || other.name == "SouthWall"){
			GetComponent<Rigidbody> ().velocity = new Vector3 (velocity.x, velocity.y, -velocity.z);
		}
		else if(other.name == "WestWall" || other.name == "EasthWall"){
			GetComponent<Rigidbody> ().velocity = new Vector3 (-velocity.x, velocity.y, velocity.z);
		}
		else{
			var normal = other.transform.position - this.transform.position;
			if (normal.x*normal.x > normal.z*normal.z) {
				GetComponent<Rigidbody> ().velocity = new Vector3 (-velocity.x, velocity.y, velocity.z);
			} else {
				GetComponent<Rigidbody> ().velocity = new Vector3 (velocity.x, velocity.y, -velocity.z);
			}
		}
	}

    private void HitRock(Collider other)
    {
        foreach (var audioSource in other.GetComponents<AudioSource>())
        {
            audioSource.Play();
        }
        levelManager.HitRock();
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}