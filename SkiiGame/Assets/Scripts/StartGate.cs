using System;
using UnityEngine;

public class StartGate : MonoBehaviour
{
    public static event GameManager.TimerEvent TimerStart;
    private void OnTriggerEnter(Collider other)
    {
        TimerStart.Invoke();
    }
}
