using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public TextMeshProUGUI text;

    int num = 0;

    public void TestClickedButton()
    {
        num++;
        text.SetText(num.ToString());
    }
}
