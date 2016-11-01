using UnityEngine;

public class CameraController : MonoBehaviour 
{
	public GameObject Player;
	private Vector3 _offset;

	void Start () {
		_offset = transform.position;
	}
	
	void LateUpdate () {
	    if (Player != null)
	    {
	        transform.position = Player.transform.position + _offset;
	    }
	}
}
