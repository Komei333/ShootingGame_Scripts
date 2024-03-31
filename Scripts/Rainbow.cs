using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rainbow : MonoBehaviour
{
    [SerializeField] Score score;
    [SerializeField] Text rainbowText;

    private float timer = 0.0f;
    private float resetTime = 5.0f;
    private int rand = 0;
    private int previousRand = 0;

    private float power = 500.0f;
    private int destroyCount = 0;
    private bool rainbowFlag = true;


    void Start()
    {
        rainbowText.text = "";

        //ゲーム開始から一秒経過するまで虹色の景品は出現しない
        Invoke("ResetFlag", 1.0f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= resetTime)
        {
            rainbowText.text = "";
        }
    }

    public bool CheckRainbowFlag()
    {
        if (rainbowFlag == false)
        {
            //虹色の景品の生成を可能にする
            rainbowFlag = true;
            return (true);
        }
        else
        {
            //すでに虹色の景品が生成されている場合
            return (false);
        }
    }

    public void RandomEvent()
    {
        rand = Random.Range(1, 8);

        if (rand == previousRand)
        {
            //同じイベントを2連続で引いた場合はやり直し
            RandomEvent();
        }
        else
        {
            //7個のイベントからランダムで選ばれる
            if (rand == 1)
            {
                Event1();
            }
            else if (rand == 2)
            {
                Event2();
            }
            else if (rand == 3)
            {
                Event3();
            }
            else if (rand == 4)
            {
                Event4();
            }
            else if (rand == 5)
            {
                Event5();
            }
            else if (rand == 6)
            {
                Event6();
            }
            else if (rand == 7)
            {
                Event7();
            }
        }    
    }

    void Event1() //獲得スコアが二倍になるベント
    {
        if (score.eventValue == 2)
        {
            RandomEvent();
            return;
        }
        else
        {
            score.ScoreUpEvent();
        }

        rainbowText.text = "一定時間獲得スコアアップ！";
        ResetFlag();
    }

    void Event2() //10000点獲得できるイベント
    {
        score.ScoreGetEvent();

        rainbowText.text = "10000点獲得！";
        ResetFlag();
    }

    void Event3() //15000点獲得できるイベント
    {
        score.ScoreGetEvent2();

        rainbowText.text = "15000点獲得！";
        ResetFlag();
    }

    void Event4() //景品の復活速度が速くなるイベント
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Prize");

        foreach (GameObject target in targets)
        {
            //オブジェクトが持っている特定のスクリプトを取得
            Prize script = target.GetComponent<Prize>();

            if (script.generateTime <= 0.5f)
            {
                //やり直し
                RandomEvent();
                return;
            }
            else
            {
                script.GenerateEarly();
            }
        }

        rainbowText.text = "景品の復活速度アップ！";
        ResetFlag();
    }

    void Event5()  //全ての景品を獲得できるイベント
    {
        if (score.eventValue == 2)
        {
            RandomEvent();
            return;
        }
        else
        {
            //ターゲットのオブジェクトを見つける
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Prize");

            //各ターゲットに後ろ向きの力を加える
            foreach (GameObject target in targets)
            {
                Rigidbody rb = target.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 prizeForce = -target.transform.forward * power;
                    rb.AddForce(prizeForce);
                }
            }
        }

        score.HitBounusStop();  //少しの間連射ボーナスが途切れないようにする

        rainbowText.text = "全景品獲得！";
        ResetFlag();
    }

    void Event6() //虹色の景品が出やすくなるイベント
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Prize");

        foreach (GameObject target in targets)
        {
            Prize script = target.GetComponent<Prize>();

            if (script.rainbowRand <= 2)
            {
                RandomEvent();
                return;
            }
            else
            {
                script.ChanceRainbow();
            }
        }

        rainbowText.text = "虹色の景品の出現率アップ！";
        ResetFlag();
    }

    void Event7() //壁の動きが止まるイベント
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Wall");

        foreach (GameObject target in targets)
        {
            Wall script = target.GetComponent<Wall>();

            if(script.stopEvent == true || destroyCount == 9)
            {
                RandomEvent();
                return;
            }
            else
            {
                script.StopEvent();
            }
        }

        rainbowText.text = "一定時間全ての壁が停止！";
        ResetFlag();
    }

    void ResetFlag()
    {
        previousRand = rand;
        rainbowFlag = false;
        timer = 0.0f;
    }
}
