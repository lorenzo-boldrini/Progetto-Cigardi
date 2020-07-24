using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller_busto : MonoBehaviour
{
    public GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.transform.rotation;
    }
}
