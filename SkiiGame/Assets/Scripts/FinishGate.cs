using UnityEngine;

public class FinishGate : MonoBehaviour
{
    public static event GameManager.TimerEvent TimerEnd;
    
    private void OnTriggerEnter(Collider other)
    {
        TimerEnd.Invoke();
    }
}
