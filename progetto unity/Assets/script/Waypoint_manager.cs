using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint_manager : MonoBehaviour
{
    Transform Player;
    public float distance;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        distance = Vector3.Distance(Player.position, transform.position);
    }

}
