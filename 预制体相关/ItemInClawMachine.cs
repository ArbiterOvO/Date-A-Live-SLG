using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInClawMachine : MonoBehaviour
{
    public Item item;
    void Start() {
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("碰到");
        if (other.gameObject.CompareTag("抓钩")) {
            ShopUI.instance.isCaching=true;
            StartCoroutine(moveBack(other.gameObject));
        }
    }
    IEnumerator moveBack(GameObject zhua) {
        transform.SetParent(ShopUI.instance.grapple.transform.Find("抓到的物品"));
        while(zhua.transform.position.y<2)
        {
            zhua.transform.Translate(Vector3.up*Time.deltaTime*ShopUI.instance.speed);
            yield return null;
        }
        ShopUI.instance.showCachingUI(item);
        Destroy(ShopUI.instance.grapple.transform.Find("抓到的物品").GetChild(0).gameObject);
    }
}
