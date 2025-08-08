using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPauser : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject pauseIcon;
    public bool isUserPaused = false;

    public void OnClick()
    {
        SetIsPaused(!isUserPaused);
    }

    public void SetIsPaused(bool paused)
    {
        isUserPaused = paused;
        if (paused) videoPlayer.Pause();
        else videoPlayer.Play();
        pauseIcon.SetActive(paused);
    }
}
