using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Animator _anim;
    Rigidbody _RG;

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
        //Animrotaion();
       
    }


   /* void Animrotaion()
    {
        float RotationDirection = oldEulerAnglesY - transform.rotation.eulerAngles.y;
        oldEulerAnglesY = transform.rotation.eulerAngles.y;

        Debug.Log(RotationDirection);
        if(RotationDirection > 1f)
        {
            _anim.SetFloat("Idle", 1);
        }else if(RotationDirection < -1f)
        {
            _anim.SetFloat("Idle", -1);
        }
        else
        {
            _anim.SetFloat("Idle", 0);
        }
        

    }*/
    void Test()
    {



           
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");


        _anim.SetFloat("moveZ", X);

        transform.Translate(Vector3.left * X * LRSpeed * Time.deltaTime * -1);


        _anim.SetFloat("moveX", Speed);




        if (Input.GetAxis("Vertical") > 0.1f)
        {
            Speed = Z;
        }
        if (Input.GetButton("Run") && Input.GetAxis("Vertical") != 0 && RunBarCounter > 0)
        {
            Speed = Z * 2;
            RunBarCounter -= 1 * Time.deltaTime;
        }
        else if(Input.GetAxis("Vertical") == 0)
        {
            Speed = 0;
        }
       

    }
   
}
