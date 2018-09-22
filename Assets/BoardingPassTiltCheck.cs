using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardingPassTiltCheck : MonoBehaviour {

	public Done_PlayerController playerController;
	public TextMesh text;
	public Transform target;

	// Update is called once per frame
	void FixedUpdate () {
		if (target != null && target.transform != null && target.position != null)
		{
			Vector3 targetDir = target.position - transform.position;
			float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.forward);
			//float angle = transform.rotation.y + 90.0f;
			text.text = target.position.ToString();//angle.ToString();
			playerController.Move(angle / 25);//(transform.rotation.y + 90) / 15);
		}
		else
		{
			playerController.Move(0.0f);
		}
	}
}
