using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public List<Waves> waves;
    public static int enemyNum = 0;
    public float enemySpawnCooldown = 5;
    public Transform playerPos;
    public Text playerText;

    private bool nextWave = false;

    //Audio
    public AudioSource audioSourceSpawning;

    //Get wave data from inspector
    [System.Serializable]
    public class Waves
    {
        public float waveDuration;
        public List<GameObject> enemies;
        public int maxEnemies;
    }

    //Function to spawn enemies in
    public void SpawnEnemy(GameObject enemy)
    {
        try { playerPos = GameObject.FindGameObjectWithTag("EnemySpawnPos").transform; }
        catch { };

        //playerText.text = (playerPos.position).ToString();
        //Vector3 randPosition = -playerPos.right * 25;
        //Spawn car in relative to player's coordinates
        Instantiate(enemy, new Vector3(playerPos.position.x,0.21f,playerPos.position.z), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyNum = 0;
        StartCoroutine(WaveSpawner());
        StartCoroutine(WaveCooldown());
    }

    IEnumerator WaveSpawner()
    {
        //Loop round given data from inspector
        foreach (Waves i in waves)
        {
            //Tell wave countdown to start again
            nextWave = false;
            while (nextWave == false) //Loop until wave countdown had ended
            {
                if (enemyNum < i.maxEnemies)
                {
                    //Get a random enemy from given list
                    int num = Random.Range(0, i.enemies.Count);
                    GameObject enemyToSpawn = i.enemies[num];

                    //Call spawn function
                    SpawnEnemy(enemyToSpawn);
                    audioSourceSpawning.Play(); //Play spawning sound

                    enemyNum += 1;
                }
                yield return new WaitForSeconds(enemySpawnCooldown);
                yield return null;
            }
        }
        yield return null;
    }
    
    IEnumerator WaveCooldown()
    {
        //Wait until next wave
        foreach (Waves i in waves)
        {
            yield return new WaitForSeconds(i.waveDuration);
            yield return null;
            nextWave = true;
            enemySpawnCooldown *= 0.9f;
        }
        yield return null;
    }
}
