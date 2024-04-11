using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarSpawner : MonoBehaviour
{
    public Vector3 spawnLocation;
    public bool inMenu = false;

    GameObject[] originalCar;

    void Start()
    {
        try
        {
            SpawnCar(ChangeCarScript.selectedCar, transform.position, transform.rotation);
        }
        catch { };
    }

    public void SpawnCar(GameObject car, Vector3 location, Quaternion dir)
    {
        if (inMenu == false)
        {
            originalCar = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject i in originalCar)
            {
                Destroy(i);
            }
            Instantiate(car, location, dir);
        }
        else if (inMenu == true)
        {
            originalCar = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject i in originalCar)
            {
                Destroy(i);
            }
            GameObject temp = Instantiate(car, location, dir);
            temp.GetComponent<AfkTimer>().enabled = false;
            temp.GetComponent<CarHealth>().enabled = false;
        }
    }
}
