using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;

    [SerializeField] GameObject windowPanel;

    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;

    [SerializeField] Button windowScreenButton;
    [SerializeField] Button fullScreenButton;


    private float bgmValue;
    private float seValue;

    private Image windowScreenImage;
    private Image fullScreenImage;

    private Color windowScreenColor;
    private Color fullScreenColor;

    void Start()
    {
        //BGM�X���C�_�[�𓮂��������̏�����o�^
        bgmSlider.onValueChanged.AddListener(SetBGM);

        //SE�X���C�_�[�𓮂��������̏�����o�^
        seSlider.onValueChanged.AddListener(SetSE);

        // �X���C�_�[�ɒl�𔽉f
        bgmSlider.value = (PlayerPrefs.GetFloat("BGM") + 40f) / 40f;
        seSlider.value = (PlayerPrefs.GetFloat("SE") + 40f) / 40f;

        windowScreenImage = windowScreenButton.GetComponent<Image>();
        fullScreenImage = fullScreenButton.GetComponent<Image>();

        windowScreenColor = windowScreenImage.color;
        fullScreenColor = fullScreenImage.color;

        ChangeAlpha();
    }

    public void SetBGM(float value)
    {
        //-40�`0�ɕϊ��i���Ηʂ�dB�ɕϊ��j
        bgmValue = -40f + (value * 40f);

        //�ۑ�
        PlayerPrefs.SetFloat("BGM", bgmValue);
        PlayerPrefs.Save();

        musicManager.SetBGM();
    }

    public void SetSE(float value)
    {
        //-40�`0�ɕϊ��i���Ηʂ�dB�ɕϊ��j
        seValue = -40f + (value * 40f);

        //�ۑ�
        PlayerPrefs.SetFloat("SE", seValue);
        PlayerPrefs.Save();

        musicManager.SetSE();
    }

    public void SetWindowScreen()
    {
        musicManager.PlaySE1();

        //�ۑ�
        PlayerPrefs.SetString("Screen", "Window");
        PlayerPrefs.Save();

        ChangeAlpha();

        windowPanel.SetActive(true);
    }

    public void SetFullScreen()
    {
        musicManager.PlaySE1();

        //�ۑ�
        PlayerPrefs.SetString("Screen", "Full");
        PlayerPrefs.Save();

        ChangeAlpha();

        // 1920�~1080�A�t���X�N���[���L���A���t���b�V�����[�g60Hz
        Screen.SetResolution(1920, 1080, true, 60);
    }

    public void ChangeAlpha()
    {
        if(PlayerPrefs.GetString("Screen") == "Window")
        {
            // a�͓����x��\��
            windowScreenColor.a = 1.0f;
            fullScreenColor.a = 0.5f;
        }
        else if (PlayerPrefs.GetString("Screen") == "Full")
        {
            // a�͓����x��\��
            windowScreenColor.a = 0.5f;
            fullScreenColor.a = 1.0f;
        }

        windowScreenImage.color = windowScreenColor;
        fullScreenImage.color = fullScreenColor;
    }

    public void WindowSize1()
    {
        // 640�~360�A�t���X�N���[�������A���t���b�V�����[�g60Hz
        Screen.SetResolution(640, 360, false, 60);

        musicManager.PlaySE1();
        windowPanel.SetActive(false);
    }

    public void WindowSize2()
    {
        // 1280�~720�A�t���X�N���[�������A���t���b�V�����[�g60Hz
        Screen.SetResolution(1280, 720, false, 60);

        musicManager.PlaySE1();
        windowPanel.SetActive(false);
    }

    public void WindowSize3()
    {
        // 1920�~1080�A�t���X�N���[�������A���t���b�V�����[�g60Hz
        Screen.SetResolution(1920, 1080, false, 60);

        musicManager.PlaySE1();
        windowPanel.SetActive(false);
    }
}
