using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        destoryCheck();
    }

    void move()
    {
        transform.Translate(new Vector3(0,speed*Time.deltaTime,0));
    }

    void destoryCheck()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        if(pos.x<0||pos.x>Screen.width||pos.y<0||pos.y>Screen.height)
        {
            Destroy(this.gameObject);
        }
    }


}
