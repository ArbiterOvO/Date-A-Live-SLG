using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //主游戏单例
    public static GameManager instance;
    void Awake() {
        if(GameManager.instance!=null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance=this;
        }
        DontDestroyOnLoad(instance);
        createRoles();
        createTeam();
    }
    void Start() {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
            currentMap=1;
            break;
            case "Shine":
            currentMap=2;
            break;        
        }
    }
    //游戏状态
    public GameStatus gameStatus=GameStatus.UI可用;
    //特殊战斗
    public int specialBattleNum=0;
    //当前所在的地图 1开始
    public int currentMap;
    //通关地图数
    public int winNum;
    //角色列表(总体)
    public List<BaseRole> roles=new List<BaseRole>();
    //编队
    public List<BaseRole> rolesInTeam=new List<BaseRole>();
    //总能量
    public float totalPower;
    //当前能量
    public float currentPower;
    //金钱
    public int money;
    //日期
    public int date=1;
    //时间
    public int time=0;//0上午 1中午 2下午 3傍晚 4晚上 5结束一天
    //初始化编队
    public void createTeam()
    {
        rolesInTeam.Clear();
        for (int i = 0; i < roles.Count; i++)
        {
            rolesInTeam.Add(roles[i]);
        }
        
    }

    //初始化角色
    public void createRoles()
    {
        //初始化角色
        roles.Add(new BaseRole(0,"五河琴里",QinLi.blood,QinLi.cost,QinLi.power,QinLi.ad,QinLi.ap,QinLi.def,QinLi.mdf));
        roles.Add(new BaseRole(1,"夜刀神十香",ShiXiang.blood,ShiXiang.cost,ShiXiang.power,ShiXiang.ad,ShiXiang.ap,ShiXiang.def,ShiXiang.mdf));
        //roles.Add(new BaseRole(2,"冰芽川四糸乃",SiSiNai.blood,SiSiNai.cost,SiSiNai.power,SiSiNai.ad,SiSiNai.ap,SiSiNai.def,SiSiNai.mdf));
        roles.Add(new BaseRole(3,"时崎狂三",KuangSan.blood,KuangSan.cost,KuangSan.power,KuangSan.ad,KuangSan.ap,KuangSan.def,KuangSan.mdf));
        //测试
        foreach (BaseRole role in roles)
        {
            role.Exp=50;
            for (int i = 0; i < 3; i++)
            {
                role.levelUp();
            }
            Debug.Log(role.toString());
        }
    }
    //通过姓名找角色
    public BaseRole findRoleByName(String name)
    {
        foreach (BaseRole role in roles)
        {
            if(role.Name.Equals(name))
            {
                return role;
            }
        }
        return null;
    }
    //通过id找角色
    public BaseRole findRoleById(int id)
    {
        foreach (BaseRole role in roles)
        {
            if(role.Id==id)
            {
                return role;
            }
        }
        return null;
    }
    
}
