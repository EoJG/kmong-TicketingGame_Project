using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Ÿ��Ʋ ȭ�� ��� ������ ���� Ŭ����
public class Title : MonoBehaviour
{
    public CanvasMgr canvasMgr;
    public GameObject pannel;
    public GameObject calendarObj;
    public GameObject group1;
    public GameObject group2;
    public TextMeshProUGUI captchaText;
    public InputField inputfield;
    public Text inputtext;

    string captcha;

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    WebGLInput.captureAllKeyboardInput = false;
    WebGLInput.mobileKeyboardSupport = true;
#endif
    }

    // ������ ������ ���� Ķ������ Ű�� �÷ο츦 �����Ű�� �ڵ�
    public void GameStart()
    {
        calendarObj.SetActive(true);
        group1.SetActive(false);
        group2.SetActive(false);
        pannel.SetActive(false);

        canvasMgr.Init();
        canvasMgr.OpenAreaSelectionCanvas();
        this.gameObject.SetActive(false);
    }

    // ���� ���� ��ư�� ���
    public void StartButton()
    {
        pannel.SetActive(true);
    }

    // ȸ���� �����ϴ� ȭ���� ȣ��
    public void OpenSelectShow()
    {
        group1.SetActive(true);
        group2.SetActive(false);
    }

    // ���� ���� �Է� ȭ���� ȣ��
    public void OpenCaptchaUI()
    {
        group2.SetActive(true);
        group1.SetActive(false);
        calendarObj.SetActive(false);

        MakeCaptcha();
    }

    // ���� ���� �Է� ȭ���� �ݴ� �ڵ�
    public void CloseCaptcha()
    {
        calendarObj.SetActive(true);
        group1.SetActive(false);
        group2.SetActive(false);
    }

    // ���� ���ڸ� �����ϰ� �����ϴ� �ڵ�
    public void MakeCaptcha()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 5; i++)
        {
            sb.Append(chars[Random.Range(0, chars.Length)]);
        }

        captcha = sb.ToString();
        captchaText.text = captcha;
    }

    // �Է��� �ʿ���� ������ �����ϴ� �ڵ�
    string CleanInput(string str)
    {
        return Regex.Replace(str, @"[\u200B-\u200D\uFEFF]", "");
    }

    // ���ȹ��ڸ� �ùٸ��� �Է��ߴ��� Ȯ���ϴ� �ڵ�
    public void ConfirmCaptcha()
    {
        string input = CleanInput(inputtext.text).Trim().ToUpper();
        string answer = CleanInput(captcha).Trim().ToUpper();

        if (input == answer)
        {
            GameStart();
        }

        inputfield.text = "";
    }
}
