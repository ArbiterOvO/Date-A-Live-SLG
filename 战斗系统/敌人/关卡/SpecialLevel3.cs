using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialLevel3:BaseLevel
{
    public SpecialLevel3()
    {
        id=3;
        enemies.Add(new List<BaseEnemy>{new KuangSanEnemy()});
    }
}
