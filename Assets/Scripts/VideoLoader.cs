using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingObject;
    [SerializeField] private VideoPauser pauser;
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        loadingObject.SetActive(!videoPlayer.isPlaying && !pauser.isUserPaused);
    }
}
