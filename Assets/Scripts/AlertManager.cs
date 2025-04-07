using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

// �¼� ����, Ŭ����, �߸��� �¼� ���� �� �˸��� �����ϴ� Ŭ����
public class AlertManager : MonoBehaviour
{
    // �˶� ����
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

    // �¼� ���ÿ� ���� �˶� ����
    public void Alert(AlertState state)
    {
        manageScore.isGameStart = false;

        this.state = state;

        StringBuilder sb = new StringBuilder();

        switch (state)
        {
            case AlertState.Clear:
                sb.Append("���� ����!");
                break;
            case AlertState.Fail:
                if (Random.Range(0, 10) >= 5)
                    sb.Append("���� ����");
                else
                    sb.Append("�̹� ���õ�\n�¼��Դϴ�.");
                break;
            case AlertState.End:
                sb.Append("���� ����!\n����: ").Append(manageScore.GetScore());
                break;
        }

        text.text = sb.ToString();

        infoAlert.SetActive(false);
        clearAlert.SetActive(true);
        this.gameObject.SetActive(true);
    }

    // �˶� â �ݱ�
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

    // ������ ó�� ���� �ܼ��� �˶��� �ݱ����� ���
    public void SimpleCloseAlert()
    {
        this.gameObject.SetActive(false);
    }

    // �¼� ���� ȭ������ �Ѿ�� �ڵ�
    public void InSeat()
    {
        clearAlert.SetActive(true);
        infoAlert.SetActive(false);
        this.gameObject.SetActive(false);

        canvasMgr.OpenSeatSelectionCanvas();
    }

    // ������ �������� �� �¼��� ���� ������ ����ִ� �ڵ�
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
        sb.Append(" ���� ").Append(area);
        areaInfoText.text = sb.ToString();

        if(isSeat)
        {
            seatInfoText.text = "�ܿ� �¼� <color=#0000FF>����</color>";
        }
        else
        {
            seatInfoText.text = "�ܿ� �¼� <color=#FF0000>����</color>";
        }
    }
}
