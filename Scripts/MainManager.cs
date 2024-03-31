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

    private bool start = false;
    private bool pause = false;

    void Start()
    {
        musicManager.PlayBGM2();

        Time.timeScale = 0;
        gameCamera.StopMove();
        gun.StopShot();
    }

    void Update()
    {
        if(start)
        {
            //���N���b�N
            if (Input.GetMouseButtonDown(0) && pause == false)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            //�E�N���b�N
            if (Input.GetMouseButtonDown(1))
            {
                if (pause)
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

            //�G�X�P�[�v�L�[
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void OpenPause()
    {
        pause = true;
        Time.timeScale = 0;

        musicManager.PlaySE1();
        gameCamera.StopMove();
        gun.StopShot();

        pausePanel.SetActive(true);
        otherPanel.SetActive(false);
    }

    public void ExitPause()
    {
        pause = false;
        Time.timeScale = 1;

        musicManager.PlaySE1();
        gameCamera.CanMove();
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
        start = true;
        Time.timeScale = 1;
        startPanel.SetActive(false);
        otherPanel.SetActive(true);

        Cursor.visible = false;  //�J�[�\����\��     
        Cursor.lockState = CursorLockMode.Locked; //�J�[�\������ʒ����Ƀ��b�N����

        musicManager.PlaySE1();
        gameCamera.CanMove();
        gun.CanShot();
    }
}
