using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Drawing;

public class ResultManager : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;

    [SerializeField] Text scoreText;

    void Start()
    {
        musicManager.PlayBGM3();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        var scoreValue = PlayerPrefs.GetInt("Score");
        scoreText.text = scoreValue.ToString();
    }

    public void RetryButton()
    {
        musicManager.PlaySE1();
        SceneManager.LoadScene("MainGameScene");
    }

    public void TitleButton()
    {
        musicManager.PlaySE1();
        SceneManager.LoadScene("TitleScene");
    }
}
