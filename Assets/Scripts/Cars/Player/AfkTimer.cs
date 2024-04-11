using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfkTimer : MonoBehaviour
{
    public float timer = 60f;
    public int afkChecks = 20;
    public bool isMoving = false;
    public List<bool> movementValues;

    private int falseCount = 0;

    PlayerMovement playerMovement;
    CarHealth carHealth;

    // Start is called before the first frame update
    void Start()
    {
        //Get Scripts from player
        playerMovement = gameObject.GetComponent<PlayerMovement>(); 
        carHealth = gameObject.GetComponent<CarHealth>();
        StartCoroutine(Afk()); //Start the afk loop
    }

    void Update()
    {
        //Determine if the player is moving or not. If so, tell the sript
        if (playerMovement.speed > 0)
        {
            isMoving = true;
        }
        else if (isMoving == true)
        {
            isMoving = false;
        }
    }

    //When moving reset the timer
    IEnumerator Afk()
    {
        while (true)
        {
            //Performs specified number of checks on wether the car is moving or not
            for (int j = 0; j < afkChecks; j++)
            {
                movementValues.Add(isMoving);
                yield return new WaitForSeconds(timer / afkChecks);
            }

            //Loop through list of logged checks and count number of falses
            foreach (bool i in movementValues)
            {
                if (i == false)
                {
                    falseCount += 1;
                }
            }

            //If number of falses is greater than specified number of checks, kill the car because of afk
            if (falseCount >= afkChecks)
            {
                carHealth.Kill();
            }
            else
            {
                //Reset counter if returns false
                falseCount = 0;
            }
            //Reset log of afk checks
            movementValues.Clear();
            yield return null;
        }
    }
}
