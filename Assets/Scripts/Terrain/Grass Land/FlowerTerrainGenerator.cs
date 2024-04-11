using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerTerrainGenerator : MonoBehaviour
{
    public GameObject[] flower;
    public int flowerNum;
    GameObject flowerClone;

    // Start is called before the first frame update
    void Start()
    {
        int num;

        for (int i = 0; i < flowerNum; i++)
        {
            num = Random.Range(0, flower.Length);
            flowerClone = Instantiate(flower[num], new Vector3(transform.position.x + Random.Range(-7.51f, 7.51f), transform.position.y + 0.393f, transform.position.z + Random.Range(-7.51f, 7.51f)), Quaternion.identity);
            flowerClone.transform.SetParent(transform, true);
        }
    }
}
