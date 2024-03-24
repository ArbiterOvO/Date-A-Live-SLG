using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SchoolGateUI : Singleton<SchoolGateUI>
{
    public new GameObject tag;
    public Text tagText;
    public Button yes,no;
    public void exit()
    {
        SceneManager.LoadScene("Main");
    }
    void Start() {
        
    }
    void clearAllTag()
    {
        tag.SetActive(false);
        yes.onClick.RemoveAllListeners();
        no.onClick.RemoveAllListeners();
    }
    public void enterMain()
    {
        if(GameManager.instance.gameStatus!=GameStatus.UI可用)
        return;
        tag.SetActive(true);
        tagText.text="是否要前往主街区";
        yes.onClick.AddListener(()=>{
            SceneManager.LoadScene("Main");
        });
        no.onClick.AddListener(()=>{
            clearAllTag();
        });
    }
    public void enterSchool()
    {
        if(GameManager.instance.gameStatus!=GameStatus.UI可用)
        return;
        tag.SetActive(true);
        tagText.text="是否要前往学校";
        yes.onClick.AddListener(()=>{
            SceneManager.LoadScene("School");
        });
        no.onClick.AddListener(()=>{
            clearAllTag();
        });
    }
    public void enterHighPlatform()
    {
        if(GameManager.instance.gameStatus!=GameStatus.UI可用)
        return;
        tag.SetActive(true);
        tagText.text="是否要前往高台";
        yes.onClick.AddListener(()=>{
            SceneManager.LoadScene("HighPlatForm");
        });
        no.onClick.AddListener(()=>{
            clearAllTag();
        });
    }
}
