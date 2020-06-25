using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

    [SerializeField] float Speed = 2;
    [SerializeField] float MouseSensibility = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float AsseWS = Input.GetAxis("Vertical");
        float AsseAD = Input.GetAxis("Horizontal");
        float asseXM = Input.GetAxis("Mouse X");
        float asseYM = Input.GetAxis("Mouse Y");
        transform.Translate(AsseAD * Speed * Time.deltaTime, 0, AsseWS * Speed * Time.deltaTime);
        transform.Rotate(asseYM * MouseSensibility, asseXM * MouseSensibility, 0);

    }
}
