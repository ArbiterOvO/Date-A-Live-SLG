using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FightEnemy : MonoBehaviour,IPointerClickHandler
{
    public Slider bloodSlider;
    public Text text;
    public BaseEnemy enemy;
    public bool isChosen = false;
    public GameObject chosenLight;
    public bool isAttacking = false;
    //正在攻击图片
    public GameObject attackingImage;
    //buff栏对象
    public GameObject buffSet;
    //buff预制体
    public GameObject buffPrefab;
    //buff栏
    public List<Debuff> debuffs=new List<Debuff>();
    [Header("特殊状态")]
    //冰冻图片
    public GameObject icy;
    //眩晕图片
    public GameObject vertigo;
    //时停图片
    public GameObject timeStop;
    //特殊状态
    public FightSpecialStatus specialStatus=FightSpecialStatus.正常;
    //特殊状态持续时间
    public int statusTime;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        bloodSlider.value =enemy.Blood / enemy.MaxBlood;
        text.text=((int)(enemy.Blood+0.5)).ToString();
        testBlood();
        changeStatusImage();
        setAttackingImage();
        checkBuffTime();
    }
    //设置是否正在进攻图片
    void setAttackingImage()
    {
        attackingImage.SetActive(isAttacking);
    }
    //清空特殊状态图片
    void clearStatus()
    {
        icy.SetActive(false);
        vertigo.SetActive(false);
        timeStop.SetActive(false);
    }
    //改变特殊状态图片
    void changeStatusImage()
    {
        clearStatus();
        if(specialStatus==FightSpecialStatus.冰冻)
        {
            icy.SetActive(true);
        }
        if(specialStatus==FightSpecialStatus.眩晕)
        {
            vertigo.SetActive(true);
        }
        if(specialStatus==FightSpecialStatus.时间停止)
        {
            timeStop.SetActive(true);
        }
    }
    public void testBlood()
    {
        if(enemy.Blood<=0)
        {
            FightManager.instance.enemyRoles.Remove(enemy);
            FightUI.instance.enemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
    public void showChosenLight()
    {
        chosenLight.SetActive(true);
    }

    public void hideChosenLight()
    {
        chosenLight.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(FightManager.instance.chosenStatus)
        {
            isChosen=true;
            FightManager.instance.chosenEnemies.Add(enemy);
            FightManager.instance.chosenEnemyNum++;
            hideChosenLight();
        }
    }
    //检测Buff是否已经存在
    bool checkBuff(BuffType buffType)
    {
        for (int i = 0; i < buffSet.transform.childCount; i++)
        {
            if (buffType.ToString().Equals(buffSet.transform.GetChild(i).name))
            {
                return true;
            }
        }
        return false;
    }
    //有时间限制的buff 第二个参数停止的回合数
    public void addDeBuffTip(BuffType buffType,int stopRound)
    {
        if(checkBuff(buffType))
        return;
        //根据Buff类型生成Buff提示
        GameObject buffTip=null;
        switch (buffType)
        {
            case BuffType.ad:
                buffTip=createBuffTip("ad","AD-");
                break;
            case BuffType.ap:
                buffTip=createBuffTip("ap","AP-");
                break;
            case BuffType.def:
                buffTip=createBuffTip("def","DEF-");
                break;
            case BuffType.mdf:
                buffTip=createBuffTip("mdf","MDF-");
                break;
            case BuffType.hp:
                buffTip=createBuffTip("hp","HP-");
                break;
            case BuffType.普攻强化:
                buffTip=createBuffTip("普攻强化","UP");
                break;
        }
        StartCoroutine(buffTipDestory(stopRound,buffTip));
    }
    IEnumerator buffTipDestory(int stopRound,GameObject buffTip)
    {
        yield return new WaitUntil(()=>FightManager.instance.roundNum>=stopRound);
        Destroy(buffTip);
    }
    GameObject createBuffTip(string buffName,string buffText)
    {
        GameObject buffTip = Instantiate(buffPrefab, buffSet.transform);
        buffTip.name = buffName;
        buffTip.GetComponent<TextMeshProUGUI>().text = buffText;
        return buffTip;
    }
    //有时间限制的buff 第二个参数停止的回合数
    public void addDeBuff(Debuff debuff)
    {
        debuffs.Add(debuff);
        switch(debuff.type)
        {
            case BuffType.ad:
            enemy.Ad*=1-debuff.rate;
            break;
            case BuffType.ap:
            enemy.Ap*=1-debuff.rate;
            break;
            case BuffType.def:
            Debug.Log(enemy.Def);
            enemy.Def*=1-debuff.rate;
            Debug.Log(enemy.Def);
            break;
            case BuffType.mdf:
            enemy.Mdf*=1-debuff.rate;
            break;
            case BuffType.hp:
            enemy.Blood*=1-debuff.rate;
            break;
        }
        addDeBuffTip(debuff.type,debuff.endRound);
    }
    //检查buff时间
    void checkBuffTime()
    {
        for(int i=0;i<debuffs.Count;i++)
        {
            if(FightManager.instance.roundNum>=debuffs[i].endRound)
            switch(debuffs[i].type)
            {
                case BuffType.ad:
                enemy.Ad/=1-debuffs[i].rate;
                break;
                case BuffType.ap:
                enemy.Ap/=1-debuffs[i].rate;
                break;
                case BuffType.def:
                enemy.Def/=1-debuffs[i].rate;
                break;
                case BuffType.mdf:
                enemy.Mdf/=1-debuffs[i].rate;
                break;
                case BuffType.hp:
                enemy.Blood/=1-debuffs[i].rate;
                break;
            }
            debuffs.Remove(debuffs[i]);
        }
    }
}
