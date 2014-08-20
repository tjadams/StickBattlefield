using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	// called before rendering a frame, game code goes here
	void Update ()
	{

	}

	// called before a physics action, physics code goes here
	public float speed;

	void FixedUpdate () 
	{
		//TODO change this input stuff to work on Android
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");


		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

		rigidbody.AddForce(movement * speed * Time.deltaTime);
	}
}
