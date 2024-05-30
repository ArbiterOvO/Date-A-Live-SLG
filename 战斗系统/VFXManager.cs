using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class VFXManager : Singleton<VFXManager>
{
    [Header("特效所在位置")]
    public GameObject VFXParent;
    [Header("己方普通攻击")]
    public GameObject autoAttack;
    [Header("火焰攻击特效")]
    public GameObject fireAttack;
    [Header("冰爆特效")]
    public GameObject iceAttack;
    [Header("射击特效")]
    public GameObject shootAttack;
    [Header("增益特效")]
    public GameObject upSelf;
    [Header("debuff")]
    public GameObject debuff;
    [Header("敌方普通攻击")]
    public GameObject enemyAttack;
    [Header("十香武器")]
    public GameObject wangZuoPrefab;
    [Header("准星预制体")]
    public GameObject shootStarPrefab;
    //准星移动速度
    private float startSpeed=0.01f;


    //生成特效
    public void createVFX(GameObject VFXprefab,Transform position)
    {
        Instantiate(VFXprefab, position.position, Quaternion.identity,VFXParent.transform);
    }
    //强化自身
    public void upSelfVFX(BaseFightRole fightRole,Transform transform,GameObject VFXprefab,AudioClip audioClip)
    {
        createVFX(VFXprefab,transform);
        //设置音效
        AudioSource audioSource = FightUI.instance.findRoleById(fightRole.Id).GetComponent<AudioSource>();
        FightSoundManager.Instance.setAudio(audioSource, audioClip);
    }
    //敌方攻击 生成特效 
    public void createVFX(FightEnemy enemy,GameObject VFXprefab,Transform position)
    {
        GameObject VFX=Instantiate(VFXprefab, position.position, Quaternion.identity,VFXParent.transform);
        StartCoroutine(attackCheck(enemy,VFX));
    }
    //判断是否攻击完
    IEnumerator attackCheck(FightEnemy enemy,GameObject VFX)
    {
        yield return new WaitUntil(() => !VFX.GetComponent<ParticleSystem>().isPlaying);
        enemy.isAttacking=false;
    }
    //王座
    public void wangZuoJiangLing(Transform position)
    {
        StartCoroutine(wangZuoJiangLingCoroutine(position));
    }
    IEnumerator wangZuoJiangLingCoroutine(Transform position)
    {
        Vector2 vector2=new Vector2(position.position.x,position.position.y+1f);
        GameObject wangZuo=Instantiate(wangZuoPrefab,vector2,Quaternion.identity,VFXParent.transform);
        while(wangZuo.transform.position.y>position.position.y)
        {
            wangZuo.transform.Translate(new Vector3(0,-0.01f,0));
            yield return 0;
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(wangZuo);
    }
    

    //清除所有特效
    public void clearAllVFX()
    {
        for (int i = 0; i < VFXParent.transform.childCount; i++)
        {
            if(VFXParent.transform.GetChild(i).GetComponent<ParticleSystem>().isPaused)
            Destroy(VFXParent.transform.GetChild(i).gameObject);
        }
    }
    //敌人射击特效 音效 攻击
    public void shoot(GameObject self,GameObject target,AudioClip shootClip,float rate,SpecialStatus specialStatus)
    {
        StartCoroutine(shootCoroutine(self,target,shootClip,rate,specialStatus));
    }
    IEnumerator shootCoroutine(GameObject self,GameObject target,AudioClip shootClip,float rate,SpecialStatus specialStatus)
    {
        Transform selfTransform=self.transform;
        Transform targetTransform=target.transform;
        FightEnemy fightEnemy=self.GetComponent<FightEnemy>();
        FightRole fightRole=target.GetComponent<FightRole>();

        GameObject shootStar=Instantiate(shootStarPrefab,selfTransform.position,Quaternion.identity,VFXParent.transform);
        float time=0;     
        while (shootStar.transform.position!=targetTransform.position)
        {
            time+=Time.deltaTime;
            if(time>1f)
            break;
            shootStar.transform.position=Vector2.Lerp(shootStar.transform.position,targetTransform.position,startSpeed);
            yield return 0;
        }
        Debug.Log("敌人射击");
        //摧毁准星
        Destroy(shootStar);
        //攻击
        roleUtil.Attack(fightEnemy.enemy,fightRole.role,rate);
        //音效
        AudioSource audioSource = FightUI.instance.findRoleById(fightRole.role.Id).GetComponent<AudioSource>();
        FightSoundManager.Instance.setAudio(audioSource, shootClip);
        //特殊状态
        fightRole.GetComponent<FightRole>().specialStatus=specialStatus.statusType;
        fightRole.GetComponent<FightRole>().statusTime=specialStatus.statusTime;
        //更改状态
        fightEnemy.isAttacking=false;
    }
}
