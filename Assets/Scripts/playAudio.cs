using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class playAudio : MonoBehaviour
{
    public float fadeTimeInSeconds = 1;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(FadeIn(true));
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            audioSource.Stop();
        }
    }
   
    private IEnumerator FadeIn(bool fadeIn)
    {
        float timer = 0;
        audioSource.Play();
        while (timer < fadeTimeInSeconds)
        {
            audioSource.volume = Mathf.Lerp(0, 1, timer / fadeTimeInSeconds);
            timer += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 1;

    }
    private IEnumerator FadeAudio(bool fadeIn)
    {
        float timer = 0;
        float start = fadeIn ? 0 : 1;
        float end = fadeIn ? 1 : 0;
        if (fadeIn) audioSource.Play();
        while (timer < fadeTimeInSeconds) {
            {
                audioSource.volume = Mathf.Lerp(start, end, timer / fadeTimeInSeconds);
                timer += Time.deltaTime;
                yield return null;
            }
            audioSource.volume = 0;
            if (!fadeIn) audioSource.Pause();
        }
    }
}
