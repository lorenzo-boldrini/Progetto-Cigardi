using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Animator _anim;
    public CharacterController controller;

    public float Speed = 12f;

    public float gravity = -9.81f;


    Vector3 velotity;
    bool IsGrounded;

    public Transform Groundceck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    public float RunBarCounter = 20;


    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame

    private void Update()
    {
        Debug.Log(RunBarCounter);

        if (Input.GetButton("Run") == false && RunBarCounter <= 20)
        {
            Debug.Log("Riprendi il fiato");
            RunBarCounter += 1 * Time.deltaTime;
            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
                RunBarCounter += 1 * Time.deltaTime;
        }



        IsGrounded = Physics.CheckSphere(Groundceck.position, GroundDistance, GroundMask);
        if (IsGrounded && velotity.y < 0)
        {
            velotity.y = -2f;
        }
        velotity.y += gravity * Time.deltaTime;
        controller.Move(velotity * Time.deltaTime);
        Test();
    }

    void Test()
    {
        if (Input.GetAxis("Vertical") != 0 && Input.GetAxis("Run") == 0)
        {

            Speed = 10;
            _anim.SetBool("Run", false);
        }
        if (Input.GetButton("Run") && Input.GetAxis("Vertical") != 0)
        {
            RunBarCounter -= 1 * Time.deltaTime;
            _anim.SetBool("Run", true);
            Speed = 20;
        }



           
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * X + transform.forward * Z;

        controller.Move(move * Speed * Time.deltaTime);

       

        _anim.SetFloat("inputX", Z);
        _anim.SetFloat("inputZ", X);



    }

   
}
