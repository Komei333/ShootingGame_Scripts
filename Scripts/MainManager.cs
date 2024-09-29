using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    [SerializeField] GameCamera gameCamera;
    [SerializeField] Gun gun;

    [SerializeField] GameObject otherPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject time;
    [SerializeField] GameObject score;
    [SerializeField] GameObject gunModel;

    public bool isSlowTimeEvent = false;
    private bool isGameStarted = false;
    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 0;

        musicManager.PlayBGM2();
        gameCamera.StopMoveCamera();
        gun.StopShot();
    }

    void Update()
    {
        CheckInputKey();
    }

    void CheckInputKey()
    {
        if (isGameStarted)
        {
            //エスケープキー
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            //左クリック
            if (Input.GetMouseButtonDown(0) && isPaused == false)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            //右クリック
            if (Input.GetMouseButtonDown(1))
            {
                if (isPaused)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    ExitPause();
                }
                else
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    OpenPause();
                }
            }
        }
    }

    public void OpenPause()
    {
        isPaused = true;
        Time.timeScale = 0;

        musicManager.PlaySE1();
        gameCamera.StopMoveCamera();
        gun.StopShot();

        pausePanel.SetActive(true);
        otherPanel.SetActive(false);
    }

    public void ExitPause()
    {
        isPaused = false;

        if (isSlowTimeEvent)
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1;
        }

        musicManager.PlaySE1();
        gameCamera.CanMoveCamera();
        gun.CanShot();

        pausePanel.SetActive(false);
        otherPanel.SetActive(true);
    }

    public void ReturnTitle()
    {
        musicManager.PlaySE1();
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        isGameStarted = true;
        startPanel.SetActive(false);
        otherPanel.SetActive(true);

        Cursor.visible = false;  //カーソル非表示     
        Cursor.lockState = CursorLockMode.Locked; //カーソルを画面中央にロックする

        musicManager.PlaySE1();
        gameCamera.CanMoveCamera();
        gunModel.SetActive(true);
        gun.CanShot();
    }

    void EndSlowTimeEvent()
    {
        isSlowTimeEvent = false;
        Time.timeScale = 1f;
    }

    public void SlowTimeEvent()
    {
        isSlowTimeEvent = true;
        Time.timeScale = 0.5f;
        Invoke("EndSlowTimeEvent", 4f);
    }
}
