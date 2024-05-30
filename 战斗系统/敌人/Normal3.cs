using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal3 : BaseEnemy
{
    //小怪 较肉
    public Normal3()
    {
        Id=2;
        Name="Normal1";
        Level=1;
        MaxBlood=150;
        Blood=MaxBlood;
        Ad=60;
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
            isActed=true;//已经攻击过
            int i;
            int time=0;
            do
            {
                time++;
                i=Random.Range(0,FightManager.instance.fightRoles.Count);
                if(time>20)
                break;
            }while(FightUI.instance.findRoleById(FightManager.instance.fightRoles[i].Id).GetComponent<FightRole>().died);
            roleUtil.Attack(this,FightManager.instance.fightRoles[i],1);
            //设置音效
            AudioSource audioSource= FightUI.instance.findEnemy(this).GetComponent<AudioSource>();
            FightSoundManager.Instance.setAudio(audioSource,FightSoundManager.Instance.enemyAttackClip);
            //设置特效
            VFXManager.Instance.createVFX(FightUI.instance.findEnemy(this).GetComponent<FightEnemy>(),VFXManager.Instance.enemyAttack,FightUI.instance.roles[i].transform);
            
        }
    }

    public override void speicalSkill()
    {
        
    }
}
