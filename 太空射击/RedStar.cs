using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedStar : BaseStar
{
    // Start is called before the first frame update
    void Start()
    {
        speed=1f;
        blood=3;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    void boom()
    {
        Instantiate(StarShootUI.Instance.explodeEffect,transform.position,Quaternion.identity);
        RaycastHit2D[] objs=Physics2D.CircleCastAll(transform.position, 3.5f, Vector2.zero, 0);
        foreach (var obj in objs)
        {
            if(obj.collider.gameObject.tag.Equals("陨石"))
            {
                obj.collider.gameObject.GetComponent<BaseStar>().blood-=3;
            }
            else if(obj.collider.gameObject.tag.Equals("飞机"))
            {
                StarShootUI.Instance.hitPlane();
            }

        }
        
    }

    void OnDestroy() {
        if(!this.gameObject.scene.isLoaded) 
        return;
        boom();
    }
}
