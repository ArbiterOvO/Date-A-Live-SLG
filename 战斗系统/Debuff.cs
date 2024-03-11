using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff
{
    public BuffType type;//Buff类别
    public float rate;//减少百分百 
    public int endRound;//结束回合
    public Debuff(BuffType type, float rate, int endRound)
    {
        this.type = type;
        this.rate = rate;
        this.endRound = endRound;
    }
}
