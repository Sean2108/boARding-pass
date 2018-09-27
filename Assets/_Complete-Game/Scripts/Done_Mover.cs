﻿using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
	public float speed;
	public float speedIncrement;

	void Start ()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * speed * Random.Range(5f, 10f);
	}

	public void IncSpeed()
	{
		speed += speedIncrement;
	}
}
