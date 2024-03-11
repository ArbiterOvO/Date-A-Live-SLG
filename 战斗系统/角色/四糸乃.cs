using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 四糸乃 : BaseFightRole
{
    public 四糸乃(int id,String name,float level,float maxBlood,float cost,float power,float ad,float ap,float def,float mdf,int activeSkillNum)
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
        skills.Add(0,SiSiNai.activeSkill1);
        skills.Add(1,SiSiNai.activeSkill2);
    }
    public override void passiveSkill()
    {
        
    }
    public override void normalAttack()
    {
        if(!isActed)
        {
            Debug.Log(Name+"使用普通攻击");
            SpecialStatus status=new SpecialStatus(FightSpecialStatus.冰冻,1,0.3f);
            FightManager.instance.normalAttack(this,1,status);
        }
    }
    public override void activeSkill1()
    {
        if(ActiveSkillNum<1||isActed)
        return;
        FightManager.instance.skillAttack(this,4,1,VFXManager.Instance.iceAttack,FightSoundManager.Instance.skillAttackClip,null);
    }

    public override void activeSkill2()
    {
        if(ActiveSkillNum<2)
        return;
        Debuff debuff=new Debuff(BuffType.def,0.5f,FightManager.instance.roundNum+3);
        FightManager.instance.skillAttack(this,1,0,VFXManager.Instance.debuff,FightSoundManager.Instance.debuff,null,debuff);
        
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
