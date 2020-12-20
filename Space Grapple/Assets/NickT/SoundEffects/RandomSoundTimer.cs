using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundTimer : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioListener audioListener;

    public float PlayTimeVariation = 2;
    public float PlayTimeInterval = 12;
    
    private float NextPlay;
    private float TimeCount;
    private int CurrentMusic;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        NextPlay = Random.Range(PlayTimeInterval - PlayTimeVariation, PlayTimeInterval + PlayTimeVariation);
    }

    void Update()
    {
        if (TimeCount > NextPlay)
        {
            NextPlay = Random.Range(PlayTimeInterval - PlayTimeVariation, PlayTimeInterval + PlayTimeVariation);
            TimeCount = 0.0f;
            CurrentMusic = 0; //Random.next(music.Length);
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();
        }

        TimeCount += Time.deltaTime;
    }
}
