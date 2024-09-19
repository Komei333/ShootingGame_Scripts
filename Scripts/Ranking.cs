using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [SerializeField, Header("��ʂɕ\������e�L�X�g")]
    Text[] rankingText = new Text[5];

    string[] ranking = { "1��", "2��", "3��", "4��", "5��" };
    int[] rankingValue = new int[5];

    void Start()
    {
        GetRanking();

        if(SceneManager.GetActiveScene().name == "ResultScene")
        {
            SetRanking(PlayerPrefs.GetInt("Score"));
        }

        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = ranking[i] + " : " + rankingValue[i].ToString();
        }
    }

    private void GetRanking()
    {
        //�����L���O�Ăяo��
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
        }
    }

    private void SetRanking(int value)
    {
        //�������ݗp
        for (int i = 0; i < ranking.Length; i++)
        {
            //�擾�����l��Ranking�̒l���r���ē���ւ�
            if (value > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = value;
                value = change;
            }
        }

        //����ւ����l��ۑ�
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
    }
}
