using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static String savePath;
    
    void Awake()
    {
        //获取存档位置
        savePath=Path.Combine(Application.persistentDataPath, "saveData.json");
        DontDestroyOnLoad(this);
    }

    public static void Save()
    {
        PlayerGameData data=new PlayerGameData();
        data.roleList=new List<string>();
        foreach (BaseRole role in GameManager.instance.roles)
        {
            Debug.Log(JsonConvert.SerializeObject(role));
            data.roleList.Add(JsonConvert.SerializeObject(role));
        };
        data.winNum=GameManager.instance.winNum;
        String jsonData=JsonConvert.SerializeObject(data);
        File.WriteAllText(savePath,jsonData);
        Debug.Log("保存成功/n路劲为"+savePath);
    }

    public static void Load()
    {
        if(File.Exists(savePath))
        {
            String jsonData=File.ReadAllText(savePath);
            //json反序列化为对象
            PlayerGameData data=JsonConvert.DeserializeObject<PlayerGameData>(jsonData);
            GameManager.instance.roles.Clear();
            foreach (String jsondata in data.roleList)
            {
                GameManager.instance.roles.Add(JsonConvert.DeserializeObject<BaseRole>(jsondata));
            }
           
            Debug.Log("加载成功");
            foreach (BaseRole role in GameManager.instance.roles)
            {
                Debug.Log(role.toString());
            }
            
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("1");
            Save();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            Load();
            foreach (BaseRole role in GameManager.instance.roles)
            {
                Debug.Log(role.toString());
            }
        }
    }
}
