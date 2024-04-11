using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarHealth : MonoBehaviour
{
    public float health = 100f;
    public int destroyedScore = 100;
    public float destroyDistance = 40f;
    public float bustedTimer = 10f;
    public Transform playerPos;
    public Transform spawnPos;
    public GameObject destroyedSelf;

    public bool killObject = false;

    Animation anim;
    EnemyMovement enemyMovement;
    Scoreboard scoreboard;

    //Audio
    public AudioSource audioSourceSpawning;

    public void DecreaseHealth(float damage)
    {
        health -= damage;
    }

    public void Kill()
    {
        Destroy(gameObject);
        EnemySpawner.enemyNum -= 1; 
        Instantiate(destroyedSelf, transform.position, transform.rotation); //Spawn destroyed self
        scoreboard.ChangeScore(destroyedScore); //Increase player score
    }

    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        enemyMovement = gameObject.GetComponent<EnemyMovement>();
        //anim.Play("PoliceSiren"); //Play siren animation
        anim.Blend("EnemySpawn"); //Play spawn animation
    }

    void Update()
    {
        try
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
            scoreboard = GameObject.FindGameObjectWithTag("BackgroundScripts").GetComponent<Scoreboard>();
            spawnPos = GameObject.FindGameObjectWithTag("EnemySpawnPos").transform;


            if (health <= 0f || killObject == true)
            {
                Kill();
            }
            else if (Vector3.Distance(transform.position, playerPos.position) > destroyDistance)
            {
                //Vector3 randPosition = Random.insideUnitCircle.normalized; //Get random position on edge of circle
                //randPosition = randPosition * 100; //Increase the radius by x

                Vector3 randPosition = -playerPos.right * 25;

                //Teleport car in relative to player's coordinates
                transform.position = new Vector3(spawnPos.position.x, 0.21f, spawnPos.position.z);
                transform.LookAt(new Vector3(playerPos.position.x, 0, playerPos.position.y)); //Point towards player
                enemyMovement.ResetSpeed(300f); //Reset speed upon teleportation
                gameObject.GetComponent<Animation>().Play("EnemySpawn");//Play spawn animation
                audioSourceSpawning.Play(); //Play spawn sound
            }
        }
        catch { };
    }
}
