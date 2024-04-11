using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStop : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //If flipped or in the air, set to true and disable movement and decrease health
        if (transform.position.y > 0.25f)
        {
            gameObject.GetComponent<EnemyMovement>().enabled = false;
            gameObject.GetComponent<EnemyMovement>().inAir = true;
            //gameObject.GetComponent<EnemyCarHealth>().DecreaseHealth(1f);
        }
        else
        {
            gameObject.GetComponent<EnemyMovement>().enabled = true;
            gameObject.GetComponent<EnemyMovement>().inAir = false;
        }
    }
}
