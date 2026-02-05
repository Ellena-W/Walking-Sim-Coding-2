using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
[RequireComponent(typeof(AudioSource))]

public class playAudio1 : MonoBehaviour
{
    public float fadeTimeInSeconds = 1;
    private AudioSource audioSource;
    private AudioMixer audioMixer;
    public AudioMixerSnapshot mutedSnapshot;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            audioSource.Stop();
        }
    }
    }
