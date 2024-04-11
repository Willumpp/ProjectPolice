using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnPlayer : MonoBehaviour
{
    public GameObject mainMenuPlayer;
    public float xOffset;
    public float zOffset;

    // Update is called once per frame
    void Update()
    {
        try {
            mainMenuPlayer = GameObject.FindGameObjectWithTag("Player");
            transform.position = new Vector3(mainMenuPlayer.transform.position.x + xOffset, transform.position.y, mainMenuPlayer.transform.position.z + zOffset);
        }
        catch { };
        
    }
}
