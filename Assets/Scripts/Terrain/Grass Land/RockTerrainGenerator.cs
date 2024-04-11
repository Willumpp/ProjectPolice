using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTerrainGenerator : MonoBehaviour
{
    public GameObject[] rock;
    public int rockNum;
    GameObject rockClone;

    // Start is called before the first frame update
    void Start()
    {
        int num;

        for (int i = 0; i < rockNum; i++)
        {
            num = Random.Range(0, rock.Length);
            rockClone = Instantiate(rock[num], new Vector3(transform.position.x + Random.Range(-7.51f, 7.51f), transform.position.y + 0.009f, transform.position.z + Random.Range(-7.51f, 7.51f)), Quaternion.identity);
            rockClone.transform.SetParent(transform, true);
        }
    }
}
