using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_time_sound : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter sound;
    float secondcounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        secondcounter += Time.deltaTime;
        if(secondcounter > Random.Range(5, 10))
        {
            sound.Play();
            secondcounter = 0;
        }
    }
}
