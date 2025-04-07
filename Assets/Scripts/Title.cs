using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 타이틀 화면 기능 관리를 위한 클래스
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

    // 게임의 시작을 위해 캘린더를 키며 플로우를 진행시키는 코드
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

    // 게임 시작 버튼의 기능
    public void StartButton()
    {
        pannel.SetActive(true);
    }

    // 회차를 선택하는 화면을 호출
    public void OpenSelectShow()
    {
        group1.SetActive(true);
        group2.SetActive(false);
    }

    // 보안 문자 입력 화면을 호출
    public void OpenCaptchaUI()
    {
        group2.SetActive(true);
        group1.SetActive(false);
        calendarObj.SetActive(false);

        MakeCaptcha();
    }

    // 보안 문자 입력 화면을 닫는 코드
    public void CloseCaptcha()
    {
        calendarObj.SetActive(true);
        group1.SetActive(false);
        group2.SetActive(false);
    }

    // 보안 문자를 랜덤하게 생성하는 코드
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

    // 입력의 필요없는 공백을 제거하는 코드
    string CleanInput(string str)
    {
        return Regex.Replace(str, @"[\u200B-\u200D\uFEFF]", "");
    }

    // 보안문자를 올바르게 입력했는지 확인하는 코드
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
