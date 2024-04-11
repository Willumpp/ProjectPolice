using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    private bool collided = false;

    void OnCollisionStay(Collision collider)
    {
        //Has collided with floor
        if (collider.gameObject.tag == "Floor")
        {
            collided = true;
        }
        else
        {
            collided = false;
        }
    }

    public bool HasCollided()
    {
        //Return true or false depending on floor collision
        if (collided == true)
        {
            //gameObject.GetComponent<PlatformCollider>().enabled = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}
