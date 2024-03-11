using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 十香 : BaseFightRole
{
    
    public 十香(int id,String name,float level,float maxBlood,float cost,float power,float ad,float ap,float def,float mdf,int activeSkillNum)
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
        skills.Add(0,ShiXiang.activeSkill1);
        skills.Add(1,ShiXiang.activeSkill2);
    }
    public override void passiveSkill()
    {
        foreach (var role in FightManager.instance.fightRoles)
        {
            role.Def+=this.MaxBlood*0.05f;
            FightUI.instance.findRoleById(role.Id).GetComponent<FightRole>().addBuffTip(BuffType.def);
        }
    }
    public override void normalAttack()
    {
        if(!isActed)
        {
            Debug.Log(Name+"使用普通攻击");
            FightManager.instance.normalAttack(this,1,null);
        }
    }
    public override void activeSkill1()
    {
        if(ActiveSkillNum<1||isActed)
        return;
        SpecialStatus status=new SpecialStatus(FightSpecialStatus.眩晕,1,1);
        FightManager.instance.skillAttack(this,1,1.5f,(Transform pos)=>{VFXManager.Instance.wangZuoJiangLing(pos);},FightSoundManager.Instance.skillAttackClip,status);
        Debug.Log("十香释放了技能1");
    }

    public override void activeSkill2()
    {
        if(ActiveSkillNum<2||isActed)
        return;
        Debug.Log("十香释放了技能2");
        foreach (var role in FightManager.instance.fightRoles)
        {
            if(role.Ad!=0)
            {
                Buff buff=new Buff(BuffType.ad,Ad*0.2f,2+FightManager.instance.roundNum);
                FightUI.instance.findRoleById(role.Id).GetComponent<FightRole>().addBuff(buff);
            }
            if(role.Ap!=0)
            {
                Buff buff=new Buff(BuffType.ap,Ad*0.2f,2+FightManager.instance.roundNum);
                FightUI.instance.findRoleById(role.Id).GetComponent<FightRole>().addBuff(buff);
            }
            VFXManager.Instance.upSelfVFX(this,FightUI.instance.findRoleById(role.Id).transform,VFXManager.Instance.upSelf,FightSoundManager.Instance.upSlef);
            
        }
        isActed=true;
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
        
    }
}
