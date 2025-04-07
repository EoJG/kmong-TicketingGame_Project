using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// 구역 선택을 위한 코드
public class AreaButton : MonoBehaviour
{
    // 구역의 등급을 보여주는 상태
    public enum ColorState
    {
        VIP,
        R,
        S,
        A,
        B,
        Accessible
    }

    // 스탠딩, 좌석 중 한가지를 선택하기 위한 상태
    public enum StandingState
    {
        None,
        Standing
    }

    ColorMgr colorMgr;
    CanvasMgr canvasMgr;
    RandomSeatMgr randomSeatMgr;

    public ColorState state = ColorState.VIP;
    public StandingState standing = StandingState.None;

    TextMeshProUGUI text;

    private void Awake()
    {
        colorMgr = GameObject.FindWithTag("ColorManager").GetComponent<ColorMgr>();
        canvasMgr = GameObject.FindWithTag("CanvasManager").GetComponent<CanvasMgr>();
        randomSeatMgr = GameObject.FindWithTag("RandomSeatManager").GetComponent<RandomSeatMgr>();
    }

    // 선택된 등급에 따른 구역 색상 변경을 위한 코드
    public void ChooseColor(ColorState colorState)
    {
        Image img = GetComponent<Image>();

        switch (colorState)
        {
            case ColorState.VIP:
                img.color = colorMgr.vipColor;
                break;
            case ColorState.R:
                img.color = colorMgr.rColor;
                break;
            case ColorState.S:
                img.color = colorMgr.sColor;
                break;
            case ColorState.A:
                img.color = colorMgr.aColor;
                break;
            case ColorState.B:
                img.color = colorMgr.bColor;
                break;
            case ColorState.Accessible:
                img.color = colorMgr.accessibleColor;
                break;
        }
    }

    private void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        ChooseColor(state);
    }

    // 구역을 눌렀을 때 해당 좌석에 대한 정보 알람을 표시하기 위한 코드
    public void InSeat()
    {
        if (this.gameObject == randomSeatMgr.targetObj)
            canvasMgr.PaintSeatColor(randomSeatMgr.buttonsPos, true);
        else
            canvasMgr.PaintSeatColor(randomSeatMgr.buttonsPos, false);

        canvasMgr.CurrectAreaText(text.text);
        canvasMgr.OpenInfo(standing, text.text, this.gameObject == randomSeatMgr.targetObj);
    }
}
