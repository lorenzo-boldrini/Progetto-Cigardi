using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Animator _anim;
    Rigidbody _RG;
    CharacterController _CC;

    public float Speed = 12f;

    public float gravity = -9.81f;


    Vector3 velotity;
    bool IsGrounded;

    public Transform Groundceck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    public float RunBarCounter = 20;

    public float LRSpeed;
    // Start is called before the first frame update
    public void Start()
    {
        _RG = GetComponent<Rigidbody>();
        _CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame

    private void Update()
    {

        if (Input.GetButton("Run") == false && RunBarCounter <= 20)
        {
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
        Test();
       
    }


  
    void Test()
    {



           
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");


        _anim.SetFloat("moveZ", X);


        _anim.SetFloat("moveX", Speed);


        Vector3 move = transform.right * X + transform.forward * Speed;

        _CC.Move(move * LRSpeed * Time.deltaTime);

        if (Input.GetAxis("Vertical") > 0.1f)
        {
            Speed = Z;
        }
        if (Input.GetButton("Run") && Input.GetAxis("Vertical") != 0 && RunBarCounter > 0)
        {
            Speed = Z * 4;
            RunBarCounter -= 1 * Time.deltaTime;
        }
        else if(Input.GetAxis("Vertical") == 0)
        {
            Speed = 0;
        }
       

    }
   
}
