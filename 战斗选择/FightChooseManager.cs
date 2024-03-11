using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightChooseManager : PersistentSingleton<FightChooseManager>
{
    public int winMoney;//总共钱的数量
    public Dictionary<Item,int> winItem=new Dictionary<Item, int>();//总共物品 数量
    public SerializedDictionary<int,int> winNormalMoney=new SerializedDictionary<int, int>();//正常情况钱的数量 参数1:获得钱的数量 参数2:第几关卡
    public SerializedDictionary<int,SerializedDictionary<Item,int>> winNormalItem=new SerializedDictionary<int, SerializedDictionary<Item, int>>();//正常情况获得的物品 参数1:(1.物品2.物品数量) 参数2:第几关卡
    public int currentLayerNum;//当前层数
    public int fightLayerNum;//战斗层数
    public bool needReset=true;//是否需要重置
    public Vector3 rolePos;
    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        needReset=false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FightChoose"&&needReset)
        {
            FightChooseManager.Instance.currentLayerNum=0;
            FightChooseManager.Instance.fightLayerNum=0;
            FightChooseManager.Instance.winItem.Clear();
            rolePos=new Vector3(0,-4.2f,0);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void Start() {

        GameManager.instance.createTeam();
        winMoney+=winNormalMoney[1];//获得第一关的金钱数量
        foreach(var item in winNormalItem[1])
        {
            winItem.Add(item.Key,item.Value);//获得第一关的物品数量
        }
    }
    //回复全部生命
    public void healAll()
    {
        foreach (var role in GameManager.instance.rolesInTeam)
        {
            role.Blood=role.MaxBlood;
        }
    }
    //进入战斗
    public void enterFight()
    {
        SceneManager.LoadScene("Fighting");
    }
    //随机事件
    public void randomEvent()
    {
        int random = Random.Range(0,1);
        switch(random)
        {
            case 0:
            DelegateManager d=new DelegateManager();
            d.addDelegate("上前",()=>{
                GameManager.instance.currentPower=GameManager.instance.totalPower;
                DialogSystem.instance.clearChoose();
                });
            d.addDelegate("退后",()=>{
                Debug.Log("退后");
                DialogSystem.instance.clearChoose();
                });
            DialogSystem.instance.startDialog(random,d);
            break;
        }
    }

}
