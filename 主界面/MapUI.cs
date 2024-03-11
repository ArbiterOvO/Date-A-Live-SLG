using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public static MapUI instance;
    public new GameObject tag;
    public Text tagText;
    public Button yes,no;

    void Awake() {
        if(instance!=null)
        Destroy(this.gameObject);
        instance=this;
    }
    //进入家场景
    public void enterHome()
    {
        SceneManager.LoadScene("Home");
    }
    //进入战斗场景
    public void mainFight()
    {   
        if(FightChooseManager.Instance!=null)
        FightChooseManager.Instance.needReset=true;
        SceneManager.LoadScene("FightChoose");
    }
    //进入商店场景
    public void enterShop()
    {
        SceneManager.LoadScene("Shop");
    }
    //进入精灵公寓
    public void enterApartment()
    {
        SceneManager.LoadScene("Apartment");
    }
    public void enterSchool()
    {
        tag.SetActive(true);
        tagText.text="是否要前往学校";
        yes.onClick.AddListener(()=>{
            SceneManager.LoadScene("SchoolGate");
        });
        no.onClick.AddListener(()=>{
            clearAllTag();
        });
    }
    public void enterShrine()
    {
        SceneManager.LoadScene("Shrine");
        GameManager.instance.currentMap=2;
    }
    void clearAllTag()
    {
        tag.SetActive(false);
        yes.onClick.RemoveAllListeners();
        no.onClick.RemoveAllListeners();
    }

}
