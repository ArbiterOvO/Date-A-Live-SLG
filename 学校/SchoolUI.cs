using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SchoolUI : Singleton<SchoolUI>
{
    //主界面 学校
    public GameObject Main,classRoom;
    //随机教室事件
    public GameObject randomClassRoom;
    //人物图片
    public GameObject shiXiangImage;
    // Start is called before the first frame update
    void Start()
    {
        reMeetShiXiang();
    }

    // Update is called once per frame
    void Update()
    {
        createEvent();
    }
    void clearAllScene()
    {
        Main.SetActive(false);
        classRoom.SetActive(false);
    }
    public void returnMain()
    {
        clearAllScene();
        Main.SetActive(true);
        shiXiangImage.SetActive(false);
    }
    public void enterClassRoom()
    {
        clearAllScene();
        classRoom.SetActive(true);
    }
    public void exitSchool()
    {
        SceneManager.LoadScene("SchoolGate");
    }
    //遇到十香进入战斗
    public void meetShiXiang()
    {
        enterClassRoom();
        shiXiangImage.SetActive(true);
        DelegateManager d=new DelegateManager();
            d.addDelegate("我是五河士道，是来救你的",()=>{
                DialogSystem.instance.clearChoose();
                StartCoroutine(waitForChoose(1));
                });
            d.addDelegate("我是你老公",()=>{
                DialogSystem.instance.clearChoose();
                StartCoroutine(waitForChoose(2));
                });
        DialogSystem.instance.startDialog(0,d);
    }
    //等待选择完成 出现对应对话
    IEnumerator waitForChoose(int chose)
    {
        yield return new WaitUntil(()=>!DialogSystem.instance.dialogPaneg.activeSelf);
        Debug.Log(chose);
        DialogSystem.instance.startDialog(chose);
        yield return new WaitUntil(()=>!DialogSystem.instance.dialogPaneg.activeSelf);
        yield return new WaitForSeconds(1);
        GameManager.instance.specialBattleNum=1;
        GameManager.instance.roles.Add(new BaseRole(1,"夜刀神十香",ShiXiang.blood,ShiXiang.cost,ShiXiang.power,ShiXiang.ad,ShiXiang.ap,ShiXiang.def,ShiXiang.mdf));
        GameManager.instance.rolesInTeam.Add(GameManager.instance.roles[GameManager.instance.roles.Count-1]);
        SceneManager.LoadScene("fighting");
    }
    //设置事件
    void createEvent()
    {
        if(GameManager.instance.date<5&&GameManager.instance.findRoleByName(ShiXiang.name)==null)
        {
            randomClassRoom.SetActive(true);
        }
        else
        {
            randomClassRoom.SetActive(false);
        }
    }
    void reMeetShiXiang()
    {
        if(GameManager.instance.specialBattleNum==1)
        {
            GameManager.instance.specialBattleNum=0;
            enterClassRoom();
            DialogSystem.instance.startDialog(3);
        }
    }
}
    

