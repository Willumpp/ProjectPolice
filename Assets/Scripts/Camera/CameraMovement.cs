using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public Vector3 cameraOffset;
    public Transform playerPos;
    public float glideSpeed = 1;
    public int cameraSetting = 1;

    private Vector3 playerDirection;
    private Quaternion angle;

    void Start()
    {

    }

    void CameraAngle1()
    {
        //Change camera offset behind the car
        cameraOffset = playerPos.transform.right.normalized * 5.5f;
        transform.position = new Vector3(transform.position.x, 6.5f, transform.position.z);

        //Move the camera to the offset
        transform.position = Vector3.Lerp(transform.position, (playerPos.position + cameraOffset), glideSpeed);

        //Rotate the camera towards the player
        // NOT MY CODE //
        //find the vector pointing from our position to the target
        playerDirection = (new Vector3(playerPos.position.x - playerPos.right.x, playerPos.position.y, playerPos.position.z - playerPos.right.z)
            - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        angle = Quaternion.LookRotation(playerDirection);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, angle, 0.25f);
    }

    void CameraAngle2()
    {
        //Change camera offset above the car
        cameraOffset = playerPos.transform.up.normalized * 10f;
        transform.position = new Vector3(transform.position.x, 10f, transform.position.z);

        //Move the camera to the offset
        transform.position = Vector3.Lerp(transform.position, (playerPos.position + cameraOffset), glideSpeed);

        //Rotate the camera towards the player
        // NOT MY CODE //
        //find the vector pointing from our position to the target
        playerDirection = (new Vector3(playerPos.position.x - playerPos.right.x * 0.3f, playerPos.position.y, playerPos.position.z - playerPos.right.z * 0.3f)
            - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        angle = Quaternion.LookRotation(playerDirection);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, angle, 0.25f);
    }

    void CameraAngle3()
    {
        cameraOffset = new Vector3(5f, 7f, -5f);
        transform.position = playerPos.position + cameraOffset;

        //Rotate the camera towards the player
        // NOT MY CODE //
        //find the vector pointing from our position to the target
        playerDirection = (new Vector3(playerPos.position.x - playerPos.right.x * 0.3f, playerPos.position.y, playerPos.position.z - playerPos.right.z * 0.3f)
            - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        angle = Quaternion.LookRotation(playerDirection);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, angle, 0.25f);
    }

    void CameraAngle4()
    {
        //Change camera offset behind the car
        cameraOffset = playerPos.transform.right.normalized * -7.5f;
        transform.position = new Vector3(transform.position.x, 6.5f, transform.position.z);

        //Move the camera to the offset
        transform.position = Vector3.Lerp(transform.position, (playerPos.position + cameraOffset), glideSpeed);

        //Rotate the camera towards the player
        // NOT MY CODE //
        //find the vector pointing from our position to the target
        playerDirection = (new Vector3(playerPos.position.x - playerPos.right.x, playerPos.position.y, playerPos.position.z - playerPos.right.z)
            - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        angle = Quaternion.LookRotation(playerDirection);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, angle, 0.25f);
    }

    public void ChangeCameraAngle()
    {
        if (cameraSetting == 4)
        {
            cameraSetting = 1;
        }
        else if (cameraSetting == 3)
        {
            cameraSetting = 4;
        }
        else if (cameraSetting == 2)
        {
            cameraSetting = 3;
        }
        else if (cameraSetting == 1)
        {
            cameraSetting = 2;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPos = PlayerMovement.playerTransform;
        try
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ChangeCameraAngle();
            }

            if (cameraSetting == 1)
            {
                CameraAngle1();
            }
            if (cameraSetting == 2)
            {
                CameraAngle2();
            }
            if (cameraSetting == 3)
            {
                CameraAngle3();
            }
            if (cameraSetting == 4)
            {
                CameraAngle4();
            }
        }
        catch { }
    }
}
