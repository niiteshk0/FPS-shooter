using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementScript : MonoBehaviour
{

    public float speed = 10f;
    Rigidbody rb;

    [Header("Camera")]
    public float mouseSensitivity = 5f;
    public Camera cam;


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
        float mouseX = Input.GetAxis("Horizontal");
        float mouseY = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(mouseX, 0, mouseY);

        transform.Translate(moveDirection * speed * Time.fixedDeltaTime, Space.Self);
    }

    void playerRotation()
    {
        float rotateY = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float rotateX = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        Vector3 mouseRotation = new Vector3(0, rotateY, 0);   // for the left and right movement
        transform.Rotate(mouseRotation);

        Vector3 camRotation = new Vector3(rotateX, 0, 0);    // for the up and down movement
        cam.transform.Rotate(-camRotation);

    }
}
