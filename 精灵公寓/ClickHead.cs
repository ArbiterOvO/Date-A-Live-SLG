using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHead : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    float fadeSpeed=0.05f;//渐变速度
    public void OnPointerEnter(PointerEventData eventData)
    {
        //鼠标进入 改变鼠标图片为手
        Cursor.SetCursor(ApartmentUI.Instance.hand,Vector2.zero,CursorMode.Auto);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //鼠标进入 改变鼠标图片为默认
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //如果不是在改变表情 防止多次点击
        if(!ApartmentUI.Instance.inChangeEmotion){
            Role role=ApartmentUI.Instance.role.GetComponent<Role>();
            StartCoroutine(beShy(role));
            ApartmentUI.Instance.showExpUp1();
        }
    }
    IEnumerator beShy(Role role)
    {
        //设置表情图片和状态
        ApartmentUI.Instance.inChangeEmotion=true;
        role.roleEmote.sprite=role.roleImages[ApartmentUI.Instance.chosenRole.Id]["害羞"];
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


}
