using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// ���� ������ ���� �ڵ�
public class AreaButton : MonoBehaviour
{
    // ������ ����� �����ִ� ����
    public enum ColorState
    {
        VIP,
        R,
        S,
        A,
        B,
        Accessible
    }

    // ���ĵ�, �¼� �� �Ѱ����� �����ϱ� ���� ����
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

    // ���õ� ��޿� ���� ���� ���� ������ ���� �ڵ�
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

    // ������ ������ �� �ش� �¼��� ���� ���� �˶��� ǥ���ϱ� ���� �ڵ�
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
