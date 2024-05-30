using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingControl : Singleton<SettingControl>
{
    public GameObject settingPanel;
    void Update() {
        keyListener();
    }
    public void open()
    {
        settingPanel.SetActive(true);
    }
    public void close()
    {
        settingPanel.SetActive(false);
    }

    public void keyListener()
    {
        // 监听按键
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingPanel.activeSelf)
            {
                close();
            }
            else
            {
                open();
            }
        }
    }
}
