using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [SerializeField, Header("画面に表示するテキスト")]
    Text[] rankingText = new Text[5];

    string[] ranking = { "1位", "2位", "3位", "4位", "5位" };
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
        //ランキング呼び出し
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
        }
    }

    private void SetRanking(int value)
    {
        //書き込み用
        for (int i = 0; i < ranking.Length; i++)
        {
            //取得した値とRankingの値を比較して入れ替え
            if (value > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = value;
                value = change;
            }
        }

        //入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
    }
}
