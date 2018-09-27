using UnityEngine;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private Done_GameController gameController;
    private GameObject healthBarCanvas;
    private HealthBarScriptNew newScript;

    void Start ()
	{
        healthBarCanvas = GameObject.FindGameObjectWithTag("Health Bar");
        if (healthBarCanvas == null)
        {
            Debug.Log("Can't find health bar");
        }
        newScript = healthBarCanvas.GetComponent<HealthBarScriptNew>();
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy")
		{
			return;
		}

		if (tag == "Coin" && other.tag == "Player")
		{
            newScript.currentHealth = Mathf.Min(newScript.currentHealth + 20, 100);
            gameController.AddScore(10);
			Instantiate(explosion, transform.position, transform.rotation);
			Handheld.Vibrate();
			Destroy (gameObject);
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.tag == "Player")
		{
            newScript.currentHealth = newScript.currentHealth - 20;
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			//gameController.GameOver();
		}

		
		gameController.AddScore(scoreValue);
		//Destroy (other.gameObject);
		Destroy (gameObject);
	}
}