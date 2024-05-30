using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IEnumeratorUtil : MonoBehaviour
{
    public static IEnumerator fadeIn(GameObject gameObject)
    {
        gameObject.SetActive(true);
        Debug.Log(gameObject);
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

    public static IEnumerator fadeInSprite(GameObject gameObject)
    {
        gameObject.SetActive(true);
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.color=new Color(sprite.color.r,sprite.color.g,sprite.color.b,0);
        while (sprite.color.a < 1)
        {
            sprite.color=new Color(sprite.color.r,sprite.color.g,sprite.color.b,sprite.color.a+0.05f);
            yield return new WaitForFixedUpdate();
        } 
    }

    public static IEnumerator fadeOutSprite(GameObject gameObject)
    {
        SpriteRenderer image = gameObject.GetComponent<SpriteRenderer>();
        image.color=new Color(image.color.r,image.color.g,image.color.b,1);
        while (image.color.a >0)
        {
            image.color=new Color(image.color.r,image.color.g,image.color.b,image.color.a-0.05f);
            yield return new WaitForFixedUpdate();
        } 
        gameObject.SetActive(false);
    }

    public static IEnumerator delayDisppear(GameObject gameObject,float time)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

}
