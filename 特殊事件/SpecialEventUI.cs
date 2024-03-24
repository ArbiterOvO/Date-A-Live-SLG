using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialEventUI : Singleton<SpecialEventUI>
{
    [Header("场景")]
    public GameObject catCoffee;
    [Header("图片")]
    public GameObject sleepingCat;
    public GameObject catCoffeeCup;
    [Header("角色")]
    public GameObject KuangSanZC;
    public GameObject KuangSanThy;
    public GameObject KuangSanSmile;
    [Header("提示")]
    public GameObject KuangSanJoin;
    void Start() {
        defaultSet();
    }
    public void clearAllScene()
    {
        catCoffee.SetActive(false);
    }
    public void defaultSet()
    {
        clearAllScene();
        switch (GameManager.instance.specialEventType)
        {
            case SpecialEventType.猫咪咖啡厅:
            catCoffee.SetActive(true);
            startCatCoffee();
            break;
        }
    }

    public void startCatCoffee()
    {
        // DelegateManager d=new DelegateManager();
        //     d.addDelegate("人偶会说话，是腹语吗？",()=>{
        //         DialogSystem.instance.clearChoose();
        //         StartCoroutine(waitForChoose(1));
        //         });
        //     d.addDelegate("我是来陪你玩的",()=>{
        //         DialogSystem.instance.clearChoose();
        //         StartCoroutine(waitForChoose(2));
        //         });
        DialogSystem.instance.startDialog(0);
        StartCoroutine(wait());
    }

    //等待选择完成 出现对应对话
    IEnumerator wait()
    {
        yield return new WaitUntil(()=>!DialogSystem.instance.dialogPaneg.activeSelf);
        StartCoroutine(IEnumeratorUtil.fadeIn(sleepingCat));
        DialogSystem.instance.startDialog(1);
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        StartCoroutine(IEnumeratorUtil.fadeOut(sleepingCat));
        yield return new WaitForSeconds(1f);
        StartCoroutine(IEnumeratorUtil.fadeIn(KuangSanThy));
        DialogSystem.instance.startDialog(2);
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        StartCoroutine(IEnumeratorUtil.fadeIn(KuangSanSmile));
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(IEnumeratorUtil.fadeOut(KuangSanThy));
        DialogSystem.instance.startDialog(3);
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        StartCoroutine(IEnumeratorUtil.fadeIn(catCoffeeCup));
        DialogSystem.instance.startDialog(4);
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        StartCoroutine(IEnumeratorUtil.fadeOut(catCoffeeCup));
        DialogSystem.instance.startDialog(5);
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        GameManager.instance.roles.Add(new BaseRole(3,"时崎狂三",KuangSan.blood,KuangSan.cost,KuangSan.power,KuangSan.ad,KuangSan.ap,KuangSan.def,KuangSan.mdf));
        GameManager.instance.rolesInTeam.Add(GameManager.instance.roles[GameManager.instance.roles.Count-1]);
        KuangSanJoin.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main");
    }
    
}
