using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// ������������ ������ �Ѱ��� �¼��� �����ϱ� ���� Ŭ����
public class RandomSeatMgr : MonoBehaviour
{
    public Transform buttons;

    public GameObject targetObj;
    public int buttonsPos;
    List<GameObject> childList = new List<GameObject>();

    public ColorMgr colorMgr;
    SortedDictionary<string, List<AreaButton.ColorState>> seatAreaList;
    SortedDictionary<string, List<AreaButton.ColorState>> standAreaList;

    public TextMeshProUGUI hintText;

    //debug code
    public bool isDebug = false;

    private void Awake()
    {
        seatAreaList = new SortedDictionary<string, List<AreaButton.ColorState>>();
        standAreaList = new SortedDictionary<string, List<AreaButton.ColorState>>();
    }

    void Start()
    {
        foreach (Transform child in buttons)
        {
            childList.Add(child.gameObject);

            string key = child.GetChild(0).GetComponent<TextMeshProUGUI>().text;

            if (child.GetComponent<AreaButton>().standing == AreaButton.StandingState.None)
            {
                if (!seatAreaList.ContainsKey(key))
                    seatAreaList[key] = new List<AreaButton.ColorState>();

                seatAreaList[key].Add(child.GetComponent<AreaButton>().state);
            }
            else
            {
                if (!standAreaList.ContainsKey(key))
                    standAreaList[key] = new List<AreaButton.ColorState>();

                standAreaList[key].Add(child.GetComponent<AreaButton>().state);
            }
        }
    }

    private void Update()
    {
        if (isDebug)
        {
            if (targetObj != null)
            {
                targetObj.GetComponent<Image>().color = Color.white;
            }
        }
    }

    // ��ü ���� �� �Ѱ��� �¼��� �����ϴ� �ڵ�
    public void MakeRandomSeat()
    {
        if (isDebug)
        {
            if (targetObj != null)
            {
                //debug code
                targetObj.GetComponent<AreaButton>().ChooseColor(targetObj.GetComponent<AreaButton>().state);
            }
        }

        targetObj = childList[Random.Range(0, childList.Count)];
        buttonsPos = Random.Range(0, 400);

        AreaButton areaBtn = targetObj.GetComponent<AreaButton>();
        switch (areaBtn.standing)
        {
            case AreaButton.StandingState.None:
                hintText.text = "��Ŭ�� ������ �Ⱓ�� ������\n�÷ξ� ���ĵ� �¼��� ����";
                hintText.text += "\n" + GetSeatRandomHint(areaBtn);
                break;
            case AreaButton.StandingState.Standing:
                hintText.text = "��Ŭ�� �����Űǵ� ���ǥ Ǯ���� �ð��̴�!\n���ĵ� �¼��� �������.";
                hintText.text += "\n" + GetStandingRandomHint(areaBtn);
                break;
        }
    }

    // ������ �¼��� �� ���� ���� ��Ʈ�� �����ϴ� �ڵ�
    string GetSeatRandomHint(AreaButton tempButton)
    {
        SortedDictionary<string, List<AreaButton.ColorState>> tmpSeatDict = seatAreaList;

        string answer = tempButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

        List<string> randList = new List<string>();
        if (tmpSeatDict.ContainsKey(answer))
        {
            randList.Add(SetCharColor(answer, tempButton.state));
            tmpSeatDict.Remove(answer);
        }

        List<string> keys = tmpSeatDict.Keys.ToList();
        keys = keys.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < 2; i++)
        {
            string key = keys[i];
            List<AreaButton.ColorState> colorStates = tmpSeatDict[key];

            if (colorStates != null && colorStates.Count > 0)
            {
                int randomIndex = Random.Range(0, colorStates.Count);
                randList.Add(SetCharColor(key, colorStates[randomIndex]));
            }
        }

        return Shuffle(randList);
    }

    // ������ ���ĵ����� �� ���� ���� ��Ʈ�� �����ϴ� �ڵ�
    string GetStandingRandomHint(AreaButton tempButton)
    {
        SortedDictionary<string, List<AreaButton.ColorState>> tmpStandDict = standAreaList;

        string answer = tempButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

        List<string> randList = new List<string>();
        if (tmpStandDict.ContainsKey(answer))
        {
            randList.Add(SetCharColor(answer, tempButton.state));
            tmpStandDict.Remove(answer);
        }

        List<string> keys = tmpStandDict.Keys.ToList();
        keys = keys.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < 2; i++)
        {
            string key = keys[i];
            List<AreaButton.ColorState> colorStates = tmpStandDict[key];

            if (colorStates != null && colorStates.Count > 0)
            {
                int randomIndex = Random.Range(0, colorStates.Count);
                randList.Add(SetCharColor(key, colorStates[randomIndex]));
            }
        }

        return Shuffle(randList);
    }

    // ��Ʈ�� ������ �����ִ� �ڵ�
    string Shuffle(List<string> strList)
    {
        int maxIndex = strList.Count;
        string outStr = "";

        int index = 0;
        for (int i = 0; i < maxIndex; i++)
        {
            index = Random.Range(0, strList.Count);
            outStr += strList[index];
            strList.RemoveAt(index);

            if (i < (maxIndex - 1))
            {
                outStr += ", ";
            }
        }

        return outStr;
    }

    // ��Ʈ�� �� ������ ������ ĥ���ִ� �ڵ�
    string SetCharColor(string str, AreaButton.ColorState state)
    {
        StringBuilder sb = new StringBuilder();
        Color32 color;
        string tmp = "";

        sb.Append("<color=#");
        switch (state)
        {
            case AreaButton.ColorState.VIP:
                color = colorMgr.vipColor;
                sb.Append(color.r.ToString("X2"));
                sb.Append(color.g.ToString("X2"));
                sb.Append(color.b.ToString("X2")).Append(">");
                break;
            case AreaButton.ColorState.R:
                color = colorMgr.rColor;
                sb.Append(color.r.ToString("X2"));
                sb.Append(color.g.ToString("X2"));
                sb.Append(color.b.ToString("X2")).Append(">");
                break;
            case AreaButton.ColorState.S:
                color = colorMgr.sColor;
                sb.Append(color.r.ToString("X2"));
                sb.Append(color.g.ToString("X2"));
                sb.Append(color.b.ToString("X2")).Append(">");
                break;
            case AreaButton.ColorState.A:
                color = colorMgr.aColor;
                sb.Append(color.r.ToString("X2"));
                sb.Append(color.g.ToString("X2"));
                sb.Append(color.b.ToString("X2")).Append(">");
                break;
            case AreaButton.ColorState.B:
                color = colorMgr.bColor;
                sb.Append(color.r.ToString("X2"));
                sb.Append(color.g.ToString("X2"));
                sb.Append(color.b.ToString("X2")).Append(">");
                break;
            case AreaButton.ColorState.Accessible:
                color = colorMgr.accessibleColor;
                sb.Append(color.r.ToString("X2"));
                sb.Append(color.g.ToString("X2"));
                sb.Append(color.b.ToString("X2")).Append(">");
                break;
        }
        sb.Append(str).Append("</color>");
        tmp = sb.ToString();

        return tmp;
    }
}
