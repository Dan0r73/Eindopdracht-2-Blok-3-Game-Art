using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    public static DayNightManager Instance;

    public delegate void DayNightChangedDelegate(bool isDay);
    public event DayNightChangedDelegate OnDayNightChanged;

    public Light sunLight; // Referentie naar het zonlicht
    public Light moonLight; // Referentie naar het maanlicht

    private bool isDay = true; // Voorbeeldwaarde, pas aan op basis van je eigen logica

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Andere initialisatielogica...

        // Abonneer op de OnDayNightChanged gebeurtenis
        OnDayNightChanged += HandleDayNightChanged;

        // Simuleer het detecteren van dag/nacht-veranderingen
        InvokeRepeating("SimulateDayNightChange", 5f, 10f);
    }

    private void SimulateDayNightChange()
    {
        // Voorbeeld: Willekeurige simulatie van dag/nacht-veranderingen
        isDay = !isDay;

        // Roep de gebeurtenis aan om andere scripts, zoals het CrystalController-script, op de hoogte te stellen
        OnDayNightChanged?.Invoke(isDay);
    }

    private void HandleDayNightChanged(bool isDay)
    {
        if (isDay)
        {
            // Het is dag, voer de acties uit die moeten plaatsvinden tijdens de dag
            // Hier kun je het zonlicht inschakelen en het maanlicht uitschakelen
        }
        else
        {
            // Het is nacht, voer de acties uit die moeten plaatsvinden tijdens de nacht
            // Hier kun je het maanlicht inschakelen en het zonlicht uitschakelen
        }
    }
}
