using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class Game_Manager : MonoBehaviour
{
    private bool _gameOver = false;
    public static Game_Manager instance;
    public float gameSpeed = 1f; //m/s
    private float obstacleSpawnDistance = 20f;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private ParticleSystem _crashParticles;
    private Random _random = new Random();
    public float speedIncrement = 1f;
    private Audio_Manager _audioManager;
    [SerializeField] GameObject _bg;
    private static Vector3 _bgPosition = new Vector3(35.91f,10,4);
    public int score = 0;
    private float _spawnTimer;
    private float prePauseGameSpeed;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _gameOverMenu;
    public bool paused = false;
    private void Awake()
    {
        _gameOverMenu.SetActive(false);
        _pauseMenu.SetActive(false);
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        _audioManager = GetComponent<Audio_Manager>();
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine(ObstacleSpawn());
        
    }

    // Update is called once per frame
    void Update()
    {
        _bg.transform.Translate(Vector3.left * (gameSpeed * Time.deltaTime));
        
        
        if (_bg.transform.position.x <= -20.52f)
        {
            
            _bg.transform.position = _bgPosition;
        }

        if (!_gameOver && !paused)
        {
            if(_spawnTimer > 0)
                _spawnTimer -= Time.deltaTime;
            else
            {
                gameSpeed += speedIncrement;
                var t = obstacleSpawnDistance / gameSpeed;
                var tA = t + UnityEngine.Random.Range(-t/5, t/5);
                SpawnObstacle();
                print("spawning" + tA);
                
                _spawnTimer = tA;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused)
                Resume();
            else
                Pause();
            paused = !paused;
            
        }

        

    }
    
    
    private void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, transform.position, Quaternion.identity);   
    }
    
    
    public void ObstacleHit()
    {
        ParticleSystem crashP = Instantiate(_crashParticles, GameObject.FindGameObjectsWithTag("Player")[0].transform.position, Quaternion.identity);
        crashP.Play();
        
        _audioManager.PlayObstacleHitSound();
        GameOver();
    }
    
    public void GameOver()
    {
        _gameOverMenu.SetActive(true);
        _gameOver = true;
        print("game over");
        var player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player_Movement>();
        gameSpeed = 0f;
        player.OnDeath();
    }
    
    public void AddScore()
    {
        
        if(!_gameOver) score += 1;
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        prePauseGameSpeed = gameSpeed;
        gameSpeed = 0f;
        GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player_Movement>()._runningParticles.Pause();
        
    }
    
    public void Resume()
    {
        _pauseMenu.SetActive(false);
        gameSpeed = prePauseGameSpeed;
        var player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player_Movement>();
        if (!player._isJumping) player._runningParticles.Play();
        
    }

}
