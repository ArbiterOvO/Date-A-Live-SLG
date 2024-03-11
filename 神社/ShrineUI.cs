using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShrineUI : Singleton<ShrineUI>
{
    [Header("遇到四糸乃")]
    public GameObject randomEvent;
    public GameObject siSiNaiImage;
    public GameObject rainVFX;
    // Start is called before the first frame update
    void Start()
    {
        createEvent();
        reMeetSiSiNai();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exitShrine()
    {
        SceneManager.LoadScene("Main");
    }
    //设置事件
    void createEvent()
    {
        if(GameManager.instance.time==4&&GameManager.instance.findRoleByName(SiSiNai.name)==null)
        {
            randomEvent.SetActive(true);
        }
        else
        {
            randomEvent.SetActive(false);
        }
    }
    //遇到四糸乃进入战斗
    public void meetSiSiNai()
    {
        rainVFX.SetActive(true);
        siSiNaiImage.SetActive(true);
        DelegateManager d=new DelegateManager();
            d.addDelegate("人偶会说话，是腹语吗？",()=>{
                DialogSystem.instance.clearChoose();
                StartCoroutine(waitForChoose(1));
                });
            d.addDelegate("我是来陪你玩的",()=>{
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
        if(chose==1)
        {
            siSiNaiImage.SetActive(false);
        }
        DialogSystem.instance.startDialog(chose);
        yield return new WaitUntil(()=>!DialogSystem.instance.dialogPaneg.activeSelf&&chose==2);
        GameManager.instance.specialBattleNum=2;
        GameManager.instance.roles.Add(new BaseRole(2,"冰芽川四糸乃",SiSiNai.blood,SiSiNai.cost,SiSiNai.power,SiSiNai.ad,SiSiNai.ap,SiSiNai.def,SiSiNai.mdf));
        GameManager.instance.rolesInTeam.Add(GameManager.instance.roles[GameManager.instance.roles.Count-1]);
        SceneManager.LoadScene("fighting");
    }

    void reMeetSiSiNai()
    {
        if(GameManager.instance.specialBattleNum==2)
        {
            GameManager.instance.specialBattleNum=0;
            DialogSystem.instance.startDialog(3);
        }
    }

    public void enterFight()
    {
        SceneManager.LoadScene("FightChoose");
    }

}
