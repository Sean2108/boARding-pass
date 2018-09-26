using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

    public RectTransform healthBar;
    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    private float time = 0.0f;
    public float interpolationPeriod = 0.1f;

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        if (time >= interpolationPeriod && currentHealth > 0)
        {
            time = 0.0f;

            currentHealth = currentHealth - 1;
            healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        }
       
    }
}
