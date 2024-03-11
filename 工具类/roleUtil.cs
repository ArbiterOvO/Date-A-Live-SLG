using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class roleUtil
{
    public static Type getRoleType(int id)
    {
        Type role=null;
        switch(id)
        {
            case 0:
            role=typeof(QinLi);
            break;
            case 1:
            role=typeof(ShiXiang);
            break;
            case 2:
            role=typeof(SiSiNai);
            break;
            case 3:
            role=typeof(KuangSan);
            break;
        }
        return role;
    }
    //己方攻击
    public static void Attack(BaseFightRole role,BaseEnemy enemy,float rate)
    {
        
        //物理减伤率=敌方def/(敌方def+MAX(5(等级差),-30)+30)
        float defRate = enemy.Def/(enemy.Def+MathF.Max(5*(role.Level-enemy.Level),-30)+30);
        //物理伤害=己方ad*(1-物理减伤率)
        float defDamage = role.Ad*rate*(1-defRate);
        //魔法减伤率=敌方mdf/(敌方mdf+MAX(5(等级差),-30)+30)
        float magicRate = enemy.Mdf/(enemy.Mdf+MathF.Max(5*(role.Level-enemy.Level),-30)+30);
        //魔法伤害=己方ad*(1-魔法减伤率)
        float mdfDamage = role.Ap*rate*(1-magicRate);
        Debug.Log(defDamage+" "+mdfDamage);
        enemy.Blood=enemy.Blood-(defDamage+mdfDamage);
        
        //特殊状态检查
        //1.冰冻
        if(FightUI.instance.findEnemy(enemy).GetComponent<FightEnemy>().specialStatus==FightSpecialStatus.冰冻&&defDamage!=0)
        {
            FightUI.instance.findEnemy(enemy).GetComponent<FightEnemy>().specialStatus=FightSpecialStatus.正常;
            FightUI.instance.findEnemy(enemy).GetComponent<FightEnemy>().statusTime=0;
        }
        //2.眩晕
        if(FightUI.instance.findEnemy(enemy).GetComponent<FightEnemy>().specialStatus==FightSpecialStatus.眩晕&&mdfDamage!=0)
        {
            FightUI.instance.findEnemy(enemy).GetComponent<FightEnemy>().specialStatus=FightSpecialStatus.正常;
            FightUI.instance.findEnemy(enemy).GetComponent<FightEnemy>().statusTime=0;
        }

    }
    //敌方攻击
    public static void Attack(BaseEnemy enemy,BaseFightRole role,float rate)
    {
        //物理减伤率=敌方def/(敌方def+MAX(5(等级差),-30)+30)
        float defRate = role.Def/(role.Def+MathF.Max(5*(enemy.Level-role.Level),-30)+30);
        //物理伤害=敌方ad*(1-物理减伤率)
        float defDamage = enemy.Ad*rate*(1-defRate);
        //魔法减伤率=敌方mdf/(敌方mdf+MAX(5(等级差),-30)+30)
        float magicRate = role.Mdf/(role.Mdf+MathF.Max(5*(enemy.Level-role.Level),-30)+30);
        //物理伤害=敌方ad*(1-物理减伤率)
        float mdfDamage = enemy.Ap*rate*(1-magicRate);
        role.Blood=role.Blood-(defDamage+mdfDamage);
    }
}
