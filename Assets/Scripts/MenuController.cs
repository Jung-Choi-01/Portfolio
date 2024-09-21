using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MenuController : MonoBehaviour
{
    [SerializeField] private float duration;
    private float startTime;
    private bool rotating;
    private float targetRotation;
    private float startRotation;
    [SerializeField] private VideoPlayer videoPlayer;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openMenuSound;
    [SerializeField] private AudioClip closeMenuSound;
    [SerializeField] private AudioSource typing;

    void Start()
    {
        rotating = false;
        startRotation = 0f;
        typing.volume = 0f;
    }

    void Update()
    {
        if(!rotating) return;
        float t = (Time.time-startTime)/(duration);
        // Debug.Log(t);
        if(t > 1f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(targetRotation, 0f, 0f));
            rotating = false;
        }
        else transform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(startRotation, targetRotation, Mathf.SmoothStep(0f, 1f, t)), 0f, 0f));
    }

    public void OpenMenu()
    {
        startRotation = transform.rotation.eulerAngles.x;
        startTime = Time.time;
        rotating = true;
        targetRotation = 0f;
        audioSource.PlayOneShot(openMenuSound);
        typing.volume = 0.6f;
        videoPlayer.Play();
    }

    public void CloseMenu()
    {
        startRotation = transform.rotation.eulerAngles.x;
        startTime = Time.time;
        rotating = true;
        targetRotation = 90f;
        audioSource.PlayOneShot(closeMenuSound);
        typing.volume = 0f;
        videoPlayer.Pause();
    }
}