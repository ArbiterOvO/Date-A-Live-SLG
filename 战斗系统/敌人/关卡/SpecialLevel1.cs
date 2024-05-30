using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialLevel1:BaseLevel
{
    public SpecialLevel1()
    {
        id=1;
        enemies.Add(new List<BaseEnemy>{new Normal3(),new Normal3()});
    }
}
