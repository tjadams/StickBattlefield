using UnityEngine;
using System.Collections;

public class PlayerController : TouchLogic {
	
	public GameObject shot;
	public Transform shotSpawn;

	void OnTouchBegan()
	{
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
	}
}
