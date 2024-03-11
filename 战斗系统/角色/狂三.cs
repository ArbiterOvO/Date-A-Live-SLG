using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 狂三 : BaseFightRole
{
    public 狂三(int id,String name,float level,float maxBlood,float cost,float maxPower,float ad,float ap,float def,float mdf,int activeSkillNum)
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
        this.Power=maxPower;
        this.MaxPower=maxPower;
        this.ActiveSkillNum=activeSkillNum;
        this.isActed=false;
        skills=new Dictionary<int, string>();
        skills.Add(0,KuangSan.activeSkill1);
        skills.Add(1,KuangSan.activeSkill2);
    }
    public override void passiveSkill()
    {

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
        SpecialStatus status=new SpecialStatus(FightSpecialStatus.时间停止,1,1);
        FightManager.instance.skillAttack(this,1,1,VFXManager.Instance.shootAttack,FightSoundManager.Instance.shootClip,status);
        
    }

    public override void activeSkill2()
    {
        if(ActiveSkillNum<2)
        return;
        FightManager.instance.healSlef(this,1,0.5f,VFXManager.Instance.upSelf,FightSoundManager.Instance.upSlef);
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
