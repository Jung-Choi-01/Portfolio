using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string parameterName;

    public void OnValueChanged(float amount)
    {
        mixer.SetFloat(parameterName, amount);
    }
}
