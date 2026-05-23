using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private AudioSource source;

    public AudioClip hitSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Obstacle.OnObstacleHit += PlayHitsound;
    }

    private void OnDisable()
    {
        Obstacle.OnObstacleHit -= PlayHitsound;
    }

    private void PlayHitsound()
    {
        source.PlayOneShot(hitSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
