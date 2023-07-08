using UnityEngine;

public class CrystalController : MonoBehaviour
{
    public Material sunorbOn; // Het materiaal van het kristal wanneer het oplicht
    public Material sunorbOff;

    private Renderer crystalRenderer; // De renderer-component van het kristal

    private void Start()
    {
        // Haal de renderer-component op van het kristal
        crystalRenderer = GetComponent<Renderer>();

        // Zet het materiaal van het kristal naar het standaardmateriaal
        crystalRenderer.material = sunorbOff;
    }

    public void IlluminateCrystal()
    {
        // Verander het materiaal van het kristal naar het lichtgevende materiaal
        crystalRenderer.material = sunorbOn;

        // Start een timer om na 1 seconde het materiaal terug te veranderen naar sunorbOff
        Invoke("ResetCrystalMaterial", 1f);
    }

    private void ResetCrystalMaterial()
    {
        // Verander het materiaal van het kristal terug naar sunorbOff
        crystalRenderer.material = sunorbOff;
    }
}
