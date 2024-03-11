using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleButtonControl : MonoBehaviour
{
    public BaseRole role;
    public Image image;
    public Text text;
    public GameObject backGround; 

    void Update() {
        if(RoleUI.instance.currentRole==role.Id)
        {
            backGround.SetActive(true);
        }
        else
        {
            backGround.SetActive(false);
        }
    }
}
