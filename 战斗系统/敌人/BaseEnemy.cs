using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy
{
    //对应游戏对象
    GameObject enemyObject;
    //id
    private int id;
    public int Id
    {
        get{
            return id;
        }
        set{
            id = value;
        }
    }
    //名称
    private string name;
    public string Name
    {
        get{
            return name;
        }
        set{
            name = value;
        }
    }
    //等级
    private int level;
    public int Level
    {
        get{
            return level;
        }
        set{
            level=value;
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
    //
    //是否行动过
    public bool isActed;
    public abstract void normalAttack();
}
