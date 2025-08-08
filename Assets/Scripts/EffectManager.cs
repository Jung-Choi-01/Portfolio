using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private float cameraShakeInterval;
    [SerializeField] private float cameraShakeMax;
    [SerializeField] private AnimationCurve effectCurve;

    [SerializeField] private float audioMax;
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private float distortionMax;
    private AudioDistortionFilter audioDistortionFilter;
    private float effectTime;
    private bool started;
    private float timeStarted;

    void Awake()
    {
        audioDistortionFilter = GetComponent<AudioDistortionFilter>();
    }

    public void StartShake(float duration)
    {
        effectTime = duration;
        StartCoroutine(DoCameraShake());
        timeStarted = Time.time;
        started = true;
    }

    void Update()
    {
        if (!started) return;
        foreach (AudioSource source in audioSources)
        {
            source.volume = effectCurve.Evaluate((Time.time - timeStarted) / effectTime) * audioMax + 0.108f;
            audioDistortionFilter.distortionLevel = effectCurve.Evaluate((Time.time - timeStarted) / effectTime) * distortionMax;
        }

        if (Time.time - timeStarted > effectTime)
            started = false;
    }

    private IEnumerator DoCameraShake()
    {
        while (Time.time - timeStarted <= effectTime)
        {
            transform.localPosition = effectCurve.Evaluate((Time.time - timeStarted) / effectTime) * cameraShakeMax * Random.onUnitSphere;
            yield return new WaitForSeconds(cameraShakeInterval);
        }
        transform.localPosition = Vector3.zero;
    }
}
