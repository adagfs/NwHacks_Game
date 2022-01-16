using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public float FollowSpeed = 2f;
	public float yOffset = 1f;
	public Transform target;
	
	// Update is called once per frame
	void Update () {
		float newYPos = 0f;
		if (target.position.y > Screen.height) {
			newYPos = target.position.y - yOffset;
		} else if (target.position.y < Screen.height) {
			newYPos = target.position.y + yOffset;
		}
		Vector3 newPos = new Vector3(target.position.x, newYPos, -10f);
		transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
	}
}
