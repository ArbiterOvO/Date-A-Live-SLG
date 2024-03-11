using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//地图事件
public class BaseMapEvent : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.time+=1;
    }


}
