using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUI : MonoBehaviour
{
    public static HomeUI instance;
    public GameObject sleepPane;//是否休息提示

    void Awake() {
        //如果实例已经存在，则销毁当前实例
        if(instance!=null)
        Destroy(this.gameObject);
        //将当前实例赋值给实例变量
        instance=this;
    }

    public void exitHome()
    {
        SceneManager.LoadScene("Main");
    }
    //弹出提示窗口
    public void openSleepPane()
    {
        sleepPane.SetActive(true);
    }
    //关闭提示窗口
    public void closeSleepPane()
    {
        sleepPane.SetActive(false);
    }

}
