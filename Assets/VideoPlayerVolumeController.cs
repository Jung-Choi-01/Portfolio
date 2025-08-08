using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerVolumeController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    public void OnValueChanged(float amount)
    {
        videoPlayer.SetDirectAudioVolume(0, amount);
    }
}
