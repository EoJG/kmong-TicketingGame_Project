using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ������ ������ ���õ� ����� �����ϴ� Ŭ����
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

    // �ð��� �帧�� �����ϴ� �ڵ�
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

    // ������ ������Ű�� �ؽ�Ʈ�� �����ϸ� �˶��� ���� �ڵ�
    public void PlusScore()
    {
        score += 10;

        StringBuilder sb = new StringBuilder();
        sb.Append(defaultScoreStr).Append(score);

        scoreText.text = sb.ToString();

        alertMgr.Alert(AlertManager.AlertState.Clear);
    }

    // ���� ���� �� ������ ���̰� �ؽ�Ʈ�� �����ϸ� �˶��� ���� �ڵ�
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

    // ���� �������� ���� �� ���� ������� ���� ���� �ʱ�ȭ �ڵ�
    public void InitChance()
    {
        chance = 3;

        StringBuilder sb = new StringBuilder();
        sb.Append(defaultChanceStr).Append(chance);

        chanceText.text = sb.ToString();
    }

    // ���� ������� ���� ���� �ʱ�ȭ �ڵ�
    public void InitScore()
    {
        score = 0;

        StringBuilder sb = new StringBuilder();
        sb.Append(defaultScoreStr).Append(score);

        scoreText.text = sb.ToString();
    }

    // Ÿ�̸� �ʱ�ȭ �ڵ�
    public void InitTimer()
    {
        timer = 30f;
    }

    public int GetScore()
    {
        return score;
    }
}
