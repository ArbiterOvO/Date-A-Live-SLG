using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{
    float speed=10;
    float shootInterval=0.15f;

    public GameObject ziDanPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        shoot();
    }
    //移动
    void move()
    {
        float x=Input.GetAxis("Horizontal");
        float y=Input.GetAxis("Vertical");
        if(transform.position.x+x*Time.deltaTime*speed<=-7.5f||transform.position.x+x*Time.deltaTime*speed>=13.5f)
        x=0;
        if(transform.position.y+y*0.6f*Time.deltaTime*speed<=-7.5f||transform.position.y+y*0.6f*Time.deltaTime*speed>=7.5f)
        y=0;
        transform.Translate(new Vector3(x,y*0.6f,0)*Time.deltaTime*speed);
    }
    //射击
    float time=0;
    void shoot()
    {
        time+=Time.deltaTime;
        if(time>shootInterval)
        {
            Instantiate(ziDanPrefab,transform.position,Quaternion.identity);
            time=0;
        }
        
    }
}
