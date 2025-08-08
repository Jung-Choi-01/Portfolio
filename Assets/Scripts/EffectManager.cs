using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EffectManager : MonoBehaviour
{
    [Header("camera shake")]
    [SerializeField] private float cameraShakeInterval;
    [SerializeField] private float cameraShakeMax;
    [SerializeField] private AnimationCurve effectCurve;

    [Header("sfx volume")]
    [SerializeField] private float audioMax;
    [SerializeField] private AudioSource[] audioSources;

    [Header("sfx distortion")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private float distortionMax;
    private float effectTime;
    private bool started;
    private float timeStarted;

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
            mixer.SetFloat("SFXDistortion", effectCurve.Evaluate((Time.time - timeStarted) / effectTime) * distortionMax);
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
