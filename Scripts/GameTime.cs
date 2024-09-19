using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour
{
    [SerializeField] Score score;
    [SerializeField] GameObject time;

    public float countDownTime = 60;
    private Text timeText;

    void Start()
    {
        timeText = time.GetComponent<Text>();
    }

    void Update()
    {
        countDownTime -= Time.deltaTime;

        var timeSpan = new TimeSpan(0, 0, (int)countDownTime);
        timeText.text = timeSpan.ToString(@"mm\:ss");

        if (countDownTime <= 0)
        {
            score.SaveScore();
            SceneManager.LoadScene("ResultScene");
        }
    }
}
