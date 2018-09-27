using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScriptNew : MonoBehaviour
{

    public RectTransform healthBar;
    public const float maxHealth = 100;
    public float currentHealth = maxHealth;
    private float time = 0.0f;
    public float interpolationPeriod = 0.1f;
    private Done_GameController gameController;
    // Update is called once per frame
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<Done_GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= interpolationPeriod && currentHealth > 0 && gameController.startedGame == true)
        {
            time = 0.0f;

            currentHealth = currentHealth - 0.2f;
            healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        }
        if (currentHealth <= 0){
            gameController.GameOver();
        }

    }
}