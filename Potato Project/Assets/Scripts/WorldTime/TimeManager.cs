using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged; // Event to notify when the minute changes
    public static Action OnHourChanged;   // Event to notify when the hour changes
    
    public static int Minute { get; private set; } // Current minute in the world time
    public static int Hour { get; private set; }   // Current hour in the world time

    private float minuteToRealTime = 0.5f; // How many seconds in real time for one minute in the game world
    private float timer;

    public float speedMultiplier = 1.0f;
    public int dayTime = 8;
    public int nightTime = 20;

    public GameObject fadeOut;
    public string sceneName;
    public bool canStartDay = true;

    private DayChecker dC;
    public PotatoFarming[] potatoes; 
    public bool timeCanProgress = true;

    void Start()
    {
        Minute = 00; // Initialize minute
        Hour = 8;   // Initialize hour
        timer = minuteToRealTime;
        dC = GetComponent<DayChecker>();
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeCanProgress)
        {
            timer -= Time.fixedDeltaTime * speedMultiplier; // Decrease the timer by the time passed since last frame
            if (timer <= 0) // If the timer has reached zero
            {
                Minute++;
                OnMinuteChanged?.Invoke(); 
                if (Minute >= 60)
                {
                    Minute = 0; // Reset minute to 0
                    Hour++; // Increment hour
                    OnHourChanged?.Invoke();
                }

                timer = minuteToRealTime; // Reset the timer to the duration of one minute in real time
            
            }

            if (Hour >= 20 && canStartDay)
            {
                //End Day
                StartCoroutine(EndDay(sceneName));
                canStartDay = false;
            }
        }
            
    }

    public IEnumerator EndDay(string nameScene)
    {
        fadeOut.SetActive(true);
        //PlayerPrefs.Save();
        Debug.Log(sceneName);
        yield return new WaitForSeconds(3f); 
        Minute = 00; // Initialize minute
        Hour = 8;   // Initialize hour   
        dC.Day++;
        foreach (var potato in potatoes)
        {
            potato.hasDayBeenCounted = false;
        }
        SceneManager.LoadScene(nameScene);
        canStartDay = true;
        
    }
}
