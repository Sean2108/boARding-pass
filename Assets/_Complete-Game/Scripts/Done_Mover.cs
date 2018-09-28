using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
	public float initialSpeed;
	private float speed;
	public float speedIncrement;

	void Start ()
	{
		speed = initialSpeed;
		GetComponent<Rigidbody>().velocity = transform.forward * speed * Random.Range(5f, 10f);
	}

	public void IncSpeed()
	{
		speed += speedIncrement;
	}
}
