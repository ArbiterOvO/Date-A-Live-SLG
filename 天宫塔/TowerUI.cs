using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerUI : Singleton<TowerUI>
{
    public GameObject starFall;
    void Start() {

    }
    public void returnMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void enterFight()
    {
        SceneManager.LoadScene("FightChoose");
    }

    public void randomEvent()
    {
        int index=Random.Range(0,3);
        if(index==0)
        {
            starFall.SetActive(true);
        }
    }

    public void starFallEvent()
    {
        DialogSystem.instance.startDialog(0);
    }
}
