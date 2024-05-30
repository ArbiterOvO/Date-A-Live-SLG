using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighPlatFormUI : Singleton<HighPlatFormUI>
{
    float fadeSpeed = 0.05f;//渐变速度
    public GameObject meetKuangSan2Event, meetKuangSan3Event;
    [Header("狂三和刻刻帝")]
    public GameObject KuangSanZC;
    public GameObject KuangSanLing;
    public GameObject KuangSanThy;
    public GameObject kekedi;
    public GameObject cat;

    void Start()
    {
        setRandomEvent();
        remeetKuangSan();
    }
    public void returnSchoolGate()
    {
        SceneManager.LoadScene("SchoolGate");
    }
    public void setRandomEvent()
    {
        if (GameManager.instance.time == 4 && GameManager.instance.findRoleByName(KuangSan.name) == null)
        {
            if (GameManager.instance.meetKuangSanTime == 1)
            {

                int i = Random.Range(0, 3);
                if (i == 0)
                {
                    Debug.Log("狂三");
                    meetKuangSan2Event.SetActive(true);
                }
            }
            if (GameManager.instance.meetKuangSanTime == 2)
            {
                int i = Random.Range(0, 3);
                if (i == 0)
                {
                    meetKuangSan3Event.SetActive(true);
                }
            }
        }
    }
    //第二次遇见狂三
    public void meetKuangSan2()
    {
        KuangSanZC.SetActive(true);

        DelegateManager d = new DelegateManager();
        d.addDelegate("停下吧，狂三。", () =>
        {
            DialogSystem.instance.clearChoose();
            StartCoroutine(waitForChoose(1));
        });
        d.addDelegate("和我约会吧，狂三", () =>
        {
            DialogSystem.instance.clearChoose();
            StartCoroutine(waitForChoose(3));
        });
        DialogSystem.instance.startDialog(0, d);


    }
    //等待选择完成 出现对应对话
    IEnumerator waitForChoose(int chose)
    {

        if (chose == 1)
        {
            yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
            DialogSystem.instance.startDialog(chose);
            yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
            Image zc = KuangSanZC.GetComponent<Image>();
            KuangSanLing.SetActive(true);
            Image ling = KuangSanLing.GetComponent<Image>();
            kekedi.SetActive(true);
            Image kkd = kekedi.GetComponent<Image>();
            while (zc.color.a > 0)
            {
                zc.color = new Color(zc.color.r, zc.color.g, zc.color.b, zc.color.a - fadeSpeed);
                ling.color = new Color(ling.color.r, ling.color.g, ling.color.b, ling.color.a + fadeSpeed);
                kkd.color = new Color(kkd.color.r, kkd.color.g, kkd.color.b, kkd.color.a + fadeSpeed);
                yield return new WaitForFixedUpdate();
            }
            DialogSystem.instance.startDialog(chose + 1);
            yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
            yield return new WaitForSeconds(0.5f);
            GameManager.instance.specialBattleNum = 3;
            SceneManager.LoadScene("fighting");
        }
        else if (chose == 3)
        {
            yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
            DialogSystem.instance.startDialog(chose);
            yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
            Image zc = KuangSanZC.GetComponent<Image>();
            while (zc.color.a > 0)
            {
                zc.color = new Color(zc.color.r, zc.color.g, zc.color.b, zc.color.a - fadeSpeed);
                yield return new WaitForFixedUpdate();
            }
            DialogSystem.instance.startDialog(chose + 1);
        }
        meetKuangSan2Event.SetActive(false);
    }
    public void meetKuangSan3()
    {

        DelegateManager d = new DelegateManager();
        d.addDelegate("上前查看", () =>
        {
            DialogSystem.instance.clearChoose();
            StartCoroutine(waitForChoose2(6));
        });
        d.addDelegate("默默离开", () =>
        {
            DialogSystem.instance.clearChoose();
            StartCoroutine(waitForChoose2(8));
        });
        DialogSystem.instance.startDialog(5, d);
    }
    IEnumerator waitForChoose2(int chose)
    {
        if(chose==6)
        {
            yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
            DialogSystem.instance.startDialog(chose);
            StartCoroutine(IEnumeratorUtil.fadeIn(KuangSanThy));
            yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
            StartCoroutine(IEnumeratorUtil.fadeIn(cat));
            DialogSystem.instance.startDialog(chose+1);
            yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
            StartCoroutine(IEnumeratorUtil.fadeOut(KuangSanThy));
            StartCoroutine(IEnumeratorUtil.fadeIn(KuangSanZC));
            DialogSystem.instance.startDialog(chose + 2);
            yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
            GameManager.instance.specialEventType=SpecialEventType.猫咪咖啡厅;
        }
        yield return 0;
    }
    public void remeetKuangSan()
    {
        if(GameManager.instance.specialBattleNum == 3)
        {
            DialogSystem.instance.startDialog(9);
            IEnumeratorUtil.fadeIn(KuangSanLing);
            GameManager.instance.specialBattleNum=0;
            StartCoroutine(wait());
        }
    }
    IEnumerator wait()
    {
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        IEnumeratorUtil.fadeOut(KuangSanLing);
    }
}
