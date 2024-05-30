using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStar : BaseStar
{

    // Start is called before the first frame update
    void Start()
    {
        speed=2f;
        blood=1;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

}
