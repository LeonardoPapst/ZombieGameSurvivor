using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string Name;
        public Transform Enemy;
        public float EnemySpeed;
        public int Count;
        public float Rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float TimeBetweenWaves = 5f;
    private float waveCountdown;
    private ControlZombie controlZombie;
    private ControlPlayer controlPlayer;

    private float searchCountdown = 1f;

    public Text WaitText;
    public Text WaveText;
    public Text VictoryText;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        controlPlayer = GameObject.FindWithTag("Player").GetComponent<ControlPlayer>();
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points reference");
        }
        WaveText.text = waves[nextWave].Name.ToString();
        waveCountdown = TimeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }

    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");
        WaitText.gameObject.SetActive(true);

        state = SpawnState.COUNTING;
        waveCountdown = TimeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            //nextWave = 0;
            Debug.Log("ALL WAVES COMPLETED!!");
            WaitText.gameObject.SetActive(false);
            VictoryText.gameObject.SetActive(true);
            controlPlayer.Victory = true;
            Time.timeScale = 0;
        }
        else
        {
            nextWave++;
            WaveText.text = waves[nextWave].Name.ToString();
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Inimigo") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.Name);
        WaitText.gameObject.SetActive(false);

        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.Count; i++)
        {
            controlZombie = _wave.Enemy.GetComponent<ControlZombie>();
            controlZombie.Speed = _wave.EnemySpeed;
            SpawnEnemy(_wave.Enemy);
            yield return new WaitForSeconds(1f / _wave.Rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }


    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);


        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
