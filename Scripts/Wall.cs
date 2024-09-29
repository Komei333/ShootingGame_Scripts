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
    private float currentHeight;
    private float timer = 0.0f;
    private float delayTimer = 0.0f;
    private float delayRandomValue = 0.0f;

    private bool isMovingUp = false;
    private bool isMovingDown = false;
    private bool isMovingStop = true;
    public bool isHappenedWallRainbowEvent = false;


    void Start()
    {
        initialPosition = transform.position;
        currentPosition = transform.position;
        currentHeight = transform.localScale.y;
    }

    void Update()
    {
        if (isHappenedWallRainbowEvent) return;

        MovingWall();
        SettingWallScale();
    }

    private void MovingWall()
    {
        if (isMovingUp) //�ǂ��㏸���Ă���Ƃ�
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            currentHeight += expandSpeed * Time.deltaTime;
            currentPosition = transform.position;

            if (currentHeight < maxHeight) return;

            currentHeight = maxHeight;
            isMovingUp = false;
            isMovingStop = true;
            timer = 0.0f;
        }
        else if (isMovingDown) //�ǂ����~���Ă���Ƃ�
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            currentHeight -= expandSpeed * Time.deltaTime;
            currentPosition = transform.position;

            if (currentHeight > minHeight) return;

            currentHeight = minHeight;
            isMovingDown = false;
            isMovingStop = true;
            timer = 0.0f;
        }
        else if (isMovingStop) //�ǂ��~�܂��Ă���Ƃ�
        {
            timer += Time.deltaTime;

            if (timer >= stopTime)
            {
                WallStartMovingDelay();
            }
        }
    }

    private void SettingWallScale()
    {
        //�ǂ̍�����ݒ�
        var newScale = transform.localScale;
        newScale.y = currentHeight;
        transform.localScale = newScale;
    }

    private void WallStartMovingDelay()
    {
        if (delayRandomValue == 0.0f)
        {
            if (currentHeight == minHeight)
            {
                delayRandomValue = Random.Range(1f, 10f); // �㏸�J�n�܂ł̃����_���x��
            }
            else
            {
                delayRandomValue = Random.Range(1f, 2.5f); // ���~�J�n�܂ł̃����_���x��
            }
        }

        delayTimer += Time.deltaTime;

        if (delayTimer >= delayRandomValue) // ��莞�Ԃ��o�߂�����ǂ������o��
        {
            if (currentHeight == minHeight)
            {
                isMovingUp = true;
            }
            else
            {
                isMovingDown = true;
            }

            delayTimer = 0.0f;
            delayRandomValue = 0.0f;
            isMovingStop = false;
        }
    }

    private void ResetWallPosition()
    {
        //������ԂɃ��Z�b�g����
        isMovingUp = false;
        isMovingDown = false;
        isMovingStop = true;
        timer = 0.0f;
        delayTimer = 0.0f;
        delayRandomValue = 0.0f;

        var newScale = transform.localScale;
        newScale.y = minHeight;
        transform.localScale = newScale;
        currentHeight = newScale.y;

        currentPosition = initialPosition;
        transform.position = currentPosition;
    }

    private void EndStopWallEvent()
    {
        isHappenedWallRainbowEvent = false;
    }

    private void EndDisappearWallEvent()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = true;
        Collider collider = GetComponent<Collider>();
        collider.enabled = true;

        isHappenedWallRainbowEvent = false;
        ResetWallPosition();
    }

    public void StopWallEvent()
    {
        isHappenedWallRainbowEvent = true;
        Invoke("EndStopWallEvent", 7.5f);
    }

    public void DisappearWallEvent()
    {
        // Renderer�𖳌������Č����ڂ�����
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = false;

        // Collider�𖳌������ē����蔻�������
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        isHappenedWallRainbowEvent = true;
        Invoke("EndDisappearWallEvent", 5f);
    }
}