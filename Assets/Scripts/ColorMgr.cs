using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임에 필요한 색상을 정의 한 클래스
public class ColorMgr : MonoBehaviour
{
    [Header("Area Color")]
    public Color vipColor;
    public Color rColor;
    public Color sColor;
    public Color aColor;
    public Color bColor;
    public Color accessibleColor;

    [Header("Seat Color")]
    public Color defaultSeatColor;
    public Color currectSeatColor;
}
