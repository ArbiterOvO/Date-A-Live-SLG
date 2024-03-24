using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IEnumeratorUtil : MonoBehaviour
{
    public static IEnumerator fadeIn(GameObject gameObject)
    {
        gameObject.SetActive(true);
        Image image = gameObject.GetComponent<Image>();
        image.color=new Color(image.color.r,image.color.g,image.color.b,0);
        while (image.color.a < 1)
        {
            image.color=new Color(image.color.r,image.color.g,image.color.b,image.color.a+0.025f);
            yield return new WaitForFixedUpdate();
        } 
    }

    public static IEnumerator fadeOut(GameObject gameObject)
    {
        Image image = gameObject.GetComponent<Image>();
        image.color=new Color(image.color.r,image.color.g,image.color.b,1);
        while (image.color.a >0)
        {
            image.color=new Color(image.color.r,image.color.g,image.color.b,image.color.a-0.025f);
            yield return new WaitForFixedUpdate();
        } 
        gameObject.SetActive(false);
    }
}
