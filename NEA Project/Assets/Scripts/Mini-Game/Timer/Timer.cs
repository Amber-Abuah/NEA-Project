using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    float currentTime;
    float timeElapsed;
    int maxTime;
    bool isEndOfGame;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] EndOfGameManager endOfGameManager;

    public float TimeElapsed { get { return timeElapsed; } }

    void Update()
    {
        if (!isEndOfGame)
        {
            // Used to keep track of time in play session
            timeElapsed += Time.deltaTime;

            // Used for the countdown timer
            currentTime -= Time.deltaTime;

            // Nested ternary operator, used to display the correct amount of digits to the UI 
            timerText.text = (currentTime > 100) ? currentTime.ToString("000") : (currentTime > 10) ? currentTime.ToString("00") : currentTime.ToString("0");

            if (currentTime <= 0)
                GameOver();
        }
    }

    public void SetMaxTime(float time)
    {
        maxTime = (int)time;
        currentTime = time;
    }

    public void ModifyTime(float amount)
    {
        currentTime += amount;
    }

    public void StopTimer()
    {
        isEndOfGame = true;
    }

    void GameOver()
    {
        StopTimer();
        endOfGameManager.GameOver();
    }
}