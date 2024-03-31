using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource seAudioSource;

    [SerializeField] AudioMixer audioMixer;

    [SerializeField] AudioClip bgm1;
    [SerializeField] AudioClip bgm2;
    [SerializeField] AudioClip bgm3;

    [SerializeField] AudioClip se1;
    [SerializeField] AudioClip se2;
    [SerializeField] AudioClip se3;

    [SerializeField] AudioClip prizeSE1;
    [SerializeField] AudioClip prizeSE2;
    [SerializeField] AudioClip prizeSE3;
    [SerializeField] AudioClip prizeSE4;
    [SerializeField] AudioClip prizeSE5;

    private bool prizeSEinterval = false;

    public void SetBGM()
    {
        audioMixer.SetFloat("BGM", PlayerPrefs.GetFloat("BGM"));
    }

    public void SetSE()
    {
        audioMixer.SetFloat("SE", PlayerPrefs.GetFloat("SE"));
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    public void StopSE()
    {
        seAudioSource.Stop();
    }

    public void PlayBGM1()
    {
        //bgmAudioSource.PlayOneShot(bgm1);
    }

    public void PlayBGM2()
    {
        //bgmAudioSource.PlayOneShot(bgm2);
    }

    public void PlayBGM3()
    {
        //bgmAudioSource.PlayOneShot(bgm3);
    }

    public void PlaySE1()
    {
        seAudioSource.PlayOneShot(se1);
    }

    public void PlaySE2()
    {
        seAudioSource.PlayOneShot(se2);
    }

    public void PlaySE3()
    {
        seAudioSource.PlayOneShot(se3);
    }

    public void PrizeSE1()
    {
        if (!prizeSEinterval)
        {
            seAudioSource.PlayOneShot(prizeSE1);
            StartInterval();
        }

    }

    public void PrizeSE2()
    {
        if (!prizeSEinterval)
        {
            seAudioSource.PlayOneShot(prizeSE2);
            StartInterval();
        }
    }

    public void PrizeSE3()
    {
        if (!prizeSEinterval)
        {
            seAudioSource.PlayOneShot(prizeSE3);
            StartInterval();
        }
    }

    public void PrizeSE4()
    {
        if (!prizeSEinterval)
        {
            seAudioSource.PlayOneShot(prizeSE4);
            StartInterval();
        }
    }

    public void PrizeSE5()
    {
        if (!prizeSEinterval)
        {
            seAudioSource.PlayOneShot(prizeSE5);
            StartInterval();
        }
    }

    void StartInterval()
    {
        prizeSEinterval = true;
        Invoke("EndIntrval", 0.05f);
    }

    void EndIntrval()
    {
        prizeSEinterval = false;
    }
}