using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
    public static HomeUI instance;
    public GameObject sleepPane;//是否休息提示
    public GameObject specialEvntButton;//特殊事件按钮
    public Image roleImage;
    public SerializedDictionary<string, Sprite> shiXiangImages;
    public SerializedDictionary<string, Sprite> qinLiImages;
    public SerializedDictionary<string, Sprite> SiSiNaiImages;
    public SerializedDictionary<string,Sprite> kuangSanImages;
    //cg图
    public SerializedDictionary<string,GameObject> cgImages;
    public GameObject unCheckTagSet;
    public GameObject powerUpTag;
    public GameObject catCookie;


    void Awake()
    {
        //如果实例已经存在，则销毁当前实例
        if (instance != null)
            Destroy(this.gameObject);
        //将当前实例赋值给实例变量
        instance = this;
    }
    void Start()
    {
        specialEvent();
    }
    public void exitHome()
    {
        SceneManager.LoadScene("Main");
    }
    //弹出提示窗口
    public void openSleepPane()
    {
        sleepPane.SetActive(true);
        
    }
    //关闭提示窗口
    public void closeSleepPane()
    {
        sleepPane.SetActive(false);
    }

    //特殊事件
    public void specialEvent()
    {
        int index = Random.Range(0, GameManager.instance.roles.Count);
        int id = GameManager.instance.roles[index].Id;
        Debug.Log(index + " " + id);
        if (GameManager.instance.findRoleByName(QinLi.name) != null && id == 0 && GameManager.instance.findRoleByName(QinLi.name).specialEventNum < GameManager.instance.findRoleByName(QinLi.name).maxSpecialEventNum)
        {
            specialEvntButton.SetActive(true);
            switch (GameManager.instance.findRoleByName(QinLi.name).specialEventNum)
            {
                case 0:
                    specialEvntButton.GetComponent<Button>().onClick.AddListener(QinLiEvent1);
                    GameManager.instance.findRoleByName(QinLi.name).specialEventNum++;
                    break;
            }
        }
        if (GameManager.instance.findRoleByName(ShiXiang.name) != null && id == 1 && GameManager.instance.findRoleByName(ShiXiang.name).specialEventNum < GameManager.instance.findRoleByName(ShiXiang.name).maxSpecialEventNum)
        {
            specialEvntButton.SetActive(true);
            switch (GameManager.instance.findRoleByName(ShiXiang.name).specialEventNum)
            {
                case 0:
                    specialEvntButton.GetComponent<Button>().onClick.AddListener(ShiXiangEvent1);
                    GameManager.instance.findRoleByName(ShiXiang.name).specialEventNum++;
                    break;
            }
        }
        if (GameManager.instance.findRoleByName(SiSiNai.name) != null && id == 2 && GameManager.instance.findRoleByName(SiSiNai.name).specialEventNum < GameManager.instance.findRoleByName(SiSiNai.name).maxSpecialEventNum)
        {
            specialEvntButton.SetActive(true);
            switch (GameManager.instance.findRoleByName(SiSiNai.name).specialEventNum)
            {
                case 0:
                    specialEvntButton.GetComponent<Button>().onClick.AddListener(SiSiNaiEvent1);
                    GameManager.instance.findRoleByName(SiSiNai.name).specialEventNum++;
                    break;
            }
        }
        if (GameManager.instance.findRoleByName(KuangSan.name) != null && id == 3 && GameManager.instance.findRoleByName(KuangSan.name).specialEventNum < GameManager.instance.findRoleByName(KuangSan.name).maxSpecialEventNum)
        {
            specialEvntButton.SetActive(true);
            switch (GameManager.instance.findRoleByName(KuangSan.name).specialEventNum)
            {
                case 0:
                    specialEvntButton.GetComponent<Button>().onClick.AddListener(KuangSanEvent1);
                    GameManager.instance.findRoleByName(KuangSan.name).specialEventNum++;
                    break;
            }
        }

    }

    //十香特殊事件1
    void ShiXiangEvent1()
    {
        specialEvntButton.SetActive(false);
        StartCoroutine(ShiXiangEvent1Coroutine());
    }

    IEnumerator ShiXiangEvent1Coroutine()
    {
        StartCoroutine(IEnumeratorUtil.fadeIn(cgImages["十香_看书"]));
        yield return new WaitForSeconds(2f);
        StartCoroutine(IEnumeratorUtil.fadeOut(cgImages["十香_看书"]));
        roleImage.gameObject.SetActive(true);
        roleImage.sprite = shiXiangImages["正常"];
        StartCoroutine(IEnumeratorUtil.fadeIn(roleImage.gameObject));
        DelegateManager d = new DelegateManager();
        d.addDelegate("我很期待哦。", () =>
        {
            DialogSystem.instance.clearChoose();
            StartCoroutine(waitForChoose1(1));
        });
        d.addDelegate("还是算了吧。", () =>
        {
            DialogSystem.instance.clearChoose();
            StartCoroutine(waitForChoose1(2));
        });
        DialogSystem.instance.startDialog(0, d);
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
    }

    IEnumerator waitForChoose1(int num)
    {
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        if (num == 1)
        {
            roleImage.sprite = shiXiangImages["微笑"];
            DialogSystem.instance.startDialog(1);
        }
        else
        {
            roleImage.sprite = shiXiangImages["正常"];
            DialogSystem.instance.startDialog(2);
            yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
            roleImage.sprite = shiXiangImages["微笑"];
            DialogSystem.instance.startDialog(3);
        }
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        StartCoroutine(IEnumeratorUtil.fadeOut(roleImage.gameObject));
        GameManager.instance.totalPower += 10;
        StartCoroutine(IEnumeratorUtil.delayDisppear(powerUpTag, 2));
    }

    //琴里特殊事件1
    void QinLiEvent1()
    {
        specialEvntButton.SetActive(false);
        StartCoroutine(QinLiEvent1Coroutine());
    }

    IEnumerator QinLiEvent1Coroutine()
    {
        StartCoroutine(IEnumeratorUtil.fadeIn(cgImages["琴里_吃棒棒糖"]));
        yield return new WaitForSeconds(2f);
        StartCoroutine(IEnumeratorUtil.fadeOut(cgImages["琴里_吃棒棒糖"]));
        roleImage.gameObject.SetActive(true);
        roleImage.sprite = qinLiImages["白_正常"];
        StartCoroutine(IEnumeratorUtil.fadeIn(roleImage.gameObject));
        DialogSystem.instance.startDialog(4);
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        roleImage.sprite = qinLiImages["白_生气"];
        DelegateManager d = new DelegateManager();
        d.addDelegate("真拿你没办法，最后1颗哦。", () =>
        {
            DialogSystem.instance.clearChoose();
            StartCoroutine(waitForChoose2(6));
        });
        d.addDelegate("不行，先吃饭。", () =>
        {
            DialogSystem.instance.clearChoose();
            StartCoroutine(waitForChoose2(7));
        });
        DialogSystem.instance.startDialog(5, d);
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
    }

    IEnumerator waitForChoose2(int num)
    {
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        if (num == 6)
        {
            roleImage.sprite = qinLiImages["白_微笑"];
            DialogSystem.instance.startDialog(6);
        }
        else if (num == 7)
        {
            roleImage.sprite = qinLiImages["白_微笑"];
            DialogSystem.instance.startDialog(7);
        }
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        StartCoroutine(IEnumeratorUtil.fadeOut(roleImage.gameObject));
        GameManager.instance.totalPower += 10;
        StartCoroutine(IEnumeratorUtil.delayDisppear(powerUpTag, 2));
    }

    //四糸乃特殊事件1
    void SiSiNaiEvent1()
    {
        specialEvntButton.SetActive(false);
        StartCoroutine(SiSiNaiEvent1Coroutine());
    }

    IEnumerator SiSiNaiEvent1Coroutine()
    {
        StartCoroutine(IEnumeratorUtil.fadeIn(cgImages["四糸乃_找四糸奈"]));
        yield return new WaitForSeconds(2f);
        StartCoroutine(IEnumeratorUtil.fadeOut(cgImages["四糸乃_找四糸奈"]));
        roleImage.gameObject.SetActive(true);
        roleImage.sprite = SiSiNaiImages["正常"];
        StartCoroutine(IEnumeratorUtil.fadeIn(roleImage.gameObject));
        DelegateManager d = new DelegateManager();
        d.addDelegate("在客厅寻找。", () =>
        {
            DialogSystem.instance.clearChoose();
            StartCoroutine(waitForChoose3(9));
        });
        d.addDelegate("在洗手间寻找。", () =>
        {
            DialogSystem.instance.clearChoose();
            StartCoroutine(waitForChoose3(10));
        });
        DialogSystem.instance.startDialog(8, d);


        yield return 0;
    }

    IEnumerator waitForChoose3(int num)
    {
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        if (num == 9)
        {
            DialogSystem.instance.startDialog(9);
        }
        else if (num == 10)
        {
            DialogSystem.instance.startDialog(10);
        }
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        roleImage.sprite = SiSiNaiImages["害羞"];
        DialogSystem.instance.startDialog(11);
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        StartCoroutine(IEnumeratorUtil.fadeOut(roleImage.gameObject));
        GameManager.instance.totalPower += 10;
        StartCoroutine(IEnumeratorUtil.delayDisppear(powerUpTag, 2));
    }
    //狂三特殊事件1
    void KuangSanEvent1()
    {
        specialEvntButton.SetActive(false);
        StartCoroutine(KuangSanEvent1Coroutine());
    }

    IEnumerator KuangSanEvent1Coroutine()
    {
        roleImage.gameObject.SetActive(true);
        roleImage.sprite = kuangSanImages["正常"];
        StartCoroutine(IEnumeratorUtil.fadeIn(roleImage.gameObject));
        DialogSystem.instance.startDialog(12);
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        IEnumeratorUtil.fadeIn(catCookie);
        roleImage.sprite = kuangSanImages["害羞"];
        catCookie.SetActive(true);
        DialogSystem.instance.startDialog(13);
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        catCookie.SetActive(false);
        roleImage.sprite = kuangSanImages["含着饼干"];
        DelegateManager d = new DelegateManager();
        d.addDelegate("凑过去吃狂三嘴上的饼干", () =>
        {
            DialogSystem.instance.clearChoose();
            StartCoroutine(waitForChoose4(15));
        });
        d.addDelegate("用手拦住狂三的嘴", () =>
        {
            DialogSystem.instance.clearChoose();
            StartCoroutine(waitForChoose4(16));
        });
        DialogSystem.instance.startDialog(14, d);
        yield return 0;
    }
    IEnumerator waitForChoose4(int num)
    {
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        if (num == 15)
        {
            roleImage.sprite = kuangSanImages["微笑"];
            DialogSystem.instance.startDialog(15);
        }
        else if (num == 16)
        {
            roleImage.sprite = kuangSanImages["微笑"];
            DialogSystem.instance.startDialog(16);
        }
        yield return new WaitUntil(() => !DialogSystem.instance.dialogPaneg.activeSelf);
        StartCoroutine(IEnumeratorUtil.fadeOut(roleImage.gameObject));
        GameManager.instance.totalPower += 10;
        StartCoroutine(IEnumeratorUtil.delayDisppear(powerUpTag, 2));
    }
    public void clearAllUnCheckTag()
    {
        for (int i = 0; i < unCheckTagSet.transform.childCount; i++)
        {
            unCheckTagSet.transform.GetChild(i).gameObject.SetActive(false);
        }

    }
}
