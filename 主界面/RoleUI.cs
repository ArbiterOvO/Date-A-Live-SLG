using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoleUI : MonoBehaviour
{
    public static RoleUI instance;
    public List<Sprite> roleHeadImages;//头像背景
    public List<Sprite> roleImages;//角色立绘图片
    public Image roleImage;
    public Image weapenImage;//武器图片
    public List<Sprite> weapens;//武器图片素材
    public TextMeshProUGUI level,hp,cost,ad,ap,def,mdf,power;//数值
    public Text normalAttack,passiveSkill,powerSkill;//技能
    public List<GameObject> activeSkills;//主动技能
    public GameObject roleButtonSet;//头像按钮集合
    public GameObject roleButtonPrefab;//头像按钮预制体
    public GameObject rolePane;
    public GameObject lingZhuangPane,skillPane,chosenPane,lingChosenPane;
    //宝石按钮
    public List<GameObject> lings;
    //初始宝石图片
    public Sprite yuanLing;
    public GameObject tagPreb;
    List<GameObject> tags=new List<GameObject>();
    [SerializeField]
    bool rolePaneOpen=false;
    bool chosenPaneOpen=false,lingChosenPaneOpen=false;
    public int currentRole;//0开始
    void Awake() {
        //如果实例已经存在，则销毁当前实例
        if(instance!=null)
        Destroy(this.gameObject);
        //将当前实例赋值给实例变量
        instance=this;
    }
    void Start()
    {

    }
    public void clearAllPane()
    {
        chosenPaneOpen=false;
        chosenPane.SetActive(false);
        lingChosenPaneOpen=false;
        lingChosenPane.SetActive(false);
        rolePaneOpen=false;
        rolePane.SetActive(false);
        showLingZhuangPane();
    }
    //打开角色面板
    public void roleButton()
    {
        if(GameManager.instance.gameStatus!=GameStatus.UI可用)
        return;
        if(!rolePaneOpen)
        {
            clearAllPane();
            rolePaneOpen=true;
            rolePane.SetActive(rolePaneOpen);
            checkRole(0);
        }
        else
        {
            clearAllPane();
        }
    }
    public void changeRoleData(int i)
    {
        BaseRole role=GameManager.instance.findRoleById(i);
        rolePane.transform.Find("经验条").gameObject.GetComponent<Slider>().value=role.Exp/float.Parse(Constant.Level1Exp);
        rolePane.transform.Find("经验条/Text").gameObject.GetComponent<TextMeshProUGUI>().text=role.Exp+"/"+Constant.Level1Exp;
        rolePane.transform.Find("经验条/等级/Text").gameObject.GetComponent<TextMeshProUGUI>().text=role.Level.ToString();
        level.text=role.Level.ToString();
        hp.text=role.MaxBlood.ToString();
        cost.text=role.Cost.ToString();
        ad.text=role.Ad.ToString();
        ap.text=role.Ap.ToString();
        def.text=role.Def.ToString();
        mdf.text=role.Mdf.ToString();
        power.text=role.Power.ToString();
    }
    //动态创建角色按钮
    void createRoleButton()
    {
        for(int i=0;i<roleButtonSet.transform.childCount;i++)
        {
            if(roleButtonSet.transform.childCount==0)
            return;
            Destroy(roleButtonSet.transform.GetChild(i).gameObject);
        }
        for(int i=0;i<GameManager.instance.roles.Count;i++)
        {
            GameObject roleButton=Instantiate(roleButtonPrefab,roleButtonSet.transform);
            RoleButtonControl roleControl = roleButton.GetComponent<RoleButtonControl>();
            roleControl.role=GameManager.instance.roles[i];
            roleControl.image.sprite=roleHeadImages[GameManager.instance.roles[i].Id];
            roleControl.text.text=GameManager.instance.roles[i].Name;
            int index=i;
            roleButton.GetComponent<Button>().onClick.AddListener(()=>checkRole(GameManager.instance.roles[index].Id));
        }
    }
    //设置角色
    public void checkRole(int id)
    {
        clearAllTag();
        createRoleButton();
        currentRole=id;
        //设置角色图片
        roleImage.sprite=roleImages[id];
        //设置角色数值
        changeRoleData(id);
        //设置技能
        setSkill(id);
        //设置武器图片
        weapenImage.sprite=weapens[id];
        //设置灵结晶
        setLing();
    }
    void setSkill(int i)
    {
        Type role=roleUtil.getRoleType(i);
        for (int j = 8; j > GameManager.instance.findRoleById(i).ActiveSkillNum()-1; j--)
        {
            activeSkills[j].SetActive(false);
        }
        String sNormalAttack = role.GetField("normalAttack").GetRawConstantValue().ToString();
        String sNormalDesc = role.GetField("normalDesc").GetRawConstantValue().ToString();
        String sPassiveSkill = role.GetField("passiveSkill").GetRawConstantValue().ToString();
        String sPassiveDesc = role.GetField("passiveDesc").GetRawConstantValue().ToString();
        if(GameManager.instance.findRoleById(i).ActiveSkillNum()>=1)
        {
            String sActiveSkill1 = role.GetField("activeSkill1").GetRawConstantValue().ToString();
            String sActiveDesc1 = role.GetField("activeDesc1").GetRawConstantValue().ToString();
            activeSkills[0].transform.Find("Text").GetComponent<Text>().text=sActiveSkill1;
        }
        if(GameManager.instance.findRoleById(i).ActiveSkillNum()>=2)
        {
            String sActiveSkill2 = role.GetField("activeSkill2").GetRawConstantValue().ToString();
            String sActiveDesc2 = role.GetField("activeDesc2").GetRawConstantValue().ToString();
            activeSkills[1].transform.Find("Text").GetComponent<Text>().text=sActiveSkill2;
        }
        if(GameManager.instance.findRoleById(i).ActiveSkillNum()>=3)
        {
            String sActiveSkill3 = role.GetField("activeSkill3").GetRawConstantValue().ToString();
            String sActiveDesc3 = role.GetField("activeDesc3").GetRawConstantValue().ToString();
            activeSkills[2].transform.Find("Text").GetComponent<Text>().text=sActiveSkill3;
        }
        if(GameManager.instance.findRoleById(i).ActiveSkillNum()>=4)
        {
            String sActiveSkill4 = role.GetField("activeSkill4").GetRawConstantValue().ToString();
            String sActiveDesc4 = role.GetField("activeDesc4").GetRawConstantValue().ToString();
            activeSkills[3].transform.Find("Text").GetComponent<Text>().text=sActiveSkill4;
        }
        if(GameManager.instance.findRoleById(i).ActiveSkillNum()>=5)
        {
            String sActiveSkill5 = role.GetField("activeSkill5").GetRawConstantValue().ToString();
            String sActiveDesc5 = role.GetField("activeDesc5").GetRawConstantValue().ToString();
            activeSkills[4].transform.Find("Text").GetComponent<Text>().text=sActiveSkill5;
        }
        if(GameManager.instance.findRoleById(i).ActiveSkillNum()>=6)
        {
            String sActiveSkill6 = role.GetField("activeSkill6").GetRawConstantValue().ToString();
            String sActiveDesc6 = role.GetField("activeDesc6").GetRawConstantValue().ToString();
            activeSkills[5].transform.Find("Text").GetComponent<Text>().text=sActiveSkill6;
        }
        if(GameManager.instance.findRoleById(i).ActiveSkillNum()>=7)
        {
            String sActiveSkill7 = role.GetField("activeSkill7").GetRawConstantValue().ToString();
            String sActiveDesc7 = role.GetField("activeDesc7").GetRawConstantValue().ToString();
            activeSkills[6].transform.Find("Text").GetComponent<Text>().text=sActiveSkill7;
        }
        if(GameManager.instance.findRoleById(i).ActiveSkillNum()>=8)
        {
            String sActiveSkill8 = role.GetField("activeSkill8").GetRawConstantValue().ToString();
            String sActiveDesc8 = role.GetField("activeDesc8").GetRawConstantValue().ToString();
            activeSkills[7].transform.Find("Text").GetComponent<Text>().text=sActiveSkill8;
        }
        if(GameManager.instance.findRoleById(i).ActiveSkillNum()>=9)
        {
            String sActiveSkill9 = role.GetField("activeSkill9").GetRawConstantValue().ToString();
            String sActiveDesc9 = role.GetField("activeDesc9").GetRawConstantValue().ToString();
            activeSkills[8].transform.Find("Text").GetComponent<Text>().text=sActiveSkill9;
        }
        String sPowerSkill = role.GetField("powerSkill").GetRawConstantValue().ToString();
        String sPowerDesc = role.GetField("powerDesc").GetRawConstantValue().ToString();
        normalAttack.text=sNormalAttack;
        passiveSkill.text=sPassiveSkill;
        powerSkill.text=sPowerSkill;
        
    }
    public void clearAllTag()
    {
        if(tags.Count==0)
        return;
        foreach (GameObject tag in tags)
        {
            if(tag!=null)
            {
                Destroy(tag);
            }
        }
        tags.Clear();
    }
    public void showTag(int i)
    {
        GameObject newTag;
        clearAllTag();
        Type role=null;
        switch(currentRole)
        {
            case 0:
            role=typeof(QinLi);
            break;
            case 1:
            role=typeof(ShiXiang);
            break;
            case 2:
            role=typeof(SiSiNai);
            break;
            case 3:
            role=typeof(KuangSan);
            break;
        }
        switch (i)
        {
            case 0:
            tags.Add(newTag = GameObject.Instantiate(tagPreb,normalAttack.gameObject.transform.position,Quaternion.identity,rolePane.transform));
            newTag.transform.Find("Text").gameObject.GetComponent<Text>().text= role.GetField("normalDesc").GetRawConstantValue().ToString();
            break;
            case 1:
            tags.Add(newTag = GameObject.Instantiate(tagPreb,passiveSkill.gameObject.transform.position,Quaternion.identity,rolePane.transform));
            newTag.transform.Find("Text").gameObject.GetComponent<Text>().text= role.GetField("passiveDesc").GetRawConstantValue().ToString();
            break;
            case 2:
            tags.Add(newTag = GameObject.Instantiate(tagPreb,activeSkills[0].gameObject.transform.position,Quaternion.identity,rolePane.transform));
            newTag.transform.Find("Text").gameObject.GetComponent<Text>().text= role.GetField("activeDesc1").GetRawConstantValue().ToString();
            break;
            case 3:
            tags.Add(newTag = GameObject.Instantiate(tagPreb,activeSkills[1].gameObject.transform.position,Quaternion.identity,rolePane.transform));
            newTag.transform.Find("Text").gameObject.GetComponent<Text>().text= role.GetField("activeDesc2").GetRawConstantValue().ToString();
            break;
            case 4:
            tags.Add(newTag = GameObject.Instantiate(tagPreb,activeSkills[2].gameObject.transform.position,Quaternion.identity,rolePane.transform));
            newTag.transform.Find("Text").gameObject.GetComponent<Text>().text= role.GetField("activeDesc3").GetRawConstantValue().ToString();
            break;
            case 5:
            tags.Add(newTag = GameObject.Instantiate(tagPreb,activeSkills[3].gameObject.transform.position,Quaternion.identity,rolePane.transform));
            newTag.transform.Find("Text").gameObject.GetComponent<Text>().text= role.GetField("activeDesc4").GetRawConstantValue().ToString();
            break;
            case 6:
            tags.Add(newTag = GameObject.Instantiate(tagPreb,activeSkills[4].gameObject.transform.position,Quaternion.identity,rolePane.transform));
            newTag.transform.Find("Text").gameObject.GetComponent<Text>().text= role.GetField("activeDesc5").GetRawConstantValue().ToString();
            break;
            case 7:
            tags.Add(newTag = GameObject.Instantiate(tagPreb,activeSkills[5].gameObject.transform.position,Quaternion.identity,rolePane.transform));
            newTag.transform.Find("Text").gameObject.GetComponent<Text>().text= role.GetField("activeDesc6").GetRawConstantValue().ToString();
            break;
            case 8:
            tags.Add(newTag = GameObject.Instantiate(tagPreb,activeSkills[6].gameObject.transform.position,Quaternion.identity,rolePane.transform));
            newTag.transform.Find("Text").gameObject.GetComponent<Text>().text= role.GetField("activeDesc7").GetRawConstantValue().ToString();
            break;
            case 9:
            tags.Add(newTag = GameObject.Instantiate(tagPreb,activeSkills[7].gameObject.transform.position,Quaternion.identity,rolePane.transform));
            newTag.transform.Find("Text").gameObject.GetComponent<Text>().text= role.GetField("activeDesc8").GetRawConstantValue().ToString();
            break;
            case 10:
            tags.Add(newTag = GameObject.Instantiate(tagPreb,activeSkills[8].gameObject.transform.position,Quaternion.identity,rolePane.transform));
            newTag.transform.Find("Text").gameObject.GetComponent<Text>().text= role.GetField("activeDesc9").GetRawConstantValue().ToString();
            break;
            case 11:
            tags.Add(newTag = GameObject.Instantiate(tagPreb,powerSkill.gameObject.transform.position,Quaternion.identity,rolePane.transform));
            newTag.transform.Find("Text").gameObject.GetComponent<Text>().text= role.GetField("powerDesc").GetRawConstantValue().ToString();
            break;
        }
        
    }

    public void showLingZhuangPane()
    {
        clearAllTag();
        lingZhuangPane.SetActive(true);
        skillPane.SetActive(false);
    }
    //设置灵结晶图片
    public void setLing()
    {
        for (int i = 0; i < lings.Count; i++)
        {
            if(GameManager.instance.findRoleById(currentRole).lingJieJings[i]!=null)
            {
                lings[i].gameObject.GetComponent<Image>().sprite=GameManager.instance.findRoleById(currentRole).lingJieJings[i].itemImage;
            }
            else
            {
                lings[i].gameObject.GetComponent<Image>().sprite=yuanLing;
            }
            
        }
    }
    public void showSkillPane()
    {
        lingZhuangPane.SetActive(false);
        skillPane.SetActive(true);

    }
    
    public void closeOrOpenChosenPane()
    {
        chosenPaneOpen=!chosenPaneOpen;
        chosenPane.SetActive(chosenPaneOpen);
        if(chosenPaneOpen)
        ChosenBagManager.refreshgifts();
    }

    public void openLingChosenPane(int n)
    {
        LingBagManager.instance.chosenGrid=n;
        lingChosenPaneOpen=!lingChosenPaneOpen;
        lingChosenPane.SetActive(lingChosenPaneOpen);
        if(lingChosenPaneOpen)
        LingBagManager.instance.refreshBag();
    }

    public void closeLingChosenPane()
    {
        lingChosenPaneOpen=!lingChosenPaneOpen;
        lingChosenPane.SetActive(lingChosenPaneOpen);
        if(lingChosenPaneOpen)
        LingBagManager.instance.refreshBag();
    }
    
}
