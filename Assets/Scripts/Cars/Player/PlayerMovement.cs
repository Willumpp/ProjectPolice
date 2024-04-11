using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Movement and rotation
    public float acceleration = 300;
    public float accelerationStep = 0.55f;
    public float accelerationCooldown = 2f;
    public float speed = 0f;
    public float speedCap = 400f;
    private float startAcceleration;
    public float rotateSpeed = 1.5f;
    public float rotateSpeedModifier = 0.2f;
    public float reverseSpeed = 100f;
    Quaternion currentRotation;

    //Positioning
    Rigidbody rb;
    public static Vector3 playerPosition;
    public static Transform playerTransform;

    //Colliding
    public bool hasCollidedTerrain = false;
    public float shuntMultiplier = 0.03f;

    //Raycatsing
    public float rayRange = 0.5f;
    public float rayOffset = 0.5f;
    public float rayOffsetHorizontal = 0.1f;
    RaycastHit hitMovement;

    //Other
    public static bool inMud = false;
    public bool heightLimit = true;
    public MeshCollider floor;
    public PhysicMaterial frictionFloor;
    public PhysicMaterial frictionlessFloor;

    //Wheels
    public GameObject fRWheel;
    public GameObject fLWheel;
    public GameObject bRWheel;
    public GameObject bLWheel;
    public float wheelRotateSpeed;
    public bool hasRotated = false;

    //Audio
    public AudioSource audioSourceEngineDriving;
    bool startedAudioSourceDecceleration = false;
    public AudioSource audioSourceDecceleration;
    bool startedAudioSourceAcceleration = false;
    public AudioSource audioSourceAcceleration;

    // Start is called before the first frame update
    void Start()
    {
        //Reset collision variables
        hasCollidedTerrain = false;
        inMud = false;

        floor = GameObject.FindGameObjectWithTag("LargeFloor").GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>(); //Get rigidbody
        playerTransform = GetComponent<Transform>(); //Get transform
        startAcceleration = acceleration;

        //Start acceleration timers
        StartCoroutine(Accelerate());
        StartCoroutine(AccelerationChange());
        StartCoroutine(StartSpeedBoost());
        StartCoroutine(RotationAcceleration());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPosition = transform.position;

        //Turn wheels left or right when not moving or reversing
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) == false)
        {
            TurnWheels(); 
        }

        if (transform.position.y < 0.25f & heightLimit == true)
        {
            MovementCheck();
        }
        else if (heightLimit == false & transform.position.y < 0.5f)
        {
            MovementCheck();
        }
        else
        {
            if(Vector3.Dot(transform.up,Vector3.down) > 0)
            {
                gameObject.GetComponent<CarHealth>().DecreaseHealth(1f);
            }
        }
    }

    bool ForwardKeyCheck()
    {
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool DownKeyCheck()
    {
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool LeftKeyCheck()
    {
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool RightKeyCheck()
    {
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void MovementCheck()
    {
        //Move forward
        if (ForwardKeyCheck() && hasCollidedTerrain == false && inMud == false && CountDownTimer.inCountdown == false)
        {
            if (audioSourceAcceleration.isPlaying == false && startedAudioSourceAcceleration == false)
            {
                audioSourceAcceleration.Play(); //Play acceleration sound
                startedAudioSourceAcceleration = true; //Dont repeat until stopped
            };
            if (audioSourceEngineDriving.isPlaying == false && audioSourceAcceleration.isPlaying == false) { audioSourceEngineDriving.Play(); }; //Play driving sound
            startedAudioSourceDecceleration = false;

            ChangeFloorMaterial(true); //Make floor frictionless
            RotateAllWheels(); //Move wheels
            rb.velocity = new Vector3(-transform.right.x,transform.position.y - 0.4f,-transform.right.z) * speed * Time.deltaTime; //Move Forwards
            Turn(); //Activate truning function
        }
        else if (DownKeyCheck() == true && CountDownTimer.inCountdown == false)
        {
            ChangeFloorMaterial(true); //Make floor frictionless
            RotateAllWheels();
            rb.velocity = new Vector3(transform.right.x,transform.position.y - 0.4f,transform.right.z) * speed * Time.deltaTime; //Reverse
            hasCollidedTerrain = false;
            Turn();
        }
        else if (ForwardKeyCheck() && inMud == true && speed > 175f)
        {
            ChangeFloorMaterial(true); //Make floor frictionless
            RotateAllWheels();
            rb.velocity = new Vector3(-transform.right.x, transform.position.y - 0.4f, -transform.right.z) * speed / 2 * Time.deltaTime; //Move Forwards in mud
            Turn();
        }
        else
        {
            ChangeFloorMaterial(false); //Make floor friction
            startedAudioSourceAcceleration = false;
            if (startedAudioSourceDecceleration == false && audioSourceEngineDriving.isPlaying == true)
            {
                audioSourceDecceleration.Play(); //Play decceleration sound
                startedAudioSourceDecceleration = true; //Dont repeat
            }
            audioSourceEngineDriving.Stop(); //Stop audio source
            audioSourceAcceleration.Stop(); 
            //Rotate if slowing down
            Turn();
        }
    }

    void RotateAllWheels()
    {
        //Turn all wheels clockwise whilst moving forward
        fRWheel.transform.Rotate(wheelRotateSpeed, 0, 0, Space.Self);
        fLWheel.transform.Rotate(wheelRotateSpeed, 0, 0, Space.Self);
        bRWheel.transform.Rotate(wheelRotateSpeed, 0, 0, Space.Self);
        bLWheel.transform.Rotate(wheelRotateSpeed, 0, 0, Space.Self);
    }

    void TurnWheels()
    {
        //Rotate wheels left or right relative to its rotation
        if (LeftKeyCheck() == true)
        {
            //Left
            fRWheel.transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y - 110, transform.rotation.z);
            fLWheel.transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y - 110, transform.rotation.z);
            hasRotated = false;
        }
        else if (RightKeyCheck() == true)
        {
            //Right
            fRWheel.transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y - 70, transform.rotation.z);
            fLWheel.transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y - 70, transform.rotation.z);
            hasRotated = false;
        }
        else
        {
            //Face wheels forward. Toggle so they can spin whilst moving forward
            if (hasRotated == false)
            {
                fRWheel.transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z);
                fLWheel.transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z); 
                hasRotated = true;
            }
        }
    }

    void Turn()
    {
        //Move Left and Right
        if (LeftKeyCheck() && CountDownTimer.inCountdown == false && speed > 0)
        {
            transform.Rotate(0, -rotateSpeed, 0, Space.World); //Left
        }
        if (RightKeyCheck() && CountDownTimer.inCountdown == false && speed > 0)
        {
            transform.Rotate(0, rotateSpeed, 0, Space.World); //Right
        }
    }

    public float GetSpeed() { return speed; }

    void ChangeFloorMaterial(bool frictionless)
    {
        if (frictionless == true)
        {
            floor.material = frictionlessFloor; //Make floor frictionless
        }
        else
        {
            floor.material = frictionFloor; //Make floor friction
        }
    }

    //Shunt damage
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyCar" && speed > 75f)
        {
            collision.gameObject.GetComponent<EnemyCarHealth>().DecreaseHealth(speed * shuntMultiplier); //Decrease enemies health by multiplier
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.tag == "Floor" || collision.gameObject.tag == "EnemyCar") && hasCollidedTerrain == false && DownKeyCheck() == false)
        {
            if (collision.gameObject.tag == "EnemyCar")
            {
                ChangeFloorMaterial(false);
            }
            hasCollidedTerrain = true;
            //ChangeFloorMaterial(false);
        }
        else if (collision.gameObject.tag != "Floor" && collision.gameObject.tag != "EnemyCar")
        {
            hasCollidedTerrain = false;
        }
    }

    /*
    void OnCollisionExit(Collision collision)
    {
        Quaternion temp = transform.rotation;
        if (collision.gameObject.tag == "Floor")
        {
            transform.rotation = temp;
            //ChangeFloorMaterial(false);
        }
    }
    */

    IEnumerator RotationAcceleration()
    {
        float startRotateSpeedModifier = rotateSpeedModifier;
        float startRotateSpeed = rotateSpeed;

        while (true)
        {
            if ((LeftKeyCheck() || RightKeyCheck()) && CountDownTimer.inCountdown == false) //If moving left or right, increase rotate speed
            {
                rotateSpeed += rotateSpeedModifier; //Increase rotate speed
                rotateSpeedModifier *= 0.9f;
                yield return new WaitForSeconds(0.08f);
            }
            else
            {
                rotateSpeedModifier = startRotateSpeedModifier; //Set to default when not turning
                rotateSpeed = startRotateSpeed;
            }
            yield return null;
        }
    }
    
    IEnumerator Accelerate()
    {
        //Accelerate at x m/0.01s
        while (true)
        {
            if ((ForwardKeyCheck() || DownKeyCheck()) == true && hasCollidedTerrain == false && CountDownTimer.inCountdown == false && speed <= speedCap)
            {
                speed += acceleration / 100;
                wheelRotateSpeed += acceleration / 1000f; //Accelerate wheel rotation speed
                yield return new WaitForSeconds(0.01f);
            }
            else if (hasCollidedTerrain == true)
            {
                wheelRotateSpeed = 0;
                speed = 0;
                acceleration = startAcceleration;
            }
            else
            {
                if ((ForwardKeyCheck() || DownKeyCheck()) == false || (ForwardKeyCheck() && DownKeyCheck()) == true)
                {
                    wheelRotateSpeed = 0;
                    speed = 0;
                    acceleration = startAcceleration;
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
            if ((ForwardKeyCheck() || DownKeyCheck()) == true && inMud == false && CountDownTimer.inCountdown == false)
            {
                acceleration = acceleration * accelerationStep;
                yield return new WaitForSeconds(accelerationCooldown);
            }
            else if ((ForwardKeyCheck() || DownKeyCheck()) == true && inMud == true)
            {
                acceleration = acceleration * accelerationStep;
                acceleration = acceleration / 2;
                yield return new WaitForSeconds(accelerationCooldown);
            }
            yield return null;
        }
    }

    IEnumerator StartSpeedBoost()
    {
        //Give a huge speed increase at the start
        accelerationStep = 1.7f;
        yield return new WaitForSeconds(6f);
        accelerationStep = 0.65f;
        yield return null;
    }
}
