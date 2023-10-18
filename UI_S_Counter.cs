using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_S_Counter : MonoBehaviour
{
    private Game_Manager gameManager;
    [SerializeField] private TMPro.TextMeshProUGUI _scoreText;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = Game_Manager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = gameManager.score.ToString();
    }
}
