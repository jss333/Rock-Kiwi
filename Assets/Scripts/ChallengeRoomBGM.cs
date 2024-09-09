using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeRoomBGM : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip firstHalfBGM;
    public AudioClip secondHalfBGM;
    public AudioClip victoryBGM;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StopAndPlayNewClip(firstHalfBGM);
    }

    public void PlaySecondHalfBGM()
    {
        StopAndPlayNewClip(secondHalfBGM);
    }
    public void PlayVictoryBGM()
    {
        StopAndPlayNewClip(victoryBGM);
    }

    private void StopAndPlayNewClip(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
