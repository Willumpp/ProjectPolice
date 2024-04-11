using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTerrainGenerator : MonoBehaviour
{
    public GameObject[] grass;
    public int grassNum;
    GameObject grassClone;

    // Start is called before the first frame update
    void Start()
    {
        int num;

        for (int i = 0; i < grassNum; i++)
        {
            num = Random.Range(0, grass.Length);
            grassClone = Instantiate(grass[num], new Vector3(transform.position.x + Random.Range(-7.51f, 7.51f),transform.position.y + 0.1f, transform.position.z + Random.Range(-7.51f, 7.51f)), Quaternion.identity);
            grassClone.transform.SetParent(transform, true);
        }
    }
}
