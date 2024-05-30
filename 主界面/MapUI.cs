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
    void Start() {
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
    //进入学校
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
    //进入神社
    public void enterShrine()
    {
        tag.SetActive(true);
        tagText.text="是否要前往神社";
        yes.onClick.AddListener(()=>{
            SceneManager.LoadScene("Shrine");
        });
        no.onClick.AddListener(()=>{
            clearAllTag();
        });
        
    }
    //进入天宫塔
    public void enterTower()
    {
        tag.SetActive(true);
        tagText.text="是否要前往天宫塔";
        yes.onClick.AddListener(()=>{
            SceneManager.LoadScene("Tower");
        });
        no.onClick.AddListener(()=>{
            clearAllTag();
        });
    }
    void clearAllTag()
    {
        tag.SetActive(false);
        yes.onClick.RemoveAllListeners();
        no.onClick.RemoveAllListeners();
    }

}
