using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Run_Bar_man : MonoBehaviour
{
    public movement Player;
    float Run_counter;
    float StartNumber;
    Image _image;
    // Start is called before the first frame update
    void Start()
    {
        Run_counter = Player.RunBarCounter;
        StartNumber = Run_counter;
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Run_counter = Player.RunBarCounter / StartNumber;
        _image.fillAmount = Run_counter;
    }
}
