using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRenderer : MonoBehaviour
{
    MeshRenderer mrSelf;
    Transform tf;
    public float renderDistance = 40;
    public float destroyDistance = 65f;
    public Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        mrSelf = gameObject.GetComponent<MeshRenderer>();
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var mrChildren = GetComponentsInChildren<MeshRenderer>();
        try { playerPos = GameObject.FindGameObjectWithTag("Player").transform.position; }
        catch { };

        //When the player is a certain distance away from the platform, turn of mesh renderer and gameObject
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

        //Destroy self after specified distance from player is passed
        if (Vector3.Distance(transform.position, playerPos) > destroyDistance - 25f)
        {
            PlayerTerrainCreator.used.Remove(transform.position);
            gameObject.SetActive(false);
        }
    }
}
