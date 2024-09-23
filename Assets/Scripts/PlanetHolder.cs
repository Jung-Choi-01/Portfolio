using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlanetHolder : MonoBehaviour
{
    [SerializeField] private float rotateTime;
    [SerializeField] private float[] rotationTargets;
    private float startRotateTime;
    private bool rotating;
    private float targetRotation;
    private float startRotation;
    private int currentTarget;
    // menu controll stuff
    [SerializeField] private MenuController menuController;

    // navbar control stuff
    [SerializeField] private PortfolioEntryList[] entryLists;
    [SerializeField] private Navbar navbar;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip moveSound;

    void Start()
    {
        currentTarget = 0;
        targetRotation = 0f;
        rotating = false;
        navbar.SetCurrentList(entryLists[currentTarget]);
    }

    void Update()
    {
        if(!rotating) return;
        float t = (Time.time-startRotateTime)/(rotateTime);
        // Debug.Log(t);
        if(t > 1f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, targetRotation, 0f));
            rotating = false;
            navbar.SetCurrentList(entryLists[currentTarget]);
            menuController.OpenMenu();
        }
        else transform.rotation = Quaternion.Euler(new Vector3(0f, Mathf.Lerp(startRotation, targetRotation, Mathf.SmoothStep(0f, 1f, Mathf.SmoothStep(0f, 1f, t))), 0f));
    }

    public void SetRotationTarget(int target)
    {
        if(currentTarget == target) return;
        audioSource.PlayOneShot(moveSound);

        currentTarget = target;
        startRotation = transform.rotation.eulerAngles.y;
        rotating = true;
        startRotateTime = Time.time;
        targetRotation = rotationTargets[target];
        menuController.CloseMenu();
    }
}

[Serializable]
public class PortfolioEntry // class, not struct, because you can't serialize structs
{
    public string titleText;
    public string videoUrl;
    public string yapText;
    public string toolsText;
}

[Serializable]
public class PortfolioEntryList // necessary because unity can't serialize 2d arrays
{
    public PortfolioEntry[] entries;
}