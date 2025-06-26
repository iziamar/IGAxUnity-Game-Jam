using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappynessBar : MonoBehaviour
{

    [SerializeField, Range(0, 100)] private float currentHappiness = 100;
    [SerializeField] private int maxHappiness = 100;
    
    [SerializeField] private Slider happinessSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Gradient fillGradient;

    [SerializeField] private float decayPerSecond = 0.1f; // Rate of happiness decay per second
  
    public void SetHappiness(float value)
    {
        currentHappiness = Mathf.Clamp(value, 0, maxHappiness);
        UpdateBar();
    }

    public void AddHappiness(int value)
    {
        SetHappiness(currentHappiness + value);
    }

    public float GetHappiness()
    {
        return currentHappiness;
    }

    private void Awake()
    {
        if (happinessSlider == null)
        {
            Debug.LogError("HappynessBar: Slider component not found!");
        }
        else
        {
            happinessSlider.minValue = 0;
            happinessSlider.maxValue = maxHappiness;
            happinessSlider.wholeNumbers = false;
            UpdateBar();
        }
    }

    public void Update()
    {
        if (happinessSlider != null && currentHappiness > 0f)
        {
            
            currentHappiness = Mathf.Max(0f, currentHappiness - decayPerSecond * Time.deltaTime);
            UpdateBar();
        }
    
    }

    private void UpdateBar()
    {
        happinessSlider.value = currentHappiness;
        if (fillImage != null && fillGradient != null)
        {
            fillImage.color = fillGradient.Evaluate(currentHappiness/maxHappiness);
        }

    }

}
