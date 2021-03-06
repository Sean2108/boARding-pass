﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float initialSpawnWait;
    public float startWait;
    public float initialWaveWait;
    public float scoreIncrementWait;
    private float spawnWait;
    private float waveWait;

    public Text scoreText;
    public Text gameOverText;

    public GameObject instructionText;
    public GameObject welcomeText;
    public GameObject playAgainText;
    public GameObject instructionArrows;
    public GameObject boardingWarning;

    public GameObject leaderboard;

    public Done_Mover cloudMover;
    public Done_Mover batteryMover;

    private bool gameOver;
    private bool restart;
    public bool waitingForPass;
    private int score;
    public bool startedGame;
    public int spawnRateInc;

    void Start()
    {
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        scoreText.text = "";
        score = 0;
        waitingForPass = true;
        spawnWait = initialSpawnWait;
        waveWait = initialWaveWait;
    }

    public void StartCountdown()
    {  
        waitingForPass = false;
        Destroy(welcomeText);
        instructionText.SetActive(true);
        instructionArrows.SetActive(true);
        StartCoroutine(Countdown(3));
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {
            gameOverText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        gameOverText.text = " ";
        startedGame = true;
        // count down is finished...
        UpdateScore();
        Destroy(instructionText);
        Destroy(instructionArrows);
        StartCoroutine(SpawnWaves());
        StartCoroutine(AddScoreEverySecond());
    }

    public void ShowWarning()
    {
        StartCoroutine(ShowWarningTrigger());
    }

    IEnumerator ShowWarningTrigger()
    {
        boardingWarning.SetActive(true);
        yield return new WaitForSeconds(5);
        Destroy(boardingWarning);
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetButton("Fire1"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                playAgainText.SetActive(true);
                restart = true;
                break;
            }
        }
    }

    IEnumerator AddScoreEverySecond()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(scoreIncrementWait);
            AddScore(1);
        }

    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        if (score % 10 == 0)
        {
            batteryMover.IncSpeed();
            cloudMover.IncSpeed();
            spawnWait /= spawnRateInc;
            waveWait /= spawnRateInc;
        }
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    void DeleteAll(string toDelete){
        var obj = GameObject.FindGameObjectsWithTag(toDelete);
        foreach (GameObject enemyObj in obj)
        {
            Debug.Log("deleting" + toDelete);
            Destroy(enemyObj);
        }
    }
   

    public void GameOver()
    {
        DeleteAll("Asteroid");
        DeleteAll("Coin");
        gameOverText.text = "";
        scoreText.text = "";
        leaderboard.SetActive(true);
        gameOver = true;

    }
}