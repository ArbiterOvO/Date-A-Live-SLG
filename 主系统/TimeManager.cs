using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public Slider timeSlider;
    public Image timeImage;
    public TextMeshProUGUI day;
    public GameObject evening;
    public List<Sprite> dayOrNight;//0 白天 1晚上
    
    void Awake() {
        if(instance!=null)
        Destroy(this.gameObject);
        instance=this;
    }

    void Start() {

    }

    void Update() {
        changeTimeUI();
    }

    void changeTimeUI()
    {
        //夜晚未回家休息 第二天自动扣除2点体力
        if(GameManager.instance.time>5)
        {
            //todo 切换天效果
            GameManager.instance.date++;
            GameManager.instance.time=2;
        }
        timeSlider.value=1-GameManager.instance.time/5f;
        if(GameManager.instance.time<4)
        {
            timeImage.sprite=dayOrNight[0];
            if(evening!=null)
            evening.SetActive(false);
            else
            Debug.Log("没有夜晚图片");
        }
        else if(evening!=null)
        {
            timeImage.sprite=dayOrNight[1];
            if(evening!=null)
            evening.SetActive(true);
            else
            Debug.Log("没有夜晚图片");
        }
        day.text=GameManager.instance.date.ToString();
    }

    public void nextDay()
    {
        GameManager.instance.time=0;
        GameManager.instance.date++;
        HomeUI.instance.closeSleepPane();
    }
}
