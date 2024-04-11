using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatTerrainGenerator : MonoBehaviour
{
    public GameObject wheat;
    public int wheatNum = 3;
    GameObject wheatClone;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < wheatNum; i++)
        {
            wheatClone = Instantiate(wheat, new Vector3(transform.position.x + Random.Range(-7.51f, 7.51f), transform.position.y + 0.239f, transform.position.z + Random.Range(-7.51f, 7.51f)), Quaternion.identity);
            wheatClone.transform.SetParent(transform, true); //Make platform created on its parent
        }
    }
}
