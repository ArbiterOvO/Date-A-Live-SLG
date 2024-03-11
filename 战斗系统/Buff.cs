using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public BuffType type;//Buff类别
    public float value;//提升数值
    public int endRound;//结束回合
    public Buff(BuffType type, float value, int endRound)
    {
        this.type = type;
        this.value = value;
        this.endRound = endRound;
    }
}
