using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Canvas menuCanvas;

    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private Text scoreText;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        gameCanvas.enabled = false;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void StartGame()
    {
        menuCanvas.enabled = false;
        gameCanvas.enabled = true;
    }
}
