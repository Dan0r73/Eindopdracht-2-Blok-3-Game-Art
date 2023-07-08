using UnityEngine;
using UnityEngine.UI;

public class ButtonRotation : MonoBehaviour
{
    public Button switchButton;
    public float rotationAngle = 180f;
    public float rotationSpeed = 5f;

    private bool isDay = true;
    private bool buttonClicked = false;
    private Quaternion targetRotation;
    private float rotationDirection = 1f;
    private CrystalController crystalController;

    private void Start()
    {
        switchButton.onClick.AddListener(SwitchDayNight);
        crystalController = GameObject.Find("Sunorb").GetComponent<CrystalController>();
        DayNightManager.Instance.OnDayNightChanged += HandleDayNightChanged;
    }

    private void Update()
    {
        if (buttonClicked)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (transform.rotation == targetRotation)
            {
                buttonClicked = false;
            }
        }
    }

    private void SwitchDayNight()
    {
        if (buttonClicked)
        {
            return;
        }

        isDay = !isDay;
        rotationDirection *= -1f;

        if (isDay)
        {
            targetRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            targetRotation = Quaternion.Euler(0f, 0f, rotationAngle * rotationDirection);
        }

        crystalController.IlluminateCrystal();

        buttonClicked = true;

        UpdateTargetRotation(); // Call UpdateTargetRotation when the button is clicked for the first time
    }

    private void HandleDayNightChanged(bool isDay)
    {
        this.isDay = isDay;
        UpdateTargetRotation();
    }

    private void UpdateTargetRotation()
    {
        if (buttonClicked)
        {
            if (isDay)
            {
                targetRotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                targetRotation = Quaternion.Euler(0f, 0f, rotationAngle * rotationDirection);
            }
        }
    }
}
