/*/
* Script by Devin Curry
* www.Devination.com
* www.youtube.com/user/curryboy001
* Please like and subscribe if you found my tutorials helpful :D
* Google+ Community: https://plus.google.com/communities/108584850180626452949
* Twitter: https://twitter.com/Devination3D
* Facebook Page: https://www.facebook.com/unity3Dtutorialsbydevin
/*/
using UnityEngine;
using System.Collections;

public class Joystick : TouchLogic 
{
	public enum JoystickType {Movement, LookRotation, SkyColor};
	public JoystickType joystickType;
	public Transform player = null;
	public float playerSpeed = 2f, maxJoyDelta = 0.05f, rotateSpeed = 100.0f;
	private Vector3 oJoyPos, joyDelta;
	private Transform joyTrans = null;
	private float pitch = 0.0f,
	yaw = 0.0f;
	//[NEW]//cache initial rotation of player so pitch and yaw don't reset to 0 before rotating
	private Vector3 oRotation;
	void Start () 
	{
		joyTrans = this.transform;
		oJoyPos = joyTrans.position;
		//NEW//cache original rotation of player so pitch and yaw don't reset to 0 before rotating
		oRotation = player.eulerAngles;
		pitch = oRotation.x;
		yaw = oRotation.y;
	}
	
	void OnTouchBegan()
	{
		//Used so the joystick only pays attention to the touch that began on the joystick
		touch2Watch = TouchLogic.currTouch;
	}
	
	void OnTouchMovedAnywhere()
	{
		if(TouchLogic.currTouch == touch2Watch)
		{
			//move the joystick
			joyTrans.position = MoveJoyStick();
			ApplyDeltaJoy();
		}
	}
	
	void OnTouchStayedAnywhere()
	{
		if(TouchLogic.currTouch == touch2Watch)
		{
			ApplyDeltaJoy();
		}
	}
	
	void OnTouchEndedAnywhere()
	{
		//the || condition is a failsafe so joystick never gets stuck with no fingers on screen
		if(TouchLogic.currTouch == touch2Watch || Input.touches.Length <= 0)
		{
			//move the joystick back to its orig position
			joyTrans.position = oJoyPos;
			touch2Watch = 64;
		}
	}
	
	void ApplyDeltaJoy()
	{
		switch(joystickType)
		{
		case JoystickType.Movement:
			// this works
			//player.rigidbody.AddForce(new Vector3(1.0f,1.0f,1.0f));

			// but this works PROPERLY
			player.rigidbody.AddForce(joyDelta * playerSpeed);


			//player.rigidbody.AddForce((player.forward * joyDelta.z + player.right * joyDelta.x) * playerSpeed * Time.deltaTime);


			// my controller is not what will move stuff
			//troller.Move ((player.forward * joyDelta.z + player.right * joyDelta.x) * playerSpeed * Time.deltaTime);
			break;
			// TODO may use this rotation stuff later in a different joystick
			/*
		case JoystickType.LookRotation:
			pitch -= Input.GetTouch(touch2Watch).deltaPosition.y * rotateSpeed * Time.deltaTime;
			yaw += Input.GetTouch(touch2Watch).deltaPosition.x * rotateSpeed * Time.deltaTime;
			
			//limit so we dont do backflips
			pitch = Mathf.Clamp(pitch, -80, 80);
			
			//do the rotations of our camera 
			player.eulerAngles += new Vector3 ( pitch, yaw, 0.0f);
			break;
			*/
			
		}
	}
	
	Vector3 MoveJoyStick()
	{
		//convert the touch position to a % of the screen to move our joystick
		float x = Input.GetTouch (touch2Watch).position.x / Screen.width,
		y = Input.GetTouch (touch2Watch).position.y / Screen.height;
		//combine the floats into a single Vector3 and limit the delta distance
		
		//If you want a rectangularly limited joystick (used in video), use this
		Vector3 position = new Vector3 (Mathf.Clamp(x, oJoyPos.x - maxJoyDelta, oJoyPos.x + maxJoyDelta),
		                                Mathf.Clamp(y, oJoyPos.y - maxJoyDelta, oJoyPos.y + maxJoyDelta), 0);//use Vector3.ClampMagnitude instead if you want a circular clamp instead of a square
		
		//If you want a circularly limited joystick, use this (uncomment it)
		//Vector3 position = Vector3.ClampMagnitude(new Vector3 (x-oJoyPos.x, y-oJoyPos.y, 0), maxJoyDelta) + oJoyPos;
		
		
		//joyDelta used for moving the player
		joyDelta = new Vector3(position.x - oJoyPos.x, 0, position.y - oJoyPos.y).normalized;
		//position used for moving the joystick
		return position;
	}
	
	void LateUpdate()
	{
		//if(!troller.isGrounded)
		//	troller.Move(Vector3.down * 2);
	}
}