using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigStar : BaseStar
{
    // Start is called before the first frame update
    void Start()
    {
        speed=0.5f;
        blood=10;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
