using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseController : MonoBehaviour
{
    public float mouse_Sesibility = 100f;
    public Transform playerBody;

    float Xrotation = 0f;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

   
    private void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y") * mouse_Sesibility * Time.deltaTime;
        
        Xrotation -= mouseY;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(Xrotation, 0f, 0f);
       
       

        playerBody.Rotate(Vector3.up * mouseX * mouse_Sesibility * Time.deltaTime);

    }
}
