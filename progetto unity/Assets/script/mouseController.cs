using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseController : MonoBehaviour
{
    public float mouse_Sesibility = 100f;
    public Transform playerBody;
    public Transform head;
    public Animator _anim;

    float Xrotation = 0f;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

   
    private void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouse_Sesibility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouse_Sesibility * Time.deltaTime;
        
        Xrotation -= mouseY;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(Xrotation, 0f, 0f);
        _anim.SetFloat("Idle", mouseX);

        playerBody.Rotate(Vector3.up * mouseX);

    }
}
