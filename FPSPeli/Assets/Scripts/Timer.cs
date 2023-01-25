using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public static Timer instance;

    public Text timeCounter;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;
    public GameObject gameComplete;
    public Text yourTime;
    public GameObject pauseMenuUI;
    private float bestTime;
    public Text recordTime;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeCounter.text = "Time: 00:00.00";
        timerGoing = false;
        bestTime = PlayerPrefs.GetFloat(nameof(bestTime));
        BeginTimer();
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    private void Update()
    {
         if (bestTime > elapsedTime)
            {
                bestTime = elapsedTime;
                string bestTimeStr = "Best time: " + bestTime.ToString("mm':'ss'.'ff");
                recordTime.text = bestTimeStr;
                PlayerPrefs.SetFloat(nameof(bestTime), elapsedTime);
                PlayerPrefs.Save();
                
            }
    }

    public void EndTimer()
    {
        timerGoing = false;
        gameComplete.SetActive(true);
        Destroy(timeCounter);
        Destroy(pauseMenuUI);
        Time.timeScale = 0f;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
            yourTime.text = timePlayingStr;
            yield return null;
        }
    }
}
