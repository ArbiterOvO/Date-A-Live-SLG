using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TeamRole : MonoBehaviour,IPointerClickHandler
{
    public BaseRole role;
    public bool isChoosen=false;
    public Image image;
    public Text text;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!isChoosen)
        {
            for(int i=0;i<GameManager.instance.rolesInTeam.Count;i++)
            {
                if(GameManager.instance.rolesInTeam[i]==null)
                {
                    GameManager.instance.rolesInTeam[i]=role;
                    break;
                }
            }
        }
        else
        {
            for(int i=0;i<GameManager.instance.rolesInTeam.Count;i++)
            {
                if(GameManager.instance.rolesInTeam[i]==null)
                continue;
                if(GameManager.instance.rolesInTeam[i].Equals(role))
                {
                    GameManager.instance.rolesInTeam.Remove(role);
                    GameManager.instance.rolesInTeam.Add(null);
                    break;
                }
            }
        }
        TeamUI.instance.refreshTeam();
        foreach (BaseRole role in GameManager.instance.rolesInTeam)
        {
            if(role!=null)
            Debug.Log(role.Name);
        }
    }
    public void setRole(BaseRole role)
    {
        this.role=role;
        image.sprite=TeamUI.instance.sprites[role.Id];
        text.text=role.Name;
    }

}
