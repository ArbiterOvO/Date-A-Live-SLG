using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager instance;
    void Awake() {
        //如果实例已经存在，则销毁当前实例
        if(instance!=null)
        Destroy(this.gameObject);
        //将当前实例赋值给实例变量
        instance=this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startNewGame()
    {
        GameManager.instance.createRoles();
        SceneManager.LoadScene("Main");
    }

    public void continueGame()
    {
        DataManager.Load();
        SceneManager.LoadScene("Main");
    }

    public void exitGame()
    {
        Application.Quit();
    }
    
}
