using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushTerrainGenerator : MonoBehaviour
{
    public GameObject bush;
    public int bushNum = 3;
    GameObject bushClone;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bushNum; i++)
        {
            bushClone = Instantiate(bush, new Vector3(transform.position.x + Random.Range(-7.51f, 7.51f), transform.position.y + 0.81f, transform.position.z + Random.Range(-7.51f, 7.51f)), Quaternion.identity);
            bushClone.transform.SetParent(transform, true); //Make platform created on its parent
        }
    }
}
