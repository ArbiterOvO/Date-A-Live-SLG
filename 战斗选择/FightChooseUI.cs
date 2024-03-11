using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightChooseUI : Singleton<FightChooseUI>
{
    //人物对象
    public GameObject player;
    public Slider powerSlider;
    public Text powerText;
    //所有事件
    public List<GameObject> events1;
    public List<GameObject> events2;
    public List<GameObject> currentEvents;
    //角色对象
    public List<GameObject> roles=new List<GameObject>();
    //角色预制体
    public GameObject rolePrefab;
    //角色集合对象
    public GameObject roleCollection;
    //角色头像图片
    public List<Sprite> roleImages;
    //是否离开提示
    public GameObject leaveTip;
    [Header("胜利UI")]
    //面板
    public GameObject winPanel;
    //获得的钱
    public TextMeshProUGUI moneyText;
    //获得的物品集合
    public GameObject itemSet;
    //获得物品预制体
    public GameObject itemPrefab;
    //场景 公园 神社
    public GameObject garden,shrine;
    bool leaveTipOpen;
    void Start() {
        showRole();
        defaultSet();
    }
    void Update() {
        updateUI();
        setLayer();
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     showWinPane();
        // }

    }
    public void exit()
    {
        switch(GameManager.instance.currentMap)
        {
            case 1:
            SceneManager.LoadScene("Main");
            break;
            case 2:
            SceneManager.LoadScene("Shrine");
            break;
        }
        
    }

    void updateUI()
    {
        powerSlider.value=GameManager.instance.currentPower/GameManager.instance.totalPower;
        powerText.text=GameManager.instance.currentPower.ToString();
    }
    void clearAllScene()
    {
        garden.SetActive(false);
        shrine.SetActive(false);
    }
    void changeGarden()
    {
        clearAllScene();
        garden.SetActive(true);
    }
    void changeShrine()
    {
        clearAllScene();
        shrine.SetActive(true);
    }
    //初始化设置
    public void defaultSet()
    {
        //当前场景
        switch(GameManager.instance.currentMap)
        {
            case 1:
            changeGarden();
            currentEvents=events1;
            break;
            case 2:
            changeShrine();
            currentEvents=events2;
            break;
        }
        //角色头像初始位置
        if(FightChooseManager.Instance.rolePos!=Vector3.zero)
        {
            player.transform.position=FightChooseManager.Instance.rolePos;
        }
    }
    //设置每层是否可用
    void setLayer()
    {
        foreach (var gameObject in currentEvents)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Event eventComponent = gameObject.transform.GetChild(i).GetComponent<Event>();
                if(eventComponent.layerNum==FightChooseManager.Instance.currentLayerNum)
                {
                    eventComponent.isActive=true;
                    eventComponent.turnWhite();
                }
                else if(eventComponent.layerNum<=FightChooseManager.Instance.currentLayerNum)
                {
                    eventComponent.isActive=false;
                    eventComponent.gameObject.SetActive(false);
                }
                else
                {
                    eventComponent.isActive=false;
                    eventComponent.turnGrey();
                }
            }
        }
    }
    //显示角色
    void showRole()
    {
        //清空
        for (int i = 0; i < roleCollection.transform.childCount; i++)
        {
            if(roleCollection.transform.childCount==0)
            break;
            Destroy(roleCollection.transform.GetChild(i).gameObject);
        }
        //初始化人物对象
        roles.Clear();
        for (int i = 0; i < GameManager.instance.rolesInTeam.Count; i++)
        {
            roles.Add(Instantiate(rolePrefab));
            roles[i].transform.SetParent(roleCollection.transform);
            roles[i].GetComponent<FightChooseRole>().image.sprite=roleImages[GameManager.instance.rolesInTeam[i].Id];
            roles[i].GetComponent<FightChooseRole>().role=GameManager.instance.rolesInTeam[i];
            roles[i].transform.localScale=new Vector3(1,1,1);
        }
    }
    //显示是否离开提示
    public void showLeaveTipOrNot()
    {
        leaveTipOpen=!leaveTipOpen;
        leaveTip.SetActive(leaveTipOpen);
    } 
    //显示胜利面板
    public void showWinPane()
    {
        winPanel.SetActive(true);
        moneyText.text=FightChooseManager.Instance.winMoney.ToString();
        //显示获得物品
        for (int i = 0; i < itemSet.transform.childCount; i++)
        {
            if(itemSet.transform.childCount==0)
            break;
            Destroy(itemSet.transform.GetChild(i).gameObject);
        }
        foreach (var item in FightChooseManager.Instance.winItem)
        {
            GameObject itemObj=Instantiate(itemPrefab);
            itemObj.transform.SetParent(itemSet.transform);
            itemObj.GetComponent<Image>().sprite=item.Key.itemImage;
            itemObj.GetComponentInChildren<TextMeshProUGUI>().text=item.Value.ToString();
            itemObj.transform.localScale=new Vector3(1,1,1);
        }
    }
}
