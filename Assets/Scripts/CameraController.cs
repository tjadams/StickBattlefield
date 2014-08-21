using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// NOTE: public variables can be accessed in the Inspector which allows for quick edits to the variable without recompiling
	public GameObject player;

	// this value will be used by the camera to follow the player's position
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position;
	}
	
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}

}
