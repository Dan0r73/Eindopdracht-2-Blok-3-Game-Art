using UnityEngine;
using UnityEngine.UI;

public class DayNightController : MonoBehaviour
{
    public Light directionalLight;
    public Light moonLight;
    public Button switchButton;

    public float rotationSpeedDay = 1f;
    public float rotationSpeedNight = 5f;
    public Vector3 targetAnglesDay = new Vector3(140f, 20f, 20f);
    public Vector3 targetAnglesNight = new Vector3(-90f, 0f, 0f);

    private bool isDay = true;
    private Quaternion targetRotation;
    private float targetMoonIntensity = 0f;
    private float currentMoonIntensity = 0f;
    private float intensityThreshold = 0.01f; // Drempelwaarde voor intensiteit

    private void Start()
    {
        switchButton.onClick.AddListener(SwitchDayNight);
        UpdateTargetRotation();
        directionalLight.transform.rotation = targetRotation;
        moonLight.intensity = 0f;
    }

    private void Update()
    {
        directionalLight.transform.rotation = Quaternion.Slerp(directionalLight.transform.rotation, targetRotation, GetRotationSpeed() * Time.deltaTime);

        if (Mathf.Abs(currentMoonIntensity - targetMoonIntensity) > intensityThreshold)
        {
            currentMoonIntensity = Mathf.Lerp(currentMoonIntensity, targetMoonIntensity, GetRotationSpeed() * Time.deltaTime);
            moonLight.intensity = Mathf.Clamp01(currentMoonIntensity);
        }
        else
        {
            currentMoonIntensity = targetMoonIntensity;
            moonLight.intensity = Mathf.Clamp01(currentMoonIntensity);
        }
    }

    private void SwitchDayNight()
    {
        isDay = !isDay;
        UpdateTargetRotation();

        if (isDay)
        {
            targetMoonIntensity = 0f;
            directionalLight.intensity = 1f;
        }
        else
        {
            targetMoonIntensity = 1f;
            directionalLight.intensity = 0f;
        }
    }

    private void UpdateTargetRotation()
    {
        if (isDay)
        {
            targetRotation = Quaternion.Euler(targetAnglesDay);
        }
        else
        {
            targetRotation = Quaternion.Euler(targetAnglesNight);
        }
    }

    private float GetRotationSpeed()
    {
        if (isDay)
        {
            return rotationSpeedDay;
        }
        else
        {
            return rotationSpeedNight;
        }
    }
}
