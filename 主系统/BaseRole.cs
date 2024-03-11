using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BaseRole
{
    private int id;
    public int Id
    {
        get
        {
            return id;
        }
    }
    //名称
    private String name;
    public String Name
    {
        get
        {
            return name;
        }
        set
        {
            name=value;
        }
    }
    //等级
    private float level;
    public float Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }
    //血量
    private float maxBlood;
    public float MaxBlood
    {
        get
        {
            return maxBlood;
        }
        set
        {
            maxBlood = value;
        }
    }
    //血量
    private float blood;
    public float Blood
    {
        get
        {
            return blood;
        }
        set
        {
            blood = value;
        }
    }
    //花费
    private float cost;
    public float Cost
    {
        get
        {
            return cost;
        }
        set
        {
            cost = value;
        }
    }
    //攻击力
    private float ad;
    public float Ad
    {
        get
        {
            return ad;
        }
        set
        {
            ad = value;
        }
    }
    //魔法攻击力
    private float ap;
    public float Ap
    {
        get
        {
            return ap;
        }
        set
        {
            ap=value;
        }
    }
    //物抗
    private float def;
    public float Def
    {
        get
        {
            return def;
        }
        set
        {
            def=value;
        }
    }
    //魔抗
    private float mdf;
    public float Mdf
    {
        get
        {
            return mdf;
        }
        set
        {
            mdf=value;
        }
    }
    //最大灵力
    private float power;
    public float Power
    {
        get
        {
            return power;
        }
        set
        {
            power = value;
        }
    }
    //经验
    private float exp;
    public float Exp
    {
        get
        {
            return exp;
        }
        set
        {
            exp = value;
        }
    }

    private int activeSkillNum;
    //主动技能数量
    public int ActiveSkillNum()
    {
        return activeSkillNum;
    }
    public List<LingJieJing> lingJieJings=new List<LingJieJing>(3);
    //是否拥有
    public bool isHaving; 
    public BaseRole(int id,String name,float maxBlood,float cost,float power,float ad,float ap,float def,float mdf)
    {
        this.id=id;
        this.Name=name;
        this.Level=1;
        this.MaxBlood=maxBlood;
        this.Blood=maxBlood;
        this.Cost=cost;
        this.Ad=ad;
        this.Ap=ap;
        this.Def=def;
        this.Mdf=mdf;
        this.Power=power;
        this.exp=0;
        this.activeSkillNum=1;
        this.isHaving=false;
        this.lingJieJings.Add(null);
        this.lingJieJings.Add(null);
        this.lingJieJings.Add(null);
    }
    public String toString()
    {
        return name+" Level:"+level+" Blood:"+blood+" Cost:"+cost+" ad:"+ad+" Ap:"+ap+" AttackSpeed:"+
        def+" Mdf:"+mdf+" Power:"+power+" Exp:"+exp+" activeSkillNum:"+activeSkillNum+" IsHaving:"+isHaving;
    }
    public void updateDate()
    {
        float upBlood=0,upAd=0,upAp=0,upDef=0,upMdf=0;
        Type type=roleUtil.getRoleType(id);
        Debug.Log(type.GetField("upBlood").GetRawConstantValue());
        upBlood=(float)type.GetField("upBlood").GetRawConstantValue();
        upAd=(float)type.GetField("upAd").GetRawConstantValue();
        upAp=(float)type.GetField("upAp").GetRawConstantValue();
        upDef=(float)type.GetField("upDef").GetRawConstantValue();
        upMdf=(float)type.GetField("upMdf").GetRawConstantValue();
        Level++;
        blood+=upBlood;
        ad+=upAd;
        ap+=upAp;
        def+=upDef;
        mdf+=upMdf;
        exp=0;
    }
    public void levelUp()
    {
        if(level==10)
        return;
        updateDate();
        if(level<3)  //1-2
        {
            activeSkillNum=1;
        }
        else if(level<5) //3-4
        {
            activeSkillNum=2;
        }
        else if(level<7) //5-6
        {
            activeSkillNum=3;
        }
        else if(level<8) //7
        {
            activeSkillNum=4;
        }
        else //8-10
        {
            activeSkillNum=5;
        }
    }

    public void addExp(float n)
    {
        this.exp+=n;
        if(exp>=100)
        {
            levelUp();
        }
    }   
}
