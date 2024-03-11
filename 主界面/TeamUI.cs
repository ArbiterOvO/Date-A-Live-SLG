using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamUI : MonoBehaviour
{
    public static TeamUI instance;
    public List<Sprite> sprites;
    //编队面板
    public GameObject teamPane;
    //出战队伍父集合
    public GameObject team;
    //待战队伍父集合
    public GameObject waitTeam;
    //出战队伍游戏对象
    public List<GameObject> teamGameObjects=new List<GameObject>(4);
    //代战队伍游戏对象
    public List<GameObject> waitTeamGameObjects=new List<GameObject>();
    //出战人物预制体
    public GameObject rolePrefab;
    public bool isOpen=false;
    void Awake()
    {
        if (instance != null)
        Destroy(this.gameObject);
        instance = this;

    }
    public void openOrCloseTeamPane()
    {
        if(GameManager.instance.gameStatus!=GameStatus.UI可用)
        return;
        isOpen=!isOpen;
        teamPane.SetActive(isOpen);
        refreshTeam();
    }

    public void refreshTeam()
    {
        //刷新出战
        for (int i = 0; i < instance.team.transform.childCount; i++)
        {
            //如果出战队伍为空，则跳出循环
            if (instance.team.transform.childCount == 0)
                break;
            //销毁出战角色
            Destroy(instance.team.transform.GetChild(i).gameObject);
            //清空出战角色集合
            instance.teamGameObjects.Clear();
        }
        for (int i = 0; i < GameManager.instance.rolesInTeam.Count; i++)
        {
            instance.teamGameObjects.Add(null);
        }
        for (int i = 0; i < GameManager.instance.rolesInTeam.Count; i++)
        {
            if(GameManager.instance.rolesInTeam[i]==null)
            {
                continue;
            }
            instance.teamGameObjects[i]=Instantiate(instance.rolePrefab);
            instance.teamGameObjects[i].transform.SetParent(instance.team.transform);
            instance.teamGameObjects[i].transform.localScale = Vector3.one;
            instance.teamGameObjects[i].GetComponent<TeamRole>().setRole(GameManager.instance.rolesInTeam[i]);
        }
        foreach (GameObject teamRole in instance.teamGameObjects)
        {
            if(teamRole==null)
            continue;
            TeamRole role = teamRole.GetComponent<TeamRole>();
            if(GameManager.instance.rolesInTeam.Contains(role.role))
            role.isChoosen=true;
            else
            role.isChoosen=false;
        }
        //刷新待战
        for (int i = 0; i < instance.waitTeam.transform.childCount; i++)
        {
            if (instance.waitTeam.transform.childCount == 0)
                break;
            Destroy(instance.waitTeam.transform.GetChild(i).gameObject);
            instance.waitTeamGameObjects.Clear();
        }
        for(int i=0;i<GameManager.instance.roles.Count;i++)
        {
            instance.waitTeamGameObjects.Add(GameObject.Instantiate(instance.rolePrefab));
            instance.waitTeamGameObjects[i].transform.SetParent(instance.waitTeam.transform);
            instance.waitTeamGameObjects[i].transform.localScale = Vector3.one;
            instance.waitTeamGameObjects[i].GetComponent<TeamRole>().setRole(GameManager.instance.roles[i]);
        }
        foreach (GameObject teamRole in instance.waitTeamGameObjects)
        {
            TeamRole role = teamRole.GetComponent<TeamRole>();
            if(GameManager.instance.rolesInTeam.Contains(role.role))
            role.isChoosen=true;
            else
            role.isChoosen=false;
        }
    }
}
