using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float scoreIncrementWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private bool notStarted;
    private int score;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        StartCoroutine(Countdown(3));
        //UpdateScore();
        //StartCoroutine(SpawnWaves());
        //StartCoroutine(AddScoreEverySecond());
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
        // count down is finished...
        UpdateScore();
        StartCoroutine(SpawnWaves());
        StartCoroutine(AddScoreEverySecond());
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
                restartText.text = "Tap Screen for Restart";
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
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}