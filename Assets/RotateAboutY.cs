using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAboutY : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3(0, 0, 45);
		transform.rotation = rotation;
		GetComponent<Rigidbody>().angularVelocity = Vector3.up;
	}
}
