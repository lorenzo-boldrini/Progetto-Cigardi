using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_FootSteps : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter StepSound;
    public void SoundStep()
    {
        StepSound.Play();
    }
}
