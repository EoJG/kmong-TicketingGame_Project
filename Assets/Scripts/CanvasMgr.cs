using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 캔버스간의 이동을 관리하는 코드
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

    // 구역 선택 화면을 띄우는 코드
    public void OpenAreaSelectionCanvas()
    {
        areaSelectionCanvas.SetActive(true);
        seatSelectionCanvas.SetActive(false);
    }

    // 좌석 선택 화면을 띄우는 코드
    public void OpenSeatSelectionCanvas()
    {
        seatSelectionCanvas.SetActive(true);
        areaSelectionCanvas.SetActive(false);
    }

    // 메인 타이틀 화면을 띄우는 코드
    public void OpenTitle()
    {
        titleCanvas.SetActive(true);
        seatSelectionCanvas.SetActive(false);
        areaSelectionCanvas.SetActive(false);
    }

    // 구역에 대한 정보 알림을 띄우는 코드
    public void OpenInfo(AreaButton.StandingState standingState, string area, bool isSeat)
    {
        alertManager.ChangeInfo(standingState, area, isSeat);

        alertCanvas.SetActive(true);
        clear.SetActive(false);
        info.SetActive(true);
    }

    // 선택 가능한 좌석의 색상을 변경하는 코드
    public void PaintSeatColor(int seat, bool check)
    {
        if (check)
            seatSelectionCanvas.transform.GetChild(1).GetChild(seat).GetComponent<Image>().color = colorMgr.currectSeatColor;
        else
            seatSelectionCanvas.transform.GetChild(1).GetChild(seat).GetComponent<Image>().color = colorMgr.defaultSeatColor;
    }

    // 현재 선택된 구역의 이름을 알려주기위한 텍스트 변경 코드
    public void CurrectAreaText(string area)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(defaultAreaStr).Append(area).Append("-");
        text.text = sb.ToString();
    }

    // 다음 스테이지 이동을 위한 초기화 코드
    public void Init()
    {
        OpenAreaSelectionCanvas();
        randomSeatMgr.MakeRandomSeat();
        manageScore.InitChance();
        manageScore.InitTimer();

        manageScore.isGameStart = true;
    }

    // 게임 종료, 오버 시 초기화 후 다시 시작하기 위한 코드
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
