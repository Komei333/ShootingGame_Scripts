using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    [SerializeField] GameObject score;

    private Text scoreText;
    private bool hitBonusStop = false;
    private int totalScore = 0;

    private int hitValue = 1;  //�A���q�b�g���̃{�[�i�X�l
    public int eventValue = 1;  //�C�x���g���̃{�[�i�X�l
    public int prizeValue = 100;  //�i�i�̊l���X�R�A�̒l


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
        totalScore += prizeValue * hitValue * eventValue;

        //�A�˃{�[�i�X��5�{�܂�
        if (hitValue < 5 && hitBonusStop == false)
        {
            hitValue++;
        }

        //�A�˃{�[�i�X�ɂ���ĉ���ς���
        if (hitValue == 1)
        {
            musicManager.PrizeSE1();
        }
        else if (hitValue == 2)
        {
            musicManager.PrizeSE2();
        }
        else if (hitValue == 3)
        {
            musicManager.PrizeSE3();
        }
        else if (hitValue == 4)
        {
            musicManager.PrizeSE4();
        }
        else if (hitValue == 5)
        {
            musicManager.PrizeSE5();
        }
    }

    public void RainbowSE()
    {
        musicManager.PlaySE3();
    }

    public void HitReset()
    {
        if(hitBonusStop == false)
        {
            hitValue = 1;
        }
    }

    public void HitBounusReturn()
    {
        hitBonusStop = false;
    }

    public void HitBounusStop()
    {
        hitBonusStop = true;
        Invoke("HitBounusReturn", 1.0f);
    }

    void EndEvent()
    {
        eventValue = 1;
    }

    public void ScoreUpEvent()
    {
        eventValue = 2;
        Invoke("EndEvent", 10f);
    }

    public void ScoreGetEvent()
    {
        totalScore += 10000;
    }

    public void ScoreGetEvent2()
    {
        totalScore += 15000;
    }

    public void prizeUpEvent()
    {
        prizeValue += 50;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", totalScore);
        PlayerPrefs.Save();    
    }
}
