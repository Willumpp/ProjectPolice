using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCarScript : MonoBehaviour
{
    public List<GameObject> cars;
    public List<int> prices;
    //public List<GameObject> panels;
    public static GameObject selectedCar;

    PlayerCarSpawner carSpawner;
    //Audio
    public AudioSource audioSourceFailedEquip;

    void Start()
    {
        carSpawner = GameObject.FindGameObjectWithTag("CarSpawner").GetComponent<PlayerCarSpawner>();
        carSpawner.SpawnCar(cars[PlayerPrefs.GetInt("SelectedCar",0)], carSpawner.transform.position, transform.rotation);
        selectedCar = cars[PlayerPrefs.GetInt("SelectedCar", 0)];
    }

    public void ChangeCar(int carID)
    {
        if (PlayerPrefs.GetInt("OverallScore",0) >= prices[carID])
        {
            selectedCar = cars[carID];
            PlayerPrefs.SetInt("SelectedCar", carID);
            carSpawner.SpawnCar(selectedCar, carSpawner.transform.position, transform.rotation);
            //panels[carID].SetActive(false);
        }
        else
        {
            audioSourceFailedEquip.Play();
        }
    }
}
