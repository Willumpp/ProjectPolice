using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPositionChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.z, transform.position.y - 1.69f, transform.position.x);
    }
}
