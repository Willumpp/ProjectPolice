using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudTerrainGenerator : MonoBehaviour
{
    public GameObject mud;
    public int mudNum;
    GameObject mudClone;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < mudNum; i++)
        {
            mudClone = Instantiate(mud, new Vector3(transform.position.x + Random.Range(-7.51f, 7.51f), transform.position.y + 1.35f, transform.position.z + Random.Range(-7.51f, 7.51f)), Quaternion.identity);
            mudClone.transform.SetParent(transform, true);
        }
    }
}
