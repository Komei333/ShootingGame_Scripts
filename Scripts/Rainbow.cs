using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Rainbow : MonoBehaviour
{
    [SerializeField] Score score;
    [SerializeField] Text rainbowText;

    private float timer = 0.0f;
    private float resetTime = 5.0f;
    private int rand = 0;
    private int previousRand = 0;
    private bool isHappenedRainbowEvent = true;


    void Start()
    {
        rainbowText.text = "";

        //�Q�[���J�n�����b�o�߂���܂œ��F�̌i�i�͏o�����Ȃ�
        Invoke("ResetAll", 1.0f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= resetTime)
        {
            rainbowText.text = "";
        }
    }

    public bool CheckHappenedRainbowEvent()
    {
        if (isHappenedRainbowEvent == false)
        {
            //���F�̌i�i�̐������\�ɂ���
            isHappenedRainbowEvent = true;
            return true;
        }
        else
        {
            //���łɓ��F�̌i�i����������Ă���ꍇ
            return false;
        }
    }

    public void RandomEvent()
    {
        rand = Random.Range(1, 8);

        if (rand == previousRand)
        {
            //�����C�x���g��2�A���ň������ꍇ�͂�蒼��
            RandomEvent();
        }
        else
        {
            //7�̃C�x���g���烉���_���őI�΂��
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

    void Event1() //�l���X�R�A��2�{�ɂȂ�x���g
    {
        if (score.eventScoreValue == 2)
        {
            //��蒼��
            RandomEvent();
            return;
        }
        else
        {
            score.ScoreUpEvent();
        }

        rainbowText.text = "��莞�Ԋl���X�R�A�A�b�v�I";
        ResetAll();
    }

    void Event2() // 3000�_�l���ł���C�x���g
    {
        score.ScoreGetEvent();

        rainbowText.text = "3000�_�l���I";
        ResetAll();
    }

    void Event3() //5000�_�l���ł���C�x���g
    {
        score.ScoreGetEvent2();

        rainbowText.text = "5000�_�l���I";
        ResetAll();
    }

    void Event4() //�i�i�̕������x�������Ȃ�C�x���g
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Prize");

        foreach (GameObject target in targets)
        {
            //�I�u�W�F�N�g�������Ă������̃X�N���v�g���擾
            Prize script = target.GetComponent<Prize>();

            var generateTimeLimit = 1.0f;

            if (script.generatePrizeTime <= generateTimeLimit)
            {
                RandomEvent();
                return;
            }
            else
            {
                script.GeneratePrizeEarly();
            }
        }

        rainbowText.text = "�i�i�̕������x�A�b�v�I";
        ResetAll();
    }

    void Event5()  //�S�Ă̌i�i���l���ł���C�x���g
    {
        // �A���q�b�g�{�[�i�X�����Z�b�g����
        score.HitBonusReset();

        // ��莞�ԃq�b�g�{�[�i�X�����f����Ȃ��悤�ɂ���
        score.StopBonusScoreValue();

        //�^�[�Q�b�g�̃I�u�W�F�N�g��������
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Prize");

        var prizePushPower = 500.0f;

        //�e�^�[�Q�b�g�Ɍ������̗͂�������
        foreach (GameObject target in targets)
        {
            Rigidbody rb = target.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 prizeForce = -target.transform.forward * prizePushPower;
                rb.AddForce(prizeForce);
            }
        }

        rainbowText.text = "�S�i�i�l���I";
        ResetAll();
    }

    void Event6() //���F�̌i�i���o�₷���Ȃ�C�x���g
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Prize");

        foreach (GameObject target in targets)
        {
            Prize script = target.GetComponent<Prize>();

            var randomValueLimit = 4;

            if (script.rainbowRandomValue <= randomValueLimit)
            {
                RandomEvent();
                return;
            }
            else
            {
                script.ChanceRainbow();
            }
        }

        rainbowText.text = "���F�̌i�i�̏o�����A�b�v�I";
        ResetAll();
    }

    void Event7() //�ǂ̓������~�܂�C�x���g
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Wall");

        foreach (GameObject target in targets)
        {
            Wall script = target.GetComponent<Wall>();

            if(script.isHappenedStopEvent)
            {
                RandomEvent();
                return;
            }
            else
            {
                script.StopWallEvent();
            }
        }

        rainbowText.text = "��莞�ԑS�Ă̕ǂ���~�I";
        ResetAll();
    }

    private void ResetAll()
    {
        previousRand = rand;
        isHappenedRainbowEvent = false;
        timer = 0.0f;
    }
}
