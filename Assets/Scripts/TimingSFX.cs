using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TimingSFX : MonoBehaviour
{
    [SerializeField] private AudioClip chargingSFX;
    [SerializeField] private AudioClip BadTimingSFX;
    [SerializeField] private AudioClip OkTimingSFX;
    [SerializeField] private AudioClip GoodTimingSFX;
    [SerializeField] private AudioClip PerfextTimingSFX;

    private AudioSource gradasTimingSFXAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        gradasTimingSFXAudioSource = GetComponent<AudioSource>();
        gradasTimingSFXAudioSource.clip = chargingSFX;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartChargeSFX()
    {
        gradasTimingSFXAudioSource.PlayOneShot(chargingSFX);
    }

    public void PlayTimingSFX(RootRegion.QualityTiming qualityTiming)
    {
        gradasTimingSFXAudioSource.Stop();
        switch (qualityTiming)
        {
            case RootRegion.QualityTiming.Bad:
                gradasTimingSFXAudioSource.PlayOneShot(BadTimingSFX);
                break;

            case RootRegion.QualityTiming.Ok:
                gradasTimingSFXAudioSource.PlayOneShot(OkTimingSFX);
                break;

            case RootRegion.QualityTiming.Good:
                gradasTimingSFXAudioSource.PlayOneShot(GoodTimingSFX);
                break;

            case RootRegion.QualityTiming.Perfect:
                gradasTimingSFXAudioSource.PlayOneShot(PerfextTimingSFX);
                break;

            default:
                break;
        }
    }
}
