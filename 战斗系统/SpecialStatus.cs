using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialStatus
{
    public FightSpecialStatus statusType;//异常类别
    public int statusTime;//异常持续时间
    public float probability;//异常可能性
    public SpecialStatus(FightSpecialStatus statusType, int statusTime, float probability)
    {
        this.statusType=statusType;
        this.statusTime=statusTime;
        this.probability=probability;
    }
}
