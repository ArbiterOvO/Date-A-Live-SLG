using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3:BaseLevel
{
    public Level3()
    {
        id=3;
        enemies.Add(new List<BaseEnemy>{new Normal1(),new Normal1(),new Normal1()});
        enemies.Add(new List<BaseEnemy>{new Normal1(),new Normal1()});
    }
}