using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float acceleration = 300;
    public float accelerationStep = 0.55f;
    public float speed = 0f;
    public float accelerationCooldown = 4f;
    private float startAcceleration;
    public float rotateSpeed = 0f;
    public float maxRotateSpeed = 4f;
    public float reverseSpeed = 100f;

    public float shuntMultiplier = 0.03f;
    public bool inAir = false;

    public float rayRange = 0.5f;
    public float rayOffset = 0.5f;
    public float rayOffsetHorizontal = 0.1f;

    public Transform playerPos;
    private Vector3 playerDirection;
    private Quaternion angle;

    public static bool hasCollided = false;
    private bool touchedPlayer = false;

    public GameObject fRWheel;
    public GameObject fLWheel;
    public GameObject bRWheel;
    public GameObject bLWheel;
    public float wheelRotateSpeed;
    public bool hasRotated = false;

    Vector3 cross;
    RaycastHit hitPlayer;
    RaycastHit hitMovement;
    Rigidbody rb;
    PlayerMovement playerMovement;
    CarHealth playerHealthScript;

    //Audio
    public AudioSource audioSourceCollision;

    #region Start

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Get rigidbody
        try
        {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            playerPos = PlayerMovement.playerTransform;
        }
        catch { };

        startAcceleration = acceleration;
        //Start acceleration timers
        StartCoroutine(Accelerate());
        StartCoroutine(AccelerationChange());
    }

    #endregion

    // Update is called once per frame
    void FixedUpdate()
    {
        try
        {
            playerPos = PlayerMovement.playerTransform;
        }
        catch { };

        if (transform.position.y < 0.25f && CountDownTimer.inCountdown == false)
        {
            inAir = false;

            //Raycast detection
            if (Physics.Raycast(transform.position - (transform.right * rayOffset), transform.forward, out hitPlayer) && hitPlayer.transform.tag == "Player") //Has the raycast hit a gameobject which is the player
            {
                //Face wheels forward. Toggle so they can spin whilst moving forward
                if (hasRotated == false)
                {
                    fRWheel.transform.localEulerAngles = -Vector3.right;
                    fLWheel.transform.localEulerAngles = -Vector3.right;
                    hasRotated = true;
                }
            }
            else
            {
                //Rotate the car towards the player
                // NOT MY CODE //
                //find the vector pointing from our position to the target
                try
                {
                    playerDirection = (new Vector3(playerPos.position.x - playerPos.right.x, transform.position.y, playerPos.position.z - playerPos.right.z)
                  - transform.position).normalized;
                }
                catch { };

                //create the rotation we need to be in to look at the target
                angle = Quaternion.LookRotation(playerDirection);

                //rotate us over time according to speed until we are in the required rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, angle, 0.05f);
            }

            if (hasCollided == false)
            {
                RotateAllWheels();
                rb.velocity = transform.forward * speed * Time.deltaTime; //Move Forwards
            }
        }
        else
        {
            //If flipped or in the air, set to true
            inAir = true;
        }
    }

    #region Collision

    void RotateAllWheels()
    {
        //Turn all wheels clockwise whilst moving forward
        fRWheel.transform.Rotate(wheelRotateSpeed, 0, 0, Space.Self);
        fLWheel.transform.Rotate(wheelRotateSpeed, 0, 0, Space.Self);
        bRWheel.transform.Rotate(wheelRotateSpeed, 0, 0, Space.Self);
        bLWheel.transform.Rotate(wheelRotateSpeed, 0, 0, Space.Self);
    }

    //Shunt damage
    void OnCollisionEnter(Collision collision)
    {
        //Play collision sound 
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "EnemyCar" && audioSourceCollision.isPlaying == false) { audioSourceCollision.Play(); }
        if (collision.gameObject.tag == "Player" && speed > 75f)
        {
            collision.gameObject.GetComponent<CarHealth>().DecreaseHealth(speed * shuntMultiplier); //Decrease player's health by multiplier
            audioSourceCollision.Play(); //Play collision sound
        }
    }

    #endregion

    public void ResetSpeed(float speedChange)
    {
        speed = speedChange;
    }

    IEnumerator Accelerate()
    {
        //Accelerate at x m/0.01s
        while (true)
        {
            //Perform raycast to detect specified tags and conditions are true or false
            if (Physics.Raycast(transform.position - (transform.right * rayOffset), transform.forward, out hitMovement, rayRange) == false ||
                Physics.Raycast(transform.position - (transform.right * rayOffset) + (transform.up * rayOffsetHorizontal), transform.forward, out hitMovement, rayRange) == false ||
                Physics.Raycast(transform.position - (transform.right * rayOffset) - (transform.up * rayOffsetHorizontal), transform.forward, out hitMovement, rayRange) == false)
            {
                if (inAir == false)
                {
                    //Increase speed
                    speed += acceleration / 150;
                    wheelRotateSpeed += acceleration / 1000f; //Accelerate wheel rotation speed
                    //rotateSpeed = (speed / 1000f) / maxRotateSpeed;
                    yield return new WaitForSeconds(0.01f); //Every 0.01 seconds
                }
            }
            else
            {
                //If conditions are true, stop the car
                if (hitMovement.transform.tag == "Player" || hitMovement.transform.tag == "CollidableTerrain" || inAir == true || (hitMovement.transform.tag == "EnemyCar" && hitMovement.transform.GetComponent<EnemyMovement>().speed < 10f))
                {
                    if (hitMovement.transform.tag == "Player"  && playerMovement.speed < 10f)
                    {
                        speed = 0; //Stop self
                        //playerMovement.speed *= 0.75f; //Slow player
                        acceleration = startAcceleration; //Reset acceleration

                        //Start busted timer
                        CarHealth.policeTouching = true;
                        touchedPlayer = false;
                        playerHealthScript = hitMovement.transform.GetComponent<CarHealth>();
                        StartCoroutine(playerHealthScript.BustedTimer());
                    }
                    else if (hitMovement.transform.tag == "Floor")
                    {
                        speed = 0;
                        acceleration = startAcceleration;
                    }
                }
                else
                {
                    if (touchedPlayer == false)
                    {
                        CarHealth.policeTouching = false;
                        touchedPlayer = true;
                    }
                }
            }
            yield return null;
        }
    }

    IEnumerator AccelerationChange()
    {
        //Decrease acceleration every x seconds by y
        while (true)
        {
            //Only activate when not flipped or in the air
            if (inAir == false || speed < 300f)
            {
                //Change acceleration
                acceleration = acceleration * accelerationStep;
                yield return new WaitForSeconds(accelerationCooldown); //Every x seconds
            }
            yield return null;
        }
    }

}
