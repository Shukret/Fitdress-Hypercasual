using System;
using DG.Tweening;
using Game;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioController : MonoBehaviour
{
    private static AudioController Intstance;
    public static event Action<AudioAction> onClick;
    
    [Header("Clicks")]
    public Sound click;
    public Sound pickUp;
    public Sound finish;
    public Sound fail;
    
    [Space(5)]
    [Header("Audio Sources")]
    public AudioSource soundSource;
    public AudioSource musicSource;
    
    [Space(5)]
    [Header("Music Audio Clips")]
    public AudioClip musicClip;
    public float volume = 0.75f;
    public float fadeVolume = 0.3f;
    
    private void Awake()
    {
        if (!Intstance) 
        {
            DontDestroyOnLoad(gameObject); 
            Intstance = this;
            
            musicSource.clip = musicClip;
            musicSource.Play();
            
            musicSource.volume = fadeVolume;
            musicSource.GetComponent<AudioLowPassFilter>().cutoffFrequency = 1000;
        }
        else
        {
            if (Intstance != this)
            {
                Destroy(gameObject);
                
                return;
            }
        }
        
        RegisterEvents();
    }
    
    public void OnDisable()
    {
        UnregisterEvents();
    }

    private void RegisterEvents()
    {
        onClick += PlaySound;
        
        GameEvents.onGameStateChange += OnGameStateChange;
    }

    private void UnregisterEvents()
    {
        onClick -= PlaySound;
        
        GameEvents.onGameStateChange -= OnGameStateChange;
    }
    
    public static void PlayAction(AudioAction action)
    {
        onClick?.Invoke(action);
    }
    
    private Tween _lowPassFade;
    private void OnGameStateChange(GameState state)
    {
        if (state == GameState.Start)
        {
            musicSource.DOFade(fadeVolume, 0.5f);
            musicSource.GetComponent<AudioLowPassFilter>().cutoffFrequency = 1000;
        }
        else if (state == GameState.Play)
        {
            musicSource.DOKill();
            musicSource.DOFade(volume, 0.5f);
            var lowPass = musicSource.GetComponent<AudioLowPassFilter>();
            _lowPassFade?.Kill();
            _lowPassFade = DOTween.To(() => lowPass.cutoffFrequency, x => lowPass.cutoffFrequency = x, 22000, 0.5f);
            
            MMVibrationManager.Haptic (HapticTypes.HeavyImpact, true, this);
        }
        else if (state == GameState.Finish)
        {
            musicSource.DOKill();
            musicSource.DOFade(0f, 1f);
            
            //PlaySound(AudioAction.Finish);
        }
        else if (state == GameState.Fail)
        {
            musicSource.DOKill();
            musicSource.DOFade(fadeVolume, 0.5f);
            var lowPass = musicSource.GetComponent<AudioLowPassFilter>();
            _lowPassFade?.Kill();
            _lowPassFade = DOTween.To(() => lowPass.cutoffFrequency, x => lowPass.cutoffFrequency = x, 1000, 0.5f);
            
            //PlaySound(AudioAction.Fail);
        }
    }


    public void PlaySound(AudioAction action)
    {
        switch (action)
        {
            case AudioAction.Click:
                soundSource.volume = click.volume;
                MMVibrationManager.Haptic (HapticTypes.Selection, true, this);
                soundSource.PlayOneShot(click.clip);
                break;
            case AudioAction.PickUp:
                soundSource.volume = pickUp.volume;
                MMVibrationManager.Haptic (HapticTypes.MediumImpact, true, this);
                soundSource.PlayOneShot(pickUp.clip);
                break;
            case AudioAction.Finish:
                soundSource.volume = finish.volume;
                soundSource.PlayOneShot(finish.clip);
                break;
            case AudioAction.Fail:
                soundSource.volume = fail.volume;
                soundSource.PlayOneShot(fail.clip);
                break;
        }
    }
    
    [Serializable]
    public struct Sound
    {
        public float volume;
        public AudioClip clip;
    }
}

public enum AudioAction
{
    Click,
    PickUp,
    Finish,
    Fail,
}
