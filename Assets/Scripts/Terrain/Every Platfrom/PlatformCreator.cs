using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    public GameObject box1;
    public GameObject box2;
    public GameObject box3;
    public GameObject box4;
    public GameObject box5;
    public GameObject box6;
    public GameObject box7;
    public GameObject box8;

    PlatformCollider box1Collider;
    PlatformCollider box2Collider;
    PlatformCollider box3Collider;
    PlatformCollider box4Collider;
    PlatformCollider box5Collider;
    PlatformCollider box6Collider;
    PlatformCollider box7Collider;
    PlatformCollider box8Collider;

    public Vector3 playerPos;
    public GameObject platform;
    public bool spawnTerrain = false;
    public bool startingTerrain = false;

    Transform tf;
    Animation anim;

    public GameObject scoreText;
    Scoreboard scoreboard; //Change score when player moves
    public bool hasIncreasedScore = false;

    void Start()
    {
        box1Collider = box1.GetComponent<PlatformCollider>();
        box2Collider = box2.GetComponent<PlatformCollider>();
        box3Collider = box3.GetComponent<PlatformCollider>();
        box4Collider = box4.GetComponent<PlatformCollider>();
        box5Collider = box5.GetComponent<PlatformCollider>();
        box6Collider = box6.GetComponent<PlatformCollider>();
        box7Collider = box7.GetComponent<PlatformCollider>();
        box8Collider = box8.GetComponent<PlatformCollider>();

        //anim = gameObject.GetComponent<Animation>();
        tf = GetComponent<Transform>();

        //playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (spawnTerrain == true)
        {
            TerrainTest(spawnTerrain);
        }
    }
     
    void Update()
    {
        TerrainTest(spawnTerrain);
    }

    void TerrainTest(bool smallerRange)
    {
        float width = GetComponent<Renderer>().bounds.size.x;
        try
        {
            scoreboard = GameObject.FindGameObjectWithTag("BackgroundScripts").GetComponent<Scoreboard>(); //Get scoreboard script
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        catch { };

        //Add the starting terrain to the taken list
        if (startingTerrain == true && PlayerTerrainCreator.used.Contains(transform.position) == false)
        {
            PlayerTerrainCreator.used.Add(transform.position);
        }

        if (Vector3.Distance(transform.position, playerPos) < 15 && smallerRange == false)
        {
            //Test if the collider boxes have collided and the "used" list doesnt contain the specified coordinates

            //Increase score when generating new terrain
            if (hasIncreasedScore == false)
            {
                scoreboard.ChangeScore(5);
                hasIncreasedScore = true;
            }


            //East
            if (box1Collider.HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x + width, tf.position.y, tf.position.z)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x + width, tf.position.y, tf.position.z)); //Add position to que of platform creator
            }
            //West
            if (box2Collider.HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x - width, tf.position.y, tf.position.z)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x - width, tf.position.y, tf.position.z));
            }
            //North
            if (box3Collider.HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x, tf.position.y, tf.position.z + width)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x, tf.position.y, tf.position.z + width));
            }
            //South
            if (box4Collider.HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x, tf.position.y, tf.position.z - width)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x, tf.position.y, tf.position.z - width));
            }

            //South East
            if (box5Collider.HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x + width, tf.position.y, tf.position.z - width)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x + width, tf.position.y, tf.position.z - width));
            }
            //North East
            if (box6Collider.HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x + width, tf.position.y, tf.position.z + width)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x + width, tf.position.y, tf.position.z + width));
            }
            //North West
            if (box7Collider.HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x - width, tf.position.y, tf.position.z + width)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x - width, tf.position.y, tf.position.z + width));
            }
            //South West
            if (box8Collider.HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x - width, tf.position.y, tf.position.z - width)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x - width, tf.position.y, tf.position.z - width));
            }
            //hasPlaced = true;
            //this.enabled = false;
        }
        else
        {
            hasIncreasedScore = false;
        }
        /*
        else if (Vector3.Distance(transform.position, playerPos) < 10 && smallerRange == true)
        {
            //Test if the collider boxes have collided and the "used" list doesnt contain the specified coordinates

            //East
            if (box1.GetComponent<PlatformCollider>().HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x + width, tf.position.y, tf.position.z)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x + width, tf.position.y, tf.position.z)); //Add position to que of platform creator
            }
            //West
            if (box2.GetComponent<PlatformCollider>().HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x - width, tf.position.y, tf.position.z)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x - width, tf.position.y, tf.position.z));
            }
            //North
            if (box3.GetComponent<PlatformCollider>().HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x, tf.position.y, tf.position.z + width)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x, tf.position.y, tf.position.z + width));
            }
            //South
            if (box4.GetComponent<PlatformCollider>().HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x, tf.position.y, tf.position.z - width)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x, tf.position.y, tf.position.z - width));
            }

            //South East
            if (box5.GetComponent<PlatformCollider>().HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x + width, tf.position.y, tf.position.z - width)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x + width, tf.position.y, tf.position.z - width));
            }
            //North East
            if (box6.GetComponent<PlatformCollider>().HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x + width, tf.position.y, tf.position.z + width)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x + width, tf.position.y, tf.position.z + width));
            }
            //North West
            if (box7.GetComponent<PlatformCollider>().HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x - width, tf.position.y, tf.position.z + width)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x - width, tf.position.y, tf.position.z + width));
            }
            //South West
            if (box8.GetComponent<PlatformCollider>().HasCollided() != true && PlayerTerrainCreator.used.Contains(new Vector3(tf.position.x - width, tf.position.y, tf.position.z - width)) == false)
            {
                PlayerTerrainCreator.positions.Add(new Vector3(tf.position.x - width, tf.position.y, tf.position.z - width));
            }
            //hasPlaced = true;
            //this.enabled = false;
        }
        */
    }

    public void PlatformAnim()
    {
        //anim.Blend("PlatformSpawn");
    }
}
