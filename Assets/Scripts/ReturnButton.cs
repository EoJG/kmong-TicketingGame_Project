using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڷΰ��� ��ư�� ���� Ŭ����
public class ReturnButton : MonoBehaviour
{
    CanvasMgr canvasMgr;

    void Start()
    {
        canvasMgr = GameObject.FindWithTag("CanvasManager").GetComponent<CanvasMgr>();
    }

    // �¼� ���� ȭ�鿡�� ���� ����ȭ������ ���ư������� �ڵ�
    public void ReturnSeatSelection()
    {
        canvasMgr.OpenAreaSelectionCanvas();
    }
}
