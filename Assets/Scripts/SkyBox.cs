using UnityEngine;
using System.Collections;

public class SkyboxCycle : MonoBehaviour
{
    public Cubemap[] skyboxes; 
    public Material blendedSkyboxMaterial; 
    public float blendDuration = 5f; 
    public float delayBetweenTransitions = 10f;

    private int currentIndex = 0;
    private int nextIndex = 1;

    void Start()
    {
        blendedSkyboxMaterial.SetTexture("_Skybox1", skyboxes[currentIndex]);
        blendedSkyboxMaterial.SetTexture("_Skybox2", skyboxes[nextIndex]);
        blendedSkyboxMaterial.SetFloat("_Blend", 0f);

        RenderSettings.skybox = blendedSkyboxMaterial;
        DynamicGI.UpdateEnvironment();

        StartCoroutine(BlendLoop());
    }

    IEnumerator BlendLoop()
    {
        while (true)
        {
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime / blendDuration;
                blendedSkyboxMaterial.SetFloat("_Blend", Mathf.Clamp01(t));
                DynamicGI.UpdateEnvironment();
                yield return null;
            }

            currentIndex = (currentIndex + 1) % skyboxes.Length;
            nextIndex = (currentIndex + 1) % skyboxes.Length;

            blendedSkyboxMaterial.SetTexture("_Skybox1", skyboxes[currentIndex]);
            blendedSkyboxMaterial.SetTexture("_Skybox2", skyboxes[nextIndex]);
            blendedSkyboxMaterial.SetFloat("_Blend", 0f);
            DynamicGI.UpdateEnvironment();

            yield return new WaitForSeconds(delayBetweenTransitions);
        }
    }
}
