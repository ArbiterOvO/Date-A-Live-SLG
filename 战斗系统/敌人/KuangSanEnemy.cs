using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuangSanEnemy : BaseEnemy
{
    //boss 狂三
    public KuangSanEnemy()
    {
        Id=3;
        Name="KuangSan";
        Level=5;
        MaxBlood=200;
        Blood=MaxBlood;
        Ad=50;
        Ap=0;
        Def=30;
        Mdf=10;
        isActed=false;
    }
    public override void normalAttack()
    {
        if(FightUI.instance.findEnemy(this).GetComponent<FightEnemy>().specialStatus!=FightSpecialStatus.正常)
        return;
        if(FightManager.instance.fightStatus==FightStatus.敌方回合&&!isActed)
        {
            FightUI.instance.findEnemy(this).GetComponent<FightEnemy>().isAttacking=true;//正在攻击
            isActed=true;
            int i;
            int time=0;
            do
            {
                time++;
                i=Random.Range(0,FightManager.instance.fightRoles.Count);
                if(time>20)
                break;
            }while(FightUI.instance.findRoleById(FightManager.instance.fightRoles[i].Id).GetComponent<FightRole>().died);
            //FightManager.instance.fightRoles[i]
            SpecialStatus status=new SpecialStatus(FightSpecialStatus.时间停止,1,1);
            VFXManager.Instance.shoot(FightUI.instance.findEnemy(this),FightUI.instance.findRoleById(FightManager.instance.fightRoles[i].Id),FightSoundManager.Instance.shootClip,1f,status);  
        }
    }
    public override void speicalSkill()
    {
        //血量<50  恢复30% 1次
        if(Blood<MaxBlood*0.5f)
        {
            FightManager.instance.healEnemy(this,0.3f,1,VFXManager.Instance.upSelf,FightSoundManager.Instance.upSlef);
        }
    }
}
