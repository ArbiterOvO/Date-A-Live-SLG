using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GoodUI : MonoBehaviour,IPointerClickHandler
{
    public Item item;
    public Text text;
    public void OnPointerClick(PointerEventData eventData)
    {
        ShopUI.instance.currentItem = item;
        ShopUI.instance.showGoodDetail(item);
    }

}
