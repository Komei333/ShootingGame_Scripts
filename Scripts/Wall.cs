using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private float moveSpeed = 0.05f; //壁の移動速度
    private float expandSpeed = 0.1f; //壁の伸縮速度
    private float maxHeight = 0.22f; //壁の最大高さ
    private float minHeight = 0.05f; //壁の最小高さ
    private float stopTime = 1.0f; //伸縮停止時間

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
        if (isMovingUp) //壁が上昇しているとき
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
        else if (isMovingDown) //壁が下降しているとき
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
        else if (isMovingStop) //壁が止まっているとき
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
        //壁の高さを設定
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
                delayRandomValue = Random.Range(1f, 10f); // 上昇開始までのランダム遅延
            }
            else
            {
                delayRandomValue = Random.Range(1f, 2.5f); // 下降開始までのランダム遅延
            }
        }

        delayTimer += Time.deltaTime;

        if (delayTimer >= delayRandomValue) // 一定時間が経過したら壁が動き出す
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
        //初期状態にリセットする
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
        // Rendererを無効化して見た目を消す
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = false;

        // Colliderを無効化して当たり判定を消す
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        isHappenedWallRainbowEvent = true;
        Invoke("EndDisappearWallEvent", 5f);
    }
}