using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

// 좌석 정보, 클리어, 잘못된 좌석 선택 등 알림을 관리하는 클래스
public class AlertManager : MonoBehaviour
{
    // 알람 상태
    public enum AlertState
    {
        Clear,
        Fail,
        End
    }

    public GameObject clearAlert;
    public GameObject infoAlert;

    public TextMeshProUGUI text;

    public TextMeshProUGUI areaInfoText;
    public TextMeshProUGUI seatInfoText;

    AlertState state;

    CanvasMgr canvasMgr;
    ManageScore manageScore;

    private void Start()
    {
        canvasMgr = GameObject.FindWithTag("CanvasManager").GetComponent<CanvasMgr>();
        manageScore = GameObject.FindWithTag("GlobalCanvas").GetComponent<ManageScore>();

        this.gameObject.SetActive(false);
    }

    // 좌석 선택에 따른 알람 결정
    public void Alert(AlertState state)
    {
        manageScore.isGameStart = false;

        this.state = state;

        StringBuilder sb = new StringBuilder();

        switch (state)
        {
            case AlertState.Clear:
                sb.Append("예매 성공!");
                break;
            case AlertState.Fail:
                if (Random.Range(0, 10) >= 5)
                    sb.Append("예매 실패");
                else
                    sb.Append("이미 선택된\n좌석입니다.");
                break;
            case AlertState.End:
                sb.Append("예매 성공!\n총점: ").Append(manageScore.GetScore());
                break;
        }

        text.text = sb.ToString();

        infoAlert.SetActive(false);
        clearAlert.SetActive(true);
        this.gameObject.SetActive(true);
    }

    // 알람 창 닫기
    public void CloseAlert()
    {
        if (state == AlertState.Clear)
        {
            canvasMgr.Init();
        }
        else if (state == AlertState.End)
        {
            canvasMgr.ResetGame();
        }
        else
            manageScore.isGameStart = true;

        this.gameObject.SetActive(false);
        
    }

    // 별도의 처리 없이 단순히 알람을 닫기위한 기능
    public void SimpleCloseAlert()
    {
        this.gameObject.SetActive(false);
    }

    // 좌석 선택 화면으로 넘어가는 코드
    public void InSeat()
    {
        clearAlert.SetActive(true);
        infoAlert.SetActive(false);
        this.gameObject.SetActive(false);

        canvasMgr.OpenSeatSelectionCanvas();
    }

    // 구역을 선택했을 때 좌석에 대한 정보를 띄워주는 코드
    public void ChangeInfo(AreaButton.StandingState standingState, string area, bool isSeat)
    {
        StringBuilder sb = new StringBuilder();

        switch (standingState)
        {
            case AreaButton.StandingState.None:
                sb.Append("Seat");
                break;
            case AreaButton.StandingState.Standing:
                sb.Append("Standing");
                break;
        }
        sb.Append(" 구역 ").Append(area);
        areaInfoText.text = sb.ToString();

        if(isSeat)
        {
            seatInfoText.text = "잔여 좌석 <color=#0000FF>있음</color>";
        }
        else
        {
            seatInfoText.text = "잔여 좌석 <color=#FF0000>없음</color>";
        }
    }
}
