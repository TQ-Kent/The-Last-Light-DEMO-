using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject restartCanvas;
    [SerializeField] GameObject gameCanvas;

    [SerializeField] Text score;
    [SerializeField] Text finalScore;
    [SerializeField] Text health;

    int point = 0;
    void Start()
    {
        Time.timeScale = 1;
        score.text = point.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndGame()
    {
        Time.timeScale = 0;
        restartCanvas.SetActive(true);
        finalScore.text = "Score:" + point.ToString();
        gameCanvas.SetActive(false);
    }

    public void IncreasePoint()
    {
        point++;
        score.text = point.ToString();
    }

    public void HealthUpdate(int playerHealth)
    {
        health.text = playerHealth.ToString();
    }
}
