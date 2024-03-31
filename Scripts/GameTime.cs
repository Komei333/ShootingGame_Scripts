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
    public float countdownSecond = 60;
    private Text timeText;

    void Start()
    {
        timeText = time.GetComponent<Text>();
        countdownSecond += 1;
    }

    void Update()
    {
        countdownSecond -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)countdownSecond);
        timeText.text = span.ToString(@"mm\:ss");

        if (countdownSecond <= 0)
        {
            score.SaveScore();
            SceneManager.LoadScene("ResultScene");
        }
    }
}
