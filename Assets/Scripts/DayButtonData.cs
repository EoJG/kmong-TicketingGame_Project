using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

// 달력의 일자별 기능을 담당하는 클래스
public class DayButtonData : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI firstText, secondText;

    public int year, month, day;

    public void SetDate(int y, int m, int d)
    {
        year = y;
        month = m;
        day = d;
    }

    // 날짜 선택 시 날짜에 맞는 회차 버튼을 출력 후 텍스트를 변경하는 코드
    public void UpdateText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(year.ToString()).Append(".").Append(month.ToString()).Append(".").Append(day.ToString()).Append(" - ");

        firstText.text = sb.ToString();
        secondText.text = sb.ToString();

        firstText.text += "오전";
        secondText.text += "오후";
    }
}
