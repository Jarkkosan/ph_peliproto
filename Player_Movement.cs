using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float jumpHeight = 1f;
    [SerializeField] private AnimationCurve jumpCurve;
    public bool _isJumping;
    [SerializeField] Game_Manager gameManager;
    public float jumpDistance = 7f;
    private float _jumpTime;
    public Animator _animator;
    [SerializeField] GameObject _player;
    [SerializeField] public ParticleSystem _runningParticles;
    [SerializeField] public ParticleSystem _landingParticles;
    private Audio_Manager _audioManager;
    public bool isDead = false;
    private Game_Manager _gameManager;


    private void Start()
    {
        
        _animator = _player.GetComponent<Animator>();
        _audioManager = Audio_Manager.instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        var paused = gameManager.paused;
        if (!isDead)
        {
            var gamespeed = gameManager.gameSpeed;
            _animator.speed = gamespeed / 3f;



            if (!paused)
            {
                if (Input.GetKeyDown(KeyCode.Space) & !_isJumping)
                {
                    OnJump();
                }

                if (_isJumping)
                {
                    //calculate jump height based on distance travelled
                    var pos = transform.position;
                    var jumpTime = jumpDistance / gamespeed;
                    var yPos = jumpCurve.Evaluate(_jumpTime/jumpTime) * jumpHeight;
                    transform.position = new Vector3(pos.x,yPos, pos.z);
                    _jumpTime += Time.deltaTime;
            
            
            
                    if (_jumpTime > jumpTime)
                    {
                        OnLand();
                    }
                }
            
            }
        }

    }
    
    
    private void OnJump()
    {
        _isJumping = true;
        _jumpTime = 0f;
        _animator.SetBool("isJumping", true);
        _runningParticles.Stop();
        _audioManager.PlayJumpSound();
    }
    private void OnLand()
    {
        _isJumping = false;
        _animator.SetBool("isJumping", false);
        _runningParticles.Play();
        _landingParticles.Play();
        _audioManager.PlayLandSound();
    }
    
    public void OnDeath()
    {
        _animator.SetBool("isDead", true);
        _runningParticles.Stop();
        _landingParticles.Stop();
        isDead = true;
        _animator.speed = 1f;
    }
}
