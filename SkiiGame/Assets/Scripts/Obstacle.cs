using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public delegate void OnHitAction();

    public static event OnHitAction OnObstacleHit;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            OnObstacleHit.Invoke();
    }
}
