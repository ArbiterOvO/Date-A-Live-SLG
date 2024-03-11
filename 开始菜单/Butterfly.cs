using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Butterfly : MonoBehaviour, IPointerDownHandler
{
    float time,x,y;
    Vector3 point;
    GameObject effectGo;
    Camera ca;
    RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        ca=GameObject.Find("Main Camera").GetComponent<Camera>();
        effectGo=Resources.Load<GameObject>("Prefabs/LiZi");
        rt=GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        time+=Time.deltaTime;
        if(time>0.5f)
        {
            float newX,newY;
            time=0;
            x=Random.Range(-10,10);
            y=Random.Range(-6,6);
            newX=rt.anchoredPosition.x+x*0.1f*Time.deltaTime;
            newY=rt.anchoredPosition.y+y*0.1f*Time.deltaTime;
            if(newX<-800f||newX>-500f)
            x*=-1;
            if(newY<200f||newY>400f)
            y*=-1;
        }
        rt.Translate(new Vector3(x*0.1f*Time.deltaTime,y*0.1f*Time.deltaTime,0));
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Destroy(this.gameObject);
        point = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 4f);
        point = ca.ScreenToWorldPoint(point);//从屏幕空间转换到世界空间
        GameObject go = Instantiate(effectGo);//生成特效
        go.transform.position = point;
        Destroy(go, 0.5f);
    }
}
