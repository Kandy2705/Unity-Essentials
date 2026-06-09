using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Day/Night Settings")]
    [Tooltip("How many real seconds it takes for one full day to pass.")]
    [SerializeField] private float secondsPerDay = 120f;

    [Tooltip("Starting time of day in degrees. 0 = sunrise, 90 = noon, 180 = sunset, 270 = midnight.")]
    [SerializeField] private float startAngle = 0f;

    private float currentAngle;

    private void Start()
    {
        currentAngle = startAngle;
        ApplyRotation();
    }

    private void Update()
    {
        if (secondsPerDay <= 0f) return;

        float degreesPerSecond = 360f / secondsPerDay;
        currentAngle += degreesPerSecond * Time.deltaTime;
        currentAngle %= 360f;

        ApplyRotation();
    }

    private void ApplyRotation()
    {
        transform.rotation = Quaternion.Euler(currentAngle, 170f, 0f);
    }
}