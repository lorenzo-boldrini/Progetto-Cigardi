using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_ia : MonoBehaviour
{
    public NavMeshAgent _NMA;
    public Animator AnimatorController;
    Transform Player;




    // Start is called before the first frame update
    void Start()
    {
        _NMA = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _NMA.destination = Player.position;
        if(_NMA.velocity != null)
        {
            AnimatorController.SetBool("move", true);
        }
        else
        {
            AnimatorController.SetBool("move", false);
        }
    }
}
