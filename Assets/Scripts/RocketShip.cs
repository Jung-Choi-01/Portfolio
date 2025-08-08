using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float extraForwardSpeed;
    [SerializeField] private ParticleSystem rocketFlames;
    private bool started;
    private float timeStarted;

    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.forward * rotationSpeed * Time.deltaTime);

        if (!started) return;
        transform.position += transform.forward * extraForwardSpeed * Time.deltaTime * (Time.time - timeStarted) * 2;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.right * rotationSpeed * Time.deltaTime);

        if (Time.time - timeStarted >= 5f) Destroy(gameObject);
    }

    public void BeginFlying()
    {
        started = true;
        timeStarted = Time.time;
        rocketFlames.Play();
    }
}
