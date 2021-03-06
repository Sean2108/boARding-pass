﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardingPassTrackableEvent : DefaultTrackableEventHandler {

	public Done_PlayerController playerController;
	public TextMesh text;
	public Transform target;
	public Done_GameController gameController;
	private bool targetInView = false;

	protected override  void OnTrackingFound()
	{
		base.OnTrackingFound ();
		targetInView = true;
	}
	
	protected override void OnTrackingLost()
	{
		base.OnTrackingLost ();
		targetInView = false;
	}

	protected void FixedUpdate()
	{
		if (targetInView) {
			if (gameController.waitingForPass)
			{
				gameController.StartCountdown();
			}
			else 
			{
				Vector3 targetDir = transform.position - target.position;
				//float angle = Vector3.SignedAngle(targetDir, target.forward, Vector3.forward);
				//float angle = transform.rotation.y + 90.0f;
				text.text = transform.eulerAngles.ToString();//targetDir.x.ToString();
				playerController.Move(targetDir.x / 25);//(transform.rotation.y + 90) / 15);
			}
		}
		else {
			playerController.Move(0.0f);
			text.text = "Not in view";
		}
	}
}
