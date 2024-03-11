using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI instance;
    public GameObject shopPane;
    //滚动面板中的商品按钮预制体
    public GameObject goodPrefab;
    //名称 售价 详情 库存
    public Text nameText,priceText,descText,numText;
    //图片
    public Image image;
    //内容
    public GameObject content;
    //通用UI
    public GameObject commonUI;
    //离开按钮
    public GameObject exitButton;
    public bool isShopPaneOpen=false;
    public Item currentItem;
    [Header("娃娃机")]
    public bool isPlaying=false;//是否正在游戏中
    public float speed=2f;
    public bool isMoving=false;//抓钩是否正在移动
    public bool isCaching=false;//是否已经被抓取
    public GameObject machineCanvas;//UI
    public GameObject clawMachine;//整个娃娃机
    public GameObject grapple;//抓钩
    public GameObject itemPrefab;//物品预制体
    public GameObject itemSet;//物品集合
    public List<Item> randomItems;//随机出现的物品

    [Header("提示")]
    public GameObject noMoneyTip;//钱不够
    public GameObject winItemTip;//抓到的东西

    void Awake() {
        if(instance!=null)
        Destroy(this.gameObject);
        instance=this;
    }
    void Update() {
        moveGrapple();
    }
    //返回主界面
    public void exitShop()
    {
        if(GameManager.instance.gameStatus!=GameStatus.UI可用)
        return;
        SceneManager.LoadScene("Main");
    }

    public void openOrCloseShopPane()
    {
        if(GameManager.instance.gameStatus!=GameStatus.UI可用&&!isShopPaneOpen)
        return;
        isShopPaneOpen=!isShopPaneOpen;
        shopPane.SetActive(isShopPaneOpen);
        if(isShopPaneOpen)
        GameManager.instance.gameStatus=GameStatus.部分UI按钮不可用;
        else
        GameManager.instance.gameStatus=GameStatus.UI可用;
        refreshShopPane();
    }

    public void refreshShopPane()
    {
        //清空
        for (int i = 0; i < content.transform.childCount; i++)
        {
            if(content.transform.childCount==0)
            break;
            Destroy(content.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < ShopManager.instance.goods.Count; i++)
        {
            GameObject good=Instantiate(goodPrefab, content.transform);
            good.GetComponent<GoodUI>().item=ShopManager.instance.goods[i];
            good.GetComponent<GoodUI>().text.text=ShopManager.instance.goods[i].name;
        }
        showGoodDetail(ShopManager.instance.goods[0]);
        currentItem=ShopManager.instance.goods[0];
    }

    public void showGoodDetail(Item good)
    {
        nameText.text=good.itemName;
        descText.text=good.itemDesc;
        priceText.text=good.itemPrice.ToString();
        numText.text=good.itemNum.ToString();
        image.sprite=good.itemImage;
    }

    //买东西按钮事件
    public void buy()
    {
        if(GameManager.instance.money>=currentItem.itemPrice) //钱够
        {
            currentItem.itemNum++;
            GameManager.instance.money-=currentItem.itemPrice;
            numText.text=currentItem.itemNum.ToString();
        }
        else //跳出提示
        {
            noMoneyTip.SetActive(true);
        }
    }

    public void clearAllTip()
    {
        noMoneyTip.SetActive(false);
        winItemTip.SetActive(false);
    }
    public void moveGrapple()
    {
        if(isPlaying)
        {
            if(!isMoving)
            {
                float x=Input.GetAxis("Horizontal");
                grapple.transform.Translate(new Vector3(x*speed,0,0)*Time.deltaTime);
            }
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(moveDownGrapple());
            }
        }
    }
    IEnumerator moveDownGrapple()
    {
        while(grapple.transform.position.y>-1.3f)
        {
            if(isCaching)
            break;
            isMoving=true;
            grapple.transform.Translate(new Vector3(0,-speed,0)*Time.deltaTime);
            yield return 0;
        }
        if(!isCaching)
        {
            while(grapple.transform.position.y<2f)
            {
                grapple.transform.Translate(Vector3.up*Time.deltaTime*speed);
                yield return null;
            }
        }
    }
    public void startClawMachine()
    {
        isPlaying=true;
        if(GameManager.instance.money>=100)
        GameManager.instance.money-=100;
        else
        {
            noMoneyTip.SetActive(true);
            clearAllTipFor2();
        }
        
    }
    public void setClawMachine()
    {
        grapple.transform.position=new Vector3(-3,2,0);
        GameManager.instance.gameStatus=GameStatus.部分UI按钮不可用;
        //清空
        for(int i=0;i<itemSet.transform.childCount;i++)
        {
            if(itemSet.transform.childCount==0)
            break;
            Destroy(itemSet.transform.GetChild(i).gameObject);
        }
        //随机生成5个球
        for (int i = 0; i < 5; i++)
        {
            GameObject item=Instantiate(itemPrefab,itemSet.transform);
            float x=Random.Range(-3.5f,2.5f);
            float y=Random.Range(-2f,-0.6f);
            item.transform.localPosition=new Vector3(x,y,0);
            item.GetComponent<ItemInClawMachine>().item=randomItems[Random.Range(0,randomItems.Count)];
        }
    }
    public void showCachingUI(Item item)
    {
        //恢复设置
        isMoving=false;
        isPlaying=false;
        isCaching=false;
        //显示物品
        winItemTip.SetActive(true);
        winItemTip.transform.Find("图片").GetComponent<Image>().sprite=item.itemImage;
        winItemTip.transform.Find("名称").GetComponent<Text>().text=item.itemName;
        StartCoroutine(clearAllTipFor2());
    }
    //2s后关闭提示
    IEnumerator clearAllTipFor2()
    {
        yield return new WaitForSeconds(2f);
        clearAllTip();
    }
    public void openClawMachine()
    {
        if(GameManager.instance.gameStatus!=GameStatus.UI可用)
        return;
        commonUI.transform.Find("按钮").gameObject.SetActive(false);
        exitButton.SetActive(false);
        clawMachine.SetActive(true);
        machineCanvas.SetActive(true);
        setClawMachine();
    }
    public void exitClawMachine()
    {
        commonUI.transform.Find("按钮").gameObject.SetActive(true);
        exitButton.SetActive(true);
        clawMachine.SetActive(false);
        machineCanvas.SetActive(false);
        GameManager.instance.gameStatus=GameStatus.UI可用;
    }

}
