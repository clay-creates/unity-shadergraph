using System.Collections;
using UnityEngine;

public class GlitchControl : MonoBehaviour
{
    public float glitchChance = 0.1f; // Probability of glitch effect occurring
    public float glitchDurationMin = 0.05f;
    public float glitchDurationMax = 0.1f;

    private Material hologramMaterial;
    private WaitForSeconds glitchLoopWait = new WaitForSeconds(0.1f);
    private int baseColorID;
    private int flickerSpeedID;

    void Awake()
    {
        hologramMaterial = GetComponent<Renderer>().material;
        baseColorID = Shader.PropertyToID("_Base_Color");
        flickerSpeedID = Shader.PropertyToID("_Flicker_Speed");
    }

    IEnumerator Start()
    {
        while (true)
        {
            float glitchTest = Random.Range(0f, 1f);
            if (glitchTest <= glitchChance)
            {
                // Store original values
                Color originalBaseColor = hologramMaterial.GetColor(baseColorID);
                float originalFlickerSpeed = hologramMaterial.GetFloat(flickerSpeedID);

                // Apply glitch effect
                hologramMaterial.SetColor(baseColorID, originalBaseColor * Random.Range(0.5f, 1.5f)); // Flicker brightness
                hologramMaterial.SetFloat(flickerSpeedID, originalFlickerSpeed * Random.Range(0.5f, 2f)); // Modify flicker speed

                yield return new WaitForSeconds(Random.Range(glitchDurationMin, glitchDurationMax));

                // Reset to original values
                hologramMaterial.SetColor(baseColorID, originalBaseColor);
                hologramMaterial.SetFloat(flickerSpeedID, originalFlickerSpeed);
            }

            yield return glitchLoopWait;
        }
    }
}
