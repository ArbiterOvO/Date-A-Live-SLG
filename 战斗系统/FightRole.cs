using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FightRole : MonoBehaviour, IPointerClickHandler
{
    public BaseFightRole role;
    //是否死亡
    public bool died;
    public int deadTime = 0;
    public float normalAttackRate=1f;
    public int upNormalAttackTime = 0;
    //特殊状态
    public FightSpecialStatus specialStatus = FightSpecialStatus.正常;
    //特殊状态持续时间
    public int statusTime;
    //buff栏
    public List<Buff> buffs=new List<Buff>();
    [SerializeField]
    private Button normalAttackButton, activeAttackButton, powerSkillButton, bePowerfulButton;
    //头像图片
    public Image image;
    public GameObject normalAttack, activeAttack, powerSkill, bePowerful;
    public GameObject chosenLight;
    [Header("BUFF")]
    public GameObject buffSet;
    //buff预制体
    public GameObject buffPrefab;
    [Header("血条+能量条")]
    public Slider bloodSlider;
    public Slider powerSlider;
    public Text bloodText, powerText;
    //按钮移动时间
    float time = 0;
    //是否被选择
    bool isChosen = false;
    //按钮是否移动
    bool isMove = false;
    //按钮移动速度
    float speed = 800f;
    Vector2 autoPos = new Vector2(162.5f, 162.5f),
            activePos = new Vector2(218f, 72.5f),
            powerPos = new Vector2(218f, -40f),
            bePowerfulPos = new Vector2(162.5f, -132f);

    void Start()
    {
        catchButton();
    }

    void Update()
    {
        moveButton();
        changeData();
        lingMode();
        check();
        checkBuffTime();
    }
    //绑定按钮事件
    void catchButton()
    {
        normalAttackButton.onClick.AddListener(role.normalAttack);
        activeAttackButton.onClick.AddListener(
            () =>
            {
                if(!role.isActed)
                FightUI.instance.OpenOrCloseActiveSkillPane();
            }
            );
        powerSkillButton.onClick.AddListener(role.powerSkill);
        bePowerfulButton.onClick.AddListener(role.bePowerful);
    }
    //重置按钮
    public void clearButton()
    {
        buffSet.SetActive(false);
        normalAttack.SetActive(false);
        activeAttack.SetActive(false);
        powerSkill.SetActive(false);
        bePowerful.SetActive(false);
        normalAttack.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        activeAttack.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        powerSkill.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        bePowerful.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        isChosen = false;
        isMove = false;
    }
    //显示按钮
    public void showButton()
    {
        normalAttack.SetActive(true);
        activeAttack.SetActive(true);
        powerSkill.SetActive(true);
        bePowerful.SetActive(true);
        buffSet.SetActive(true);
        isChosen = true;
    }
    //检查血量和状态
    void check()
    {
        if (role.Blood <= 0 && !died)
        {
            role.Blood = 0;
            died = true;
            deadTime++;
            image.color = Color.grey;
        }
    }
    //移动按钮
    void moveButton()
    {

        if (!isMove && isChosen && !died)
        {
            if (time < 0.35f)
            {
                time += Time.deltaTime;
                normalAttack.GetComponent<RectTransform>().anchoredPosition += (autoPos - normalAttack.GetComponent<RectTransform>().anchoredPosition).normalized * Time.deltaTime * speed;
                activeAttack.GetComponent<RectTransform>().anchoredPosition += (activePos - activeAttack.GetComponent<RectTransform>().anchoredPosition).normalized * Time.deltaTime * speed;
                powerSkill.GetComponent<RectTransform>().anchoredPosition += (powerPos - powerSkill.GetComponent<RectTransform>().anchoredPosition).normalized * Time.deltaTime * speed;
                bePowerful.GetComponent<RectTransform>().anchoredPosition += (bePowerfulPos - bePowerful.GetComponent<RectTransform>().anchoredPosition).normalized * Time.deltaTime * speed;
            }
            else
            {
                isMove = true;
                time = 0;
            }
        }

    }
    //改变血条
    public void changeData()
    {
        bloodSlider.value = role.Blood / role.MaxBlood;
        bloodText.text = ((int)(role.Blood + 0.5)).ToString();
        powerSlider.value = role.Power / role.MaxPower;
        powerText.text = ((int)(role.Power + 0.5)).ToString();
    }
    public void lingMode()
    {
        if (role.isLing)
        {
            image.sprite = FightUI.instance.lingImage[role.Id];
            powerSkill.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            powerSkill.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }
    //鼠标点击事件
    public void OnPointerClick(PointerEventData eventData)
    {
        if(FightManager.instance.chosenSelfStatus)
        {
            FightManager.instance.chosenRoles.Add(role);
            FightManager.instance.chosenRoleNum++;
            hideChosenLight();
            return;
        }
        //不是己方回合无法点击
        if (FightManager.instance.fightStatus != FightStatus.己方回合)
            return;

        time = 0;
        foreach (GameObject item in FightUI.instance.roles)
        {
            if (item != this.gameObject)
                item.GetComponent<FightRole>().clearButton();
        }
        FightUI.instance.chosenRole = role.Id;
        showButton();
        if (FightUI.instance.isActivePaneOpen)
            FightUI.instance.OpenOrCloseActiveSkillPane();

        FightManager.instance.clearChosenEnemy();
    }
    public void showChosenLight()
    {
        chosenLight.SetActive(true);
    }
    public void hideChosenLight()
    {
        chosenLight.SetActive(false);
    }

    //被动
    void defaultSkill()
    {
        role.passiveSkill();
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
    GameObject createBuffTip(string buffName,string buffText)
    {
        GameObject buffTip = Instantiate(buffPrefab, buffSet.transform);
        buffTip.name = buffName;
        buffTip.GetComponent<TextMeshProUGUI>().text = buffText;
        return buffTip;
    }
    //增加Buff提示
    public void addBuffTip(BuffType buffType)
    {
        if(checkBuff(buffType))
        return;
        //根据Buff类型生成Buff提示
        switch (buffType)
        {
            case BuffType.ad:
                createBuffTip("ad","AD+");
                break;
            case BuffType.ap:
                createBuffTip("ap","AP+");
                break;
            case BuffType.def:
                createBuffTip("def","DEF+");
                break;
            case BuffType.mdf:
                createBuffTip("mdf","MDF+");
                break;
            case BuffType.hp:
                createBuffTip("hp","HP+");
                break;
            case BuffType.普攻强化:
                createBuffTip("普攻强化","普攻强化");
                break;
        }
    }
    //有时间限制的buff 第二个参数停止的回合数
    public void addBuffTip(BuffType buffType,int stopRound)
    {
        if(checkBuff(buffType))
        return;
        //根据Buff类型生成Buff提示
        GameObject buffTip=null;
        switch (buffType)
        {
            case BuffType.ad:
                buffTip=createBuffTip("ad","AD+");
                break;
            case BuffType.ap:
                buffTip=createBuffTip("ap","AP+");
                break;
            case BuffType.def:
                buffTip=createBuffTip("def","DEF+");
                break;
            case BuffType.mdf:
                buffTip=createBuffTip("mdf","MDF+");
                break;
            case BuffType.hp:
                buffTip=createBuffTip("hp","HP+");
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
    public void addBuff(Buff buff)
    {
        buffs.Add(buff);
        switch(buff.type)
        {
            case BuffType.ad:
            role.Ad+=buff.value;
            break;
            case BuffType.ap:
            role.Ap+=buff.value;
            break;
            case BuffType.def:
            role.Def+=buff.value;
            break;
            case BuffType.mdf:
            role.Mdf+=buff.value;
            break;
            case BuffType.hp:
            role.Blood+=buff.value;
            break;
        }
        addBuffTip(buff.type,buff.endRound);
    }
    //检查buff时间
    void checkBuffTime()
    {
        for(int i=0;i<buffs.Count;i++)
        {
            if(FightManager.instance.roundNum>=buffs[i].endRound)
            switch(buffs[i].type)
            {
                case BuffType.ad:
                role.Ad-=buffs[i].value;
                break;
                case BuffType.ap:
                role.Ap-=buffs[i].value;
                break;
                case BuffType.def:
                role.Def-=buffs[i].value;
                break;
                case BuffType.mdf:
                role.Mdf-=buffs[i].value;
                break;
                case BuffType.hp:
                role.Blood-=buffs[i].value;
                break;
            }
            buffs.Remove(buffs[i]);
        }
    }

}
