using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NormalUI : MonoBehaviour
{
    public static NormalUI instance;
    public TextMeshProUGUI money;
    void Awake() {
        if (instance != null)
        Destroy(this.gameObject);
        instance = this;
    }
    void Update() {
        updataUI();
    }
    //更新UI
    void updataUI()
    {
        //钱
        money.text = GameManager.instance.money.ToString();
    }
}
