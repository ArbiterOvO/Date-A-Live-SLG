using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightUI : MonoBehaviour
{
    //实例变量
    public static FightUI instance;
    //角色预制体
    public GameObject rolePrefab;
    //敌人预制体
    public GameObject enemyPrefab;
    //主动技能按钮预制体
    public GameObject activeSkillButtonPrefab;
    //主动技能按钮集合
    public GameObject activeSkillButtonCollection;
    //角色头像图片
    public List<Sprite> roleImages,lingImage;
    //敌人图片
    public List<Sprite> enemyImages;
    //己方
    public GameObject roleCollection;
    public List<GameObject> roles;
    //敌人
    public GameObject enemyCollection;
    public List<GameObject> enemies;
    //主动技能面板
    public GameObject activeSkillPanel;
    //是否打开主动技能面板
    public bool isActivePaneOpen=false;
    //当前选择的技能
    public int chosenRole;
    //灵力条
    public Slider powerSlider;
    public Text powerText;
    public TextMeshProUGUI roundText;
    [Header("背景图片")]
    public Image backgroundImage;
    public List<Sprite> normalBackgroundImages;
    public List<Sprite> specialBackgroundImages;
    [Header("行动方提示")]
    public GameObject ourTurnTip;
    public GameObject enemyTurnTip;
    public GameObject 己方;
    public GameObject 敌方;

    void Awake() {
        //如果实例已经存在，则销毁当前实例
        if(instance!=null)
        Destroy(this.gameObject);
        //将当前实例赋值给实例变量
        instance=this;
    }

    private void Start() {
        //初始化地图
        changeBackground();
        //初始化人物对象
        instance.roles.Clear();
        for (int i = 0; i < FightManager.instance.fightRoles.Count; i++)
        {
            instance.roles.Add(Instantiate(rolePrefab));
            instance.roles[i].transform.SetParent(roleCollection.transform);
            instance.roles[i].GetComponent<FightRole>().image.sprite=roleImages[FightManager.instance.fightRoles[i].Id];
            instance.roles[i].GetComponent<FightRole>().role=FightManager.instance.fightRoles[i];
            instance.roles[i].transform.localScale=new Vector3(1,1,1);
            
        }
        //todo 生成敌人对象
        for(int i=0;i<FightManager.instance.enemyRoles.Count;i++)
        {
            instance.enemies.Add(Instantiate(enemyPrefab));
            instance.enemies[i].transform.SetParent(enemyCollection.transform);
            instance.enemies[i].transform.localScale=new Vector3(1,1,1);
            instance.enemies[i].transform.Find("Image").GetComponent<Image>().sprite=enemyImages[FightManager.instance.enemyRoles[i].Id];
            instance.enemies[i].GetComponent<FightEnemy>().enemy=FightManager.instance.enemyRoles[i];
        }   
        FightManager.instance.startSkill();
    }

    void Update() {
        changePower();
        changeActionTip();
    }

    //打开或关闭主动技能面板
    public void OpenOrCloseActiveSkillPane()
    {
        isActivePaneOpen=!isActivePaneOpen;
        activeSkillPanel.SetActive(isActivePaneOpen);
        if(isActivePaneOpen)
        {
            createSkillButton();
        }
    }
    //清空技能按钮
    void clearSkillButton()
    {
        for (int i = 0; i < activeSkillButtonCollection.transform.childCount; i++)
        {
            //如果技能集合为空，则跳出循环
            if(activeSkillButtonCollection.transform.childCount==0)
            break;
            Destroy(activeSkillButtonCollection.transform.GetChild(i).gameObject);
        }
    }
    //创建技能按钮
    void createSkillButton()
    {
        clearSkillButton();
        for (int i = 0; i < FightManager.instance.findBaseFightRoleById(chosenRole).ActiveSkillNum; i++)
        {
            GameObject skillButton=Instantiate(activeSkillButtonPrefab);
            skillButton.transform.SetParent(activeSkillButtonCollection.transform);
            skillButton.transform.Find("Image/Text").GetComponent<Text>().text=FightManager.instance.findBaseFightRoleById(chosenRole).skills[i];
            skillButton.transform.localScale=new Vector3(1,1,1);
            switch(i)
            {
                case 0:
                skillButton.transform.Find("Image").GetComponent<Button>().onClick.AddListener(delegate () { 
                    FightManager.instance.findBaseFightRoleById(chosenRole).activeSkill1();
                    clearAllButton();
                });
                break;
                case 1:
                skillButton.transform.Find("Image").GetComponent<Button>().onClick.AddListener(delegate () { 
                    FightManager.instance.findBaseFightRoleById(chosenRole).activeSkill2();
                    clearAllButton();
                });
                break;
                case 2:
                skillButton.transform.Find("Image").GetComponent<Button>().onClick.AddListener(delegate () { 
                    FightManager.instance.findBaseFightRoleById(chosenRole).activeSkill3();
                    clearAllButton();
                });
                break;
                case 3:
                skillButton.transform.Find("Image").GetComponent<Button>().onClick.AddListener(delegate () { 
                    FightManager.instance.findBaseFightRoleById(chosenRole).activeSkill4();
                    clearAllButton();
                });
                break;
                case 4:
                skillButton.transform.Find("Image").GetComponent<Button>().onClick.AddListener(delegate () { 
                    FightManager.instance.findBaseFightRoleById(chosenRole).activeSkill5();
                    clearAllButton();
                });
                break;
            }
            
        }
        
    }
    //改变灵力And回合数
    public void changePower()
    {
        powerSlider.value=FightManager.instance.power/GameManager.instance.totalPower;
        powerText.text=FightManager.instance.power.ToString();
        roundText.text=FightManager.instance.roundNum.ToString();
    }
    //点击屏幕清空按钮和技能面板
    public void clearAllButton()
    {
        if(isActivePaneOpen)
        OpenOrCloseActiveSkillPane();
        for (int i = 0; i < roles.Count; i++)
        {
            roles[i].GetComponent<FightRole>().clearButton();
        }
    }
    //显示选择提示闪光
    public void showChosenEnemy()
    {  
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<FightEnemy>().showChosenLight();
        }
    }
    public void showChosenRole()
    {
        for (int i = 0; i < roles.Count; i++)
        {
            roles[i].GetComponent<FightRole>().showChosenLight();
        }
    }
    //隐藏选择提示闪光
    public void hideChosenEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<FightEnemy>().hideChosenLight();
        }
    }
    public void hideChosenRole()
    {
        for (int i = 0; i < roles.Count; i++)
        {
            roles[i].GetComponent<FightRole>().hideChosenLight();
        }
    }

    //找到敌人对象
    public GameObject findEnemy(BaseEnemy enemy)
    {
        foreach (var enemyObject in enemies)
        {
            if(enemyObject.GetComponent<FightEnemy>().enemy.Equals(enemy))
            return enemyObject;
        }
        return null;
    }
    //找到己方角色
    public GameObject findRoleById(int roleId)
    {
        foreach (var item in roles)
        {
            if(item.GetComponent<FightRole>().role.Id==roleId)
            return item;
        }
        return null;
    }
    //改变左上角行动方提示
    public void changeActionTip()
    {
        if(FightManager.instance.fightStatus==FightStatus.己方回合)
        {
            己方.SetActive(true);
            敌方.SetActive(false);
        }
        else if(FightManager.instance.fightStatus==FightStatus.敌方回合)
        {
            己方.SetActive(false);
            敌方.SetActive(true);
        }
    }
    //改变行动方
    public void changeturn()
    {
        if(FightManager.instance.fightStatus==FightStatus.己方回合)// 己方 提示 敌方前设置
        {
            StartCoroutine(showTurn(enemyTurnTip,0));
        }
        else if(FightManager.instance.fightStatus==FightStatus.敌方回合)//敌方 提示 己方前设置
        {
            StartCoroutine(showTurn(ourTurnTip,1));
        }
    }
    IEnumerator showTurn(GameObject tip,int i)
    {
        clearAllButton();
        if(i==0)
        {
            FightManager.instance.fightStatus=FightStatus.提示;
            yield return new WaitForSeconds(1f);
            tip.SetActive(true);
            yield return new WaitForSeconds(2f);
            tip.SetActive(false);
            FightManager.instance.fightStatus=FightStatus.敌方回合前置设置;
        }
        if(i==1)
        {
            FightManager.instance.fightStatus=FightStatus.提示;
            yield return new WaitForSeconds(1f);
            tip.SetActive(true);
            yield return new WaitForSeconds(2f);
            tip.SetActive(false);
            FightManager.instance.fightStatus=FightStatus.己方回合前置设置;
        }

        
    }
    public void changeBackground()
    {
        if (GameManager.instance.specialBattleNum != 0)
        {
            backgroundImage.sprite=specialBackgroundImages[GameManager.instance.specialBattleNum-1];
        }
        else //非特殊战斗
        {
            backgroundImage.sprite=normalBackgroundImages[GameManager.instance.currentMap-1];
        }
    }

}