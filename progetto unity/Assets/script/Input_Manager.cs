using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager INPUTMAN;

    public UnityEvent Walk;
    public UnityEvent Run;
    // Start is called before the first frame update

    private void OnEnable()
    {
        INPUTMAN = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0 && Input.GetAxis("Run") == 0)
        {
            Walk.Invoke();
        }

        if (Input.GetButton("Run") && Input.GetAxis("Vertical") != 0)
        {
            Run.Invoke();
        }
    }
}
