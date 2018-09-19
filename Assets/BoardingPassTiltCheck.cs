using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardingPassTiltCheck : MonoBehaviour {

	public Done_PlayerController playerController;
	public TextMesh text;
	public Transform target;

	// Update is called once per frame
	void FixedUpdate () {
		if (transform != null)
		{
			Vector3 targetDir = target.position - transform.position;
			float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);
			//float rotate = transform.rotation.y + 90.0f;
			text.text = angle.ToString();
			playerController.Move(angle / 100);//(transform.rotation.y + 90) / 15);
		}
	}
}
