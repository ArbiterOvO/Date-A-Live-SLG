using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Event : MonoBehaviour
{
    public int layerNum;//所在层数 0开始
    public EventType eventType;//事件种类
    private SpriteRenderer sprite;
    public bool isActive;//是否可用
    void Start() {
        sprite=gameObject.GetComponent<SpriteRenderer>();
    }
    void OnMouseDown() {
        if(isActive&&GameManager.instance.gameStatus==GameStatus.UI可用)
        {
            //记录角色位置
            FightChooseManager.Instance.rolePos=transform.position;
            //移动角色到指定位置
            FightChooseUI.Instance.player.GetComponent<Player>().movePlayer(transform,eventType);
            FightChooseUI.Instance.currentEvents[layerNum].SetActive(false);
            ++FightChooseManager.Instance.currentLayerNum;
        }
    }

    public void turnGrey()
    {
        sprite.color=Color.grey;
    }

    public void turnWhite()
    {
        sprite.color=Color.white;
    }
}
