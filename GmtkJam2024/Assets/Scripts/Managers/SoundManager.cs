using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private float _volume; 
    [SerializeField] private AudioSource _soundEffectsSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _volume = SoundManager.Instance.GetCurrentVolume();
    }

    private void Start()
    {
        _volume = SaveDataManager.Instance.GetPlayerData().SoundFxVolume;
    }

    public void PlaySound(AudioClip clip, Vector3 position)
    {
        AudioSource audioSource = Instantiate(_soundEffectsSource, position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = _volume;
        audioSource.Play();
        float clipLength = clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public AudioSource PlayAdjustableSound(AudioClip clip, Vector3 position)
    {
        
        AudioSource audioSource = Instantiate(_soundEffectsSource, position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = _volume;
        audioSource.Play();
        float clipLength = clip.length;
        //Do not destroy here(but where?) as we need to change volume and that gives a lot of errors
        return audioSource;
    }

    public void ChangeMasterVolume(float volume)
    {
        _volume = volume;
        var playerData = SaveDataManager.Instance.GetPlayerData();
        playerData.SoundFxVolume = volume;
        SaveDataManager.Instance.SetPlayerData(playerData);
    }

    public float GetCurrentVolume() { return _volume; }
}
