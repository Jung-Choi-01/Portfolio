using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPanel : MonoBehaviour
{
    [SerializeField] private CameraStartMove cameraStartMove;
    [SerializeField] private float duration;
    private bool rotating;
    private float startTime;

    public void OnClick()
    {
        cameraStartMove.CameraStartMoving();
        startTime = Time.time;
        rotating = true;
    }

    void Update()
    {
        if(!rotating) return;
        float t = (Time.time-startTime)/duration;
        if (t > 1f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            rotating = false;
            Destroy(gameObject);
        }
        else transform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(0f, 90f, Mathf.SmoothStep(0f, 1f, t)), 0f, 0f));
    }
}
