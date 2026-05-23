using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public delegate void TimerEvent();

    private bool isRacing = false;
    private float raceTime = 0;
    


    private void OnEnable()
    {
        StartGate.TimerStart += StartTimer;
        FinishGate.TimerEnd += StopTimer;
    }
    private void OnDisable()
    {
        StartGate.TimerStart -= StartTimer;
        FinishGate.TimerEnd -= StopTimer;
    }

    private void StartTimer()
    {
        Debug.Log("time started");
        isRacing = true;
    }

    private void StopTimer()
    {
        Debug.Log("time stopped. Race Time = " + raceTime);
        isRacing = false;
    }

    private void Update()
    {
        if (isRacing)
        {
            raceTime += Time.deltaTime;
        }
    }
}
