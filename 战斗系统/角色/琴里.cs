using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 琴里 : BaseFightRole
{
    public 琴里(int id,String name,float level,float maxBlood,float cost,float power,float ad,float ap,float def,float mdf,int activeSkillNum)
    {
        this.Id=id;
        this.Name=name;
        this.Level=level;
        this.Blood=maxBlood;
        this.MaxBlood=maxBlood;
        this.Cost=cost;
        this.Ad=ad;
        this.Ap=ap;
        this.Def=def;
        this.Mdf=mdf;
        this.Power=power;
        this.ActiveSkillNum=activeSkillNum;
        this.isActed=false;
        skills=new Dictionary<int, string>();
        skills.Add(0,QinLi.activeSkill1);
        skills.Add(1,QinLi.activeSkill2);
    }

    public override void passiveSkill()
    {
        if(FightUI.instance.findRoleById(Id).GetComponent<FightRole>().died)
        return;
        if(Blood<MaxBlood/2)
        {
            Blood+=MaxBlood*0.08f;
        }
    }
    public override void normalAttack()
    {
        if(!isActed)
        {
            Debug.Log("琴里使用普通攻击");
            FightManager.instance.normalAttack(this,1,null);
        }
    }
    public override void activeSkill1()
    {
        if(ActiveSkillNum<1||isActed)
        return;
        FightManager.instance.skillAttack(this,4,1,VFXManager.Instance.fireAttack,FightSoundManager.Instance.skillAttackClip,null);
        Debug.Log("琴里使用主动技能1");
    }

    public override void activeSkill2()
    {
        if(ActiveSkillNum<2||isActed)
        return;
        Debug.Log("琴里使用主动技能2");
        FightUI.instance.findRoleById(Id).GetComponent<FightRole>().normalAttackRate=1.5f;
        FightUI.instance.findRoleById(Id).GetComponent<FightRole>().upNormalAttackTime=3;
        isActed=true;
        FightUI.instance.findRoleById(Id).GetComponent<FightRole>().addBuffTip(BuffType.普攻强化,FightManager.instance.roundNum+3);
        VFXManager.Instance.upSelfVFX(this,FightUI.instance.findRoleById(Id).transform,VFXManager.Instance.upSelf,FightSoundManager.Instance.upSlef);
    
    }

    public override void activeSkill3()
    {
        if(ActiveSkillNum<3)
        return;
    }

    public override void activeSkill4()
    {
        if(ActiveSkillNum<4)
        return;
    }

    public override void activeSkill5()
    {
        if(ActiveSkillNum<5)
        return;
    }

    public override void bePowerful()
    {
        if(!isLing)
        changeLing();
    }

    public override void powerSkill()
    {
        if(isLing)
        {
            Debug.Log("琴里使用终极技能");
        }
        
    }
}
