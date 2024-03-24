using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighPlatFormUI : Singleton<HighPlatFormUI>
{
    public GameObject randomEvent;
    public GameObject KuangSan;
    void Start() {
        
    }
    public void returnMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void setRandomEvent()
    {
        if(GameManager.instance.time==4&&GameManager.instance.findRoleByName(KuangSan.name)==null)
        {
            int i=Random.Range(0, 3);
            if(i==0)
            {
                randomEvent.SetActive(true);
            }
        }
    }

    public void meetKuangSan()
    {
        KuangSan.SetActive(true);
        switch (GameManager.instance.meetKuangSanTime)
        {
            case 0:
            DelegateManager d=new DelegateManager();
            d.addDelegate("你是精灵吗？",()=>{
                DialogSystem.instance.clearChoose();
                StartCoroutine(waitForChoose(1));
                });
            d.addDelegate("我是来陪你玩的",()=>{
                DialogSystem.instance.clearChoose();
                StartCoroutine(waitForChoose(2));
                });
            DialogSystem.instance.startDialog(0,d);
            break;
        }
                
    }
        //等待选择完成 出现对应对话
    IEnumerator waitForChoose(int chose)
    {
        yield return new WaitUntil(()=>!DialogSystem.instance.dialogPaneg.activeSelf);
        Debug.Log(chose);
        if(chose==1)
        {
            //siSiNaiImage.SetActive(false);
        }
        DialogSystem.instance.startDialog(chose);
        yield return new WaitUntil(()=>!DialogSystem.instance.dialogPaneg.activeSelf&&chose==2);
        GameManager.instance.specialBattleNum=2;
        GameManager.instance.roles.Add(new BaseRole(2,"冰芽川四糸乃",SiSiNai.blood,SiSiNai.cost,SiSiNai.power,SiSiNai.ad,SiSiNai.ap,SiSiNai.def,SiSiNai.mdf));
        GameManager.instance.rolesInTeam.Add(GameManager.instance.roles[GameManager.instance.roles.Count-1]);
        SceneManager.LoadScene("fighting");
    }
    
}
