using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    [SerializeField] GameObject score;

    private Text scoreText;
    private int totalScore = 0;
    private bool canPlusBounusScoreValue = true;

    private int bonusScoreValue = 1;  //連続ヒット時のボーナス値
    public int eventScoreValue = 1;  //イベント時のボーナス値
    public int prizeScoreValue = 100;  //景品の獲得スコアの値


    void Start()
    {
        scoreText = score.GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = totalScore.ToString();
    }

    public void PrizeScore()
    {
        totalScore += prizeScoreValue * bonusScoreValue * eventScoreValue;

        // 連続ヒットボーナスは5倍まで
        if (bonusScoreValue < 5 && canPlusBounusScoreValue)
        {
            bonusScoreValue++;
        }

        // 連続ヒットボーナスによってSEを変える
        if (bonusScoreValue == 1)
        {
            musicManager.PlayPrizeSE1();
        }
        else if (bonusScoreValue == 2)
        {
            musicManager.PlayPrizeSE2();
        }
        else if (bonusScoreValue == 3)
        {
            musicManager.PlayPrizeSE3();
        }
        else if (bonusScoreValue == 4)
        {
            musicManager.PlayPrizeSE4();
        }
        else if (bonusScoreValue == 5)
        {
            musicManager.PlayPrizeSE5();
        }
    }

    public void RainbowPrizeSE()
    {
        musicManager.PlaySE3();
    }

    public void HitBonusReset()
    {
        // 連続ヒットボーナスの値を初期値に戻す
        bonusScoreValue = 1;
    }

    public void CanPlusBounusScoreValue()
    {
        canPlusBounusScoreValue = true;
    }

    public void StopBonusScoreValue()
    {
        canPlusBounusScoreValue = false;
        Invoke("CanPlusBounusScoreValue", 0.5f);
    }

    void ResetEventScoreValue()
    {
        eventScoreValue = 1;
    }

    public void ScoreUpEvent()
    {
        eventScoreValue = 2;
        Invoke("ResetEventScoreValue", 10f);
    }

    public void ScoreGetEvent()
    {
        totalScore += 3000;
    }

    public void ScoreGetEvent2()
    {
        totalScore += 5000;
    }

    public void prizeScoreUpEvent()
    {
        prizeScoreValue += 50;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", totalScore);
        PlayerPrefs.Save();    
    }
}
