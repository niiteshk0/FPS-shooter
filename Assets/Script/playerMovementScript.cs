using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerMovementScript : MonoBehaviour
{

    public float speed = 10f;
    Rigidbody rb;

    [Header("Camera")]
    public float mouseSensitivity = 5f;
    public Camera cam;

    public bool enableMobileInputs = false;
    public FixedJoystick joyStick;
    public FixedTouchField touchFie;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement();
        playerRotation();
    }

    void playerMovement()
    {
        float mouseX = 0;
        float mouseY = 0;
        if (enableMobileInputs)
        {

            mouseX = joyStick.Horizontal;
            mouseY = joyStick.Vertical;
        }
        else
        {
            mouseX = Input.GetAxis("Horizontal");
            mouseY = Input.GetAxis("Vertical");
        }
        Vector3 moveDirection = new Vector3(mouseX, 0, mouseY);

        transform.Translate(moveDirection * speed * Time.fixedDeltaTime, Space.Self);
    }

    void playerRotation()
    {
        float rotateY = 0;
        float rotateX = 0;

        if (enableMobileInputs)     // This is for the mobile controlls
        {
            rotateY = touchFie.TouchDist.x * mouseSensitivity;
            rotateX = touchFie.TouchDist.y * mouseSensitivity;
        }
        else     // this is for the pc controlls
        {

            rotateY = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
            rotateX = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        }

        Vector3 mouseRotation = new Vector3(0, rotateY, 0);   // for the left and right movement
        transform.Rotate(mouseRotation);

        Vector3 camRotation = new Vector3(rotateX, 0, 0);    // for the up and down movement
        cam.transform.Rotate(-camRotation);

    }
}
