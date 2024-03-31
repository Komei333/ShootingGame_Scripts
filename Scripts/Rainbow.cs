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

        //�Q�[���J�n�����b�o�߂���܂œ��F�̌i�i�͏o�����Ȃ�
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
            //���F�̌i�i�̐������\�ɂ���
            rainbowFlag = true;
            return (true);
        }
        else
        {
            //���łɓ��F�̌i�i����������Ă���ꍇ
            return (false);
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

    void Event1() //�l���X�R�A����{�ɂȂ�x���g
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

        rainbowText.text = "��莞�Ԋl���X�R�A�A�b�v�I";
        ResetFlag();
    }

    void Event2() //10000�_�l���ł���C�x���g
    {
        score.ScoreGetEvent();

        rainbowText.text = "10000�_�l���I";
        ResetFlag();
    }

    void Event3() //15000�_�l���ł���C�x���g
    {
        score.ScoreGetEvent2();

        rainbowText.text = "15000�_�l���I";
        ResetFlag();
    }

    void Event4() //�i�i�̕������x�������Ȃ�C�x���g
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Prize");

        foreach (GameObject target in targets)
        {
            //�I�u�W�F�N�g�������Ă������̃X�N���v�g���擾
            Prize script = target.GetComponent<Prize>();

            if (script.generateTime <= 0.5f)
            {
                //��蒼��
                RandomEvent();
                return;
            }
            else
            {
                script.GenerateEarly();
            }
        }

        rainbowText.text = "�i�i�̕������x�A�b�v�I";
        ResetFlag();
    }

    void Event5()  //�S�Ă̌i�i���l���ł���C�x���g
    {
        if (score.eventValue == 2)
        {
            RandomEvent();
            return;
        }
        else
        {
            //�^�[�Q�b�g�̃I�u�W�F�N�g��������
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Prize");

            //�e�^�[�Q�b�g�Ɍ������̗͂�������
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

        score.HitBounusStop();  //�����̊ԘA�˃{�[�i�X���r�؂�Ȃ��悤�ɂ���

        rainbowText.text = "�S�i�i�l���I";
        ResetFlag();
    }

    void Event6() //���F�̌i�i���o�₷���Ȃ�C�x���g
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

        rainbowText.text = "���F�̌i�i�̏o�����A�b�v�I";
        ResetFlag();
    }

    void Event7() //�ǂ̓������~�܂�C�x���g
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

        rainbowText.text = "��莞�ԑS�Ă̕ǂ���~�I";
        ResetFlag();
    }

    void ResetFlag()
    {
        previousRand = rand;
        rainbowFlag = false;
        timer = 0.0f;
    }
}
