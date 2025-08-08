using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStartMove : MonoBehaviour
{
    [SerializeField] private float cameraMoveTime;
    [SerializeField] private float cameraMoveDistance;
    [SerializeField] private MenuController menuController;
    [SerializeField] private RocketShip rocketShip;
    [SerializeField] private EffectManager cameraShake;
    private bool moving;
    private Vector3 targetPosition;
    private Vector3 startPosition;

    public void CameraStartMoving()
    {
        moving = true;
        targetPosition = transform.position;
        transform.position = transform.position + -1f * cameraMoveDistance * transform.forward;
        startPosition = transform.position;
        rocketShip.BeginFlying();
        cameraShake.StartShake(cameraMoveTime);
    }

    void Update()
    {
        if(!moving) return;
        float t = Time.time/cameraMoveTime;
        if(t > 1f)
        {
            transform.position = targetPosition;
            moving = false;
            menuController.OpenMenu();
        }
        else transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, t))));
    }
}
