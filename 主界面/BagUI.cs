using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BagUI : MonoBehaviour
{
    // 是否打开
    bool isOpen=false;
    // 背包面板
    public GameObject bagPane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 打开或关闭背包
    public void OpenOrCloseBag()
    {
        if(GameManager.instance.gameStatus!=GameStatus.UI可用)
        return;
        // 将isOpen取反
        isOpen=!isOpen;
        // 设置背包面板的激活状态
        bagPane.SetActive(isOpen);
        // 如果背包打开
        if(isOpen)
        {
            // 刷新物品列表
            BagManager.refreshItems();
            // 重置UI
            BagManager.instance.clearAll();
        }
        
    }

    public void useItem()
    {
        //如果没有物品被选中，则返回
        if(BagManager.instance.chosenItem==null)
        return;
        //根据物品类型进行不同的操作
        switch (BagManager.instance.chosenItem.itemType)
        {
            case ItemType.灵结晶:
            OpenOrCloseBag();
            RoleUI.instance.roleButton();
            RoleUI.instance.showLingZhuangPane();
            
            break;
            case ItemType.礼物:
            OpenOrCloseBag();
            RoleUI.instance.roleButton();
            RoleUI.instance.closeOrOpenChosenPane();
            
            break;
        }
        BagManager.instance.chosenItem.itemNum--;
        BagManager.instance.checkNull();
    }
}