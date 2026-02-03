using UnityEngine;
[RequireComponent (typeof(AudioSource))]

public class playAudio : MonoBehaviour
{
    private AudioSource audioSource;  
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            audioSource.Play();
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
