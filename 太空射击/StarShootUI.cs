using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarShootUI : Singleton<StarShootUI>
{
    [Header("飞机")]
    public GameObject plane;
    [Header("普通陨石")]
    public GameObject normalStarPrefab;
    [Header("红色陨石")]
    public GameObject redStarPrefab;
    [Header("大陨石")]
    public GameObject bigStarPrefab;
    [Header("血量")]
    public GameObject blood;
    [Header("能量")]
    public GameObject power;
    [Header("爆炸特效")]
    public GameObject explodeEffect;
    [Header("激光")]
    public GameObject ray;
    [Header("可进入数量")]
    public int enterNum=5;
    public TextMeshProUGUI enterNumText;
    [Header("胜利面板")]
    public GameObject winPanel;
    public List<Item> winItems;
    public Image ItemImage;
    public TextMeshProUGUI ItemNum;
    [Header("失败面板")]
    public GameObject losePanel;
    int starNum = 50;
    float createInterval = 1f;
    float time = 0;
    bool isPower = false;
    public List<GameObject> stars;
    void Start()
    {

    }

    void Update()
    {
        createStar();
        usePower();
        win();
        lose();
    }

    public void createStar()
    {
        time += Time.deltaTime;
        if (time > createInterval)
        {
            time = 0;
            float x = Random.Range(-7.5f, 13.5f);
            Vector3 pos = new Vector3(x, 8.7f, 9);
            if (starNum > 40)
            {
                stars.Add(Instantiate(normalStarPrefab, pos, Quaternion.identity));
                starNum--;
            }
            else if (starNum > 30)
            {
                if (starNum % 5 == 0)
                {
                    stars.Add(Instantiate(redStarPrefab, pos, Quaternion.identity));
                }
                else
                {
                    stars.Add(Instantiate(normalStarPrefab, pos, Quaternion.identity));
                }
                starNum--;
            }
            else if(starNum>0)
            {
                if (starNum % 10 == 0)
                {
                    stars.Add(Instantiate(bigStarPrefab, pos, Quaternion.identity));
                }
                else if (starNum % 5 == 0)
                {
                    stars.Add(Instantiate(redStarPrefab, pos, Quaternion.identity));
                }
                else
                {
                    stars.Add(Instantiate(normalStarPrefab, pos, Quaternion.identity));
                }
                starNum--;
            }
        }

    }
    public void hitPlane()
    {
        //血量减少
        if (blood.transform.childCount != 0)
        Destroy(blood.transform.GetChild(0).gameObject);
        //血量为0 失败
        if (blood.transform.childCount == 0)
        {
            lose();
        }
        //todo 特效
    }
    void usePower()
    {

        if(Input.GetKeyDown(KeyCode.Space)&&!isPower&&power.transform.childCount!=0)
        {
            Destroy(power.transform.GetChild(0).gameObject);
            ray.SetActive(true);
            PostManager.Instance.ShakeCamera();
            isPower=true;
            StartCoroutine(PowerShoot());
        }
        
    }

    IEnumerator PowerShoot()
    {
        StartCoroutine(IEnumeratorUtil.fadeInSprite(ray));
        for (int i = 0; i < stars.Count; i++)
        {
            Destroy(stars[i]);
        }
        stars.Clear();
        yield return new WaitForSeconds(1f);
        ray.SetActive(false);
        isPower = false;
    }
    void win()
    {
        
        if(starNum==0&&stars.Count==0&&!winPanel.activeSelf)
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
            int index=Random.Range(0,winItems.Count);
            ItemImage.sprite=winItems[index].itemImage;
            int num=Random.Range(1,4);
            ItemNum.text=num.ToString();
            BagManager.instance.addItem(winItems[index],num);

        }
    }
    public void back()
    {
        SceneManager.LoadScene("Tower");
    }
    void lose()
    {
        
        if((blood.transform.childCount==0||enterNum==0)&&!losePanel.activeSelf)
        {
            Time.timeScale = 0;
            losePanel.SetActive(true);
        }
    }
}
