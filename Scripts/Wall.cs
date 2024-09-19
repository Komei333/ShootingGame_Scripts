using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private float moveSpeed = 0.05f; //•Ç‚ÌˆÚ“®‘¬“x
    private float expandSpeed = 0.1f; //•Ç‚ÌLk‘¬“x
    private float maxHeight = 0.22f; //•Ç‚ÌÅ‘å‚‚³
    private float minHeight = 0.05f; //•Ç‚ÌÅ¬‚‚³
    private float stopTime = 1.0f; //Lk’â~ŠÔ

    private Vector3 initialPosition;
    private Vector3 currentPosition;
    private Vector3 newScale;
    private float currentHeight;
    private float timer = 0.0f;
    private float delayTimer = 0.0f;
    private float delayRandomValue = 0.0f;

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

        if (isMovingUp) //•Ç‚ªã¸‚µ‚Ä‚¢‚é‚Æ‚«
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
        else if (isMovingDown) //•Ç‚ª‰º~‚µ‚Ä‚¢‚é‚Æ‚«
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
        else if (isMovingStop) //•Ç‚ª~‚Ü‚Á‚Ä‚¢‚é‚Æ‚«
        {
            timer += Time.deltaTime;

            if (timer >= stopTime)
            {
                WallStartMovingDelay();
            }
        }

        currentPosition = transform.position;

        //•Ç‚Ì‚‚³‚ğİ’è
        newScale = transform.localScale;
        newScale.y = currentHeight;
        transform.localScale = newScale;
    }

    private void WallStartMovingDelay()
    {
        if (delayRandomValue == 0.0f)
        {
            if (currentHeight == minHeight)
            {
                delayRandomValue = Random.Range(1f, 10f); // ã¸ŠJn‚Ü‚Å‚Ìƒ‰ƒ“ƒ_ƒ€’x‰„
            }
            else
            {
                delayRandomValue = Random.Range(1f, 2.5f); // ‰º~ŠJn‚Ü‚Å‚Ìƒ‰ƒ“ƒ_ƒ€’x‰„
            }
        }

        delayTimer += Time.deltaTime;

        if (delayTimer >= delayRandomValue) // ˆê’èŠÔ‚ªŒo‰ß‚µ‚½‚ç•Ç‚ª“®‚«o‚·
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

    private void EndStopWallEvent()
    {
        isHappenedStopEvent = false;
    }

    private void EndDisappearWallEvent()
    {
        //‰Šúó‘Ô‚ÉƒŠƒZƒbƒg‚·‚é
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
        Invoke("EndStopWallEvent", 7.5f);
    }
}