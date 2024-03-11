using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TalkInApartment : MonoBehaviour,IPointerClickHandler
{
    float fadeSpeed=0.05f;
    //对话id=角色id*5+random(0,5) 0-4 5-9 10-14
    public void OnPointerClick(PointerEventData eventData)
    {
        int n=ApartmentUI.Instance.chosenRole.Id*5+Random.Range(0,5);
        DialogSystem.instance.startDialog(n);
        ApartmentUI.Instance.showExpUp1();
        //如果不是在改变表情 防止多次点击
        if(!ApartmentUI.Instance.inChangeEmotion){
            Role role=ApartmentUI.Instance.role.GetComponent<Role>();
            StartCoroutine(beSmile(role));
        }
        
    }

    IEnumerator beSmile(Role role)
    {
        //设置表情图片和状态
        ApartmentUI.Instance.inChangeEmotion=true;
        role.roleEmote.sprite=role.roleImages[ApartmentUI.Instance.chosenRole.Id]["微笑"];
        role.roleEmote.color=new Color(role.roleEmote.color.r,role.roleEmote.color.g,role.roleEmote.color.b,1);
        //原本图片渐变消失
        while(role.roleImage.color.a>0)
        {
            role.roleImage.color=new Color(role.roleImage.color.r,role.roleImage.color.g,role.roleImage.color.b,role.roleImage.color.a-fadeSpeed);
            yield return new WaitForFixedUpdate();//等待1帧
        }
        yield return new WaitForSeconds(2f);//等待2秒
        //原本图片渐变恢复
        while(role.roleImage.color.a<1)
        {
            role.roleImage.color=new Color(role.roleImage.color.r,role.roleImage.color.g,role.roleImage.color.b,role.roleImage.color.a+fadeSpeed);
            yield return new WaitForFixedUpdate();
        }
        role.roleEmote.color=new Color(role.roleEmote.color.r,role.roleEmote.color.g,role.roleEmote.color.b,0);
        ApartmentUI.Instance.inChangeEmotion=false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
