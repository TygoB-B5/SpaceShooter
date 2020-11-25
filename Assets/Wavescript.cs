using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Wavescript : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject boss;
    public int numberOfEnemies;
    private int numberOfSpawned;
    public int addEnemiesPerWave;
    private Vector3 newPos;
    public float farAway;
    public Text waveText;
    public Text enemyText;
    public Text scoreText;
    public int waveNumber;
    public int score;
    public int[] bossSpawnWave;

    void Start()
    { 
        //sets variables and starts wave loop
        NextWave();
        enemyText.enabled = true;
        waveNumber = 1;
        waveText.text = "Wave: " + waveNumber.ToString();
        waveText.enabled = false;
    }

    void Update()
    {
        //changes score text to score
        scoreText.text = "Score:" + score.ToString();
    }
    public void Killed()
    {
        //ran per killed enemy and sets number of alive enemies and changes text
        numberOfEnemies -= 1;
        enemyText.text = "Enemies:" + numberOfEnemies.ToString() + "/" + numberOfSpawned.ToString();
        score += 100;
        if (numberOfEnemies == 0)
        {
            enemyText.enabled = false;
            StartCoroutine("NextWaveText");
            numberOfEnemies = numberOfSpawned + addEnemiesPerWave;
            waveNumber += 1;
        }
    }

    public void BossKilled()
    {
        //changes score and wavenumber when the boss is killed
        score += 500;
        waveNumber += 1;
        StartCoroutine("NextWaveText");
    }
    void NextWave()
    {
        //instantiates enemies at random position in a certain range with every new wave
        enemyText.enabled = true;
        for (int i = 1; i <= numberOfEnemies; i++)
        {
            newPos = new Vector3(player.transform.position.x + Random.Range(-farAway, farAway), player.transform.position.y + Random.Range(-farAway, farAway), player.transform.position.z + Random.Range(-farAway, farAway));

            Instantiate(enemy, newPos, player.transform.rotation);
        }

        numberOfSpawned = numberOfEnemies;
        enemyText.text = "Enemies:" + numberOfEnemies.ToString() + "/" + numberOfSpawned.ToString();
    }

    IEnumerator NextWaveText()
    {
        //creates flickering animation for wave text
        waveText.text = "Wave: " + waveNumber.ToString();
        waveText.enabled = true;
        yield return new WaitForSeconds(0.25f);
        waveText.enabled = false;
        yield return new WaitForSeconds(0.25f);
        waveText.enabled = true;
        yield return new WaitForSeconds(0.25f);
        waveText.enabled = false;
        yield return new WaitForSeconds(0.25f);
        waveText.enabled = true;
        yield return new WaitForSeconds(0.25f);
        waveText.enabled = false;
        yield return new WaitForSeconds(0.25f);


        //checks to spawn regular enemies or boss
        if (waveNumber == bossSpawnWave[0] || waveNumber == bossSpawnWave[1] || waveNumber == bossSpawnWave[2])
        {
            BossBattle();
        }
        else
        {
            NextWave();
        }
        yield return 0;
    }

    void BossBattle()
    {
        //instantiates boss and shows boss hp text
        enemyText.enabled = true;
        enemyText.text = "Enemies: Boss";
            newPos = new Vector3(player.transform.position.x + Random.Range(-farAway, farAway), player.transform.position.y + Random.Range(-farAway, farAway), player.transform.position.z + Random.Range(-farAway, farAway));
            Instantiate(boss, newPos, player.transform.rotation);
    }
}
