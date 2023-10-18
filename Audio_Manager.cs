using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager instance;
    
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _landSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _bgMusic;
    [SerializeField]private AudioSource _audioSource;
    [SerializeField]private AudioSource _bgAudioSource;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        BeginPlay();
        
    }

    void BeginPlay()
    {
        _bgAudioSource.clip = _bgMusic;
        _bgAudioSource.Play();
        
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayJumpSound()
    {
        _audioSource.clip = _jumpSound;
        _audioSource.Play();
    }
    public void PlayLandSound()
    {
        _audioSource.clip = _landSound;
        _audioSource.Play();
        
    }
    
    public void PlayObstacleHitSound()
    {
        _audioSource.clip = _hitSound;
        _audioSource.Play();
        _bgAudioSource.Stop();
    }
}
