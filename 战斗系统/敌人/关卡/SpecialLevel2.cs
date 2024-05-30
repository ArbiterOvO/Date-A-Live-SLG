using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialLevel2:BaseLevel
{
    public SpecialLevel2()
    {
        id=2;
        enemies.Add(new List<BaseEnemy>{new Normal2(),new Normal2(),new Normal2()});
    }
}
