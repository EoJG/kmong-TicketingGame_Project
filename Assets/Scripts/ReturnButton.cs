using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 뒤로가기 버튼을 위한 클래스
public class ReturnButton : MonoBehaviour
{
    CanvasMgr canvasMgr;

    void Start()
    {
        canvasMgr = GameObject.FindWithTag("CanvasManager").GetComponent<CanvasMgr>();
    }

    // 좌석 선택 화면에서 구역 선택화면으로 돌아가기위한 코드
    public void ReturnSeatSelection()
    {
        canvasMgr.OpenAreaSelectionCanvas();
    }
}
