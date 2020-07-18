using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movement : MonoBehaviour
{
    private FMOD.Studio.EventInstance Instance;
    [FMODUnity.EventRef]
    public string FootSteps;

    public CharacterController controller;

    public float Speed = 12f;

    public float gravity = -9.81f;

    Vector3 velotity;
    bool IsGrounded;

    public Transform Groundceck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;
    // Start is called before the first frame update
    void Start()
    {
        Instance = FMODUnity.RuntimeManager.CreateInstance(FootSteps);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
        if(controller.isGrounded == true && controller.velocity.magnitude > 2f)
        {
            Debug.Log("Foot Steps");
            Instance.start();
        }
    }

    private void Movement()
    {
        IsGrounded = Physics.CheckSphere(Groundceck.position, GroundDistance, GroundMask);
        if (IsGrounded && velotity.y < 0)
        {
            velotity.y = -2f;
        }
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * X + transform.forward * Z;

        controller.Move(move * Speed * Time.deltaTime);

        velotity.y += gravity * Time.deltaTime;
        controller.Move(velotity * Time.deltaTime);
    }
}
