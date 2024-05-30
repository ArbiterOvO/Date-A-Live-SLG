using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour
{
    public static FightManager instance;
    //回合数
    public int roundNum = 1;
    //能量
    public float power;
    public FightStatus fightStatus = FightStatus.己方回合前置设置;
    //选择己方状态
    public bool chosenSelfStatus;//1选择状态 0不选择状态
    public List<BaseFightRole> chosenRoles = new List<BaseFightRole>();
    public int chosenRoleNum;
    //选择敌方状态
    public bool chosenStatus;//1选择状态 0不选择状态
    public List<BaseEnemy> chosenEnemies = new List<BaseEnemy>();
    public int chosenEnemyNum;
    //己方角色
    public List<BaseFightRole> fightRoles = new List<BaseFightRole>();
    //敌方角色
    public List<BaseEnemy> enemyRoles = new List<BaseEnemy>();
    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        instance = this;
    }
    void Start()
    {
        //初始化回合
        //isMyTurn=true;
        //初始化power
        power = GameManager.instance.currentPower;
        //初始化角色
        fightRoles.Clear();
        foreach (BaseRole role in GameManager.instance.rolesInTeam)
        {
            switch (role.Id)
            {
                case 0:
                    fightRoles.Add(new 琴里(role.Id, role.Name, role.Level, role.MaxBlood, role.Cost, role.Power, role.Ad, role.Ap, role.Def, role.Mdf, role.ActiveSkillNum()));
                    break;
                case 1:
                    fightRoles.Add(new 十香(role.Id, role.Name, role.Level, role.MaxBlood, role.Cost, role.Power, role.Ad, role.Ap, role.Def, role.Mdf, role.ActiveSkillNum()));
                    break;
                case 2:
                    fightRoles.Add(new 四糸乃(role.Id, role.Name, role.Level, role.MaxBlood, role.Cost, role.Power, role.Ad, role.Ap, role.Def, role.Mdf, role.ActiveSkillNum()));
                    break;
                case 3:
                    fightRoles.Add(new 狂三(role.Id, role.Name, role.Level, role.MaxBlood, role.Cost, role.Power, role.Ad, role.Ap, role.Def, role.Mdf, role.ActiveSkillNum()));
                    break;
            }
        }
        //初始化敌方角色
        //特殊战斗
        if (GameManager.instance.specialBattleNum != 0)
        {
            power = GameManager.instance.totalPower;
            enemyRoles = LevelManager.instance.getSpecialLevel().enemies[0];
        }
        else //非特殊战斗
        {
            for (int i = 0; i < LevelManager.instance.getLevel().enemies[FightChooseManager.Instance.fightLayerNum - 1].Count; i++)
            {
                enemyRoles.Add(LevelManager.instance.getLevel().enemies[FightChooseManager.Instance.fightLayerNum - 1][i]);
            }
        }
    }
    void Update()
    {
        checkSkill();
        setBeforeAct();
        checkIfAct();
        setBeforeEnemyAct();
        //enemyAttack();
        win();
    }
    //开局使用的技能
    public void startSkill()
    {
        //十香被动
        findBaseFightRoleById(1).passiveSkill();
    }
    //一直要进行判断的技能
    void checkSkill()
    {
        
        //狂三被动
        if (FightUI.instance.findRoleById(3)!=null&&FightUI.instance.findRoleById(3).GetComponent<FightRole>().died && FightUI.instance.findRoleById(3).GetComponent<FightRole>().deadTime < 1)
        {
            Debug.Log("狂三复活");
            StartCoroutine(relive(findBaseFightRoleById(3), findBaseFightRoleById(3).MaxBlood));
        }
    }
    //复活
    IEnumerator relive(BaseFightRole role, float blood)
    {
        while (role.Blood < blood)
        {
            role.Blood += 0.001f;
            yield return 0;
        }
        FightUI.instance.findRoleById(role.Id).GetComponent<FightRole>().died = false;
        FightUI.instance.findRoleById(role.Id).GetComponent<FightRole>().image.color = Color.white;
    }
    //己方回合行动前设置  
    void setBeforeAct()
    {
        if (fightStatus != FightStatus.己方回合前置设置)
            return;
        //todo 己方buff等
        //琴里被动
        foreach (var role in fightRoles)
        {
            if (role.Id == 0)
            {
                role.passiveSkill();//琴里被动
            }
        }

        //特殊状态-
        foreach (var enemy in enemyRoles)
        {
            if (FightUI.instance.findEnemy(enemy).GetComponent<FightEnemy>().specialStatus != FightSpecialStatus.正常)
            {
                FightUI.instance.findEnemy(enemy).GetComponent<FightEnemy>().statusTime--;
                if (FightUI.instance.findEnemy(enemy).GetComponent<FightEnemy>().statusTime == 0)
                    FightUI.instance.findEnemy(enemy).GetComponent<FightEnemy>().specialStatus = FightSpecialStatus.正常;
            }
        }
        fightStatus = FightStatus.己方回合;
    }
    //己方回合时检查是否行动完
    void checkIfAct()
    {
        if (fightStatus != FightStatus.己方回合)
            return;
        int i;
        for (i = 0; i < fightRoles.Count; i++)
        {
            //如果角色死亡或处于异常状态 跳过
            if (FightUI.instance.findRoleById(fightRoles[i].Id).GetComponent<FightRole>().died||FightUI.instance.findRoleById(fightRoles[i].Id).GetComponent<FightRole>().specialStatus!=FightSpecialStatus.正常)
                continue;
            if (!fightRoles[i].isActed)
            {
                break;
            }
        }
        if (i == fightRoles.Count) //己方行动完
        {
            //设置敌方可以行动
            foreach (var enemy in enemyRoles)
            {
                enemy.isActed = false;
            }
            FightUI.instance.changeturn();//改变敌方行动
        }

    }
    //敌方回合前设置
    void setBeforeEnemyAct()
    {
        if (fightStatus != FightStatus.敌方回合前置设置)
            return;
        //todo 敌方buff等
        foreach (var role in fightRoles)
        {
            if(FightUI.instance.findRoleById(role.Id).GetComponent<FightRole>().specialStatus != FightSpecialStatus.正常)
            {
                FightUI.instance.findRoleById(role.Id).GetComponent<FightRole>().statusTime--;
                if (FightUI.instance.findRoleById(role.Id).GetComponent<FightRole>().statusTime == 0)
                FightUI.instance.findRoleById(role.Id).GetComponent<FightRole>().specialStatus = FightSpecialStatus.正常;
            }
        }
        fightStatus = FightStatus.敌方回合;
        enemyAttack();
    }
    //敌方回合
    void enemyAttack()
    {
        if (fightStatus != FightStatus.敌方回合)
            return;

        Debug.Log("敌人攻击");
        StartCoroutine(enemyAttackCoroutine());
    }
    IEnumerator enemyAttackCoroutine()
    {
        foreach (BaseEnemy enemy in enemyRoles)
        {
            enemy.speicalSkill();
            yield return new WaitUntil(() => !FightUI.instance.findEnemy(enemy).GetComponent<FightEnemy>().isAttacking);
            enemy.normalAttack();
            yield return new WaitUntil(() => !FightUI.instance.findEnemy(enemy).GetComponent<FightEnemy>().isAttacking);
            yield return new WaitForSeconds(1f);
        }
        FightUI.instance.changeturn();//改变己方行动
        roundNum++;
        foreach (BaseFightRole role in fightRoles)
        {
            role.isActed = false;
        }
    }
    //普通攻击
    public void normalAttack(BaseFightRole role, int n, SpecialStatus specialStatus)
    {
        //清空选择敌人列表
        chosenEnemies.Clear();
        chosenStatus = true;
        float rate = FightUI.instance.findRoleById(role.Id).GetComponent<FightRole>().normalAttackRate;
        if (FightUI.instance.findRoleById(role.Id).GetComponent<FightRole>().upNormalAttackTime > 0)
        {
            FightUI.instance.findRoleById(role.Id).GetComponent<FightRole>().upNormalAttackTime--;
            if (FightUI.instance.findRoleById(role.Id).GetComponent<FightRole>().upNormalAttackTime == 0)
            {
                FightUI.instance.findRoleById(role.Id).GetComponent<FightRole>().normalAttackRate = 1;
            }
        }
        //选择攻击对象
        FightUI.instance.showChosenEnemy();
        StartCoroutine(waitForChosen(n, role, rate, VFXManager.Instance.autoAttack, FightSoundManager.Instance.roleAttackClip, specialStatus));
    }
    //技能攻击
    public void skillAttack(BaseFightRole role, int n, float rate, GameObject vfxPrefab, AudioClip attackClip, SpecialStatus specialStatus)
    {
        //群体
        if (n >= 4)
        {
            //攻击
            for (int i = 0; i < enemyRoles.Count; i++)
            {
                Debug.Log("技能攻击");
                roleUtil.Attack(role, enemyRoles[i], rate);
                //设置音效
                AudioSource audioSource = FightUI.instance.findRoleById(role.Id).GetComponent<AudioSource>();
                FightSoundManager.Instance.setAudio(audioSource, attackClip);
                //设置特效
                VFXManager.Instance.createVFX(vfxPrefab, FightUI.instance.findEnemy(enemyRoles[i]).transform);
                //特殊效果
                if (specialStatus != null)
                {
                    int random = Random.Range(1, 11); //1-10
                    if (random <= specialStatus.probability * 10) //例如 0.3*10=3 
                    {
                        FightUI.instance.findEnemy(enemyRoles[i]).GetComponent<FightEnemy>().specialStatus = specialStatus.statusType;
                        FightUI.instance.findEnemy(enemyRoles[i]).GetComponent<FightEnemy>().statusTime = specialStatus.statusTime;
                    }
                }
            }
            role.isActed = true;
            return;
        }

        //清空选择敌人列表
        chosenEnemies.Clear();
        chosenStatus = true;
        //选择攻击对象
        FightUI.instance.showChosenEnemy();
        StartCoroutine(waitForChosen(n, role, rate, vfxPrefab, attackClip, specialStatus));
    }
    //等待选择直到选择完
    IEnumerator waitForChosen(int n, BaseFightRole role, float rate, GameObject vfxPrefab, AudioClip attackClip, SpecialStatus specialStatus)
    {
        yield return new WaitUntil(() => { return chosenEnemyNum >= n; });
        //攻击
        for (int i = 0; i < chosenEnemyNum; i++)
        {
            Debug.Log("攻击");
            //设置音效
            AudioSource audioSource = FightUI.instance.findRoleById(role.Id).GetComponent<AudioSource>();
            FightSoundManager.Instance.setAudio(audioSource, attackClip);
            //攻击
            roleUtil.Attack(role, chosenEnemies[i], rate);
            //特殊效果
            if (specialStatus != null)
            {
                int random = Random.Range(1, 11); //1-10
                if (random <= specialStatus.probability * 10) //例如 0.3*10=3 
                {
                    FightUI.instance.findEnemy(chosenEnemies[i]).GetComponent<FightEnemy>().specialStatus = specialStatus.statusType;
                    FightUI.instance.findEnemy(chosenEnemies[i]).GetComponent<FightEnemy>().statusTime = specialStatus.statusTime;
                }
            }

            //特效
            VFXManager.Instance.createVFX(vfxPrefab, FightUI.instance.findEnemy(chosenEnemies[i]).transform);
        }
        //重置状态
        clearChosenEnemy();
        role.isActed = true;
    }
    //技能攻击 传方法替代特效
    public void skillAttack(BaseFightRole role, int n, float rate, UnityAction<Transform> action, AudioClip attackClip, SpecialStatus specialStatus)
    {
        //群体
        if (n >= 4)
        {
            //攻击
            for (int i = 0; i < enemyRoles.Count; i++)
            {
                Debug.Log("技能攻击");
                //设置音效
                AudioSource audioSource = FightUI.instance.findRoleById(role.Id).GetComponent<AudioSource>();
                FightSoundManager.Instance.setAudio(audioSource, attackClip);
                //设置特效
                action(FightUI.instance.findEnemy(enemyRoles[i]).transform);
                roleUtil.Attack(role, enemyRoles[i], rate);
                //特殊效果
                if (specialStatus != null)
                {
                    int random = Random.Range(1, 11); //1-10
                    if (random <= specialStatus.probability * 10) //例如 0.3*10=3 
                    {
                        FightUI.instance.findEnemy(enemyRoles[i]).GetComponent<FightEnemy>().specialStatus = specialStatus.statusType;
                        FightUI.instance.findEnemy(enemyRoles[i]).GetComponent<FightEnemy>().statusTime = specialStatus.statusTime;
                    }
                }

            }
            role.isActed = true;
            return;
        }

        //清空选择敌人列表
        chosenEnemies.Clear();
        chosenStatus = true;
        //选择攻击对象
        FightUI.instance.showChosenEnemy();
        StartCoroutine(waitForChosen(n, role, rate, (Transform pos) => { action(pos); }, attackClip, specialStatus));
    }

    //等待选择直到选择完 传方法替代特效
    IEnumerator waitForChosen(int n, BaseFightRole role, float rate, UnityAction<Transform> action, AudioClip attackClip, SpecialStatus specialStatus)
    {
        yield return new WaitUntil(() => { return chosenEnemyNum >= n; });
        //攻击
        for (int i = 0; i < chosenEnemyNum; i++)
        {
            Debug.Log("攻击");
            //设置音效
            AudioSource audioSource = FightUI.instance.findRoleById(role.Id).GetComponent<AudioSource>();
            FightSoundManager.Instance.setAudio(audioSource, attackClip);
            //攻击
            roleUtil.Attack(role, chosenEnemies[i], rate);
            action(FightUI.instance.findEnemy(chosenEnemies[i]).transform);
            //特殊效果
            if (specialStatus != null)
            {
                int random = Random.Range(1, 11); //1-10
                if (random <= specialStatus.probability * 10) //例如 0.3*10=3 
                {
                    FightUI.instance.findEnemy(chosenEnemies[i]).GetComponent<FightEnemy>().specialStatus = specialStatus.statusType;
                    FightUI.instance.findEnemy(chosenEnemies[i]).GetComponent<FightEnemy>().statusTime = specialStatus.statusTime;
                }
            }
        }
        //重置状态
        clearChosenEnemy();
        role.isActed = true;
    }

    //等待选择直到选择完 传方法替代特效 带debuff
    public void skillAttack(BaseFightRole role, int n, float rate, GameObject vfxPrefab, AudioClip attackClip, SpecialStatus specialStatus, Debuff debuff)
    {
        //群体
        if (n >= 4)
        {
            //攻击
            for (int i = 0; i < enemyRoles.Count; i++)
            {
                Debug.Log("技能攻击");
                roleUtil.Attack(role, enemyRoles[i], rate);
                //设置音效
                AudioSource audioSource = FightUI.instance.findRoleById(role.Id).GetComponent<AudioSource>();
                FightSoundManager.Instance.setAudio(audioSource, attackClip);
                //设置特效
                VFXManager.Instance.createVFX(vfxPrefab, FightUI.instance.findEnemy(enemyRoles[i]).transform);
                //buff
                if (debuff != null)
                    FightUI.instance.findEnemy(chosenEnemies[i]).GetComponent<FightEnemy>().addDeBuff(debuff);
                //特殊效果
                if (specialStatus != null)
                {
                    int random = Random.Range(1, 11); //1-10
                    if (random <= specialStatus.probability * 10) //例如 0.3*10=3 
                    {
                        FightUI.instance.findEnemy(enemyRoles[i]).GetComponent<FightEnemy>().specialStatus = specialStatus.statusType;
                        FightUI.instance.findEnemy(enemyRoles[i]).GetComponent<FightEnemy>().statusTime = specialStatus.statusTime;
                    }
                }
            }
            role.isActed = true;
            return;
        }

        //清空选择敌人列表
        chosenEnemies.Clear();
        chosenStatus = true;
        //选择攻击对象
        FightUI.instance.showChosenEnemy();
        StartCoroutine(waitForChosen(n, role, rate, vfxPrefab, attackClip, specialStatus, debuff));
    }
    IEnumerator waitForChosen(int n, BaseFightRole role, float rate, GameObject vfxPrefab, AudioClip attackClip, SpecialStatus specialStatus, Debuff debuff)
    {
        yield return new WaitUntil(() => { return chosenEnemyNum >= n; });
        //攻击
        for (int i = 0; i < chosenEnemyNum; i++)
        {
            Debug.Log("攻击");
            //设置音效
            AudioSource audioSource = FightUI.instance.findRoleById(role.Id).GetComponent<AudioSource>();
            FightSoundManager.Instance.setAudio(audioSource, attackClip);
            //攻击
            roleUtil.Attack(role, chosenEnemies[i], rate);
            //buff
            FightUI.instance.findEnemy(chosenEnemies[i]).GetComponent<FightEnemy>().addDeBuff(debuff);
            //特殊效果
            if (specialStatus != null)
            {
                int random = Random.Range(1, 11); //1-10
                if (random <= specialStatus.probability * 10) //例如 0.3*10=3 
                {
                    FightUI.instance.findEnemy(chosenEnemies[i]).GetComponent<FightEnemy>().specialStatus = specialStatus.statusType;
                    FightUI.instance.findEnemy(chosenEnemies[i]).GetComponent<FightEnemy>().statusTime = specialStatus.statusTime;
                }
            }
            //特效
            VFXManager.Instance.createVFX(vfxPrefab, FightUI.instance.findEnemy(chosenEnemies[i]).transform);

        }
        //重置状态
        clearChosenEnemy();
        role.isActed = true;
    }
    public void healSlef(BaseFightRole role, int n, float rate, GameObject vfxPrefab, AudioClip healClip)
    {
        //群体
        if (n >= 4)
        {
            //治愈
            for (int i = 0; i < fightRoles.Count; i++)
            {
                //设置音效
                AudioSource audioSource = FightUI.instance.findRoleById(role.Id).GetComponent<AudioSource>();
                FightSoundManager.Instance.setAudio(audioSource, healClip);
                //设置特效
                VFXManager.Instance.createVFX(vfxPrefab, FightUI.instance.findEnemy(enemyRoles[i]).transform);
            }
        }
        chosenSelfStatus = true;
        FightUI.instance.showChosenRole();
        StartCoroutine(waitForChosenSlef(role,n, rate, vfxPrefab, healClip));
    }
    IEnumerator waitForChosenSlef(BaseFightRole role,int n, float rate, GameObject vfxPrefab, AudioClip healClip)
    {
        yield return new WaitUntil(() => chosenRoleNum >= n);
        //治愈
        for (int i = 0; i < chosenRoles.Count; i++)
        {
            if(chosenRoles[i].Blood<=chosenRoles[i].MaxBlood*(1-rate))
            chosenRoles[i].Blood+=chosenRoles[i].MaxBlood*rate;
            else
            chosenRoles[i].Blood=chosenRoles[i].MaxBlood;
            //设置音效
            AudioSource audioSource = FightUI.instance.findRoleById(role.Id).GetComponent<AudioSource>();
            FightSoundManager.Instance.setAudio(audioSource, healClip);
            //设置特效
            VFXManager.Instance.createVFX(vfxPrefab, FightUI.instance.findRoleById(chosenRoles[i].Id).transform);
        }
        //重置状态
        clearChosenRole();
        role.isActed=true;
    }
    public void healEnemy(BaseEnemy enemy,float rate,int maxUse,GameObject vfxPrefab, AudioClip healClip)
    {
        
        if(enemy.speicalSkillCount>=maxUse)
        return;
        FightUI.instance.findEnemy(enemy).GetComponent<FightEnemy>().isAttacking=true;
        enemy.Blood += enemy.MaxBlood * rate;
        if (enemy.Blood > enemy.MaxBlood)
        enemy.Blood=enemy.MaxBlood;
        enemy.speicalSkillCount++;
        //设置特效
        VFXManager.Instance.createVFX(FightUI.instance.findEnemy(enemy).GetComponent<FightEnemy>(),vfxPrefab,FightUI.instance.findEnemy(enemy).transform);
        //设置音效
        AudioSource audioSource= FightUI.instance.findEnemy(enemy).GetComponent<AudioSource>();
        FightSoundManager.Instance.setAudio(audioSource,healClip);
    }
    public void clearChosenEnemy()
    {
        chosenEnemyNum = 0;
        FightUI.instance.hideChosenEnemy();
        chosenStatus = false;
        chosenEnemies.Clear();
    }
    public void clearChosenRole()
    {
        chosenRoleNum=0;
        FightUI.instance.hideChosenRole();
        chosenSelfStatus=false;
        chosenRoles.Clear();
    }
    public void win()
    {
        if (enemyRoles.Count == 0)
        {
            if (GameManager.instance.specialBattleNum == 0)
            {
                for (int i = 0; i < GameManager.instance.rolesInTeam.Count; i++)
                {
                    if (fightRoles[i].Blood > GameManager.instance.findRoleByName(fightRoles[i].Name).MaxBlood)
                        GameManager.instance.rolesInTeam[i].Blood = GameManager.instance.rolesInTeam[i].MaxBlood;
                    else
                        GameManager.instance.rolesInTeam[i].Blood = fightRoles[i].Blood;
                }
                SceneManager.LoadScene("FightChoose");

            }
            else
            {
                switch (GameManager.instance.specialBattleNum)
                {
                    case 1:
                        SceneManager.LoadScene("School");
                        break;
                    case 2:
                        SceneManager.LoadScene("Shrine");
                        break;
                    case 3:
                        SceneManager.LoadScene("HighPlatForm");
                        break;
                }
            }
        }

    }

    public BaseFightRole findBaseFightRoleById(int id)
    {
        foreach (var role in fightRoles)
        {
            if (role.Id == id)
                return role;
        }
        return null;
    }
}
