using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTerrainCreator : MonoBehaviour
{
    public static List<Vector3> positions = new List<Vector3>();
    public static List<Vector3> used = new List<Vector3>();
    string[] platformRarity = new string[40];

    //Object pooling test
    public List<Queue<GameObject>> defaultGrassPlatforms = new List<Queue<GameObject>>();
    public List<Queue<GameObject>> commonGrassPlatforms = new List<Queue<GameObject>>();
    public List<Queue<GameObject>> uncommonGrassPlatforms = new List<Queue<GameObject>>();
    public List<Queue<GameObject>> rareGrassPlatforms = new List<Queue<GameObject>>();
    public List<Pool> pools;
    public Dictionary<string, List<Queue<GameObject>>> poolPlatformDictionary = new Dictionary<string, List<Queue<GameObject>>>();

    [System.Serializable]
    public class Pool
    {
        public string rarity;
        public GameObject platform;
        public int size;
    }

    void Start()
    {
        positions.Clear();
        used.Clear();

        //Add rarities to dictionary
        poolPlatformDictionary.Add("Default", defaultGrassPlatforms);
        poolPlatformDictionary.Add("Common", commonGrassPlatforms);
        poolPlatformDictionary.Add("Uncommon", uncommonGrassPlatforms);
        poolPlatformDictionary.Add("Rare", rareGrassPlatforms);

        //Create pool objects
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.platform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            //Add pools to specific rarity
            if (pool.rarity == "Default") { defaultGrassPlatforms.Add(objectPool); };
            if (pool.rarity == "Common") { commonGrassPlatforms.Add(objectPool); };
            if (pool.rarity == "Uncommon") { uncommonGrassPlatforms.Add(objectPool); };
            if (pool.rarity == "Rare") { rareGrassPlatforms.Add(objectPool); };
        }

        #region Rarities
        for (int i = 0; i < 25; i++) { platformRarity[i] = "Default"; }; //Make a default platform % chance of spawning
        for (int i = 25; i < 33; i++) { platformRarity[i] = "Common"; }; //Make a common platform % chance of spawning
        for (int i = 33; i < 37; i++) { platformRarity[i] = "Uncommon"; }; //Make an uncommon platform % chance of spawning
        for (int i = 37; i <= 39; i++) { platformRarity[i] = "Rare"; }; //Make a rare platform % chance of spawning
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        //Loop round generation que
        foreach (Vector3 i in positions)
        {
            int num;

            if (used.Contains(i) == false)
            {
                //Choose a random rarity
                num = Random.Range(0, platformRarity.Length);
                string platformRarityName = platformRarity[num];

                List<Queue<GameObject>> platformList = poolPlatformDictionary[platformRarityName]; //Get platform rarity group

                //Choose a random platform from rarity list
                num = Random.Range(0, platformList.Count);
                Queue<GameObject> platformQueue = platformList[num];

                //Move platform
                GameObject platformSpawn = platformQueue.Dequeue(); //Get platform from chosen queue

                platformSpawn.SetActive(true); //Activate platform
                used.Remove(platformSpawn.transform.position); //Remove for use in last position
                platformSpawn.transform.position = i; //Move platform
                //platformSpawn.GetComponent<PlatformCreator>().PlatformAnim(); //Play platform animation
                used.Add(i); //Add coordinates to used list

                platformQueue.Enqueue(platformSpawn);

                /*
                Instantiate(platform, i, Quaternion.identity); //Generate platform
                used.Add(i); //Add coordinates to used list
                */

            }
        }
        foreach (Vector3 j in used)
        {
            //Remove the coordinates from the list to reduce lag (hopefully)
            positions.Remove(j);
        }
    }
}
