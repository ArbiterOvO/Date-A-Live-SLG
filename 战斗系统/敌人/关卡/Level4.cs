using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4:BaseLevel
{
    public Level4()
    {
        //每层2小怪 1boss
        id=4;
        enemies.Add(new List<BaseEnemy>{new Normal1()});
        enemies.Add(new List<BaseEnemy>{new Normal2()});
        enemies.Add(new List<BaseEnemy>{new KuangSanEnemy()});
        enemies.Add(new List<BaseEnemy>{new Normal3()});

    }
}