using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    public AudioClip enemySoundClip;
    public float maxHearingDistance = 10f;
    public float maxVolume = 1f;

    private AudioSource audioSource;
    private Transform player;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = enemySoundClip;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        float volume = Mathf.Clamp01(1 - (distanceToPlayer / maxHearingDistance)) * maxVolume;
        audioSource.volume = volume;

        if (distanceToPlayer <= maxHearingDistance && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
