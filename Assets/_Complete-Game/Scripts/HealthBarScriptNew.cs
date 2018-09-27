using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScriptNew : MonoBehaviour
{

    public RectTransform healthBar;
    public const float maxHealth = 750;
    public float currentHealth, displayedHealth = maxHealth;
    private float time = 0.0f;
    public float interpolationPeriod = 0.1f;
    //public D2FogsPE script;
    private Done_GameController gameController;
    private GameObject arCamera;
    private UB.D2FogsPE d2FogsPE;
    // Update is called once per frame
    void Start()
    {
        //for the fog
        arCamera = GameObject.FindGameObjectWithTag("MainCamera");
        d2FogsPE = arCamera.GetComponent<UB.D2FogsPE>();
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
        if (time >= interpolationPeriod && currentHealth > 0 && gameController.startedGame)
        {
            time = 0.0f;
            currentHealth = currentHealth - 0.2f;
            healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
            d2FogsPE.Density =(float)(2 * (maxHealth - currentHealth) / (maxHealth));
        }
        if (currentHealth <= 0){
            gameController.GameOver();
        }
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);

    }
}