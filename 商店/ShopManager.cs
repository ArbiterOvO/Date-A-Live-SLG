using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public List<Item> goods;
    void Awake() {
        if (instance != null) 
        Destroy(this.gameObject);
        instance=this;
    }

    

}
