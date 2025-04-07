using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ��¥ ���� ������ ���� �޷� Ŭ����
public class Calendar : MonoBehaviour
{
    [SerializeField]
    List<GameObject> dayList;

    [SerializeField]
    TextMeshProUGUI ymText;

    int year, month;
    int startIndex, daysInMonth;

    private void Awake()
    {
        year = DateTime.Now.Year;
        month = DateTime.Now.Month;

        startIndex = (int)new DateTime(year, month, 1).DayOfWeek;
        daysInMonth = DateTime.DaysInMonth(year, month);
    }

    // �޷��� �ٽ� Ȱ��ȭ ���� �� ������ ��¥�� �ٽ� �ʱ�ȭ �ϴ� �ڵ�
    private void OnEnable()
    {
        year = DateTime.Now.Year;
        month = DateTime.Now.Month;

        startIndex = (int)new DateTime(year, month, 1).DayOfWeek;
        daysInMonth = DateTime.DaysInMonth(year, month);
        
        SetYearMonthText();
        SetCalendar();
    }

    void Start()
    {
        SetYearMonthText();

        foreach (Transform child in this.transform)
        {
            dayList.Add(child.gameObject);
        }

        SetCalendar();
    }

    // �ش��ϴ� ��, ���� �޷��� �����ָ� ������ ��¥ ��ư�� ��Ȱ��ȭ �ϱ� ���� �ڵ�
    void SetCalendar()
    {
        DateTime today = DateTime.Today;

        for (int i = 0; i < dayList.Count; i++)
        {
            if (i < startIndex || i >= startIndex + daysInMonth)
            {
                DisableButton(i);
            }
            else
            {
                int day = i - startIndex + 1;
                DateTime thisDate = new DateTime(year, month, day);

                if (thisDate < today)
                {
                    HalfDisableButton(i);
                }
                else
                {
                    if (dayList[i].GetComponent<Button>().interactable == false)
                        dayList[i].GetComponent<Button>().interactable = true;
                }

                dayList[i].GetComponent<DayButtonData>().SetDate(year, month, day);

                TextMeshProUGUI dayBtnText = dayList[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                dayBtnText.text = day.ToString();
            }
        }
    }

    // ��ư ��Ȱ��ȭ�� �����ϱ� ���� �ڵ�
    void DisableButton(int index)
    {
        Button dayButton = dayList[index].GetComponent<Button>();
        ColorBlock cb = dayButton.colors;
        //cb.disabledColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);
        cb.disabledColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 0f);
        dayButton.colors = cb;

        dayButton.interactable = false;
        dayList[index].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
    }

    // ��ư�� ������ ������ �ʰ� ��Ȱ��ȭ�ϱ� ���� �ڵ�
    void HalfDisableButton(int index)
    {
        Button dayButton = dayList[index].GetComponent<Button>();
        ColorBlock cb = dayButton.colors;
        cb.disabledColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);
        dayButton.colors = cb;

        dayButton.interactable = false;
    }

    // ��, ���� �°� �ؽ�Ʈ ��ȯ
    void SetYearMonthText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(year.ToString()).Append(".").Append(month.ToString());

        ymText.text = sb.ToString();
    }

    // ���� ���� �����ϱ� ���� ��ư ���
    public void NextMonth()
    {
        month++;
        if (month > 12)
        {
            year++;
            month = 1;
        }
        startIndex = (int)new DateTime(year, month, 1).DayOfWeek;
        daysInMonth = DateTime.DaysInMonth(year, month);

        SetYearMonthText();
        SetCalendar();
    }

    // ���� ���� �����ϱ� ���� ��ư ���
    public void PrevMonth()
    {
        month--;
        if (month < 1)
        {
            year--;
            month = 12;
        }
        startIndex = (int)new DateTime(year, month, 1).DayOfWeek;
        daysInMonth = DateTime.DaysInMonth(year, month);

        SetYearMonthText();
        SetCalendar();
    }
}
