using UnityEngine;

public class CameraController : MonoBehaviour 
{
	public GameObject player;
	private Vector3 offset;

	// initialization
	void Start () {
		offset = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
