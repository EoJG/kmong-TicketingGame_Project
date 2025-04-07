using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 게임의 점수와 관련된 기능을 관리하는 클래스
public class ManageScore : MonoBehaviour
{
    int score;
    int chance;

    TextMeshProUGUI chanceText;
    TextMeshProUGUI scoreText;
    string defaultScoreStr;
    string defaultChanceStr;

    public Image timerCircle;
    float timer = 30f;
    public bool isGameStart;

    public AlertManager alertMgr;

    private void Awake()
    {
        isGameStart = false;
        score = 0;
        chance = 3;
        defaultScoreStr = "Score: ";
        defaultChanceStr = "Chance: ";
    }

    void Start()
    {
        chanceText = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        scoreText = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        TimerManager();
    }

    // 시간의 흐름을 관리하는 코드
    void TimerManager()
    {
        timerCircle.fillAmount = timer / 30;

        if (isGameStart)
        {
            if (timer <= 0)
            {
                alertMgr.Alert(AlertManager.AlertState.End);
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    // 점수를 증가시키고 텍스트를 수정하며 알람을 띄우는 코드
    public void PlusScore()
    {
        score += 10;

        StringBuilder sb = new StringBuilder();
        sb.Append(defaultScoreStr).Append(score);

        scoreText.text = sb.ToString();

        alertMgr.Alert(AlertManager.AlertState.Clear);
    }

    // 오답 선택 시 찬스를 줄이고 텍스트를 수정하며 알람을 띄우는 코드
    public void UseChance()
    {
        chance--;

        StringBuilder sb = new StringBuilder();
        sb.Append(defaultChanceStr).Append(chance);

        chanceText.text = sb.ToString();

        if (chance > 0)
            alertMgr.Alert(AlertManager.AlertState.Fail);
        else
            alertMgr.Alert(AlertManager.AlertState.End);
    }

    // 다음 스테이지 이전 및 게임 재시작을 위한 찬스 초기화 코드
    public void InitChance()
    {
        chance = 3;

        StringBuilder sb = new StringBuilder();
        sb.Append(defaultChanceStr).Append(chance);

        chanceText.text = sb.ToString();
    }

    // 게임 재시작을 위한 점수 초기화 코드
    public void InitScore()
    {
        score = 0;

        StringBuilder sb = new StringBuilder();
        sb.Append(defaultScoreStr).Append(score);

        scoreText.text = sb.ToString();
    }

    // 타이머 초기화 코드
    public void InitTimer()
    {
        timer = 30f;
    }

    public int GetScore()
    {
        return score;
    }
}
