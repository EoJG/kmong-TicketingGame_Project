using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �¼� ���� ��ư�� ����� �����ϱ� ���� Ŭ����
public class SeatButton : MonoBehaviour
{
    ColorMgr colorMgr;
    ManageScore manageScore;

    Image image;

    void Start()
    {
        colorMgr = GameObject.FindWithTag("ColorManager").GetComponent<ColorMgr>();
        manageScore = GameObject.FindWithTag("GlobalCanvas").GetComponent<ManageScore>();
        image = GetComponent<Image>();
    }

    // ������ ��Ʈ�� ����, ������ ���ο� ���� ����� ó���ϴ� �ڵ�
    public void CurrectSeat()
    {
        if (colorMgr.currectSeatColor == image.color)
        {
            image.color = colorMgr.defaultSeatColor;

            manageScore.PlusScore();
        }
        else
        {
            manageScore.UseChance();
        }
    }
}
