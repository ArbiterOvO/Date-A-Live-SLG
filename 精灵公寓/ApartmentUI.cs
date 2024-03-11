using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApartmentUI : Singleton<ApartmentUI>
{
    [Header("精灵选择面板")]
    public GameObject chooseRolePane;
    bool ischooseRolePaneOpen=false;
    public BaseRole chosenRole;
    public GameObject roleSet;
    public GameObject rolePrefab;
    [Header("角色")]
    public GameObject role;
    public bool inChangeEmotion=false;//是否在改变表情
    [Header("鼠标")]
    public Texture2D hand;
    [Header("提示")]
    public GameObject expUp1;
    //角色点击次数
    public int clickTime;

    // Start is called before the first frame update
    void Start()
    {
        //初始化第一个角色
        chosenRole=GameManager.instance.roles[0];
        Role currentRole=role.GetComponent<Role>();
        currentRole.roleImage.sprite=currentRole.roleImages[chosenRole.Id]["正常"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //离开
    public void leaveApartment() {
        SceneManager.LoadScene("Main");
    }

    //打开切换精灵面板
    public void openOrCloseChooseRolePane() {
        ischooseRolePaneOpen=!ischooseRolePaneOpen;
        chooseRolePane.SetActive(ischooseRolePaneOpen);
        if(ischooseRolePaneOpen)
        {
            refreshChoosePane();
        }
    }

    public void refreshChoosePane()
    {
        //清空
        for (int i = 0; i < roleSet.transform.childCount; i++)
        {
            if(roleSet.transform.childCount==0)
                break;
            Destroy(roleSet.transform.GetChild(i).gameObject);
        }
        //创建
        for (int i = 0; i < GameManager.instance.roles.Count; i++)
        {
            GameObject role=Instantiate(rolePrefab, roleSet.transform);
            role.GetComponent<RoleControl>().role=GameManager.instance.roles[i];
            role.transform.localScale=Vector3.one;
        }
    }

    public void showRole()
    {
        Role currentRole=role.GetComponent<Role>();
        currentRole.roleImage.sprite=currentRole.roleImages[chosenRole.Id]["正常"];
        openOrCloseChooseRolePane();
    }

    public void showExpUp1()
    {
        if(clickTime<5)
        {
            chosenRole.Exp++;
            StartCoroutine(exp1());
            clickTime++;
        }
        
    }
    IEnumerator exp1()
    {
        expUp1.SetActive(true);
        yield return new WaitForSeconds(1);
        expUp1.SetActive(false);
    }
}
