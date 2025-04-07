using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 좌석 선택 버튼의 기능을 관리하기 위한 클래스
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

    // 선택한 시트가 정답, 오답의 여부에 따라 기능을 처리하는 코드
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
