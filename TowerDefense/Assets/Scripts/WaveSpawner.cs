using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    [SerializeField] float timeBetweenWaves = 5f;

    public Text waveCountdownText;

    private float countdown = 5f; // time to spawn first wave

    private int waveIndex = 0;



    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime; // countdown is go down 1 by 1 second

        waveCountdownText.text = Mathf.Round(countdown).ToString(); // round value to whole number
    }

    IEnumerator SpawnWave() // ienumerator allows us to 'pause code'. it works with coroutines
    {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f); // every 0.5s spawn enemy
        }           
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }    
}
