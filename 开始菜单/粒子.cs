using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 粒子 : MonoBehaviour
{
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time+=Time.deltaTime;
        if(time>0.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
