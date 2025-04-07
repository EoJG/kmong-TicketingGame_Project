using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

// �޷��� ���ں� ����� ����ϴ� Ŭ����
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

    // ��¥ ���� �� ��¥�� �´� ȸ�� ��ư�� ��� �� �ؽ�Ʈ�� �����ϴ� �ڵ�
    public void UpdateText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(year.ToString()).Append(".").Append(month.ToString()).Append(".").Append(day.ToString()).Append(" - ");

        firstText.text = sb.ToString();
        secondText.text = sb.ToString();

        firstText.text += "����";
        secondText.text += "����";
    }
}
