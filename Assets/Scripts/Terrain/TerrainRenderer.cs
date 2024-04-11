using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainRenderer : MonoBehaviour
{
    MeshRenderer mrSelf;
    public float renderDistance = 20f;
    public Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        mrSelf = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var mrChildren = GetComponentsInChildren<MeshRenderer>();
        try { playerPos = GameObject.FindGameObjectWithTag("Player").transform.position; }
        catch { };

        //When the player is a certain distance away from the platform, turn of mesh renderer
        /*
        if (Vector3.Distance(transform.position, playerPos) > renderDistance)
        {
            
            mrSelf.enabled = false;
            foreach (var i in mrChildren)
            {
                i.enabled = false;
            }
        }
        else
        {
            mrSelf.enabled = true;
            foreach (var i in mrChildren)
            {
                i.enabled = true;
            }
        }
        */
    }
}
