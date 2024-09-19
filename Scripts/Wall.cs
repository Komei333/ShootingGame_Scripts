using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private float moveSpeed = 0.05f; //ï«ÇÃà⁄ìÆë¨ìx
    private float expandSpeed = 0.1f; //ï«ÇÃêLèkë¨ìx
    private float maxHeight = 0.22f; //ï«ÇÃç≈ëÂçÇÇ≥
    private float minHeight = 0.05f; //ï«ÇÃç≈è¨çÇÇ≥
    private float stopTime = 1.0f; //êLèkí‚é~éûä‘

    private Vector3 initialPosition;
    private Vector3 currentPosition;
    private Vector3 newScale;
    private float currentHeight;
    private float timer = 0.0f;

    private bool isMovingUp = false;
    private bool isMovingDown = false;
    private bool isMovingStop = true;
    public bool isHappenedStopEvent = false;
    public bool isHappenedDisappearEvent = false;


    void Start()
    {
        initialPosition = transform.position;
        currentPosition = transform.position;
        currentHeight = transform.localScale.y;
    }

    void Update()
    {
        if (isHappenedStopEvent) return;

        if (isMovingUp) //ï«Ç™è„è∏ÇµÇƒÇ¢ÇÈÇ∆Ç´
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            currentHeight += expandSpeed * Time.deltaTime;

            if (currentHeight >= maxHeight)
            {
                currentHeight = maxHeight;
                isMovingUp = false;
                isMovingStop = true;
                timer = 0.0f;
            }
        }
        else if (isMovingDown) //ï«Ç™â∫ç~ÇµÇƒÇ¢ÇÈÇ∆Ç´
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            currentHeight -= expandSpeed * Time.deltaTime;

            if (currentHeight <= minHeight)
            {
                currentHeight = minHeight;
                isMovingDown = false;
                isMovingStop = true;
                timer = 0.0f;
            }
        }
        else if (isMovingStop) //ï«Ç™é~Ç‹Ç¡ÇƒÇ¢ÇÈÇ∆Ç´
        {
            timer += Time.deltaTime;

            if (timer >= stopTime)
            {
                if (currentHeight == minHeight)
                {
                    Invoke("MovingUpTrue", Random.Range(1f, 10f));
                    isMovingStop = false;
                }
                else
                {
                    Invoke("MovingDownTrue", Random.Range(1f, 2.5f));
                    isMovingStop = false;
                }
            }
        }

        currentPosition = transform.position;

        //ï«ÇÃçÇÇ≥Çê›íË
        newScale = transform.localScale;
        newScale.y = currentHeight;
        transform.localScale = newScale;
    }

    private void MovingUpTrue()
    {
        isMovingUp = true;
    }

    private void MovingDownTrue()
    {
        isMovingDown = true;
    }

    private void EndStopWallEvent()
    {
        isHappenedStopEvent = false;
    }

    private void EndDisappearWallEvent()
    {
        //èâä˙èÛë‘Ç…ÉäÉZÉbÉgÇ∑ÇÈ
        isMovingUp = false;
        isMovingDown = false;
        isMovingStop = true;

        newScale = transform.localScale;
        newScale.y = minHeight;
        transform.localScale = newScale;
        currentHeight = newScale.y;

        currentPosition = initialPosition;
        transform.position = currentPosition;
    }

    public void StopWallEvent()
    {
        isHappenedStopEvent = true;
        Invoke("EndStopWallEvent", 7f);
    }

    public void DisappearWallEvent()
    {
        // ï«ÇâÊñ äOÇ…à⁄ìÆÇ≥ÇπÇÈ
        transform.position = new Vector3(-10f, currentPosition.y, currentPosition.z);

        isHappenedStopEvent = true;
        Invoke("EndStopWallEvent", 7f);
        Invoke("EndDisappearWallEvent", 7f);
    }
}