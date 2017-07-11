using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    public float playerWalkingSpeed = 5f;
    public float playerRunningSpeed = 15f;
    public float jumpStrength = 20f;
    public float verticalRotationLimit = 80f;
    public float speed = 5.0f;

    float forwardMovement;
    float sidewaysMovement;

    float verticalRotation = 0;

    float verticalVelocity;
    CharacterController cc;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Horizontal Rotation
        float horizontalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalRotation, 0);

        // Vertical Rotation
        verticalRotation -= Input.GetAxis("Mouse Y");
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        // Arrow key Rotation

        if (Input.GetKey("left"))
        {
            transform.Rotate(0, 1, 0);
        }
        if (Input.GetKey("right"))
        {
            transform.Rotate(0, -1, 0);
        }
        if (Input.GetKey("up"))
        {
            transform.Rotate(-1, 0, 0);
        }
        if (Input.GetKey("down"))
        {
            transform.Rotate(1, 0, 0);
        }

        // Character Movement


        forwardMovement = Input.GetAxis("Vertical") * playerWalkingSpeed;
        sidewaysMovement = Input.GetAxis("Horizontal") * playerWalkingSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            forwardMovement = Input.GetAxis("Vertical") * playerRunningSpeed;
            sidewaysMovement = Input.GetAxis("Horizontal") * playerRunningSpeed;
            DynamicCrossHair.spread = DynamicCrossHair.RUN_SPREAD;
        }

        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        if (Input.GetButton("Jump") && cc.isGrounded)
        {
            verticalVelocity = jumpStrength;
            DynamicCrossHair.spread = DynamicCrossHair.JUMP_SPREAD;
        } 


        Vector3 playerMovement = new Vector3(sidewaysMovement, verticalVelocity, forwardMovement);

      

        cc.Move(transform.rotation * playerMovement * Time.deltaTime);

    }



}
