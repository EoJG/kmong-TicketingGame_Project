using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 날짜 선택 구현을 위한 달력 클래스
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

    // 달력이 다시 활성화 됐을 때 현재의 날짜로 다시 초기화 하는 코드
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

    // 해당하는 년, 월의 달력을 보여주며 과거의 날짜 버튼은 비활성화 하기 위한 코드
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

    // 버튼 비활성화를 관리하기 위한 코드
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

    // 버튼을 완전히 숨기지 않고 비활성화하기 위한 코드
    void HalfDisableButton(int index)
    {
        Button dayButton = dayList[index].GetComponent<Button>();
        ColorBlock cb = dayButton.colors;
        cb.disabledColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);
        dayButton.colors = cb;

        dayButton.interactable = false;
    }

    // 년, 월에 맞게 텍스트 변환
    void SetYearMonthText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(year.ToString()).Append(".").Append(month.ToString());

        ymText.text = sb.ToString();
    }

    // 다음 월을 선택하기 위한 버튼 기능
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

    // 이전 월을 선택하기 위한 버튼 기능
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
