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
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y") * mouse_Sesibility * Time.deltaTime;
        float HM = Input.GetAxis("Vertical");
        
        Xrotation -= mouseY;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(Xrotation, 0f, 0f);
        if (HM < 0.1f)
        {
            if (mouseX > 5f || mouseX < -5f)
                _anim.SetFloat("Idle", mouseX);
            else
                _anim.SetFloat("Idle", 0);
        }
       

        playerBody.Rotate(Vector3.up * mouseX * mouse_Sesibility * Time.deltaTime);

    }
}
