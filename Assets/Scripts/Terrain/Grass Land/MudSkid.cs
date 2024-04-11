using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudSkid : MonoBehaviour
{

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement.inMud = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement.inMud = false;
        }
    }
}
