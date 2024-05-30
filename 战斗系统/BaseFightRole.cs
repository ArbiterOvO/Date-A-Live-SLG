using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFightRole
{
    public Dictionary<int,string> skills;
    //id
    private int id;
    public int Id
    {
        set
        {
            id=value;
        }
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
    //最大血量
    private float maxBlood;
    public float MaxBlood
    {
        get
        {
            return maxBlood;
        }
        set
        {
            maxBlood=value;
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
    //当前灵力
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
    //最大灵力
    private float maxPower;
    public float MaxPower
    {
        get
        {
            return maxPower;
        }
        set
        {
            maxPower = value;
        }
    }
    private int activeSkillNum;
    //主动技能数量
    public int ActiveSkillNum
    {
        get{
            return activeSkillNum;
        }
        set{
            activeSkillNum = value;
        }
        
    }
    //是否行动过
    public bool isActed;
    public bool isLing;
    public void changeLing()
    {
        Debug.Log(name+"灵装化");
        FightManager.instance.power-=Cost;
        GameManager.instance.currentPower-=Cost;
        isLing=true;
        Blood+=0.5f*maxBlood;
        maxBlood*=1.5f;
        ad*=1.5f;
        ap*=1.5f;
        FadeInOut.instance.FadeInAndOUt(this);
        FightUI.instance.clearAllButton();
    }
    public abstract void passiveSkill();
    public abstract void normalAttack();
    public abstract void activeSkill1();
    public abstract void activeSkill2();
    public abstract void activeSkill3();
    public abstract void activeSkill4();
    public abstract void activeSkill5();
    public abstract void powerSkill();
    public abstract void bePowerful();
}
