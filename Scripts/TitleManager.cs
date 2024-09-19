using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    [SerializeField] OptionManager optionManager;

    [SerializeField] GameObject titlePanel;
    [SerializeField] GameObject scorePanel;
    [SerializeField] GameObject optionPanel;

    void Start()
    {
        // 初回プレイ時に設定を初期化
        if (PlayerPrefs.GetString("FirstTime") == "")
        {
            // 保存
            PlayerPrefs.SetFloat("BGM", 1.0f);
            PlayerPrefs.SetFloat("SE", 1.0f);
            PlayerPrefs.SetString("Screen", "Full");
            PlayerPrefs.SetString("FirstTime", "false");
            PlayerPrefs.Save();

            // 1920×1080、フルスクリーン有効、リフレッシュレート60Hz
            Screen.SetResolution(1920, 1080, true, 60);
        }

        // 音量調整
        musicManager.SetBGM();
        musicManager.SetSE();

        musicManager.PlayBGM1();
        
        Cursor.visible = true;  //カーソル表示
        Cursor.lockState = CursorLockMode.None;  //カーソルを自由に動かせる

        optionManager.ChangeButtonAlpha();
    }

    public void StartGameButton()
    {
        musicManager.PlaySE1();
        SceneManager.LoadScene("MainGameScene");
    }

    public void OpenScoreButton()
    {
        musicManager.PlaySE1();
        scorePanel.SetActive(true);
        titlePanel.SetActive(false);
    }

    public void ExitScoreButton()
    {
        musicManager.PlaySE1();
        titlePanel.SetActive(true);
        scorePanel.SetActive(false);
    }

    public void OpenOptionButton()
    {
        musicManager.PlaySE1();
        optionPanel.SetActive(true);
        titlePanel.SetActive(false);
    }

    public void ExitOptionButton()
    {
        musicManager.PlaySE1();
        titlePanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void QuitGameButton()
    {
        musicManager.PlaySE1();
        Application.Quit();
    }
}
