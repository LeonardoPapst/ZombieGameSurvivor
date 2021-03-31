using UnityEngine;
using UnityEngine.UI;

public class ZombieRespawn : MonoBehaviour
{
    [SerializeField]
    public GameObject Zombie;

    public Text WaveWait;
    public Text NumberWave;

    public float RespawnTime = 1;
    public float WaveWaitTime = 5;
    public float NumberZombies = 5;
    public float NumberWaves = 5;
    
    private float timeCounter = 0;
    private bool canRespawn = false;
    private float numberZombiesBase;
    private float numberWavesBase;
    private float waveCount = 0;
    private bool zombieAlive = false;

    void Start()
    {
        NumberWave.text = "Wave 1/" + NumberWaves.ToString();
        numberZombiesBase = NumberZombies;
        numberWavesBase = NumberWaves;
    }

    void Update()
    {
        if (GameObject.FindWithTag("Inimigo") != null)
            zombieAlive = true;
        else
            zombieAlive = false;

        if (waveCount >= numberWavesBase)
        {
            if (zombieAlive == false)
                FinishGame();
        }
        else
        {
            ControlWave();
        }
    }

    void FinishGame()
    {
        WaveWait.text = "PARABÉNS! Você sobreviveu!!!";
        WaveWait.gameObject.SetActive(true);
    }

    void ControlWave()
    {

        if (canRespawn)
        {
            if (NumberZombies > 0)
            {
                timeCounter += Time.deltaTime;
                if (timeCounter > RespawnTime)
                {
                    Instantiate(Zombie, transform.position, transform.rotation);
                    timeCounter = 0;
                    NumberZombies -= 1;
                }
            }
            else
            {
                if (zombieAlive == false)
                {
                    canRespawn = false;
                    NumberZombies = numberZombiesBase + 2;
                }
            }
        }
        else
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > WaveWaitTime)
            {
                canRespawn = true;
                NumberWave.text = "Wave 1/" + NumberWaves.ToString();
                WaveWait.gameObject.SetActive(false);
                timeCounter = 0;
                waveCount += 1;
            }
            else
            {
                WaveWait.gameObject.SetActive(true);
                NumberWave.text = "Wave " + waveCount.ToString() + "/" + numberWavesBase.ToString();
            }
        }
    }
}
