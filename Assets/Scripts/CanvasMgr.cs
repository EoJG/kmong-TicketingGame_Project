using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ĵ�������� �̵��� �����ϴ� �ڵ�
public class CanvasMgr : MonoBehaviour
{
    ColorMgr colorMgr;
    RandomSeatMgr randomSeatMgr;
    ManageScore manageScore;
    [SerializeField]
    AlertManager alertManager;

    public GameObject areaSelectionCanvas;
    public GameObject seatSelectionCanvas;
    public GameObject titleCanvas;
    public GameObject alertCanvas;
    public GameObject clear;
    public GameObject info;

    TextMeshProUGUI text;

    string defaultAreaStr = "-Area ";

    private void Start()
    {
        colorMgr = GameObject.FindWithTag("ColorManager").GetComponent<ColorMgr>();
        randomSeatMgr = GameObject.FindWithTag("RandomSeatManager").GetComponent<RandomSeatMgr>();
        manageScore = GameObject.FindWithTag("GlobalCanvas").GetComponent<ManageScore>();
        text = seatSelectionCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // ���� ���� ȭ���� ���� �ڵ�
    public void OpenAreaSelectionCanvas()
    {
        areaSelectionCanvas.SetActive(true);
        seatSelectionCanvas.SetActive(false);
    }

    // �¼� ���� ȭ���� ���� �ڵ�
    public void OpenSeatSelectionCanvas()
    {
        seatSelectionCanvas.SetActive(true);
        areaSelectionCanvas.SetActive(false);
    }

    // ���� Ÿ��Ʋ ȭ���� ���� �ڵ�
    public void OpenTitle()
    {
        titleCanvas.SetActive(true);
        seatSelectionCanvas.SetActive(false);
        areaSelectionCanvas.SetActive(false);
    }

    // ������ ���� ���� �˸��� ���� �ڵ�
    public void OpenInfo(AreaButton.StandingState standingState, string area, bool isSeat)
    {
        alertManager.ChangeInfo(standingState, area, isSeat);

        alertCanvas.SetActive(true);
        clear.SetActive(false);
        info.SetActive(true);
    }

    // ���� ������ �¼��� ������ �����ϴ� �ڵ�
    public void PaintSeatColor(int seat, bool check)
    {
        if (check)
            seatSelectionCanvas.transform.GetChild(1).GetChild(seat).GetComponent<Image>().color = colorMgr.currectSeatColor;
        else
            seatSelectionCanvas.transform.GetChild(1).GetChild(seat).GetComponent<Image>().color = colorMgr.defaultSeatColor;
    }

    // ���� ���õ� ������ �̸��� �˷��ֱ����� �ؽ�Ʈ ���� �ڵ�
    public void CurrectAreaText(string area)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(defaultAreaStr).Append(area).Append("-");
        text.text = sb.ToString();
    }

    // ���� �������� �̵��� ���� �ʱ�ȭ �ڵ�
    public void Init()
    {
        OpenAreaSelectionCanvas();
        randomSeatMgr.MakeRandomSeat();
        manageScore.InitChance();
        manageScore.InitTimer();

        manageScore.isGameStart = true;
    }

    // ���� ����, ���� �� �ʱ�ȭ �� �ٽ� �����ϱ� ���� �ڵ�
    public void ResetGame()
    {
        OpenAreaSelectionCanvas();
        randomSeatMgr.MakeRandomSeat();
        manageScore.InitChance();
        manageScore.InitScore();
        manageScore.InitTimer();
        randomSeatMgr.MakeRandomSeat();
        manageScore.isGameStart = false;

        OpenTitle();
    }
}
