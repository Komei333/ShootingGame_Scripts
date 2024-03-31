using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private float moveSpeed = 0.05f; //�ǂ̈ړ����x
    private float expandSpeed = 0.1f; //�ǂ̐L�k���x
    private float maxHeight = 0.22f; //�ǂ̍ő卂��
    private float minHeight = 0.05f; //�ǂ̍ŏ�����
    private float stopTime = 1.0f; //�L�k��~����

    private Vector3 initialPosition;
    private Vector3 currentPosition;
    private Vector3 newScale;
    private float currentHeight;
    private float timer = 0.0f;

    private bool movingUp = false;
    private bool movingDown = false;
    private bool movingStop = true;
    public bool stopEvent = false;
    public bool disappearEvent = false;


    void Start()
    {
        initialPosition = transform.position;
        currentPosition = transform.position;
        currentHeight = transform.localScale.y;
    }

    void Update()
    {
        if(stopEvent == false)
        {
            if (movingUp) //�ǂ��㏸���Ă���Ƃ�
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                currentHeight += expandSpeed * Time.deltaTime;

                if (currentHeight >= maxHeight)
                {
                    currentHeight = maxHeight;
                    movingUp = false;
                    movingStop = true;
                    timer = 0.0f;
                }
            }
            else if (movingDown) //�ǂ����~���Ă���Ƃ�
            {
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                currentHeight -= expandSpeed * Time.deltaTime;

                if (currentHeight <= minHeight)
                {
                    currentHeight = minHeight;
                    movingDown = false;
                    movingStop = true;
                    timer = 0.0f;
                }
            }
            else if (movingStop) //�ǂ��~�܂��Ă���Ƃ�
            {
                timer += Time.deltaTime;

                if (timer >= stopTime)
                {
                    if (currentHeight == minHeight)
                    {
                        Invoke("MovingUpTrue", Random.Range(1f, 10f));
                        movingStop = false;
                    }
                    else
                    {
                        Invoke("MovingDownTrue", Random.Range(1f, 2.5f));
                        movingStop = false;
                    }
                }
            }

            currentPosition = transform.position;

            //�ǂ̍�����ݒ�
            newScale = transform.localScale;
            newScale.y = currentHeight;
            transform.localScale = newScale;
        }
    }

    void MovingUpTrue()
    {
        movingUp = true;
    }

    void MovingDownTrue()
    {
        movingDown = true;
    }

    void StopCancel()
    {
        stopEvent = false;
    }

    void DisappearCancel()
    {
        //������ԂɃ��Z�b�g����
        movingUp = false;
        movingDown = false;
        movingStop = true;

        newScale = transform.localScale;
        newScale.y = minHeight;
        transform.localScale = newScale;
        currentHeight = newScale.y;

        currentPosition = initialPosition;
        transform.position = currentPosition;
    }

    public void StopEvent()
    {
        stopEvent = true;
        Invoke("StopCancel", 7f);
    }

    public void DisappearEvent()
    {
        //��ʊO�Ɉړ�������
        transform.position = new Vector3(-10f, currentPosition.y, currentPosition.z);

        stopEvent = true;
        Invoke("StopCancel", 7f);
        Invoke("DisappearCancel", 7f);
    }

    public void DestroyEvent()
    {
        Destroy(gameObject);
    }
}