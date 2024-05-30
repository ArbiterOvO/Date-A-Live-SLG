using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStar : MonoBehaviour
{
    public float speed;
    public int blood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        move(speed);
        destoryCheck();
    }

    void move (float speed)
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    void destoryCheck()
    {
        if(transform.position.y<-8)
        {
            StarShootUI.Instance.stars.Remove(this.gameObject);
            StarShootUI.Instance.enterNum--;
            StarShootUI.Instance.enterNumText.text=StarShootUI.Instance.enterNum.ToString();
            Destroy(gameObject);
        }
        if(blood<=0)
        {
            StarShootUI.Instance.stars.Remove(this.gameObject);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="子弹")
        {
            Destroy(other.gameObject);
            blood--;
            if(blood<=0)
            {
                StarShootUI.Instance.stars.Remove(this.gameObject);
                Destroy(gameObject);
            }
        }
        else if(other.gameObject.tag=="飞机")
        {
            StarShootUI.Instance.stars.Remove(this.gameObject);
            Destroy(gameObject);
            StarShootUI.Instance.hitPlane();
        }
    }
}
