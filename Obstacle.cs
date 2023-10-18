using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
        
    private  Game_Manager gameManager;
    private bool _scored = false;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = Game_Manager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        var speed = gameManager.gameSpeed;
        transform.Translate(Vector3.left * (speed * Time.deltaTime));
        
        if (transform.position.x < -9f && !_scored)
        {
            gameManager.AddScore();
            _scored = true;
        }

        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
        
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.ObstacleHit();
           
        }
    }
}
