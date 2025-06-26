using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText; // Reference to the TextMeshProUGUI component for displaying time
    
    private void OnEnable()
    {
        // Subscribe to the events from WorldTime
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
    }

    private void OnDisable()
    {
        // Unsubscribe from the events to avoid memory leaks
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        timeText.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00}"; 
    }
}
